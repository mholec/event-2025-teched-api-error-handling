using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Workshops.Data.Entities;

public class Course
{
    public Course()
    {
        Id = Guid.NewGuid().ToString().Substring(0, 8);
        CreatedAt = DateTime.Now;
    }

    [Key]
    [Length(8, 8)]
    public string Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    [Precision(8,2)]
    public decimal Price { get; set; }

    [Required]
    [MaxLength(200)]
    public string Slug { get; set; }

    [Required]
    [Range(0,100)]
    public int Capacity { get; set; }

    public List<Student> Students { get; set; } = new();
}