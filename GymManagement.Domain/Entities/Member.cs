namespace GymManagement.Domain.Entities;

public class Member : AuditBase
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

    // === Foreign Keys ===
    public int MembershipId { get; set; }
    public int EnrollmentId { get; set; }
    public int HeadquartersId { get; set; }


    // ==== Navigation Properties ==
   // public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
   // public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
   // public Headquarters Headquarters { get; set; } = null!;
}
