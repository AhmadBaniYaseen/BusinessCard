using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard.Core.DTO
{
    public record UpdateBusinessCard (int Id, string Name, string Gender, DateTime DateOfBirth, string Email, string Phone, string Address, string Photo)
    {



    }
}
