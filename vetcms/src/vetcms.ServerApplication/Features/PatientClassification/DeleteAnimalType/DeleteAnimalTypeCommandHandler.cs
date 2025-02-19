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
    internal class DeleteAnimalTypeCommandHandler(IAnimalTypeRepository animalTypeRepository) : IRequestHandler<DeleteAnimalTypeApiCommand, DeleteAnimalTypeApiCommandResponse>
    {
        public async Task<DeleteAnimalTypeApiCommandResponse> Handle(DeleteAnimalTypeApiCommand request, CancellationToken cancellationToken)
        {
            List<int> nonExistentIds = new();
            request.Ids.ForEach(async id =>
            {
                if (!await animalTypeRepository.ExistAsync(id))
                {
                    nonExistentIds.Add(id);
                }
            });

            if (nonExistentIds.Any())
            {
                return new DeleteAnimalTypeApiCommandResponse(false)
                {
                    Message = $"Nem létező állattípus ID(s): {string.Join(",", nonExistentIds)}"
                };
            }

            foreach (int id in request.Ids)
            {
                await animalTypeRepository.DeleteAsync(id);
            }

            return new DeleteAnimalTypeApiCommandResponse()
            {
                Success = true
            };
        }
    }
}
