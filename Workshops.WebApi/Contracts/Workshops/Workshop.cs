namespace Workshops.WebApi.Contracts.Workshops;

/// <summary>
/// Kontrakt pro workshop, který se vrací z API
/// </summary>
public class Workshop
{
    public int Capacity { get; set; }
    public string Id { get; set; }
    public decimal Price { get; set; }
    public string Slug { get; set; }
    public DateTime StartDate { get; set; }
    public string Title { get; set; }
}