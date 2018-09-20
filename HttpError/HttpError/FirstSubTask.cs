using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpError
{
    /// <summary>
    /// Class what implements first subtask.
    /// </summary>
    public class FirstSubTask
    {
        /// <summary>
        /// List of HttpErrors which have been written from file.
        /// </summary>
        public List<HttpError> HttpErrors { get; }

        /// <summary>
        /// Stores path of file, where is stored information about errors.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The class default constructor.
        /// </summary>
        public FirstSubTask() : this("") { }

        /// <summary>
        /// The class constructor with parameters.
        /// </summary>
        public FirstSubTask(string filePath)
        {
            HttpErrors = new List<HttpError>();
            FilePath = filePath;
        }

        /// <summary>
        /// Read input text from file.
        /// </summary>
        /// <remarks>
        /// Throws exception if error occurs.
        /// </remarks>
        public string[] ReadDataFromFile()
        {
            try
            {
                return File.ReadAllLines(FilePath);
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
        /// <remarks>
        /// Throws exception if error occurs.
        /// </remarks>
        public void Run()
        {
            try
            {
                string[] allLines = ReadDataFromFile();
                foreach (var line in allLines)
                {
                    try
                    {
                        HttpErrors.Add(GetHttpErrorFromLine(line));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"HttpError hasn't been added. This line is incorrect: \"{line}\" .");
                    }
                }
                Console.WriteLine("First task has been ran successfully");
            }
            catch (Exception)
            {
                Console.WriteLine("Running of first task was failed.");
            }
        }

        /// <summary>
        /// Get data about Http error from line. And return HttpError object.
        /// </summary>
        /// <remarks>
        /// Throws exception if error occurs.
        /// </remarks>
        public HttpError GetHttpErrorFromLine(string line)
        {
            string[] data = line.Split('-');
            try
            {
                int HttpErrorNumber = Int32.Parse(data[(int)HttpErrorDataRepresentationOrder.indexOfErrorNumber]);
                string HttpErrorDescription = data[(int)HttpErrorDataRepresentationOrder.indexOfErrorDiscription];
                DateTime HttpErrorDate = GenerateDateTimeObject(data[(int)HttpErrorDataRepresentationOrder.indexOfErrorDate]);
                HttpError error = new HttpError(HttpErrorNumber, HttpErrorDescription, HttpErrorDate);
                return error;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Invalid format of data. Http error should be presented in such format: Error number - error description - error date.");
                throw new Exception();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format http error number.Error number should be an integer.");
                throw new Exception(); ;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Get data about Time from line. And return DateTime object.
        /// </summary>
        /// <remarks>
        /// Throws exception if error occurs.
        /// </remarks>
        public DateTime GenerateDateTimeObject(string line)
        {
            try
            {
                string[] dateData = line.Split('/');
                int year = Int32.Parse(dateData[(int)DateRepresentationOrder.year]);
                int month = Int32.Parse(dateData[(int)DateRepresentationOrder.month]);
                int day = Int32.Parse(dateData[(int)DateRepresentationOrder.day]);
                int hour = Int32.Parse(dateData[(int)DateRepresentationOrder.hour]);
                int minute = Int32.Parse(dateData[(int)DateRepresentationOrder.minute]);
                int second = Int32.Parse(dateData[(int)DateRepresentationOrder.second]);
                return new DateTime(year, month, day, hour, minute, second);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Invalid format of data. Data should be presented in such format: years,month,day,hour,minutes,second.");
                throw new Exception();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format of data. Years//month//day//hour//minutes//second should be an integer.");
                throw new Exception(); ;
            }
        }
        /// <summary>
        ///  Enum wich represents order of data representing about Http error.
        /// </summary>
        private enum HttpErrorDataRepresentationOrder { indexOfErrorNumber, indexOfErrorDiscription, indexOfErrorDate }
        /// <summary>
        ///  Enum wich represents order of data representing about DateTime. Date: year/month/day/hour/minute/second.
        /// </summary>
        private enum DateRepresentationOrder { year, month, day, hour, minute, second }
    }
}
