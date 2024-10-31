using HttpClientDemo.HttpClientServices;
using HttpClientDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly CRUDHttpService _service;
        public PostsController(CRUDHttpService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _service.GetAll<Post>();

            return Ok(result);
        }
    }
}
