namespace Workshops.WebApi.Contracts.Problems;

public class ApiValidationError(string message, string property)
{
    public string Property { get; set; } = property;
    public string Message { get; set; } = message;
}