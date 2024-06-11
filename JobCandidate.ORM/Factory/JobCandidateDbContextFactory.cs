using JobCandidate.ORM.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidate.ORM.Factory
{
    internal class JobCandidateDbContextFactory : IDesignTimeDbContextFactory<JobCandidateDbContext>
    {
        public JobCandidateDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<JobCandidateDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new JobCandidateDbContext(optionsBuilder.Options);
        }
    }
}
