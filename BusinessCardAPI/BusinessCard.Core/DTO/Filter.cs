using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard.Core.DTO
{
    public record Filter (DateTime? DateOfBirth, string? Gender, string? Email, string? Phone, string? Name)
    {

    }
 
}
