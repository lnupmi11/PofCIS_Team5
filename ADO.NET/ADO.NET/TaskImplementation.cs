using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace ADO.NET
{
    /// <summary>
    /// Class that implements quries to db.
    /// </summary>
    class TaskImplementation
    {
        string connectionString;
        SqlConnection connection;

        /// <summary>
        /// Constructor initializes connection string
        /// </summary>
        public TaskImplementation()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ToString();
        }

        /// <summary>
        /// Function to open connection to database.
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("------ Database Connection is open --------------");
                }
            }
            catch (SqlException sqlexception)
            {
                Console.WriteLine(sqlexception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Function to close connection to database.
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("------ Database Connection is closed --------------");
                }
                else
                {
                    Console.WriteLine("------ Database Connection is  already closed --------------");
                }
            }
            catch (SqlException sqlexception)
            {
                Console.WriteLine(sqlexception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }


        /// <summary>
        /// Function to query full information about employee
        /// </summary>
        /// <param name="id">employee's id</param>
        public void ShowInformationAboutEmployeeWithId(int id)
        {
            string queryText = $"SELECT * FROM [Employees] " +
                $"WHERE EmployeeID = {id}";
            string columnNamesText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = 'Employees' and t.type = 'U'";
            List<string> listOfData = new List<string>();
            List<string> listacolumnas = new List<string>();
            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Firs Task data about employee whoose id equels 8");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                reader.Read();
                object[] dataToDisplay = new object[reader.FieldCount];
                reader.GetValues(dataToDisplay);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    listOfData.Add(dataToDisplay[i].ToString());
                }
            }

            sqlCommand = new SqlCommand(columnNamesText, connection);
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                reader.Read();
                while (reader.Read())
                {
                    listacolumnas.Add(reader.GetString(0));
                }
            }


            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Employee data");
            Console.WriteLine("-------------------------------------------");

            for(int i=0;i<listacolumnas.Count;i++)
            {
                Console.WriteLine($"{listacolumnas[i]}: {listOfData[i+1]}");
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of first task");
            Console.WriteLine("-------------------------------------------\n\n");

        }


        /// <summary>
        /// Function to query employee whith same
        /// first character in first and last names
        /// </summary>
        /// <param name="startChar">firt character</param>
        public void ShowListOfEmployeeFirstAndLastNamesWhichStartsOn(char startChar)
        {
            string queryText = $"SELECT FirstName, LastName FROM [Employees] " +
                $"WHERE FirstName LIKE '{startChar}%'";//+

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Third Task All employees whoose name starts with A");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("First Name - Last Name");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of third task");
            Console.WriteLine("-------------------------------------------\n\n");
        }

        /// <summary>
        /// Function to query all employees from same city
        /// </summary>
        /// <param name="city">city name</param>
        public void ShowListOfEmployeeFirstAndLastNamesFromCity(string city)
        {
            string queryText = $"SELECT FirstName, LastName FROM [Employees] " +
                $"WHERE City = '{city}'";//+

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Second Task All employees who are from London");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("First Name - Last Name");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of Second task");
            Console.WriteLine("-------------------------------------------\n\n");
        }

        /// <summary>
        /// Function to query employees first and last name
        /// with age more then param
        /// </summary>
        /// <param name="age">minimum age</param>
        public void ShowListOfEmployeeFirstAndLastNamesWithAgeMoreThan(int age)
        {
            string queryText = $"SELECT FirstName, LastName, {DateTime.Now.ToString("yyyy")} - datepart(yyyy,BirthDate) FROM [Employees] " +
                $"WHERE {DateTime.Now.ToString("yyyy")} - datepart(yyyy,BirthDate) > {age} " +
                $"ORDER BY LastName";//+

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Fourth Task 4. List of first, last names and ages of the employees whose age is greater than 55.");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("First Name - Last Name - age");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]} - {reader[2]}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of Fourth task");
            Console.WriteLine("-------------------------------------------\n\n");
        }

        /// <summary>
        /// Function to query count of employess
        /// from same city
        /// </summary>
        /// <param name="city">city name</param>
        public void ShowCountEmployeesOfCity(string city)
        {
            string queryText = $"SELECT COUNT(EmployeeID) FROM Employees" +
                $" WHERE City = '{city}'";

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Fifth Task. employees count from London");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("City - Count ");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    Console.WriteLine($"{city} - {reader[0]}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of Fifth task");
            Console.WriteLine("-------------------------------------------\n\n");
        }

        /// <summary>
        /// Function to query max, min, average age of 
        /// employees from same city
        /// </summary>
        /// <param name="name">city name</param>
        public void ShowMaxMinAndAvgAgeOfEmployeeOfCity(string city)
        {
            string queryText = $"WITH Temp AS (SELECT * FROM Employees WHERE City = '{city}')"+
                   "SELECT MAX(BirthDate), MIN(BirthDate) FROM Temp ";

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Sixth Task. MIN , MAX age of employee from London");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Min Age - Max Age ");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    int curYear = int.Parse(DateTime.Now.ToString("yyyy"));

                    List<char> fullDate = reader[0].ToString().TakeWhile(symb=> symb != ' ').ToList();
                    string date = string.Empty;
                    for (int i = 4; i > 0 ; i--)
                    {
                         date += fullDate[fullDate.Count - i];
                    }
                    int maxYear = int.Parse(date);
                    fullDate.Clear();
                    date = string.Empty;
                    fullDate = reader[1].ToString().TakeWhile(symb => symb != ' ').ToList();
                    for (int i = 4; i > 0; i--)
                    {
                        date += fullDate[fullDate.Count - i];
                    }
                    int minYear = int.Parse(date);
                    Console.WriteLine($"{curYear - maxYear} - {curYear - minYear}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of Sixth task");
            Console.WriteLine("-------------------------------------------\n\n");
        }

        /// <summary>
        /// Function to query min, max, average age of
        /// employees of every city 
        /// </summary>
        public void ShowMinMaxAvgForEveryCity()
        {
            string queryText = $"SELECT City, MIN({DateTime.Now.ToString("yyyy")} - datepart(yyyy,BirthDate))," +
                $" MAX({DateTime.Now.ToString("yyyy")} - datepart(yyyy,BirthDate))," +
                $" AVG({DateTime.Now.ToString("yyyy")} - datepart(yyyy,BirthDate)) FROM Employees" +
                $" GROUP BY City";

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Seventh Task. city, min, max, avg");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" Ciyt - Min - Max - Avg");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]} - {reader[2]}- {reader[3]}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of Seventh task");
            Console.WriteLine("-------------------------------------------\n\n");
        }

        /// <summary>
        /// Function to query cities with 
        /// average age greater then param
        /// </summary>
        /// <param name="age">minimum age</param>
        public void ShowСitiesWithAvgAgeGT(int age)
        {
            string queryText = $"SELECT City, AVG({DateTime.Now.ToString("yyyy")} - datepart(yyyy,BirthDate)) FROM Employees" +
                  $" GROUP BY City" +
                  $" HAVING AVG({DateTime.Now.ToString("yyyy")} - datepart(yyyy,BirthDate)) >= {age}";

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Eighth Task. city, avg age >= {age}");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("City - AvgAge");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of Eighth task");
            Console.WriteLine("-------------------------------------------\n\n");
        }

        /// <summary>
        /// Function to query oldest employee
        /// first name and last name
        /// </summary>
        public void ShowOldestEmployee()
        {
            string queryText = $"SELECT TOP 1 FirstName, LastName" +
                " FROM Employees ORDER BY BirthDate";

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Ninth Task. FirstName, LastName, eldest employee");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("FirsName - LastName");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of Ninth task");
            Console.WriteLine("-------------------------------------------\n\n");
        }

        /// <summary>
        /// Function to query num of oldest employee
        /// first name and last name
        /// </summary>
        /// <param name="num">Number of oldest people</param>
        public void ShowOldestEmployees(int num)
        {
            string queryText = $"SELECT TOP {num} FirstName, LastName, {DateTime.Now.ToString("yyyy")} - datepart(yyyy,BirthDate) " +
                " FROM Employees ORDER BY BirthDate";

            SqlCommand sqlCommand = new SqlCommand(queryText, connection);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Tenth Task. FirstName, LastName, Age of {num} eldest employees");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("FirsName - LastName - Age ");
                Console.WriteLine("-------------------------------------------");
                reader.Read();
                do
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]} - {reader[2]}");
                } while (reader.Read());
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of Tenth task");
            Console.WriteLine("-------------------------------------------\n\n");
        }
    }
}


