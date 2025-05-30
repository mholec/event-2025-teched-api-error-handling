using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Workshops.Data;
using Workshops.WebApi.Contracts;
using Workshops.WebApi.Contracts.Registrations;
using Workshops.WebApi.ErrorHandling;
using Workshops.WebApi.ErrorHandling.Exceptions;
using Workshops.WebApi.Mapping;

namespace Workshops.WebApi.Endpoints;

public static class GetRegistrationsExt
{
    /// <summary>
    /// Seznam workshopu
    /// https://workshopy.apidog.io/seznam-workshop%C5%AF-16058779e0
    /// </summary>
    public static RouteHandlerBuilder GetRegistrations(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/registrations", (RegistrationsFilter filter, ContractValidator validator, AppDbContext db) =>
        {
            validator.EnsureValid(filter);

            var students = db.Students.AsNoTracking();

            students = students.Where(x=> x.CourseId == filter.WorkshopId);

            var result = new CollectionResult<Registration>()
            {
                TotalItems = students.Count()
            };

            students = students.Skip(filter.Skip).Take(filter.Take);
            result.Items = students.ToDto().ToList();

            return Results.Ok(result);
        });
    }
}