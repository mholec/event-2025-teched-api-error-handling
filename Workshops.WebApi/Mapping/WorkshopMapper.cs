using Workshops.Data.Entities;
using Workshops.WebApi.Contracts;
using Workshops.WebApi.Contracts.Registrations;
using Workshops.WebApi.Contracts.Workshops;

namespace Workshops.WebApi.Mapping;

/// <summary>
/// Třída, která slouží pro mapování objektů mezi databází a API
/// </summary>
public static class WorkshopMapper
{
    /// <summary>
    /// Převede databázový "kurz" na API "workshop"
    /// </summary>
    public static IQueryable<Workshop> ToDto(this IQueryable<Course> courses)
    {
        return courses.Select(x => new Workshop()
        {
            Id = x.Id,
            StartDate = x.StartDate,
            Capacity = x.Capacity,
            Price = x.Price,
            Slug = x.Slug,
            Title = x.Name
        });
    }
}



/// <summary>
/// Třída, která slouží pro mapování objektů mezi databází a API
/// </summary>
public static class StudentMapper
{
    /// <summary>
    /// Převede databázový "student" na API "registraci"
    /// </summary>
    public static IQueryable<Registration> ToDto(this IQueryable<Student> students)
    {
        return students.Select(x => new Registration()
        {
            Id = x.Id,
            WorkshopId = x.CourseId,
            Name = x.Name,
            Created = x.Created,
            Price = x.Price,
            PaidDate = x.PaidDate
        });
    }
}