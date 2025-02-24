﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalType
{
    public class AnimalTypeListClientQueryResponse
    {
        public List<AnimalTypeDto> AnimalType { get; set; } = new();
        public int ResultCount { get; set; } = 0;
    }
}
