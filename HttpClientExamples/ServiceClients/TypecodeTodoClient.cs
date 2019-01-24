using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClientExamples.Models;
using Newtonsoft.Json;

namespace HttpClientExamples.ServiceClients {
    
    public interface ITypecodeTodoClient
    {
        Task<List<Todo>> Get();
        Task<Todo> Get(int id);
        Task Post(Todo todo);
        Task<bool> Put(int id, Todo todo);
    }
    
    public class TypecodeTodoClient : ITypecodeTodoClient
    {
        private static string PATH = "todos";
        private static string MIME_TYPE = "application/json";

        private HttpClient _httpClient = null;
        public TypecodeTodoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task<List<Todo>> Get()
        {
            var todos = new List<Todo>();
            var request = await _httpClient.GetAsync(PATH);
            
            if (request.IsSuccessStatusCode) {
                todos = await request.Content.ReadAsAsync<List<Todo>>();
            }
            else {
                throw new HttpRequestException($"{request.StatusCode} : Error retrieving the todos");
            }

            return todos;
        }

        public async Task<Todo> Get(int id)
        {
            var todo = new Todo();

            var request = await _httpClient.GetAsync($"{PATH}/{id}");

            if(request.IsSuccessStatusCode) 
            {
                todo = await request.Content.ReadAsAsync<Todo>();
            }
            else 
            {
                throw new HttpRequestException($"{request.StatusCode} : Error retrieving the todos");
            }

            return todo;
        }

        public Task Post(Todo todo)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<bool> Put(int id, Todo todo)
        {
            var request = await _httpClient.PutAsync(
                $"{PATH}/{id}", 
                new StringContent(
                    JsonConvert.SerializeObject(todo),
                    Encoding.UTF8,
                    MIME_TYPE
                )
            );

            if(!request.IsSuccessStatusCode) 
            {
                throw new HttpRequestException($"{request.StatusCode} : Error retrieving the todos");
            }

            return true;
        }
    }
}