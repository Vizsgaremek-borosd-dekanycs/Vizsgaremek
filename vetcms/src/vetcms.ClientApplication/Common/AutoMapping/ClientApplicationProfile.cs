using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.IAM.GetUser;
using vetcms.ClientApplication.Features.IAM.UserList;
using vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList;
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalType;
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
        }
    }
}
