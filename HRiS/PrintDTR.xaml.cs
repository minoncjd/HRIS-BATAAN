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
                    EList = new List<HRiSClass.EmpCombo>();

                    var emp = db.Employees.Where(m => m.Archive == false).ToList();

                    foreach (var i in emp)
                    {
                        HRiSClass.EmpCombo ec = new HRiSClass.EmpCombo();
                        ec.EmployeeID = i.EmployeeID;
                        ec.EmployeeNumber = i.EmployeeNo;
                        ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                        EList.Add(ec);
                    }

                    cbEmployee.ItemsSource = EList.OrderBy(m => m.EmployeeName);
                    cbEmployee.DisplayMemberPath = "EmployeeName";
                    cbEmployee.SelectedValuePath = "EmployeeID";

                    cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaDepartmentName).ToList();
                    cbDepartment.DisplayMemberPath = "AcaDepartmentName";
                    cbDepartment.SelectedValuePath = "AcaDeptID";

                    cbReportType.Items.Add("DTR");
                    cbReportType.Items.Add("Attendance Report");

                    cbDesignation.ItemsSource = db.EmployeeDesignations.OrderBy(m => m.EmployeeDesignationName).ToList();
                    cbDesignation.DisplayMemberPath = "EmployeeDesignationName";
                    cbDesignation.SelectedValuePath = "EmployeeDesignationID";


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

            if (rbDesignation.IsChecked == false && rbDepartment.IsChecked == false && rbEmployeeName.IsChecked == false)
            {
                MessageBox.Show("Date fields cannot be empty.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }

            using (var db = new LetranIntegratedSystemEntities())
            {
                var startDate = dpStartDate.SelectedDate.Value.ToShortDateString();
                var endDate = dpEndDate.SelectedDate.Value.ToShortDateString();
                var reporttype = cbReportType.Text;
                var empid = Convert.ToInt32(cbEmployee.SelectedValue);
                var deptid = Convert.ToInt32(cbDepartment.SelectedValue);
                var designationid = Convert.ToInt32(cbDesignation.SelectedValue);
                List<Model.Employee> employees = new List<Model.Employee>();           
                List<GetEmployeeDTR_Result> lresult = new List<GetEmployeeDTR_Result>();

                var department = db.AcademicDepartments.Where(m => m.AcaDeptID == deptid).FirstOrDefault();
                string dept;
                if (department == null)
                {
                    dept = cbDesignation.Text;
                }
                else
                {
                    dept = department.AcaDepartmentName;
                }
                if (rbDepartment.IsChecked == true)
                {
                    employees = db.Employees.Where(m => m.EmployeeDepartmentID == deptid && m.EmployeeDepartmentID != null && m.bioid != null && m.Archive == false).ToList();
                }
                else if (rbEmployeeName.IsChecked == true)
                {
                    employees = db.Employees.Where(m => m.EmployeeID == empid).ToList();
                }
                else if (rbDesignation.IsChecked == true)
                {
                    employees = db.Employees.Where(m => m.EmployeeDesignation == designationid).ToList();
                }


                if (reporttype == "DTR")
                {
                    

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

                else if (reporttype == "Attendance Report")
                {
                    
                    foreach (var emp in employees)
                    {
                        
                        var qry = from a in db.GetEmployeeDTR(startDate, endDate, emp.bioid)
                                  select a;
                        lresult.AddRange(qry);

                    }

                    Mouse.OverrideCursor = Cursors.Arrow;
                    PrintWindow x = new PrintWindow();
                    
                    x.rptid = 30;
                    x.startDate = dpStartDate.SelectedDate.Value.ToString("MM/dd/yy");
                    x.endDate = dpEndDate.SelectedDate.Value.ToString("MM/dd/yy");

                    x.department = dept;
                    x.Report30 = lresult;
                    x.ShowDialog();
                }
           

            }                         
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


    public class ReportType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
