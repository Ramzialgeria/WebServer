using System.Security.Cryptography;

namespace ClientMonitoring.Web.Services;

public class IssuedActivation
{
    public byte[] File { get; set; } = Array.Empty<byte>();
    public string FileName { get; set; } = "";
    public string Code { get; set; } = "";
    public PreparedPackage Package { get; set; } = null!;
}

/// <summary>
/// Creates the activation files of the communes.
///
/// The RSA private key stays on central, in Activation:KeyFolder
/// (default: App_Data/activation, outside wwwroot so it is never served).
/// Only the public key goes in the CommuneServer application.
/// </summary>
public class ActivationIssuer
{
    private static readonly object KeyLock = new();

    private readonly string _folder;
    private readonly ILogger<ActivationIssuer> _logger;

    public ActivationIssuer(
        IConfiguration configuration,
        IWebHostEnvironment environment,
        ILogger<ActivationIssuer> logger)
    {
        _logger = logger;

        var configured = configuration["Activation:KeyFolder"];

        _folder = string.IsNullOrWhiteSpace(configured)
            ? Path.Combine(environment.ContentRootPath, "App_Data", "activation")
            : Path.GetFullPath(configured, environment.ContentRootPath);
    }

    private string PrivateKeyPath => Path.Combine(_folder, "central_private.xml");
    private string PublicKeyPath => Path.Combine(_folder, "central_public.xml");

    public string KeyFolder => _folder;

    public bool HasKeys => File.Exists(PrivateKeyPath);

    /// <summary>Public key XML to paste in CentralPublicKey.cs (null if no keys yet).</summary>
    public string? GetPublicKeyXml()
    {
        if (!HasKeys)
            return null;

        using var rsa = RSA.Create();
        rsa.FromXmlString(File.ReadAllText(PrivateKeyPath));
        return rsa.ToXmlString(false);
    }

    /// <summary>Creates the key pair. Refuses to replace an existing one.</summary>
    public void GenerateKeys()
    {
        lock (KeyLock)
        {
            if (HasKeys)
            {
                throw new InvalidOperationException(
                    "Keys already exist. They are never replaced: every existing " +
                    "activation file depends on them.");
            }

            Directory.CreateDirectory(_folder);

            using var rsa = RSA.Create(2048);

            File.WriteAllText(PrivateKeyPath, rsa.ToXmlString(true));
            File.WriteAllText(PublicKeyPath, rsa.ToXmlString(false));
        }

        _logger.LogWarning("Activation keys generated in {Folder}", _folder);
    }

    /// <summary>
    /// Builds the activation file of one commune.
    /// </summary>
    /// <param name="packageJson">Commune data (centres, lists, users...).</param>
    /// <param name="serverCode">ClientCode of the CommuneServer it is made for.</param>
    public IssuedActivation Issue(string packageJson, string serverCode, string issuedBy)
    {
        if (!HasKeys)
            throw new InvalidOperationException("Generate the central keys first.");

        var package = ActivationPackagePreparer.Prepare(packageJson, serverCode);

        string code = ActivationFileWriter.GenerateCode();

        byte[] file = ActivationFileWriter.Create(
            package.Json,
            code,
            File.ReadAllText(PrivateKeyPath));

        // TEMPORARY TEST: verify that the generated file can be decrypted.
        using var rsa = RSA.Create();
        rsa.FromXmlString(File.ReadAllText(PrivateKeyPath));
        string publicKeyXml = rsa.ToXmlString(false);

        // the code itself is never logged or stored
        _logger.LogInformation(
            "Activation issued by {User}: server {ServerCode}, {Type} {CommuneCode}, id {ActivationId}",
            issuedBy, package.ServerCode, package.ActivationType,
            package.CommuneCode, package.ActivationId);

        return new IssuedActivation
        {
            File = file,
            FileName = "Activation_" + SafeName(package.CommuneCode) + ".enc",
            Code = code,
            Package = package
        };
    }

    private static string SafeName(string value)
    {
        var chars = value.Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_').ToArray();
        return chars.Length == 0 ? "commune" : new string(chars);
    }

    
}
