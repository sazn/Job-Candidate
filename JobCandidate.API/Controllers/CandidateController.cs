using JobCandidate.Domain.DomainClasses;
using JobCandidate.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidate.API.Controllers
{    
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [Route("api/Candidate")]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] Candidate candidate)
        {
            if (candidate == null)
            {
                return BadRequest();
            }

            var result = await _candidateService.AddOrUpdateCandidateAsync(candidate);
            return Ok(result);
        }
    }
}
