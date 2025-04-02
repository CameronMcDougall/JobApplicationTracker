using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.Domain.Models.Enums;

namespace JobApplicationTracker.Domain.Models;

public class Application(string companyName, string position, ApplicationStatus status, DateTime appliedDate)
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(TypeName = "nvarchar")]
    [MaxLength(30)]
    [Required]
    public string CompanyName { get; set; } = companyName;

    [Column(TypeName = "nvarchar")]
    [MaxLength(30)]
    [Required]
    public string Position { get; set; } = position;

    [Required]
    public ApplicationStatus Status { get; set; } = status;

    [Required]
    public DateTime AppliedDate { get; set; } = appliedDate;
}