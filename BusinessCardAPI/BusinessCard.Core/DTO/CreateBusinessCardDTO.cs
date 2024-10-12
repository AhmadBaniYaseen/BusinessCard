namespace BusinessCard.Core.DTO
{
    public record CreateBusinessCardInput( string Name, string Gender, DateTime DateOfBirth, string Email, string Phone, string Address, string Photo)
    {

    }
}
