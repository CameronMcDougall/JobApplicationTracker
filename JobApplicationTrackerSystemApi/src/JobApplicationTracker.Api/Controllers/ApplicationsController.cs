using JobApplicationTracker.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly ILogger<ApplicationsController> _logger;

        public ApplicationsController(ILogger<ApplicationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<IActionResult> Get(GetApplicationsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
