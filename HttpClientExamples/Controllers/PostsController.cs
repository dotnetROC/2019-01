using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClientExamples.Models;
using HttpClientExamples.ServiceClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HttpClientExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IConfiguration _configuracion;
        private static HttpClient _httpClient;
        public PostsController(IConfiguration configuration)
        {
            _configuracion = configuration;

            if (_httpClient == null)
            {
                _httpClient = new HttpClient() {
                    BaseAddress = new Uri(_configuracion["Typecode:BaseAddress"])
                };
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var path = "posts";
            List<Post> posts = new List<Post>();

            var request = await _httpClient.GetAsync(path);
            
            if (request.IsSuccessStatusCode) 
            {
                posts = await request.Content.ReadAsAsync<List<Post>>();
            }
            else
            {
                return StatusCode(500);
            }

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var path = $"posts/{id}";
            Post post = new Post();

            var request = await _httpClient.GetAsync(path);
            
            if (request.IsSuccessStatusCode) 
            {
                post = await request.Content.ReadAsAsync<Post>();
            }
            else
            {
                return StatusCode(500);
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post value) {
            var path = "posts";

            var request = await _httpClient.PostAsJsonAsync(path, value);

            return StatusCode((int)request.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Post value) {
            var path = $"posts/{id}";

            var request = await _httpClient.PutAsJsonAsync(path, value);

            var content = await request.Content.ReadAsStringAsync();

            return StatusCode((int)request.StatusCode);
        }
    }
}
