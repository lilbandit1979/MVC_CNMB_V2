using NuGet.DependencyResolver;
using System.ComponentModel.DataAnnotations;

namespace MVC_CNMB_V2.Models
{
public class School
{
    public int SchoolId { get; set; }
    [Required]
    public string SchoolName { get; set; } = "";
    [Required]
    public string SchoolPhone { get; set; } = "";
    [Required]
    public string SchoolAddress { get; set; } = "";
    [Required]
    public string SchoolEircode { get; set; } = "";
    public virtual ICollection<Teacher>? Teachers { get; set; } //added as post not working

    }
}
