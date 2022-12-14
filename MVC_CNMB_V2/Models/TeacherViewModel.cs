using System.ComponentModel.DataAnnotations;

namespace MVC_CNMB_V2.Models
{
	public class TeacherViewModel
    {
    [Required]
    public int TeacherId { get; set; }
    [Required]
    public string FName { get; set; } = "";
    [Required]
    public string SName { get; set; } = "";
    [Required]
    public string TeacherPhone { get; set; } = "";
    [Required]
    public bool IsMainRep { get; set; } = false;
    }
}
