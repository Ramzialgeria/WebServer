namespace ClientMonitoring.Web.Domain.SahredKernel;

public class Error
{
    public Error() 
    {

    }  

    public Error(string code, string message) 
    {
        Code = code; 
        Description = message;
    }

    public string? Code { get; set; }

    public string? Description { get; set; }
}
public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3
}