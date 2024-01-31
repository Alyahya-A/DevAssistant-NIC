using Dev.Assistant.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Dev.Assistant.Test
{
    [TestClass]
    public class SqlServiceUT
    {

        //public SqlServiceUT();
        private readonly string _folderPath = @"C:\Project\Personal\DevAssistant\Dev.Assistant.Test\TestData";

        [TestMethod]
        public void CodeToSql_1()
        {
            var actualQuery = File.ReadAllText(@$"{_folderPath}\CodeToSql_1_actual.txt");

            string cleanedSql = SqlService.CodeToSql(actualQuery);


            var expectedQuery = File.ReadAllText(@$"{_folderPath}\CodeToSql_1_expected.txt");

            Assert.AreEqual(expectedQuery, cleanedSql, "Test1 failed");
        }

        [TestMethod]
        public void CodeToSql_2()
        {
            var actualQuery = File.ReadAllText(@$"{_folderPath}\CodeToSql_2_actual.txt");

            string cleanedSql = SqlService.CodeToSql(actualQuery);


            var expectedQuery = File.ReadAllText(@$"{_folderPath}\CodeToSql_2_expected.txt");

            Assert.AreEqual(expectedQuery, cleanedSql, "Test1 failed");
        }

        [TestMethod]
        public void CodeToSql_3()
        {
            var actualQuery = File.ReadAllText(@$"{_folderPath}\CodeToSql_3_actual.txt");

            string cleanedSql = SqlService.CodeToSql(actualQuery);


            var expectedQuery = File.ReadAllText(@$"{_folderPath}\CodeToSql_3_expected.txt");

            Assert.AreEqual(expectedQuery, cleanedSql, "Test1 failed");
        }

        [TestMethod]
        public void CodeToSql_4()
        {
            var actualQuery = File.ReadAllText(@$"{_folderPath}\CodeToSql_1_expected.txt");

            string cleanedSql = SqlService.CodeToSql(actualQuery);


            var expectedQuery = File.ReadAllText(@$"{_folderPath}\CodeToSql_1_expected.txt");

            Assert.AreEqual(expectedQuery, cleanedSql, "Test1 failed");
        }


    }
}
