using System.ComponentModel.DataAnnotations;

namespace Workshops.Data.Entities;

public class Student
{
    public Student()
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now;
    }

    [Key]
    public Guid Id { get; set; }

    public string CourseId { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public int Price { get; set; }
    public DateTime? PaidDate { get; set; }

    public Course Course { get; set; }
}