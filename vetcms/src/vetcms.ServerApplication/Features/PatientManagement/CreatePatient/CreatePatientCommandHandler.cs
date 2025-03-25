using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.CreatePatient
{
    internal class CreatePatientCommandHandler(IMapper mapper, IPatientRepository patientRepository) : IRequestHandler<CreatePatientApiCommand, CreatePatientApiCommandResponse>
    {
        public async Task<CreatePatientApiCommandResponse> Handle(CreatePatientApiCommand request, CancellationToken cancellationToken)
        {
            // Check if an animal type with the same TypeName already exists
            var existingPatient = patientRepository.Where(pt => pt.MicrochipNumber != "Nincs mikrocsip" && pt.MicrochipNumber == request.NewPatient.MicrochipNumber).FirstOrDefault();
            if (existingPatient != null)
            {
                return await Task.FromResult(new CreatePatientApiCommandResponse(false, "Már létezik ilyen nevű bejegyzés az adatbázisban."));
            }

            Patient newPatient = mapper.Map<Patient>(request.NewPatient);
            _ = await patientRepository.AddAsync(newPatient);
            return await Task.FromResult(new CreatePatientApiCommandResponse(true));
        }
    }
}
