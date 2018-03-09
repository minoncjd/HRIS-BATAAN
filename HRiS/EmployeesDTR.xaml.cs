using CrystalDecisions.CrystalReports.Engine;
using HRiS.Model;
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

namespace HRiS
{
    /// <summary>
    /// Interaction logic for EmployeesDTR.xaml
    /// </summary>
    public partial class EmployeesDTR : MetroWindow
    {
        
        public EmployeesDTR()
        {
            InitializeComponent();
            //tbEmpNo.IsEnabled = false;
            //cbDepartment.IsEnabled = false;
            var db = new LetranIntegratedSystemEntities();
           
            //cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaDepartmentName).ToList();
            //cbDepartment.DisplayMemberPath = "AcaDepartmentName";
            //cbDepartment.SelectedValuePath = "AcaDeptID";
        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {

            //try
            //{
               
            //    using (var db = new LetranIntegratedSystemEntities())
            //    {
            //        var startDate = sdate.SelectedDate.ToString();
            //        var endDate = edate.SelectedDate.ToString();
            //        ReportDocument report;
            //        var employees = db.Employees.ToList();
            //        EList = new List<EmployeeAttendanceList>();

            //        foreach (var x in employees)
            //        {
            //            var attendance = db.GetEmployeeAttendanceTest(startDate, endDate, x.bioid).ToList();

            //            foreach (var att in attendance)
            //            {
            //                EmployeeAttendanceList ea = new EmployeeAttendanceList();
            //                var employee = db.Employees.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();
            //                var department = db.AcademicDepartments.Where(m => m.AcaDeptID == employee.EmployeeDepartmentID).FirstOrDefault();
            //                var position = db.EmployeePositions.Where(m => m.EmployeePositionID == employee.EmployeePositionID).FirstOrDefault();
            //                ea.Date = Convert.ToDateTime(att.DATE);
            //                ea.Day = att.DAY;
            //                ea.EmployeeNumber = att.EmployeeNo == null ? employee.EmployeeNo : att.EmployeeNo;
            //                ea.EmployeeName = att.FullName == null ? employee.LastName + ", " + employee.FirstName : att.FullName;
            //                ea.Department = att.AcaAcronym == null ? department.AcaAcronym : att.AcaAcronym;
            //                ea.Position = att.EmployeePositionName == null ? position.EmployeePositionName : att.EmployeePositionName;
            //                ea.TimeIn = att.TIMEIN;
            //                ea.BreakOut = att.BREAKOUT;
            //                ea.BreakIn = att.BREAKIN;
            //                ea.TimeOut = att.TIMEOUT;
            //                ea.LeaveCode = att.LeaveCode;
            //                ea.StartDate = startDate;
            //                ea.EndDate = endDate;
            //                EList.Add(ea);
                            
            //            }                 
            //        }

            //        if (EList.Count != 0)
            //        {
            //            report = new Reports.EmployeeDTR2();
            //            report.SetDatabaseLogon("softrack", "softrack");
            //            report.SetDataSource(EList);
            //            crViewer2.ViewerCore.ReportSource = report;
            //        }
                
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


        }


        private void rbnAll_Checked(object sender, RoutedEventArgs e)
        {
            //tbEmpNo.IsEnabled = false;
            //cbDepartment.IsEnabled = false;
        }

        private void rbmEmployeeNumber_Checked(object sender, RoutedEventArgs e)
        {
            //tbEmpNo.IsEnabled = true;
            //cbDepartment.IsEnabled = false;


        }

        private void rbnDepartment_Checked(object sender, RoutedEventArgs e)
        {
            //tbEmpNo.IsEnabled = false;
            //cbDepartment.IsEnabled = true;

        }
    }

    public class EmployeeAttendanceList1 {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }

        public string TimeIn { get; set; }
        public string BreakOut { get; set; }
        public string BreakIn { get; set; }
        public string TimeOut { get; set; }

        public DateTime Date { get; set; }
        public string Day { get; set; }
        public string LeaveCode { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
