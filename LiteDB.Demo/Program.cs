using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LiteDB.Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var listOfTestClasses = Enumerable.Range(0, 10)
                .Select(days =>
                    new TestClassIssueDateTimeOffset
                    {
                        Id = Guid.NewGuid(),
                        DateTimeOffset = DateTimeOffset.Now.AddDays(days * -1)
                    })
                .ToList();

            using (var db = new LiteDatabase(new MemoryStream()))
            {
                var table = db.GetCollection<TestClassIssueDateTimeOffset>();

                table.Insert(listOfTestClasses);
                var fetchedObject = table.FindById(listOfTestClasses[0].Id);

                var dateTimeOffsetConstraint = DateTimeOffset.Now.AddDays(-2);
                var objectQuery = table.Find(Query.GTE(nameof(TestClassIssueDateTimeOffset.DateTimeOffset), dateTimeOffsetConstraint)).ToList();

                Debugger.Break();
            }
        }

        private class TestClassIssueDateTimeOffset
        {
            public Guid Id { get; set; }
            public DateTimeOffset DateTimeOffset { get; set; }
        }
    }
}