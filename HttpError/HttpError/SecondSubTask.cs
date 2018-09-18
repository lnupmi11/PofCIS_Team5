using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpError
{
    /// <summary>
    /// Class that implements second task.
    /// </summary>
    /// <remarks>
    /// Uses output list of error from first task.
    /// </remarks>
    public class SecondSubTask
    {
        /// <summary>
        /// Stores input text.
        /// </summary>
        public string InputText { get; private set; }
        /// <summary>
        /// Stores output text after run.
        /// </summary>
        public string OutputText { get; private set; }
        /// <summary>
        /// Stores path to input text;
        /// </summary>
        public string FilePath { get; private set; }
        /// <summary>
        /// The class default constructor.
        /// </summary>
        public SecondSubTask() : this("") { }
        /// <summary>
        /// The class constructor with parameters.
        /// </summary>
        /// <param name="filePath"> path to input text </param>
        public SecondSubTask(string filePath)
        {
            FilePath = filePath;
        }
        /// <summary>
        /// Read input text from file.
        /// </summary>
        /// <remarks>
        /// Throws exception if error occurs.
        /// </remarks>
        public string ReadDataFromFile()
        {
            try
            {
                return File.ReadAllText(FilePath);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"There is no directory with this path: {FilePath}.");
                throw new Exception();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"There is no file with this path: {FilePath}.");
                throw new Exception();
            }
        }
        /// <summary>
        /// The main method. Runs the task;
        /// </summary>
        /// <param name="filePath"> path to text for the first task </param>
        public void Run(string filePath)
        {
            try
            {
                InputText = ReadDataFromFile();
                FirstSubTask task1 = new FirstSubTask(filePath);
                task1.Run();
                OutputText = InputText;
                foreach (var error in task1.HttpErrors)
                {
                    if( OutputText.Contains(error.ErrorCode.ToString()))
                    {
                        OutputText = OutputText.Replace(error.ErrorCode.ToString(), error.ErrorDescription);
                    }
                }
                Console.WriteLine(OutputText);
            }
            catch (Exception)
            {
                Console.WriteLine("Running of first task was failed.");
            }
        }
    }
}
