using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HRiS.Model;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for sisViewingMain.xaml
    /// </summary>
    public partial class sisViewingMain : MetroWindow
    {
        List<SisFacultyList> SFList = new List<SisFacultyList>();
        public sisViewingMain()
        {
            InitializeComponent();

            using (var db = new LetranIntegratedSystemEntities())
            {
                cbEduc.ItemsSource = db.EducationalLevels.OrderBy(m => m.EducLevelID).ToList();
                cbEduc.DisplayMemberPath = "EducLevelName";
                cbEduc.SelectedValuePath = "EducLevelID";

                cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaAcronym).ToList();
                cbDepartment.DisplayMemberPath = "AcaDepartmentName";
                cbDepartment.SelectedValuePath = "AcaDeptID";
            }

        }

        private void cbEduc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new LetranIntegratedSystemEntities())
            {
                cbSchoolYear.IsEnabled = true;
                cbSchoolYear.ItemsSource = db.SchoolYears.OrderByDescending(m => m.SchoolYearName).ToList();
                cbSchoolYear.DisplayMemberPath = "SchoolYearName";
                cbSchoolYear.SelectedValuePath = "SYID";
            }
        }

        private void cbSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new LetranIntegratedSystemEntities())
            {
                int x = Convert.ToInt32(cbSchoolYear.SelectedValue);
                int y = Convert.ToInt32(cbEduc.SelectedValue);

                cbPeriod.IsEnabled = true;
                cbPeriod.ItemsSource = db.Periods.Where(m => m.SchoolYearID == x && m.EducLevelID == y).ToList();
                cbPeriod.DisplayMemberPath = "Period1";
                cbPeriod.SelectedValuePath = "PeriodID";

                if (y == 3)
                    cbSemester.IsEnabled = true;
                else
                {
                    cbSemester.IsEnabled = false;
                    cbSemester.Text = "";
                }

            }
        }

        private void cbPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new LetranIntegratedSystemEntities())
            {
                int y = Convert.ToInt32(cbEduc.SelectedValue);
                if (y == 3)
                {

                    List<SemesterList> SList = new List<SemesterList>();
                    SList.Add(new SemesterList { SemesterID = 1, Semester = "1st Semester" });
                    SList.Add(new SemesterList { SemesterID = 2, Semester = "2nd Semester" });
                    cbSemester.ItemsSource = SList;
                    cbSemester.DisplayMemberPath = "Semester";
                    cbSemester.SelectedValuePath = "SemesterID";


                }
                else
                {
                    //Load faculties
                    GetSisFaculty();
                }
            }
        }

        private void cbSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Load faculties
            GetSisFaculty();
        }

        public void GetSisFaculty()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    int PeriodID = Convert.ToInt32(cbPeriod.SelectedValue);
                    int SemesterID = Convert.ToInt32(cbSemester.SelectedValue);
                    SFList = new List<SisFacultyList>();
                    if (SemesterID == 0)
                    {
                        var sisfac = db.Schedules.Where(m => m.Section.PeriodID == PeriodID && m.Faculty.Employee.EmployeeID != null && m.Faculty.Employee.EmployeeDepartmentID != null).ToList();

                        foreach (var x in sisfac)
                        {
                            SisFacultyList sfl = new SisFacultyList();
                            sfl.EmployeeID = x.Faculty.Employee.EmployeeID;
                            sfl.FacultyID = x.Faculty.FacultyID;
                            sfl.EmployeeNo = x.Faculty.Employee.EmployeeNo;
                            sfl.EmployeeName = x.Faculty.Employee.LastName + ", " + x.Faculty.Employee.FirstName;
                            sfl.DepartmentID = x.Faculty.Employee.AcademicDepartment.AcaDeptID;
                            sfl.Department = x.Faculty.Employee.AcademicDepartment.AcaDepartmentName;
                            sfl.ScheduleID = x.ScheduleID;
                            sfl.SubjectCode = x.Subject.SubjectCode;
                            sfl.SectionName = x.Section.SectionName;
                            sfl.Description = x.Subject.Description;
                            sfl.Days = x.Days;
                            sfl.Time = x.StartTime.Value.ToString() + " - " + x.EndTime.Value.ToString();
                            sfl.RoomName = x.Room.RoomName;
                            SFList.Add(sfl);
                        }
                    }
                    else
                    {
                        var sisfac = db.Schedules.Where(m => m.Section.PeriodID == PeriodID && m.Section.Semester == SemesterID && m.Faculty.Employee.EmployeeID != null && m.Faculty.Employee.EmployeeDepartmentID != null).ToList();
                        foreach (var x in sisfac)
                        {
                            SisFacultyList sfl = new SisFacultyList();
                            sfl.EmployeeID = x.Faculty.Employee.EmployeeID;
                            sfl.FacultyID = x.Faculty.FacultyID;
                            sfl.EmployeeNo = x.Faculty.Employee.EmployeeNo;
                            sfl.EmployeeName = x.Faculty.Employee.LastName + ", " + x.Faculty.Employee.FirstName;
                            sfl.DepartmentID = x.Faculty.Employee.AcademicDepartment.AcaDeptID;
                            sfl.Department = x.Faculty.Employee.AcademicDepartment.AcaDepartmentName;
                            sfl.ScheduleID = x.ScheduleID;
                            sfl.SubjectCode = x.Subject.SubjectCode;
                            sfl.SectionName = x.Section.SectionName;
                            sfl.Description = x.Subject.Description;
                            sfl.Days = x.Days;
                            sfl.Time = x.StartTime.Value.ToString() + " - " + x.EndTime.Value.ToString();
                            sfl.RoomName = x.Room.RoomName;
                            SFList.Add(sfl);
                        }
                    }

                    SFList = SFList.OrderBy(m => m.Department).OrderBy(m => m.EmployeeName).ToList();
                    ListCollectionView sflcollection = new ListCollectionView(SFList);
                    sflcollection.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
                    sflcollection.GroupDescriptions.Add(new PropertyGroupDescription("EmployeeName"));

                    dgSisFaculty.ItemsSource = sflcollection;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        class SemesterList
        {
            public int SemesterID { get; set; }
            public string Semester { get; set; }
        }
        class SisFacultyList
        {
            public int EmployeeID { get; set; }
            public int FacultyID { get; set; }
            public string EmployeeNo { get; set; }
            public string EmployeeName { get; set; }
            public int DepartmentID { get; set; }
            public string Department { get; set; }
            public int ScheduleID { get; set; }
            public string SectionName { get; set; }
            public string SubjectCode { get; set; }
            public string Description { get; set; }
            public string Days { get; set; }
            public string Time { get; set; }
            public string RoomName { get; set; }

        }

        private void viewEval_Click(object sender, RoutedEventArgs e)
        {
            var x = ((SisFacultyList)dgSisFaculty.SelectedItem);
            using (var db = new LetranIntegratedSystemEntities())
            {
                sisFacultyEvaluationPerSchedule y = new sisFacultyEvaluationPerSchedule();
                y.PerID = Convert.ToInt32(cbPeriod.SelectedValue);
                y.SchdID = x.ScheduleID;
                y.FacID = db.Faculties.Single(m => m.EmpNo == x.EmployeeNo).FacultyID; ;
                y.ShowDialog();
            }


        }

        private void viewEvalOver_Click(object sender, RoutedEventArgs e)
        {
            var x = ((SisFacultyList)dgSisFaculty.SelectedItem);
            using (var db = new LetranIntegratedSystemEntities())
            {
                sisFacultyEvaluationPerFaculty y = new sisFacultyEvaluationPerFaculty();
                y.PerID = Convert.ToInt32(cbPeriod.SelectedValue);
                y.FacID = db.Faculties.Single(m => m.EmpNo == x.EmployeeNo).FacultyID;
                y.ShowDialog();
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    int PeriodID = Convert.ToInt32(cbPeriod.SelectedValue);
                    int SemesterID = Convert.ToInt32(cbSemester.SelectedValue);
                    SFList = new List<SisFacultyList>();
                    if (SemesterID == 0)
                    {
                        var sisfac = db.Schedules.Where(m => m.Section.PeriodID == PeriodID && m.Faculty.Employee.EmployeeID != null && m.Faculty.Employee.EmployeeDepartmentID != null).ToList();

                        foreach (var x in sisfac)
                        {
                            SisFacultyList sfl = new SisFacultyList();
                            sfl.EmployeeID = x.Faculty.Employee.EmployeeID;
                            sfl.EmployeeNo = x.Faculty.Employee.EmployeeNo;
                            sfl.EmployeeName = x.Faculty.Employee.LastName + ", " + x.Faculty.Employee.FirstName;
                            sfl.DepartmentID = x.Faculty.Employee.AcademicDepartment.AcaDeptID;
                            sfl.Department = x.Faculty.Employee.AcademicDepartment.AcaDepartmentName;
                            sfl.ScheduleID = x.ScheduleID;
                            sfl.SubjectCode = x.Subject.SubjectCode;
                            sfl.SectionName = x.Section.SectionName;
                            sfl.Description = x.Subject.Description;
                            sfl.Days = x.Days;
                            sfl.Time = x.StartTime.Value.ToString() + " - " + x.EndTime.Value.ToString();
                            sfl.RoomName = x.Room.RoomName;
                            SFList.Add(sfl);
                        }
                    }
                    else
                    {
                        var sisfac = db.Schedules.Where(m => m.Section.PeriodID == PeriodID && m.Section.Semester == SemesterID && m.Faculty.Employee.EmployeeID != null && m.Faculty.Employee.EmployeeDepartmentID != null).ToList();
                        foreach (var x in sisfac)
                        {
                            SisFacultyList sfl = new SisFacultyList();
                            sfl.EmployeeID = x.Faculty.Employee.EmployeeID;
                            sfl.EmployeeNo = x.Faculty.Employee.EmployeeNo;
                            sfl.EmployeeName = x.Faculty.Employee.LastName + ", " + x.Faculty.Employee.FirstName;
                            sfl.DepartmentID = x.Faculty.Employee.AcademicDepartment.AcaDeptID;
                            sfl.Department = x.Faculty.Employee.AcademicDepartment.AcaDepartmentName;
                            sfl.ScheduleID = x.ScheduleID;
                            sfl.SubjectCode = x.Subject.SubjectCode;
                            sfl.SectionName = x.Section.SectionName;
                            sfl.Description = x.Subject.Description;
                            sfl.Days = x.Days;
                            sfl.Time = x.StartTime.Value.ToString() + " - " + x.EndTime.Value.ToString();
                            sfl.RoomName = x.Room.RoomName;
                            SFList.Add(sfl);
                        }
                    }




                    if (!String.IsNullOrEmpty(txtEmpNumber.Text))
                    {
                        SFList = SFList.Where(m => m.EmployeeNo == txtEmpNumber.Text).ToList();
                    }
                    if (!String.IsNullOrEmpty(txtEmpName.Text))
                    {
                        SFList = SFList.Where(m => m.EmployeeName.ToUpper().Contains(txtEmpName.Text.ToUpper())).ToList();
                    }
                    if (!String.IsNullOrEmpty(cbDepartment.Text))
                    {
                        SFList = SFList.Where(m => m.Department == cbDepartment.Text).ToList();
                    }

                    SFList = SFList.OrderBy(m => m.Department).OrderBy(m => m.EmployeeName).ToList();
                    ListCollectionView sflcollection = new ListCollectionView(SFList);
                    sflcollection.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
                    sflcollection.GroupDescriptions.Add(new PropertyGroupDescription("EmployeeName"));
                    dgSisFaculty.ItemsSource = sflcollection;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetSisFaculty();
            txtEmpName.Text = "";
            txtEmpNumber.Text = "";
            cbDepartment.Text = "";
        }
    }
}
