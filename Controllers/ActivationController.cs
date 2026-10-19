using System.IO.Compression;
using System.Text.Json;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Data;
using ClientMonitoring.Web.Models;
using ClientMonitoring.Web.Services;
using ClientMonitoring.Web.Domain.Entities;

namespace ClientMonitoring.Web.Controllers;

[Authorize]
public class ActivationController : Controller
{
    private readonly StructureDbContext _structureDb;
    private readonly IdentityDbContext _identityDb;
    private readonly ReadApplicationDbContext _electionDb;
    private readonly ActivationIssuer _issuer;

    public ActivationController(
        StructureDbContext structureDb,
        ReadApplicationDbContext electionDb,
       IdentityDbContext identityDb,
        ActivationIssuer issuer)
    {
        _structureDb = structureDb;
        _electionDb = electionDb;
        _identityDb = identityDb;
        _issuer = issuer;
    }

    // ============================================================
    // INDEX
    // ============================================================

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await BuildIndexVm(null, null));
    }

    // ============================================================
    // GENERATE CENTRAL KEYS
    // ============================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GenerateKeys()
    {
        try
        {
            _issuer.GenerateKeys();

            TempData["Message"] =
                "Keys generated successfully. Copy the public key into " +
                "CommuneSecurity/Activation/CentralPublicKey.cs.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    // ============================================================
    // CREATE ALL
    // ============================================================


    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(
        NoStore = true,
        Location = ResponseCacheLocation.None)]
    public async Task<IActionResult> CreateAll(
        string activationType,
        string codeWilaya)
    {
        if (!_issuer.HasKeys)
        {
            TempData["Error"] =
                "Central activation keys have not been generated yet.";

            return RedirectToAction(nameof(Index));
        }

        activationType = activationType?.Trim().ToUpperInvariant() ?? "";
        codeWilaya = codeWilaya?.Trim() ?? "";

        if (activationType != "APC" && activationType != "APW")
        {
            TempData["Error"] = "Activation type must be APC or APW.";
            return RedirectToAction(nameof(Index));
        }

        // "ALL" means generate activation files for every Wilaya.
        bool generateAll = codeWilaya.Equals(
            "ALL",
            StringComparison.OrdinalIgnoreCase);

        if (!generateAll && string.IsNullOrWhiteSpace(codeWilaya))
        {
            TempData["Error"] =
                "Please select a Wilaya or choose All Wilayas.";

            return RedirectToAction(nameof(Index));
        }

        try
        {
            var issuedFiles = new List<IssuedZipFile>();

            if (generateAll)
            {
                var wilayaCodes = await _structureDb.Wilayas
                    .AsNoTracking()
                    .OrderBy(x => x.codeWilaya)
                    .Select(x => x.codeWilaya)
                    .ToListAsync();

                if (wilayaCodes.Count == 0)
                {
                    TempData["Error"] = "No Wilayas were found.";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var code in wilayaCodes)
                {
                    if (activationType == "APC")
                    {
                        // Each Wilaya produces one APC file per commune.
                        issuedFiles.AddRange(
                            await GenerateApcFiles(code));
                    }
                    else
                    {
                        // Each Wilaya produces exactly one APW file.
                        issuedFiles.AddRange(
                            await GenerateApwFiles(code));
                    }
                }
            }
            else if (activationType == "APC")
            {
                issuedFiles = await GenerateApcFiles(codeWilaya);
            }
            else
            {
                issuedFiles = await GenerateApwFiles(codeWilaya);
            }

            if (issuedFiles.Count == 0)
            {
                TempData["Error"] =
                    "No activation files were generated.";

                return RedirectToAction(nameof(Index));
            }

            var zipFileName = generateAll
                ? $"Activation_{activationType}_ALL.zip"
                : $"Activation_{activationType}_{codeWilaya}.zip";

            byte[] zipBytes;

            using (var memory = new MemoryStream())
            {
                using (var archive = new ZipArchive(
                    memory,
                    ZipArchiveMode.Create,
                    leaveOpen: true))
                {
                    foreach (var issued in issuedFiles)
                    {
                        // Use only the filename, not any directory path.
                        var entryName = Path.GetFileName(issued.FileName);

                        var entry = archive.CreateEntry(
                            entryName,
                            CompressionLevel.Fastest);

                        await using var entryStream = entry.Open();

                        await entryStream.WriteAsync(issued.File);
                    }
                }

                zipBytes = memory.ToArray();
            }

            TempData["Message"] =
                $"{issuedFiles.Count} activation file(s) generated " +
                $"for {activationType}" +
                (generateAll
                    ? " across all Wilayas."
                    : $" / Wilaya {codeWilaya}.");

            return File(
                zipBytes,
                "application/zip",
                zipFileName);
        }
        catch (ActivationInputException ex)
        {
            TempData["Error"] = ex.Message;
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }
        catch (Exception ex)
{
    // Log the complete exception, including inner exceptions.
    Console.Error.WriteLine(ex.ToString());

    // If using Visual Studio, inspect this in the Output window.
    System.Diagnostics.Debug.WriteLine(ex.ToString());

    TempData["Error"] = ex.ToString();

    return RedirectToAction(nameof(Index));
}

        return RedirectToAction(nameof(Index));
    }


    // ============================================================
    // APC
    // ============================================================

    private async Task<List<IssuedZipFile>> GenerateApcFiles(
        string codeWilaya)
    {
        // --------------------------------------------------------
        // 1. Load communes of the Wilaya
        // --------------------------------------------------------

        var communes = await _structureDb.Communes
            .AsNoTracking()
            .Where(x => x.codewilaya == codeWilaya)
            .OrderBy(x => x.codecommune)
            .ToListAsync();

        if (communes.Count == 0)
        {
            throw new InvalidOperationException(
                $"No communes found for Wilaya {codeWilaya}.");
        }

        var communeCodes = communes
            .Select(x => x.codecommune)
            .ToList();

        // --------------------------------------------------------
        // 2. Load ALL centres for the Wilaya
        // --------------------------------------------------------

        var centres = await _structureDb.Center_V
            .AsNoTracking()
            .Where(x =>
                communeCodes.Contains(x.id_structure))
            .ToListAsync();

        var centreIds = centres
            .Select(x => x.ID_center)
            .ToList();

        // --------------------------------------------------------
        // 3. Load ALL bureaux
        // --------------------------------------------------------

        var bureaux = centreIds.Count == 0
            ? new List<BureauV>()
            : await _structureDb.BureauV
                .AsNoTracking()
                .Where(x =>
                    x.ID_center.HasValue &&
                    centreIds.Contains(x.ID_center.Value))
                .ToListAsync();

        // --------------------------------------------------------
        // 4. Load ALL APC lists
        // --------------------------------------------------------

        var listes = await _electionDb.ListeElectoralAPC
            .AsNoTracking()
            .Where(x =>
                communeCodes.Contains(x.CodeCircoAPC))
            .OrderBy(x => x.CodeCircoAPC)
            .ThenBy(x => x.IdListe)
            .ToListAsync();

        var listeIds = listes
            .Select(x => x.IdListe)
            .ToList();

        // --------------------------------------------------------
        // 5. Load ALL candidates
        // --------------------------------------------------------

        var candidats = listeIds.Count == 0
            ? new List<Candidature>()
            : await _electionDb.Candidature
                .AsNoTracking()
                .Where(x =>
                    listeIds.Contains(x.IdListeElectoral))
                .OrderBy(x => x.IdListeElectoral)
                .ThenBy(x => x.Order)
                .ToListAsync();

        // --------------------------------------------------------
        // 6. Load ALL users for these communes
        // --------------------------------------------------------

        var users = await _identityDb.Users
                 .AsNoTracking()
                 .Where(x => communeCodes.Contains(x.IdStructure) &&
                        x.Roles.Any(r => r.Name == "DelegueWilaya" ||
                            r.Name == "SaisieCommune" ||
                            r.Name == "Magistrat" ||
                            r.Name == "Delegue"))
                 .Select(x => new ActivationUserDto
                 {
                     Id = x.Id,
                     UserName = x.UserName,
                     PasswordHash = x.PasswordHash,
                     Role = x.Roles
                            .Where(r =>r.Name == "DelegueWilaya" ||
                                r.Name == "SaisieCommune" ||
                                r.Name == "Magistrat" ||
                                r.Name == "Delegue")
                            .Select(r => r.Name!)
                            .FirstOrDefault() ?? "",
                     CodeStructure = x.IdStructure
                 })
                 .ToListAsync();

        // --------------------------------------------------------
        // 7. Group data in memory
        // --------------------------------------------------------

        var centresByCommune = centres
            .GroupBy(x => x.id_structure)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        var bureauxByCentre = bureaux
            .Where(x => x.ID_center.HasValue)
            .GroupBy(x => x.ID_center!.Value)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        var listesByCommune = listes
            .GroupBy(x => x.CodeCircoAPC)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        var candidatsByListe = candidats
            .GroupBy(x => x.IdListeElectoral)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        var usersByCommune = users
            .GroupBy(x => x.CodeStructure)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        // --------------------------------------------------------
        // 8. Generate one activation file per commune
        // --------------------------------------------------------

        var result = new List<IssuedZipFile>();

        foreach (var commune in communes)
        {
            var codeCommune = commune.codecommune;

            var packageJson = BuildApcPackageJson(
                commune,
                centresByCommune.TryGetValue(
                    codeCommune,
                    out var communeCentres)
                        ? communeCentres
                        : new List<Center_V>(),
                bureauxByCentre,
                listesByCommune.TryGetValue(
                    codeCommune,
                    out var communeListes)
                        ? communeListes
                        : new List<ListeElectoralAPC>(),
                candidatsByListe,
                usersByCommune.TryGetValue(
                    codeCommune,
                    out var communeUsers)
                        ? communeUsers
                        : new List<ActivationUserDto>());

            // IMPORTANT:
            // Change this convention if CommuneServer uses
            // another ServerCode.
            var serverCode =
                $"COMMUNE-{codeCommune}";

            var issued = _issuer.Issue(
                packageJson,
                serverCode,
                User.Identity?.Name ?? "unknown");

            result.Add(new IssuedZipFile
            {
                FileName = issued.FileName,
                File = issued.File
            });
        }

        return result;
    }

    // ============================================================
    // APC PACKAGE
    // ============================================================

    private static string BuildApcPackageJson(
        Commune commune,
        List<Center_V> centres,
        Dictionary<long, List<BureauV>> bureauxByCentre,
        List<ListeElectoralAPC> listes,
        Dictionary<long, List<Candidature>> candidatsByListe,
        List<ActivationUserDto> users)
    {
        var package = new
        {
            ActivationVersion = 1,

            ActivationType = "APC",

            // ActivationPackagePreparer will overwrite this
            // with the value passed to Issue().
            ServerCode = $"COMMUNE-{commune.codecommune}",

            Commune = new
            {
                CodeCommune = commune.codecommune,

                NameCommune = commune.libeleCommune,

                CodeWilaya = commune.codewilaya,

                Centres = centres
                    .OrderBy(x => x.ID_center)
                    .Select(center =>
                    {
                        var centerBureaux =
                            bureauxByCentre.TryGetValue(
                                center.ID_center,
                                out var b)
                                    ? b
                                    : new List<BureauV>();

                        return new
                        {
                            Id = center.ID_center,

                            // As agreed: ID_center is used
                            // as the centre Code.
                            Code = center.ID_center.ToString(),

                            Lib = center.descriptio_ar,

                            Type = "C",

                            NbInscrit =
                                centerBureaux.Sum(
                                    x => x.Nb_Inscrits ?? 0),

                            Bureaux = centerBureaux
                                .OrderBy(x => x.ID_Bureau)
                                .Select(bureau => new
                                {
                                    Id = bureau.ID_Bureau,

                                    Code = bureau.CBureau,

                                    Type = "B",

                                    NbInscrit =
                                        bureau.Nb_Inscrits ?? 0
                                })
                                .ToList()
                        };
                    })
                    .ToList()
            },

            Listes = listes
                .OrderBy(x => x.IdListe)
                .Select(liste =>
                {
                    var listeCandidats =
                        candidatsByListe.TryGetValue(
                            liste.IdListe,
                            out var c)
                                ? c
                                : new List<Candidature>();

                    return new
                    {
                        Id = liste.IdListe,

                        Lib = liste.PartiePolitique.LibelleAr,

                        NumIdentifiant =
                            liste.PartiePolitique.NumIdentifiant,

                        Candidats = listeCandidats
                            .OrderBy(x => x.Order)
                            .ThenBy(x => x.IdCandidat)
                            .Select(candidat => new
                            {
                                Id = candidat.IdCandidat,

                                FullNameAr =
                                    $"{candidat.CandidatInfo.NomAr} {candidat.CandidatInfo.PrenomAr}"
                                        .Trim(),

                                FullNameFr =
                                    $"{candidat.CandidatInfo.NomFr} {candidat.CandidatInfo.PrenomFr}"
                                        .Trim(),

                                Ordre = candidat.Order
                            })
                            .ToList()
                    };
                })
                .ToList(),

            Users = users
                .OrderBy(x => x.Id)
                .Select(x => new
                {
                    Id = x.Id,

                    UserName = x.UserName,

                    PasswordHash = x.PasswordHash,

                    Role = "Admin",

                    CodeStructure = x.CodeStructure
                })
                .ToList()
        };

        return JsonSerializer.Serialize(
            package,
            JsonOptions);
    }

    // ============================================================
    // APW
    // ============================================================

    private async Task<List<IssuedZipFile>> GenerateApwFiles(
        string codeWilaya)
    {
        // --------------------------------------------------------
        // 1. Wilaya
        // --------------------------------------------------------

        var wilaya = await _structureDb.Wilayas
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.codeWilaya == codeWilaya);

        if (wilaya == null)
        {
            throw new InvalidOperationException(
                $"Wilaya {codeWilaya} was not found.");
        }

        // --------------------------------------------------------
        // 2. All communes of Wilaya
        // --------------------------------------------------------

        var communes = await _structureDb.Communes
            .AsNoTracking()
            .Where(x => x.codewilaya == codeWilaya)
            .OrderBy(x => x.codecommune)
            .ToListAsync();

        var communeCodes = communes
            .Select(x => x.codecommune)
            .ToList();

        // --------------------------------------------------------
        // 3. All centres
        // --------------------------------------------------------

        var centres = communeCodes.Count == 0
            ? new List<Center_V>()
            : await _structureDb.Center_V
                .AsNoTracking()
                .Where(x =>
                    communeCodes.Contains(x.id_structure))
                .ToListAsync();

        var centreIds = centres
            .Select(x => x.ID_center)
            .ToList();

        // --------------------------------------------------------
        // 4. All bureaux
        // --------------------------------------------------------

        var bureaux = centreIds.Count == 0
            ? new List<BureauV>()
            : await _structureDb.BureauV
                .AsNoTracking()
                .Where(x =>
                    x.ID_center.HasValue &&
                    centreIds.Contains(x.ID_center.Value))
                .ToListAsync();

        // --------------------------------------------------------
        // 5. APW lists
        // --------------------------------------------------------

        var listes = await _electionDb.ListeElectoralAPW
            .AsNoTracking()
            .Where(x =>
                x.CodeCircoAPW == codeWilaya)
            .OrderBy(x => x.IdListe)
            .ToListAsync();

        var listeIds = listes
            .Select(x => x.IdListe)
            .ToList();

        // --------------------------------------------------------
        // 6. Candidates
        // --------------------------------------------------------

        var candidats = listeIds.Count == 0
            ? new List<Candidature>()
            : await _electionDb.Candidature
                .AsNoTracking()
                .Where(x =>
                    listeIds.Contains(x.IdListeElectoral))
                .OrderBy(x => x.IdListeElectoral)
                .ThenBy(x => x.Order)
                .ToListAsync();

        // --------------------------------------------------------
        // 7. Users of all communes in Wilaya
        // --------------------------------------------------------

        var users = await _identityDb.Users
            .AsNoTracking()
            .Where(x =>
                communeCodes.Contains(x.IdStructure))
            .Select(x => new ActivationUserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                PasswordHash = x.PasswordHash,
                CodeStructure = x.IdStructure
            })
            .ToListAsync();

        // --------------------------------------------------------
        // 8. Group
        // --------------------------------------------------------

        var bureauxByCentre = bureaux
            .Where(x => x.ID_center.HasValue)
            .GroupBy(x => x.ID_center!.Value)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        var centresByCommune = centres
            .GroupBy(x => x.id_structure)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        var candidatsByListe = candidats
            .GroupBy(x => x.IdListeElectoral)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        // --------------------------------------------------------
        // 9. Build APW package
        // --------------------------------------------------------

        var packageJson = BuildApwPackageJson(
            wilaya,
            communes,
            centresByCommune,
            bureauxByCentre,
            listes,
            candidatsByListe,
            users);

        // --------------------------------------------------------
        // 10. One APW file for the Wilaya
        // --------------------------------------------------------

        var serverCode =
            $"WILAYA-{codeWilaya}";

        var issued = _issuer.Issue(
            packageJson,
            serverCode,
            User.Identity?.Name ?? "unknown");

        return new List<IssuedZipFile>
        {
            new()
            {
                FileName = issued.FileName,
                File = issued.File
            }
        };
    }

    // ============================================================
    // APW PACKAGE
    // ============================================================

    private static string BuildApwPackageJson(
        Wilaya wilaya,
        List<Commune> communes,
        Dictionary<string, List<Center_V>> centresByCommune,
        Dictionary<long, List<BureauV>> bureauxByCentre,
        List<ListeElectoralAPW> listes,
        Dictionary<long, List<Candidature>> candidatsByListe,
        List<ActivationUserDto> users)
    {
        var package = new
        {
            ActivationVersion = 1,

            ActivationType = "APW",

            ServerCode = $"WILAYA-{wilaya.codeWilaya}",

            Wilaya = new
            {
                CodeWilaya = wilaya.codeWilaya,

                NameWilaya = wilaya.Libele
            },

            Communes = communes
                .OrderBy(x => x.codecommune)
                .Select(commune =>
                {
                    var communeCentres =
                        centresByCommune.TryGetValue(
                            commune.codecommune,
                            out var c)
                                ? c
                                : new List<Center_V>();

                    return new
                    {
                        CodeCommune = commune.codecommune,

                        NameCommune = commune.libeleCommune,

                        CodeWilaya = commune.codewilaya,

                        Centres = communeCentres
                            .OrderBy(x => x.ID_center)
                            .Select(center =>
                            {
                                var centerBureaux =
                                    bureauxByCentre.TryGetValue(
                                        center.ID_center,
                                        out var b)
                                            ? b
                                            : new List<BureauV>();

                                return new
                                {
                                    Id = center.ID_center,

                                    Code =
                                        center.ID_center.ToString(),

                                    Lib =
                                        center.descriptio_ar,

                                    Type = "C",

                                    NbInscrit =
                                        centerBureaux.Sum(
                                            x => x.Nb_Inscrits ?? 0),

                                    Bureaux = centerBureaux
                                        .OrderBy(
                                            x => x.ID_Bureau)
                                        .Select(bureau => new
                                        {
                                            Id =
                                                bureau.ID_Bureau,

                                            Code =
                                                bureau.CBureau,

                                            Type = "B",

                                            NbInscrit =
                                                bureau.Nb_Inscrits ?? 0
                                        })
                                        .ToList()
                                };
                            })
                            .ToList()
                    };
                })
                .ToList(),

            Listes = listes
                .OrderBy(x => x.IdListe)
                .Select(liste =>
                {
                    var listeCandidats =
                        candidatsByListe.TryGetValue(
                            liste.IdListe,
                            out var c)
                                ? c
                                : new List<Candidature>();

                    return new
                    {
                        Id = liste.IdListe,

                        Lib = liste.PartiePolitique.LibelleAr,

                        NumIdentifiant =
                            liste.PartiePolitique.NumIdentifiant,

                        Candidats = listeCandidats
                            .OrderBy(x => x.Order)
                            .ThenBy(x => x.IdCandidat)
                            .Select(candidat => new
                            {
                                Id = candidat.IdCandidat,

                                FullNameAr =
                                    $"{candidat.CandidatInfo.NomAr} {candidat.CandidatInfo.PrenomAr}"
                                        .Trim(),

                                FullNameFr =
                                    $"{candidat.CandidatInfo.NomFr} {candidat.CandidatInfo.PrenomFr}"
                                        .Trim(),

                                Ordre = candidat.Order
                            })
                            .ToList()
                    };
                })
                .ToList(),

            Users = users
                .OrderBy(x => x.Id)
                .Select(x => new
                {
                    Id = x.Id,

                    UserName = x.UserName,

                    PasswordHash = x.PasswordHash,

                    Role = x.Role,

                    CodeStructure = x.CodeStructure
                })
                .ToList()
        };

        return JsonSerializer.Serialize(
            package,
            JsonOptions);
    }

    // ============================================================
    // INDEX VIEW MODEL
    // ============================================================

    private async Task<ActivationIndexVm> BuildIndexVm(
        string? selectedType,
        string? selectedWilaya)
    {
        var wilayas = await _structureDb.Wilayas
            .AsNoTracking()
            .OrderBy(x => x.codeWilaya)
            .Select(x => new WilayaOption
            {
                CodeWilaya = x.codeWilaya,
                Libelle = x.Libele
            })
            .ToListAsync();

        return new ActivationIndexVm
        {
            HasKeys = _issuer.HasKeys,

            PublicKeyXml = _issuer.GetPublicKeyXml(),

            KeyFolder = _issuer.KeyFolder,

            Wilayas = wilayas,

            SelectedActivationType = selectedType,

            SelectedCodeWilaya = selectedWilaya
        };
    }

    // ============================================================
    // JSON OPTIONS
    // ============================================================

    private static readonly JsonSerializerOptions JsonOptions =
        new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

            WriteIndented = true
        };

    // ============================================================
    // INTERNAL DTOs
    // ============================================================

    private sealed class ActivationUserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; } = "";

        public string PasswordHash { get; set; } = "";

        public string CodeStructure { get; set; } = "";

        public string Role { get; set; } = "";
    }

    private sealed class IssuedZipFile
    {
        public string FileName { get; set; } = "";

        public byte[] File { get; set; } = Array.Empty<byte>();
    }
}