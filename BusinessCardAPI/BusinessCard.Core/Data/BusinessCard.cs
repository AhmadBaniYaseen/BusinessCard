using System;
using System.Collections.Generic;

namespace BusinessCard.Core.Data;

public class BusinessCard
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Photo { get; set; }
}
