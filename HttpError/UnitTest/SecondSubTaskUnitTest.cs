using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpError;

namespace UnitTest
{
    [TestClass]
    public class SecondSubTaskUnitTest
    {
        private const string FIRST_FILE = "inputSecondSubTask.txt";
        private const string SECOND_FILE = "testSecondSubTask.txt";

        [TestMethod]
        public void SecondSubTaskRunTest()
        {
            SecondSubTask task2 = new SecondSubTask(SECOND_FILE);
            task2.Run(FIRST_FILE);
            string actual = "I was googling and error Not Found happend and also Bad Requst";
            Assert.AreEqual(actual, task2.OutputText);
        }
    }
}
