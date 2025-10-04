using UserService.Domain.Common;

namespace UserService.Domain.Entities;

public class User : EntityBase
{
    public User(string email, string username, string passwordHash, string role, bool status)
    {
        Email = email;
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
        Status = status;
    }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = null!; // по умолчанию
    public bool Status { get; set; } = true!;   // true = active, false = banned
    
    /*
     * 	1.1 Id
       1.2 Email
       1.3 Username
       1.4 Password(hash)
       1.5 role
       1.6 status(active/banned)
       1.7 createdDate
     */
}