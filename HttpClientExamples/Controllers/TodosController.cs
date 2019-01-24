using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientExamples.Models;
using HttpClientExamples.ServiceClients;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private ITypecodeTodoClient TodoClient { get; set; }
        public TodosController(ITypecodeTodoClient todoClient) => TodoClient = todoClient;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok( await TodoClient.Get() );

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await TodoClient.Get(id));

        [HttpPost]
        public void Post([FromBody] Todo value) => TodoClient.Post(value);

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Todo value) => TodoClient.Put(id, value);

        // [HttpGet("completed")]
        // public async Task<IActionResult> GetCompleted() => throw new NotImplementedException();

        // [HttpGet("completed/count")]
        // public async Task<IActionResult> GetCompletedCount() => throw new NotImplementedException();
    }
}
