using AdoNetExamples.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetExamples
{
    public class WorkFlow
    {
        public void Run()
        {
            //FirstQuery();
            GetRoster();
        }
        
        public void FirstQuery()
        {
            string connection_string = "Server=localhost;Database=SimpleSchool;User Id=sa;Password=BadPass123;";    //password
                                                                                                                    //private static 
                                                                                                                    // 1 instantiate connection
            using (var connection = new SqlConnection(connection_string))
            {
                var sql = "SELECT * FROM Teacher";

                // 2 instantiate command, give it SQL and the connection to use
                var command = new SqlCommand(sql, connection);

                try
                {
                    // 3 open connection
                    connection.Open();

                    // 4 execute command, use ExecuteReader() for SELECT with multiple rows
                    using (var reader = command.ExecuteReader())
                    {
                        // 5 Loop reader
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["TeacherID"]} : {reader["LastName"]}, {reader["FirstName"]}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void GetRoster() // givensectionID
        {
            Console.Write("Enter Section ID: ");
            var SectionIdInput = Console.ReadLine();

            string connection_string = "Server=localhost;Database=SimpleSchool;User Id=sa;Password=BadPass123;";    //password
                                                                                                                    //private static 
                                                                                                                    // 1 instantiate connection
            var students = new List<SectionRosterView>();
            using (var connection = new SqlConnection(connection_string))
            {
                var sql = "SELECT c.CourseName, sm.StartDate, sm.EndDate, t.FirstName, t.LastName " +
                    "FROM SectionRoster sr " +
                    "INNER JOIN Section sc on sr.SectionID = sc.SectionID " +
                    "INNER JOIN Course c on sc.CourseID = c.CourseID " +
                    "INNER JOIN Semester sm on sc.SemesterID = sm.SemesterID " +
                    "INNER JOIN Teacher t on sc.TeacherID = t.TeacherID " +
                    "WHERE sc.SectionID = @SectionIdInput";   //1 will be swapped for user input

                // 2 instantiate command, give it SQL and the connection to use
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@SectionIdInput", SectionIdInput);

                try
                {
                    // 3 open connection
                    connection.Open();

                    // 4 execute command, use ExecuteReader() for SELECT with multiple rows
                    using (var reader = command.ExecuteReader())
                    {
                        // 5 Loop reader
                        while (reader.Read())
                        {
                            Console.WriteLine($"Course: {reader["CourseName"]}");
                            Console.WriteLine($"StartDate: {reader["StartDate"]}");
                            Console.WriteLine($"EndDate: {reader["EndDate"]}");
                            Console.WriteLine($"Teacher: {reader["TeacherLast"]}, ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                var sql2 = "SELECT st.StudentID, st.LastName, st.FirstName, sr.CurrentGrade " + //[Name] //+ ', ' +
                    "Grade from Student st " +
                    "JOIN SectionRoster sr on st.StudentID = sr.StudentID " +
                    "JOIN Section sc on sr.SectionID = sc.SectionID " +
                    "WHERE sc.SectionID = @SectionIdInput";

                var command2 = new SqlCommand(sql2, connection);
                command2.Parameters.AddWithValue("@SectionIdInput", SectionIdInput);

                try
                {

                    // 4 execute command, use ExecuteReader() for SELECT with multiple rows
                    using (var reader = command2.ExecuteReader())
                    {
                        // 5 Loop reader
                        while (reader.Read())
                        {
                            var row = new SectionRosterView();
                            row.StudentID = (int)reader["StudentId"];
                            row.StudentFirst = (string)reader["FirstName"];
                            row.StudentLast = (string)reader["LastName"];
                            row.CurrentGrade = (decimal)reader["Grade"];
                            //add
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

//"SELECT c.CourseName, sm.StartDate, sm.EndDate, t.FirstName, t.LastName" +
//"FROM Section sc" +
//"INNER JOIN Course c on sc.CourseID = c.CourseID" +
//"INNER JOIN Semester sm on sc.SemesterID = sm.SemesterID" +
//"INNER JOIN Teacher t on sc.TeacherID = t.TeacherID" +
//"WHERE sc.SectionID = 1";

//var row = new SectionDetail();

//row.CourseName = (string)reader["CourseName"];
//row.StartDate = (DateOnly)reader["StartDate"];
//row.EndDate = (DateOnly)reader["EndDate"];
//row.FirstName = (string)reader["FirstName"];
////parse row data and add to a collection?

//if (reader["CurrentGrade"] != DBNull.Value)
//{
//    row.CurrentGrade = reader["CurrentGrade"].ToString();
//}