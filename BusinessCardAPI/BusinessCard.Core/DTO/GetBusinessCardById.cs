using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard.Core.DTO
{
    public record GetBusinessCardById(int ID)
    {
    }
    public record GetBusinessCardByIdOutput(int Id, string Name, string Gender, DateTime DateOfBirth, string Email, string Phone, string Address, string Photo)
    {

    }
}
