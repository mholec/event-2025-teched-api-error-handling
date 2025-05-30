using Workshops.Data;
using Workshops.Data.Entities;

namespace Workshops.WebApi.HostedServices;

/// <summary>
/// HostedService, která při startu aplikace vytvoří databázi s testovacími daty
/// </summary>
public class InitDbService(IServiceProvider sp) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (db.Database.EnsureCreated())
        {
            for (int i = 1000; i <= 2000; i++)
            {
                var course = new Course
                {
                    Id = $"work{i}",
                    Capacity = Random.Shared.Next(10, 100),
                    Name = $"New workshop {i}",
                    Price = Random.Shared.Next(1000, 10000),
                };

                course.Slug = course.Name.ToLower().Replace(" ", "-");
                course.StartDate = DateTime.Now.AddDays(Random.Shared.Next(10, 30));

                int studentsCount = Random.Shared.Next(1, 10);
                for (int j = 1; j <= studentsCount ; j++)
                {
                    var student = new Student()
                    {
                        Name = $"Student {j} kurzu {course.Name}",
                        Created = course.CreatedAt.AddHours(Random.Shared.Next(1, 100)),

                    };

                    student.PaidDate = j % 4 == 0 ? student.Created.AddHours(2) : null;
                    course.Students.Add(student);
                }


                db.Courses.Add(course);
            }

            db.SaveChanges();
        }

        return Task.CompletedTask;
    }
}