using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientManagement.ListPatients
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public double Weight { get; set; }
        public string MicrochipNumber { get; set; }
        public bool IsSterilised { get; set; }
        public string ChronicDiseases { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
        public int BreedId { get; set; }
    }

    internal class AnimalTypeListClientQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<AnimalListClientQuery, AnimalListClientQueryResponse>
    {
        AnimalDto[] animals = new AnimalDto[]
        {
            new AnimalDto { Id = 1, Name = "Bella", Gender = 'F', Weight = 12.5, MicrochipNumber = "MC123456", IsSterilised = true, ChronicDiseases = "None", DateOfBirth = new DateTime(2019, 5, 12), OwnerId = 1, TypeId = 1, BreedId = 1 },
            new AnimalDto { Id = 2, Name = "Max", Gender = 'M', Weight = 18.2, MicrochipNumber = "MC234567", IsSterilised = false, ChronicDiseases = "Diabetes", DateOfBirth = new DateTime(2018, 8, 21), OwnerId = 2, TypeId = 1, BreedId = 2 },
            new AnimalDto { Id = 3, Name = "Luna", Gender = 'F', Weight = 9.7, MicrochipNumber = "MC345678", IsSterilised = true, ChronicDiseases = "None", DateOfBirth = new DateTime(2020, 3, 15), OwnerId = 3, TypeId = 2, BreedId = 3 },
            new AnimalDto { Id = 4, Name = "Charlie", Gender = 'M', Weight = 22.3, MicrochipNumber = "MC456789", IsSterilised = true, ChronicDiseases = "Heart Disease", DateOfBirth = new DateTime(2017, 12, 5), OwnerId = 4, TypeId = 1, BreedId = 4 },
            new AnimalDto { Id = 5, Name = "Daisy", Gender = 'F', Weight = 11.1, MicrochipNumber = "MC567890", IsSterilised = false, ChronicDiseases = "Arthritis", DateOfBirth = new DateTime(2016, 7, 30), OwnerId = 5, TypeId = 2, BreedId = 5 },
            new AnimalDto { Id = 6, Name = "Rocky", Gender = 'M', Weight = 25.4, MicrochipNumber = "MC678901", IsSterilised = true, ChronicDiseases = "None", DateOfBirth = new DateTime(2019, 9, 10), OwnerId = 6, TypeId = 1, BreedId = 6 },
            new AnimalDto { Id = 7, Name = "Milo", Gender = 'M', Weight = 14.2, MicrochipNumber = "MC789012", IsSterilised = false, ChronicDiseases = "None", DateOfBirth = new DateTime(2021, 1, 22), OwnerId = 7, TypeId = 2, BreedId = 7 },
            new AnimalDto { Id = 8, Name = "Lucy", Gender = 'F', Weight = 10.5, MicrochipNumber = "MC890123", IsSterilised = true, ChronicDiseases = "Allergies", DateOfBirth = new DateTime(2018, 4, 3), OwnerId = 8, TypeId = 1, BreedId = 8 },
            new AnimalDto { Id = 9, Name = "Cooper", Gender = 'M', Weight = 30.0, MicrochipNumber = "MC901234", IsSterilised = true, ChronicDiseases = "Hip Dysplasia", DateOfBirth = new DateTime(2015, 6, 18), OwnerId = 9, TypeId = 1, BreedId = 9 },
            new AnimalDto { Id = 10, Name = "Bailey", Gender = 'F', Weight = 17.6, MicrochipNumber = "MC012345", IsSterilised = false, ChronicDiseases = "None", DateOfBirth = new DateTime(2020, 11, 9), OwnerId = 10, TypeId = 2, BreedId = 10 }
        };

        public async Task<AnimalListClientQueryResponse> Handle(AnimalListClientQuery request, CancellationToken cancellationToken)
        {
            AnimalListClientQueryResponse animalListClientQueryResponse = new AnimalListClientQueryResponse();
            List<AnimalDto> animalDtos = animals.AsQueryable().Skip(request.Skip).Take((int)request.Take).ToList();
            animalListClientQueryResponse.Animals = animalDtos;
            animalListClientQueryResponse.ResultCount = animals.Length;
            return await Task.FromResult(animalListClientQueryResponse);
        }
    }

    internal class ListAnimalTypeApiQueryHandler : GenericApiCommandHandler<ListAnimalTypeApiQuery, ListAnimalTypeApiQueryResponse>
    {
        public ListAnimalTypeApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}

