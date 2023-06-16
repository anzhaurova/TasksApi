using System;
namespace TasksApi.Models;

public class UpdateUserRequest
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public long Phone { get; set; } 
}

