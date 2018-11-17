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
    class TaskImplementation
    {
        string connectionString;
        SqlConnection connection;

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


    }
}


