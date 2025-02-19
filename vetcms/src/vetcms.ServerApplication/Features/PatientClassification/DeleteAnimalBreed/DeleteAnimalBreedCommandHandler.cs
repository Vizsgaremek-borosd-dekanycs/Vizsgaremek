using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.DeleteAnimalType
{
    internal class DeleteAnimalBreedCommandHandler(IAnimalBreedRepository animalBreedRepository) : IRequestHandler<DeleteAnimalBreedApiCommand, DeleteAnimalBreedApiCommandResponse>
    {
        public async Task<DeleteAnimalBreedApiCommandResponse> Handle(DeleteAnimalBreedApiCommand request, CancellationToken cancellationToken)
        {
            List<int> nonExistentIds = new();
            request.Ids.ForEach(async id =>
            {
                if (!await animalBreedRepository.ExistAsync(id))
                {
                    nonExistentIds.Add(id);
                }
            });

            if (nonExistentIds.Any())
            {
                return new DeleteAnimalBreedApiCommandResponse(false)
                {
                    Message = $"Nem létező állatfaj azonosítók: {string.Join(",", nonExistentIds)}"
                };
            }

            foreach (int id in request.Ids)
            {
                await animalBreedRepository.DeleteAsync(id);
            }

            return new DeleteAnimalBreedApiCommandResponse()
            {
                Success = true
            };
        }
    }
}
