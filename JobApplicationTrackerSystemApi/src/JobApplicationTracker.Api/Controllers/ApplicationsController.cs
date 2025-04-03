using JobApplicationTracker.Api.Models.Requests;
using JobApplicationTracker.Api.Models.Responses;
using JobApplicationTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController(IApplicationService applicationService, ILogger<ApplicationsController> logger) : ControllerBase
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
        public Task<IActionResult> Get(GetApplicationsRequestDto request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetApplication application by id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("/{Id:long}")]
        [ProducesResponseType(typeof(GetApplicationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetById(GetApplicationRequestDto request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add an application
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Add(AddApplicationRequestDto request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update an application
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("/{Id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Update(UpdateApplicationRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
