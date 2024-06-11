using JobCandidate.Domain.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace JobCandidate.ORM.Context
{
    public class JobCandidateDbContext : DbContext
    {
        public JobCandidateDbContext(DbContextOptions<JobCandidateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
    }
}
