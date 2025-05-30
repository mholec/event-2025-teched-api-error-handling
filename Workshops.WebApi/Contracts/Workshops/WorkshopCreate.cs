namespace Workshops.WebApi.Contracts.Workshops;

/// <summary>
/// Kontrakt pro vytvářený workshop (typicky metoda POST)
/// </summary>
public class WorkshopCreate
{
    public string Slug { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
}