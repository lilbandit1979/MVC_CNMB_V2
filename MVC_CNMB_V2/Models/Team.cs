using MVC_CNMB_V2.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_CNMB_V2.Models
{
public enum Gender
{
    boys = 1,
    girls = 2
}
public enum TeamType
{
    U10Football = 1,
    U11Football = 2,
    SeniorFootball = 3,
    U11Camogie = 4,
    SeniorCamogie = 5,
    U11Hurling = 6,
    SeniorHurling = 7
}

public class Team
{
    [Required]
    public int TeamId { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public TeamType TeamGame { get; set; } //football, hurling etc.
    [Required]
    //public Teacher? Mentor { get; set; } //can be null //taken out 16th Dec
    
    public int SchoolId { get; set; }
    public int TeacherId { get; set; }

    }
}
