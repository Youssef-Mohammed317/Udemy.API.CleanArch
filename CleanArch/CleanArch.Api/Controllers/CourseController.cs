using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CourseViewModel courseViewModel)
        {
            _courseService.Create(courseViewModel);

            return Ok(courseViewModel);
        }
    }
}
