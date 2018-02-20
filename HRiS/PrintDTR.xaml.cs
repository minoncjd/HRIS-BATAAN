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
    /// Interaction logic for PrintDTR.xaml
    /// </summary>
    public partial class PrintDTR : MetroWindow
    {
        public List<EmployeeAttendanceList> lEmployeeAttendanceList = new List<EmployeeAttendanceList>();
        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();
        public PrintDTR()
        {
            InitializeComponent();
        }

        private void rbDepartment_Checked(object sender, RoutedEventArgs e)
        {
            cbEmployee.IsEnabled = false;
            cbEmployee.Text = "";
            cbDepartment.IsEnabled = true;
        }

        private void rbAll_Checked(object sender, RoutedEventArgs e)
        {
        
        
        }

        private void rbEmployeeNumber_Checked(object sender, RoutedEventArgs e)
        {
            cbEmployee.IsEnabled = true;
            cbDepartment.IsEnabled = false;
            cbDepartment.Text = "";
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
          
            cbEmployee.IsEnabled = false;
            cbDepartment.IsEnabled = false;
            cbEmployee.Text = "";
            cbDepartment.Text = "";
        }

        public void LoadComboBox()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    //EList = new List<HRiSClass.EmpCombo>();

                    //var emp = db.Employees.Where(m => m.Archive == false).ToList();

                    //foreach (var i in emp)
                    //{
                    //    HRiSClass.EmpCombo ec = new HRiSClass.EmpCombo();
                    //    ec.EmployeeID = i.EmployeeID;
                    //    ec.EmployeeNumber = i.EmployeeNo;
                    //    ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                    //    EList.Add(ec);
                    //}

                    //cbEmployee.ItemsSource = EList.OrderBy(m => m.EmployeeName);
                    //cbEmployee.DisplayMemberPath = "EmployeeName";
                    //cbEmployee.SelectedValuePath = "EmployeeID";

                    cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaDepartmentName).ToList();
                    cbDepartment.DisplayMemberPath = "AcaDepartmentName";
                    cbDepartment.SelectedValuePath = "AcaDeptID";


                }


            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void btnPrint_Click_1(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null)
            {
                MessageBox.Show("Date fields cannot be empty.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
             
            }

            var startDate = dpStartDate.SelectedDate.Value.ToShortDateString();
            var endDate = dpEndDate.SelectedDate.Value.ToShortDateString();
            //var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            var empid = 3029;
            var deptid = Convert.ToInt32(cbDepartment.SelectedValue);
           

            using (var db = new LetranIntegratedSystemEntities())
            {
                List<Model.Employee> employees = new List<Model.Employee>();
                lEmployeeAttendanceList = new List<EmployeeAttendanceList>();
                List<GetEmployeeDTR_Result> lresult = new List<GetEmployeeDTR_Result>();

                if (rbAll.IsChecked == true)
                {
                    //employees = (from a in db.Employees
                    //             where !(a.bioid == null) && a.Archive == false && a.EmployeeDepartmentID != null
                    //             select a).Distinct().ToList();

                    employees = db.Employees.Where(m => m.bioid != null && m.EmployeeDepartmentID != null && m.Archive == false).ToList();

                }
                else if (rbDepartment.IsChecked == true)
                {
                    employees = db.Employees.Where(m => m.EmployeeDepartmentID == deptid && m.EmployeeDepartmentID != null && m.bioid != null && m.Archive == false).ToList();
                }
                else if (rbEmployeeNumber.IsChecked == true)
                {
                    employees = db.Employees.Where(m => m.EmployeeID == empid && m.EmployeeDepartmentID != null).ToList();
                }           

                foreach (var emp in employees)
                {
                                      
                    var qry = from a in db.GetEmployeeDTR(startDate, endDate, emp.bioid)
                              select a;                 
                    lresult.AddRange(qry);

                }

                Mouse.OverrideCursor = Cursors.Arrow;
                PrintWindow x = new PrintWindow();

                x.rptid = 29;
                x.startDate = dpStartDate.SelectedDate.Value.ToString("MM/dd/yy");
                x.endDate = dpEndDate.SelectedDate.Value.ToString("MM/dd/yy");
                
                x.Report29 = lresult;
                x.ShowDialog();

            }                         
        }
    }

    public class EmployeeAttendanceList
    {
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
        public string Regular { get; set; }
        public string Late { get; set; }
        public string UnderTime { get; set; }
        public string TotalHoursRendered { get; set; }
        public string TotalLate { get; set; }
        public string TotalUndertime { get; set; }
    }


}
