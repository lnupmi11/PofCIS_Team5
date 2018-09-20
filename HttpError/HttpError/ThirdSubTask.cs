using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HttpError
{
    /// <summary>
    /// Class that implements third task.
    /// </summary>
    /// <remarks>
    /// Uses output list of error from first task.
    /// </remarks>
    class ThirdSubTask
    {
        /// <summary>
        /// List of pairs of error code and list of dates when they occured .
        /// </summary>
        public Dictionary<int, List<DateTime>> ErrorCodeDateTimesPairs;

        /// <summary>
        /// Stores path of file, where will be wrote information about errors.
        /// </summary>
        public string FilePath;

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="filePath">Path to file where will be wrote information about errors. </param>
        public ThirdSubTask(string filePath)
        {
            FilePath = filePath;
            ErrorCodeDateTimesPairs = new Dictionary<int, List<DateTime>>();
        }
        /// <summary>
        /// Method that write down information about all errors in dictionary ErrorCodeDataTimePairs. 
        /// </summary>
        /// <param name="listOfErrors">lis of http errors</param>
        public void GetDataFromListOfErrors(List<HttpError> listOfErrors)
        {
            listOfErrors.ForEach(httpError =>
            {
                if (ErrorCodeDateTimesPairs.Keys.Contains(httpError.ErrorCode))
                {
                    ErrorCodeDateTimesPairs[httpError.ErrorCode].Add(httpError.ErrorDate);
                }
                else
                {
                    ErrorCodeDateTimesPairs.Add(httpError.ErrorCode, new List<DateTime> { httpError.ErrorDate });
                }
            });
        }

        /// <summary>
        /// Method that write down information from dictionary ErrorCodeDataTimePairs in file.
        /// </summary>
        public void WriteDataInFile()
        {
            try
            {
                List<string> dataToWriteInFile = new List<string>();
                foreach (var pair in ErrorCodeDateTimesPairs)
                {
                    dataToWriteInFile.Add(DataParser(pair.Key, pair.Value));
                }
                File.WriteAllLines(FilePath, dataToWriteInFile.ToArray());
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
        /// Generate string wich contains data about one element from dictionary ErrorCodeDataTimePairs.
        /// </summary>
        /// <param name="errorCode">code of error</param>
        /// <param name="dates">Dates when error occurre</param>
        /// <returns></returns>
        public string DataParser(int errorCode, List<DateTime> dates)
        {
            string result = $"{errorCode.ToString()}: ";
            dates.ForEach(date => result += $" {date.ToString()}");
            return result;
        }

        /// <summary>
        /// The main method. Runs the task;
        /// </summary>
        public void Run(List<HttpError> errors)
        {
            try
            {
                GetDataFromListOfErrors(errors);
                WriteDataInFile();
                Console.WriteLine("Third task has been ran successfully");
            }
            catch (Exception)
            {
                Console.WriteLine("Running of third task was failed");
            }

        }
    }

}

