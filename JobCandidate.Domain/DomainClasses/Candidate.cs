using System.ComponentModel.DataAnnotations;

namespace JobCandidate.Domain.DomainClasses
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string CallTimeInterval { get; set; }
        public string LinkedInProfileUrl { get; set; }
        public string GithubProfileUrl { get; set; }
        [Required]
        public string FreeTextComment { get; set; }
    }
}
