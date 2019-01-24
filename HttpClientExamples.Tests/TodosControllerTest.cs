using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClientExamples.ServiceClients;
using DataFactories = HttpClientExamples.Tests.DataFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HttpClientExamples.Controllers;
using Microsoft.AspNetCore.Mvc;
using HttpClientExamples.Models;

namespace HttpClientExamples.Tests
{
    [TestClass]
    public class TodosControllerTest
    {
        [TestMethod]
        public async Task Get_by_id_should_return_a_valid_todo() {

            var expectedTodo = DataFactories.TodosFactory.CreateTodo(
                        DataFactories.TodosFactory.CreateId(),
                        DataFactories.TodosFactory.CreateId(),
                        DataFactories.TodosFactory.CreateRandomLorem(20),
                        true
                    );

            var todoClientMock = new Mock<ITypecodeTodoClient>();

            todoClientMock
                .Setup(client => client.Get(It.IsAny<int>()))
                .ReturnsAsync(expectedTodo);

            var controller = new TodosController(todoClientMock.Object);

            var result = await controller.Get(1) as ObjectResult;

            Assert.IsNotNull(result.Value);

            var actualTodo = result.Value as Todo;
            
            Assert.AreEqual(expectedTodo.Id, actualTodo.Id);
            Assert.AreEqual(expectedTodo.UserId, actualTodo.UserId);
            Assert.AreEqual(expectedTodo.Title, actualTodo.Title);
            Assert.AreEqual(expectedTodo.Completed, actualTodo.Completed);
        }
    }
}