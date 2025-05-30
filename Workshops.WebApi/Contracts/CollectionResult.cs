namespace Workshops.WebApi.Contracts;

/// <summary>
/// Obecný kontrakt pro vrácení seznamu položek z API
/// </summary>
/// <typeparam name="T">Kontrakt</typeparam>
public class CollectionResult<T>
{
    public List<T> Items { get; set; } = new();

    public int TotalItems { get; set; }
}