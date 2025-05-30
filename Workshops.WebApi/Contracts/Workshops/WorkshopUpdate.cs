namespace Workshops.WebApi.Contracts.Workshops;

/// <summary>
/// Kontrakt pro aktualizaci workshopu (typicky metoda PUT)
/// </summary>
public class WorkshopUpdate
{
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
}