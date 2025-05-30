namespace Workshops.WebApi.Contracts.Registrations;

public class Registration
{
    public Guid Id { get; set; }
    public string WorkshopId { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public int Price { get; set; }
    public DateTime? PaidDate { get; set; }
}