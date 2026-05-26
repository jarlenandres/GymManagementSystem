namespace GymManagement.API.DTOs.Request;

public class MemberRequestDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Document { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
    public string Address { get; set; } = string.Empty;
    public DateTime RegistrateDate { get; set; }
    public bool IsActive { get; set; }
}
