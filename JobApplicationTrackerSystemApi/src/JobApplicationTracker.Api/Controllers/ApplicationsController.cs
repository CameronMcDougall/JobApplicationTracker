using JobApplicationTracker.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController(ILogger<ApplicationsController> logger) : ControllerBase
    {
        /// <summary>
        /// Get all applications with optional pagination
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
        /// Get application by id
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
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public Task<IActionResult> Add()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update an application
        /// </summary>
        /// <returns></returns>
        [HttpPatch("/{Id:long}")]
        public Task<IActionResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
