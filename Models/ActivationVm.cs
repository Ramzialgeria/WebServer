namespace ClientMonitoring.Web.Models;


public class WilayaOption
{
    public string CodeWilaya { get; set; } = "";

    public string Libelle { get; set; } = "";
}

public class ActivationIndexVm
{
    public bool HasKeys { get; set; }

    public string? PublicKeyXml { get; set; }

    public string? KeyFolder { get; set; }

    public List<WilayaOption> Wilayas { get; set; } = new();

    public string? SelectedCodeWilaya { get; set; }

    public string? SelectedActivationType { get; set; }
}


public class ActivationResultVm
{
    public string ServerCode { get; set; } = "";
    public string ActivationType { get; set; } = "";
    public string CommuneCode { get; set; } = "";
    public string CommuneName { get; set; } = "";
    public string ActivationId { get; set; } = "";
    public int UserCount { get; set; }

    public string FileName { get; set; } = "";
    public string Code { get; set; } = "";
    public string DownloadToken { get; set; } = "";
}
