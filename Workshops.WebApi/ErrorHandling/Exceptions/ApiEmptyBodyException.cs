namespace Workshops.WebApi.ErrorHandling.Exceptions;

/// <summary>
/// Výjimka pro případy, kdy zcela chybí request body (payload)
/// </summary>
public class ApiEmptyBodyException : Exception
{
}