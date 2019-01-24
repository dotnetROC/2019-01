
using System;
using System.Collections.Generic;
using System.Linq;
using HttpClientExamples.Models;

namespace HttpClientExamples.Tests.DataFactories
{
    public abstract class TodosFactory {

        private static Random randomIndex = new Random(0);
        private static Random randomId = new Random(1);
        public static List<Todo> CreateTodoList(int count) {

            var todos = 
                Enumerable.Range(1, count)
                    .Select(n => CreateTodo(
                        CreateId(),
                        CreateId(),
                        CreateRandomLorem(5),
                        randomId.Next() % 2 == 0)
                ).ToList();

            return todos;
        }

        public static Todo CreateTodo(int id, int userId, string title, bool completed) {
            return new Todo() {
                Id = id,
                UserId = userId,
                Title = title,
                Completed = completed
            };
        }

        public static int CreateId() {

            return randomId.Next(1, 100);
        }

        public static string CreateRandomLorem(int length) {

            string[] words = {
                "Lorem", "ipsum", "dolor", "sit", "amet,", "consectetur", "adipiscing", "elit.", "Phasellus", "lacinia", "ex", "orci", 
                "sed", "facilisis", "risus", "fringilla", "maximus.", "Proin", "sed", "metus", "arcu.", "Aliquam", "id", "mattis", "tortor", 
                "Sed", "nec", "lobortis", "urna,", "eget", "consectetur", "nibh.", "Vivamus", "lobortis", "neque", "urna,", "at", "aliquam", 
                "felis", "lobortis", "non.", "Proin", "rhoncus", "elit", "elit", "ac", "dictum", "magna", "commodo", "eget"
            };

            string[] randoms = 
                Enumerable.Range(1, length)
                    .Select(
                        n => words[randomIndex.Next(0, 49)]
                    ).ToArray();

            return string.Join(" ", randoms);
        }
    }
}