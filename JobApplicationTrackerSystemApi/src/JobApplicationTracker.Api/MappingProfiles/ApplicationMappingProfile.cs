using AutoMapper;
using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.Models.Requests.Enums;
using JobApplicationTracker.Api.Models.Shared;
using DatabaseApplication = JobApplicationTracker.Domain.Models.Application;
using DatabaseApplicationStatus = JobApplicationTracker.Domain.Models.Enums.ApplicationStatus;

namespace JobApplicationTracker.Api.MappingProfiles;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<DatabaseApplication, Application>();
        CreateMap<DatabaseApplicationStatus, ApplicationStatus>();

        CreateMap<Application, ApplicationDto>();
        CreateMap<ApplicationStatus, ApplicationStatusDto>();
        CreateMap<PagingOrderDto, PagingOrder>();
        CreateMap<PagingInfo, PagingInfoDto>();
    }
}