using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;
using System;

namespace SomerenDAL
{
    public class StudentDao : BaseDao
    {
        public List<Student> GetAllStudents()
        {
            string query = "SELECT * FROM [student] JOIN [human] ON student.id = human.id";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Student> ReadTables(DataTable dataTable)
        {
            List<Student> students = new List<Student>();
            int current_student_id = 0;
            foreach (DataRow dr in dataTable.Rows)
            {
                current_student_id = (int)dr["id"];

                Student student = new Student()
                {
                    Id = current_student_id,
                    Number = (int)dr["student_id"],
                    Name = dr["first_name"].ToString() + " " +
                            dr["last_name"].ToString(),
                    BirthDate = DateTime.Parse(dr["dob"].ToString())
            };
                students.Add(student);
            }
            return students;
        }
    }
}