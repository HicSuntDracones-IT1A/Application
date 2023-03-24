using SomerenService;
using SomerenModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using System.CodeDom;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();
        }

        public async Task<dynamic> ProcessList(Func<dynamic> targetFunction)
        {
            PanelTitle.Text = "Loading...";
            return await Task.Factory.StartNew(() =>
            {
                return targetFunction();
            }); 
        }

        private void ShowDashboardPanel()
        {
            // Reset the main panel and hide it
            ResetPanel();
            PanelMain.Visible = false;

            // show dashboard
            pnlDashboard.Visible = true;
        }

        private async void ShowLecturersPanel()
        {
            try
            {
                ResetPanel();
                List<Human> teachers = await ProcessList(GetTeachers);
                ResetPanel(title: "Lecturers");

                DisplayTeachers(teachers);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the teachers: " + e.Message);
            }

        }

        private async void ShowRoomsPanel()
        {
            try
            {
                ResetPanel();
                List<Room> rooms = await ProcessList(GetRooms);
                ResetPanel(title: "Rooms");
               

                DisplayRooms(rooms);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the rooms: " + e.Message);
            }

        }

        private void DisplayRooms(List<Room> rooms)
        {
            // clear the listview before filling it
            ListViewMain.Clear();
            
            ListViewMain.Columns.Add("Number");
            ListViewMain.Columns.Add("Type");
            ListViewMain.Columns.Add("Capacity");
            ListViewMain.Columns.Add("room_number");

            foreach (Room room in rooms)
            {
                ListViewItem li = new ListViewItem(room.Id.ToString());
                
                li.SubItems.Add(room.Type.ToString());
                li.SubItems.Add(room.Capacity.ToString());
                li.SubItems.Add(room.Number.ToString());

                li.Tag = rooms;   // link room object to listview item.
                ListViewMain.Items.Add(li);
            }
            ListViewMain.Columns[0].Width = 150;
            ListViewMain.Columns[1].Width = 150;
            ListViewMain.View = View.Details;
        }

        private void ResetPanel(string title = "") {
            PanelMain.Visible = true;
            PanelTitle.Text = title;
            ListViewMain.Clear();
        }

        private async void ShowStudentsPanel()
        {

            /*
                List<Room> rooms = GetRooms();

                foreach (Room room in rooms)
                {
                    MessageBox.Show(room.ToString());
                }
            */

            try
            {
                ResetPanel();
                List<Human> students = await ProcessList(GetStudents);
                ResetPanel(title: "Students");

                DisplayStudents(students);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the students: " + e.Message);
            }
        }

        private List<Room> GetRooms()
        {
            RoomService roomService = new RoomService();
            List<Room> rooms = roomService.GetRooms();
            return rooms;
        }

        private List<Human> GetStudents()
        {
            StudentService studentService = new StudentService();
            List<Student> students = studentService.GetStudents();
            return students.Cast<Human>().ToList();
        }

        private List<Human> GetTeachers()
        {
            TeacherService teacherService = new TeacherService();
            List<Teacher> teachers = teacherService.GetTeacher();
            return teachers.Cast<Human>().ToList();
        }

        private void DisplayStudents(List<Human> students)
        {
            // clear the listview before filling it
            ListViewMain.Clear();
            ListViewMain.BeginUpdate();

            ListViewMain.Columns.Add("ID");
            ListViewMain.Columns.Add("Number");
            ListViewMain.Columns.Add("Name");
            ListViewMain.Columns.Add("BirthDate");

            foreach (Student student in students)
            {
                ListViewItem li = new ListViewItem(student.Id.ToString());
                li.Tag = student;

                li.SubItems.Add(student.Number.ToString());
                li.SubItems.Add(student.Name);
                li.SubItems.Add(student.BirthDate.ToString());

                ListViewMain.Items.Add(li);
            }
            ListViewMain.EndUpdate();
        }


        private void DisplayTeachers(List<Human> teachers)
        {
            // clear the listview before filling it
            ListViewMain.Clear();
            ListViewMain.BeginUpdate();

            ListViewMain.Columns.Add("ID");
            ListViewMain.Columns.Add("Number");
            ListViewMain.Columns.Add("Name");

            foreach (Teacher teacher in teachers)
            {
                ListViewItem li = new ListViewItem(teacher.Id.ToString());
                li.Tag = teacher;

                li.SubItems.Add(teacher.Number.ToString());
                li.SubItems.Add(teacher.Name);

                ListViewMain.Items.Add(li);
            }
            ListViewMain.EndUpdate();
        }

        private void dashboardToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            ShowDashboardPanel();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStudentsPanel();
        }
        
        private void lecturersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLecturersPanel();
        }

        private void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        }
            ShowRoomsPanel();
        {
        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
    }
}