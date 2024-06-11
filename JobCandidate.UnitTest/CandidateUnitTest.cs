using JobCandidate.Domain.DomainClasses;
using JobCandidate.ORM.Abstractions.RepositoryPattern;
using JobCandidate.ORM.Abstractions.UnitOfWorkPattern;
using JobCandidate.Service.Services;
using Moq;

namespace JobCandidate.UnitTest
{
    public class CandidateUnitTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IGenericRepository<Candidate>> _mockCandidateRepository;
        private readonly CandidateService _candidateService;

        public CandidateUnitTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCandidateRepository = new Mock<IGenericRepository<Candidate>>();
            _mockUnitOfWork.Setup(u => u.Repository<Candidate>()).Returns(_mockCandidateRepository.Object);
            _candidateService = new CandidateService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task AddCandiate()
        {
            var candidate = new Candidate
            {
                FirstName = "Sajan",
                LastName = "Joshi",
                PhoneNumber = "1235697890",
                Email = "sajan@test.com",
                CallTimeInterval = "9am-5pm",
                LinkedInProfileUrl = "https://www.linkedin.com/in/sajan",
                GithubProfileUrl = "https://github.com/sajan",
                FreeTextComment = "Testing Unit Test Application"
            };

            _mockCandidateRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(Enumerable.Empty<Candidate>());

            _mockCandidateRepository.Setup(repo => repo.AddAsync(It.IsAny<Candidate>()))
                .ReturnsAsync((Candidate candidate) => candidate);

            var result = await _candidateService.AddOrUpdateCandidateAsync(candidate);

            Assert.NotNull(result);
            Assert.Equal(candidate.Email, result.Email);
            _mockCandidateRepository.Verify(repo => repo.AddAsync(It.IsAny<Candidate>()), Times.Once);
            _mockCandidateRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Candidate>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCandiate()
        {            
            var candidate = new Candidate
            {
                FirstName = "Ram",
                LastName = "Joshi",
                PhoneNumber = "1234567890",
                Email = "sajan@test.com",
                CallTimeInterval = "9am-5pm",
                LinkedInProfileUrl = "https://www.linkedin.com/in/johndoe",
                GithubProfileUrl = "https://github.com/johndoe",
                FreeTextComment = "Hello Free Text Comment"
            };

            var existingCandidate = new Candidate
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Malik",
                PhoneNumber = "9876543210",
                Email = "sajan@test.com",
                CallTimeInterval = "10am-6pm",
                LinkedInProfileUrl = "https://www.linkedin.com/in/jane",
                GithubProfileUrl = "https://github.com/jane",
                FreeTextComment = "Let have some Fun"
            };

            _mockCandidateRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new[] { existingCandidate });
          
            var result = await _candidateService.AddOrUpdateCandidateAsync(candidate);
            
            Assert.NotNull(result);
            Assert.Equal(candidate.Email, result.Email);
            Assert.Equal(candidate.FirstName, result.FirstName);
            Assert.Equal(candidate.LastName, result.LastName);
            _mockCandidateRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Candidate>()), Times.Once);
            _mockCandidateRepository.Verify(repo => repo.AddAsync(It.IsAny<Candidate>()), Times.Never);
        }
    }
}