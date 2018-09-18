using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpError
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstSubTask task = new FirstSubTask("enterPathOfYourFileHere");
            task.Run();
            Console.ReadLine();
        }
    }
}
