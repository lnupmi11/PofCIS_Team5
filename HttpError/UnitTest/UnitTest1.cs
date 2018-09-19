using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpError;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string FIRST_FILE = "first.txt";

        [TestMethod]
        public void ReadDataFromFileTestMethodIsNotNull()
        {
            FirstSubTask firstSubTask = new FirstSubTask(FIRST_FILE);
            string[] result = firstSubTask.ReadDataFromFile();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetHttpErrorFromLinesTestMethodIsInstanceOfType()
        {
            FirstSubTask firstSubTask = new FirstSubTask(FIRST_FILE);
            string[] data = firstSubTask.ReadDataFromFile();
            foreach (string line in data)
            {
                var httpError = firstSubTask.GetHttpErrorFromLine(line);
                Assert.IsInstanceOfType(httpError, typeof(HttpError.HttpError));
            }
        }

        [TestMethod]
        public void GenerateDateTimeObjectTestMethodAreEqual()
        {
            DateTime customDateTime = new DateTime(2018, 12, 19, 18, 0, 1);

            FirstSubTask firstSubTask = new FirstSubTask();
            string line = "2018, 12, 19, 18, 0, 1";
            DateTime resultOfMethod = firstSubTask.GenerateDateTimeObject(line);

            Assert.AreEqual(customDateTime, resultOfMethod);
        }
    }
}
