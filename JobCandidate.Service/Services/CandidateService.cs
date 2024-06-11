﻿using JobCandidate.Domain.DomainClasses;
using JobCandidate.ORM.Abstractions.RepositoryPattern;
using JobCandidate.ORM.Abstractions.UnitOfWorkPattern;
using JobCandidate.Service.Interfaces;

namespace JobCandidate.Service.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Candidate> _candidateRepository;

        public CandidateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _candidateRepository = unitOfWork.Repository<Candidate>();
        }

        public async Task<Candidate> AddOrUpdateCandidateAsync(Candidate candidate)
        {
            var existingCandidate = (await _candidateRepository.GetAllAsync())
                .FirstOrDefault(c => c.Email == candidate.Email);

            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GithubProfileUrl = candidate.GithubProfileUrl;
                existingCandidate.FreeTextComment = candidate.FreeTextComment;

                await _candidateRepository.UpdateAsync(existingCandidate);
                return existingCandidate;
            }
            else
            {
                return await _candidateRepository.AddAsync(candidate);
            }
        }
    }
}
