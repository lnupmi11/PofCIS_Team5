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

        static void Main(string[] args)
        {
            FirstSubTask task = new FirstSubTask(FIRST_FILE);
            task.Run();
            SecondSubTask task2 = new SecondSubTask(SECOND_FILE);
            task2.Run(FIRST_FILE);
            Console.ReadLine();
        }
    }
}
