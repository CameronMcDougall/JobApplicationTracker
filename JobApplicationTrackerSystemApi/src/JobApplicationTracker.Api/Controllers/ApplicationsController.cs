using JobApplicationTracker.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController(ILogger<ApplicationsController> logger) : ControllerBase
    {
        /// <summary>
        /// GetApplication all applications with optional pagination
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
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
        public Task<IActionResult> Update(UpdateApplicationRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
