using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DTOs.Requests
{
    public class StudiesRequest
    {
        [Required]
        string Name { get; set; } //studies
        [Required]
        string Semester { get; set; }
    }
}
