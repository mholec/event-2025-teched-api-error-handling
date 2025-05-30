namespace Workshops.WebApi.ErrorHandling.Exceptions;

/// <summary>
/// Výjimka pro scénáře, kdy není nalezen požadovaný resource v systému
/// </summary>
public class ApiNotFoundException(string message) : Exception(message)
{
}

/// <summary>
/// Výjimka pro scénáře, kdy není nalezen požadovaný resource v systému
/// </summary>
public class ApiNotFoundException<T>() : ApiNotFoundException($"{typeof(T).Name} not found")
{
}