using System.ComponentModel.DataAnnotations;

namespace Domino.Api.Core.Entities;

public class BaseTable
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
