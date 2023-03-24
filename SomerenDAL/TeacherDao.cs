using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;

namespace SomerenDAL
{
    public class TeacherDao : BaseDao
    {
        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT * FROM [teacher] JOIN [human] ON teacher.id = human.id";

            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }



        private List<Teacher> ReadTables(DataTable dataTable)
        {
            List<Teacher> teachers = new List<Teacher>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Teacher teacher = new Teacher()
                {
                    Id = (int)dr["id"],
                    Number = (int)dr["teacher_number"],
                    Name = dr["first_name"].ToString() + " " +
                            dr["last_name"].ToString(),
                };
                teachers.Add(teacher);
            }
            return teachers;
        }
    }
}