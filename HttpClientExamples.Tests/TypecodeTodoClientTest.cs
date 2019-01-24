using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClientExamples.ServiceClients;
using DataFactories = HttpClientExamples.Tests.DataFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpClientExamples.Tests
{
    [TestClass]
    public class TypecodeTodoClientTest
    {
        [TestMethod]
        public async Task Get_returns_a_valid_list_of_todos()
        {
            var todoList = DataFactories.TodosFactory.CreateTodoList(5);
            var httpClient = DataFactories.HttpClientFactory.CreateHttpClient(HttpStatusCode.OK, todoList);

            var todoClient = new TypecodeTodoClient(httpClient);

            var actual = await todoClient.Get();

            Assert.AreEqual(todoList.Count, actual.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task Get_throws_a_HttpRequestExecption_when_response_status_code_is_500() 
        {
            var httpClient = DataFactories.HttpClientFactory.CreateHttpClient(HttpStatusCode.InternalServerError, null);

            var todoClient = new TypecodeTodoClient(httpClient);

            var actual = await todoClient.Get();
        }

        [TestMethod]
        public async Task Get_by_id_returns_a_valid_todo()
        {
            var todo = DataFactories.TodosFactory.CreateTodo(
                DataFactories.TodosFactory.CreateId(),
                DataFactories.TodosFactory.CreateId(),
                DataFactories.TodosFactory.CreateRandomLorem(5),
                true
            );

            var httpClient = DataFactories.HttpClientFactory.CreateHttpClient(HttpStatusCode.OK, todo);

            var todoClient = new TypecodeTodoClient(httpClient);

            var actual = await todoClient.Get(todo.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(todo.Id, actual.Id);
            Assert.AreEqual(todo.UserId, actual.UserId);
            Assert.AreEqual(todo.Title, actual.Title);
            Assert.AreEqual(todo.Completed, actual.Completed);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task Get_by_id_throws_a_HttpRequestExecption_when_response_status_code_is_500() 
        {
            var httpClient = DataFactories.HttpClientFactory.CreateHttpClient(HttpStatusCode.InternalServerError, null);

            var todoClient = new TypecodeTodoClient(httpClient);

            var actual = await todoClient.Get(1);
        }

        [TestMethod]
        public async Task Put_returns_a_valid_todo()
        {
            var todo = DataFactories.TodosFactory.CreateTodo(
                DataFactories.TodosFactory.CreateId(),
                DataFactories.TodosFactory.CreateId(),
                DataFactories.TodosFactory.CreateRandomLorem(5),
                true
            );

            var httpClient = DataFactories.HttpClientFactory.CreateHttpClient(HttpStatusCode.OK, null);

            var todoClient = new TypecodeTodoClient(httpClient);

            var actual = await todoClient.Put(todo.Id, todo);

            Assert.IsTrue(actual);
        }
    }
}
