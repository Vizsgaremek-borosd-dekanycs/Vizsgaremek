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
    public class TreatmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateOfTreatment { get; set; } = DateTime.Now;
        public string Type { get; set; }
        public string? Symptoms { get; set; }
        public string? Diagnosis { get; set; }
        public string? Medications { get; set; }
        public string? Procedure { get; set; }
        public string? Description { get; set; }
        public DateTime? FollowUpDate { get; set; }
    }

    internal class ListPatientTreatmentClientQueryHandler(IMediator mediator, IMapper mapper) 
        : IRequestHandler<ListPatientTreatmentClientQuery, ListPatientTreatmentClientQueryResponse>
    {
        public static TreatmentDto[] treatments = new TreatmentDto[]
        {
            new TreatmentDto { Id = 1, PatientId = 1, DoctorId = 101, DateOfTreatment = new DateTime(2023, 1, 15), Type = "Vaccination", Symptoms = "None", Diagnosis = "Healthy", Medications = "Vaccine A", Procedure = "Injection", Description = "Annual vaccination", FollowUpDate = new DateTime(2024, 1, 15) },
            new TreatmentDto { Id = 2, PatientId = 2, DoctorId = 102, DateOfTreatment = new DateTime(2023, 2, 20), Type = "Checkup", Symptoms = "Lethargy", Diagnosis = "Diabetes", Medications = "Insulin", Procedure = "Blood Test", Description = "Routine checkup", FollowUpDate = new DateTime(2023, 3, 20) },
            new TreatmentDto { Id = 3, PatientId = 3, DoctorId = 103, DateOfTreatment = new DateTime(2023, 3, 10), Type = "Surgery", Symptoms = "Limping", Diagnosis = "Fracture", Medications = "Painkillers", Procedure = "Bone Surgery", Description = "Fracture repair", FollowUpDate = new DateTime(2023, 4, 10) },
            new TreatmentDto { Id = 4, PatientId = 4, DoctorId = 104, DateOfTreatment = new DateTime(2023, 4, 5), Type = "Dental", Symptoms = "Bad breath", Diagnosis = "Gingivitis", Medications = "Antibiotics", Procedure = "Teeth Cleaning", Description = "Dental cleaning", FollowUpDate = new DateTime(2023, 5, 5) },
            new TreatmentDto { Id = 5, PatientId = 5, DoctorId = 105, DateOfTreatment = new DateTime(2023, 5, 25), Type = "Allergy Test", Symptoms = "Itching", Diagnosis = "Allergies", Medications = "Antihistamines", Procedure = "Skin Test", Description = "Allergy testing", FollowUpDate = new DateTime(2023, 6, 25) },
            new TreatmentDto { Id = 6, PatientId = 6, DoctorId = 106, DateOfTreatment = new DateTime(2023, 6, 15), Type = "Vaccination", Symptoms = "None", Diagnosis = "Healthy", Medications = "Vaccine B", Procedure = "Injection", Description = "Annual vaccination", FollowUpDate = new DateTime(2024, 6, 15) },
            new TreatmentDto { Id = 7, PatientId = 7, DoctorId = 107, DateOfTreatment = new DateTime(2023, 7, 10), Type = "Checkup", Symptoms = "Vomiting", Diagnosis = "Gastroenteritis", Medications = "Antiemetics", Procedure = "Ultrasound", Description = "Routine checkup", FollowUpDate = new DateTime(2023, 8, 10) },
            new TreatmentDto { Id = 8, PatientId = 8, DoctorId = 108, DateOfTreatment = new DateTime(2023, 8, 20), Type = "Surgery", Symptoms = "Swelling", Diagnosis = "Tumor", Medications = "Chemotherapy", Procedure = "Tumor Removal", Description = "Tumor removal surgery", FollowUpDate = new DateTime(2023, 9, 20) },
            new TreatmentDto { Id = 9, PatientId = 9, DoctorId = 109, DateOfTreatment = new DateTime(2023, 9, 5), Type = "Dental", Symptoms = "Toothache", Diagnosis = "Cavity", Medications = "Painkillers", Procedure = "Tooth Extraction", Description = "Dental extraction", FollowUpDate = new DateTime(2023, 10, 5) },
            new TreatmentDto { Id = 10, PatientId = 10, DoctorId = 110, DateOfTreatment = new DateTime(2023, 10, 15), Type = "Allergy Test", Symptoms = "Sneezing", Diagnosis = "Allergies", Medications = "Antihistamines", Procedure = "Blood Test", Description = "Allergy testing", FollowUpDate = new DateTime(2023, 11, 15) }
        };

        public async Task<ListPatientTreatmentClientQueryResponse> Handle(ListPatientTreatmentClientQuery request, CancellationToken cancellationToken)
        {
            ListPatientTreatmentClientQueryResponse treatmentListClientQueryResponse = new ListPatientTreatmentClientQueryResponse();
            List<TreatmentDto> treatments = ListPatientTreatmentClientQueryHandler.treatments.AsQueryable().Skip(request.Skip).Take((int)request.Take).ToList();
            treatmentListClientQueryResponse.Treatments = treatments;
            treatmentListClientQueryResponse.ResultCount = treatments.Count;
            return await Task.FromResult(treatmentListClientQueryResponse);
        }
    }

    //internal class ListAnimalTypeApiQueryHandler : GenericApiCommandHandler<ListAnimalTypeApiQuery, ListAnimalTypeApiQueryResponse>
    //{
    //    public ListAnimalTypeApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
    //        : base(serviceScopeFactory)
    //    {
    //    }
    //}
}

