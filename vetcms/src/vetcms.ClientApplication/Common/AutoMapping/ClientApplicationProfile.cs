using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.IAM.GetUser;
using vetcms.ClientApplication.Features.IAM.UserList;
using vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList;
using vetcms.ClientApplication.Features.PatientClassification.DeleteAnimalBreed;
using vetcms.ClientApplication.Features.PatientClassification.DeleteAnimalType;
using vetcms.ClientApplication.Features.PatientClassification.GetAnimalBreed;
using vetcms.ClientApplication.Features.PatientClassification.GetAnimalType;
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalBreedByType;
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalType;
using vetcms.ClientApplication.Features.PatientClassification.ModifyAnimalBreed;
using vetcms.ClientApplication.Features.PatientClassification.ModifyAnimalType;
using vetcms.ClientApplication.Features.PatientManagement.ListPatients;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Common.AutoMapping
{
    internal class ClientApplicationProfile : Profile
    {
        public ClientApplicationProfile()
        {
            CreateMap<GetUserApiQueryResponse, GetUserClientQueryResponse>();
            CreateMap<ListAnimalBreedApiQueryResponse, AnimalBreedListClientQueryResponse>();
            CreateMap<AnimalTypeListClientQuery, ListAnimalTypeApiQuery>();
            CreateMap<ListAnimalTypeApiQueryResponse, AnimalTypeListClientQueryResponse>();
            CreateMap<DeleteAnimalTypeClientCommand, DeleteAnimalTypeApiCommand>();
            CreateMap<DeleteAnimalBreedClientCommand, DeleteAnimalBreedApiCommand>();
            CreateMap<ModifyAnimalBreedClientCommand, ModifyAnimalBreedApiCommand>();
            CreateMap<ModifyAnimalTypeClientCommand, ModifyAnimalTypeApiCommand>();

            CreateMap<GetAnimalTypeClientQuery, GetAnimalTypeByIdApiQuery>();
            CreateMap<GetAnimalTypeByIdApiQueryResponse, GetAnimalTypeClientQueryResponse>();

            CreateMap<ListAnimalBreedByTypeClientQuery, ListAnimalBreedByTypeApiQuery>();
            CreateMap<ListAnimalBreedByTypeApiQueryResponse, ListAnimalBreedByTypeClientQueryResponse>();

            CreateMap<GetAnimalBreedClientQuery, GetAnimalBreedByIdApiQuery>();
            CreateMap<GetAnimalBreedByIdApiQueryResponse, GetAnimalBreedClientQueryResponse>();
        }
    }
}
