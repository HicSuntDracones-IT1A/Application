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
            string query = "SELECT * FROM [student]";
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

                string query = $"SELECT * FROM [human] WHERE id={current_student_id}";
                SqlParameter[] sqlParameters = new SqlParameter[0];
                DataRow dt_current_student = ExecuteSelectQuery(query, sqlParameters).Rows[0];

                Student student = new Student()
                {
                    Id = current_student_id,
                    Number = (int)dr["student_id"],
                    Name = dt_current_student["first_name"].ToString() + " " +
                            dt_current_student["last_name"].ToString(),
                    BirthDate = DateTime.Parse(dr["dob"].ToString())
            };
                students.Add(student);
            }
            return students;
        }
    }
}