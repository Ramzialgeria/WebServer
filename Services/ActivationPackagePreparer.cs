#nullable disable
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ClientMonitoring.Web.Services
{
    /// <summary>Problem in the package supplied by the operator (message is user-facing).</summary>
    public class ActivationInputException : Exception
    {
        public ActivationInputException(string message) : base(message) { }
    }

    public class PreparedPackage
    {
        public string Json { get; set; }
        public string ActivationId { get; set; }
        public string ActivationType { get; set; }
        public string ServerCode { get; set; }
        public string CommuneCode { get; set; }
        public string CommuneName { get; set; }
        public int UserCount { get; set; }
    }

    /// <summary>
    /// Checks the package JSON given by the operator and makes it ready to
    /// encrypt: sets ServerCode / ActivationId, replaces clear passwords by
    /// BCrypt hashes. The CommuneServer does the full validation again.
    /// </summary>
    public static class ActivationPackagePreparer
    {
        /// <param name="serverCodeOverride">
        /// ServerCode of the selected client. When null/empty, the package
        /// must already contain a ServerCode.
        /// </param>
        public static PreparedPackage Prepare(string json, string serverCodeOverride)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ActivationInputException("The package is empty.");

            JsonObject root;
            try
            {
                root = JsonNode.Parse(json) as JsonObject;
            }
            catch (JsonException ex)
            {
                throw new ActivationInputException("Invalid JSON: " + ex.Message);
            }

            if (root == null)
                throw new ActivationInputException("The package must be a JSON object.");

            string type = Str(root["ActivationType"]);
            if (type != "APC" && type != "APW")
                throw new ActivationInputException("ActivationType must be APC or APW.");

            // ---- server code ------------------------------------------
            if (!string.IsNullOrWhiteSpace(serverCodeOverride))
                root["ServerCode"] = serverCodeOverride.Trim();

            string serverCode = Str(root["ServerCode"]);
            if (string.IsNullOrWhiteSpace(serverCode))
                throw new ActivationInputException("ServerCode is missing.");

            // ---- ids --------------------------------------------------
            if (root["ActivationVersion"] == null)
                root["ActivationVersion"] = 1;

            if (string.IsNullOrWhiteSpace(Str(root["ActivationId"])))
                root["ActivationId"] = Guid.NewGuid().ToString();

            // ---- commune / wilaya -------------------------------------
            string communeCode, communeName;

            if (type == "APC")
            {
                var commune = root["Commune"] as JsonObject;
                if (commune == null)
                    throw new ActivationInputException("Commune is missing.");

                communeCode = Str(commune["CodeCommune"]);
                communeName = Str(commune["NameCommune"]);

                RequireNotEmpty(communeCode, "Commune.CodeCommune");
                RequireNotEmpty(communeName, "Commune.NameCommune");

                var centres = commune["Centres"] as JsonArray;
                if (centres == null || centres.Count == 0)
                    throw new ActivationInputException("Commune.Centres is empty.");
            }
            else
            {
                var wilaya = root["Wilaya"] as JsonObject;
                if (wilaya == null)
                    throw new ActivationInputException("Wilaya is missing.");

                communeCode = Str(wilaya["CodeWilaya"]);
                communeName = Str(wilaya["NameWilaya"]);

                RequireNotEmpty(communeCode, "Wilaya.CodeWilaya");
                RequireNotEmpty(communeName, "Wilaya.NameWilaya");

                var communes = root["Communes"] as JsonArray;
                if (communes == null || communes.Count == 0)
                    throw new ActivationInputException("Communes is empty.");
            }

            var listes = root["Listes"] as JsonArray;
            if (listes == null || listes.Count == 0)
                throw new ActivationInputException("Listes is empty.");

            // ---- users: clear password -> BCrypt hash -----------------
            var usersNode = root["Users"] as JsonArray;
            if (usersNode == null || usersNode.Count == 0)
                throw new ActivationInputException("Users is empty.");

            var users = new List<JsonObject>();
            foreach (var node in usersNode)
            {
                var u = node as JsonObject;
                if (u == null)
                    throw new ActivationInputException("Invalid user entry.");

                string name = Str(u["UserName"]);
                RequireNotEmpty(name, "User.UserName");

                if (string.IsNullOrEmpty(Str(u["Password"])) &&
                    string.IsNullOrEmpty(Str(u["PasswordHash"])))
                {
                    throw new ActivationInputException(
                        "User " + name + ": Password is missing.");
                }

                users.Add(u);
            }

            // BCrypt is slow on purpose: do users in parallel
            Parallel.ForEach(users, u =>
            {
                string pwd = Str(u["Password"]);
                if (!string.IsNullOrEmpty(pwd))
                {
                    u["PasswordHash"] = BCrypt.Net.BCrypt.HashPassword(pwd);
                    u.Remove("Password");
                }
            });

            return new PreparedPackage
            {
                Json = root.ToJsonString(new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }),
                ActivationId = Str(root["ActivationId"]),
                ActivationType = type,
                ServerCode = serverCode,
                CommuneCode = communeCode,
                CommuneName = communeName,
                UserCount = users.Count
            };
        }

        private static string Str(JsonNode node)
        {
            var v = node as JsonValue;
            string s;
            if (v != null && v.TryGetValue<string>(out s))
                return s;

            // numbers are tolerated for codes written as 16, 001...
            return v != null ? v.ToString() : null;
        }

        private static void RequireNotEmpty(string value, string field)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ActivationInputException(field + " is missing.");
        }
    }
}
