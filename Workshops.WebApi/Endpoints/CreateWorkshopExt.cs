using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Workshops.Data;
using Workshops.Data.Entities;
using Workshops.WebApi.Contracts;
using Workshops.WebApi.Contracts.Workshops;
using Workshops.WebApi.ErrorHandling;
using Workshops.WebApi.Mapping;

namespace Workshops.WebApi.Endpoints;

public static class CreateWorkshopExt
{
    /// <summary>
    /// Vytvoření workshopu
    /// https://workshopy.apidog.io/vytvo%C5%99en%C3%AD-workshopu-16058868e0
    /// </summary>
    public static RouteHandlerBuilder CreateWorkshop(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/workshops", (ContractValidator validator, AppDbContext db, WorkshopCreate? model) =>
        {
            validator.EnsureValid(model);

            var course = new Course()
            {
                Capacity = model.Capacity,
                StartDate = model.StartDate,
                Slug = model.Slug,
                Price = model.Price,
                Name = model.Title
            };

            db.Courses.Add(course);
            db.SaveChanges();

            var workshop = db.Courses
                .AsNoTracking()
                .Where(x => x.Id == course.Id)
                .ToDto().First();

            return Results.CreatedAtRoute("GetWorkshopById", new {id = course.Id}, workshop);
        });
    }
}