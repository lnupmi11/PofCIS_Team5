using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpError
{
    class Program
    {
        const string FIRST_FILE = "..//..//data//first.txt";
        const string SECOND_FILE = "..//..//data//second.txt";
        const string THIRD_FILE = "..//..//data//third.txt";

        static void Main(string[] args)
        {
            FirstSubTask task1 = new FirstSubTask(FIRST_FILE);
            task1.Run();
            SecondSubTask task2 = new SecondSubTask(SECOND_FILE);
            task2.Run(FIRST_FILE);
            ThirdSubTask task3 = new ThirdSubTask(THIRD_FILE);
            task3.Run(task1.HttpErrors);
            Console.ReadLine();
        }
    }
}
