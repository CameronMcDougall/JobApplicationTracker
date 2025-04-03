using AutoMapper;
using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.Models.Requests;
using JobApplicationTracker.Api.Models.Responses;
using JobApplicationTracker.Api.Models.Results.Enums;
using JobApplicationTracker.Api.Models.Shared;
using JobApplicationTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController(IApplicationService applicationService, IMapper mapper, ILogger<ApplicationsController> logger) : ControllerBase
    {
        /// <summary>
        /// GetApplication all applications with optional pagination
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [ProducesResponseType(typeof(GetApplicationsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(GetApplicationsRequestDto request)
        {
            var mappedOrder = mapper.Map<PagingOrder>(request.PageOrder);
            var application = applicationService.GetApplications(
                request.PageSize,
                request.PageNumber,
                mappedOrder
            );

            var mappedApplications = mapper.Map<IEnumerable<ApplicationDto>>(application.Applications);
            var mappedPagingInfo = mapper.Map<PagingInfoDto>(application.PagingInfo);
            return Ok(
                new GetApplicationsResponseDto
                {
                    Applications = mappedApplications,
                    PagingInfo = mappedPagingInfo
                }
            );
        }

        /// <summary>
        /// GetApplication application by id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetApplicationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute]long id, CancellationToken cancellationToken)
        {
            var getApplicationResult = await applicationService.GetApplication(id, cancellationToken);
            if (getApplicationResult.Status == GetApplicationStatus.ApplicationDoesNotExist)
            {
                return NotFound();
            }

            var mappedApplication = mapper.Map<ApplicationDto>(getApplicationResult.Application);
            return Ok(
                new GetApplicationResponseDto
                {
                    Application = mappedApplication
                }
            );
        }

        /// <summary>
        /// Add an application
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] AddApplicationRequestDto request, CancellationToken cancellationToken)
        {
            var status = mapper.Map<ApplicationStatus>(request.Status);
            await applicationService.AddApplication(
                request.CompanyName,
                request.Position,
                status,
                request.DateApplied,
                cancellationToken
            );

            // Created has a bug where it returns 204 instead of 201
            // https://github.com/dotnet/aspnetcore/issues/53734
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Update an application
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute]long id, [FromBody] UpdateApplicationRequestDto request, CancellationToken cancellationToken)
        {
            var status = mapper.Map<ApplicationStatus>(request.Status);
            var result = await applicationService.UpdateApplication(
                id,
                status,
                cancellationToken
            );

            if (result.Status == UpdateApplicationStatus.ApplicationDoesNotExist)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
