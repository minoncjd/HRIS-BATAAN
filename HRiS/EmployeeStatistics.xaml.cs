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
using MahApps.Metro.Controls;
using HRiS.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for EmployeeStatistics.xaml
    /// </summary>
    public partial class EmployeeStatistics : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<FilterEmployementStatus> empstatlist = new List<FilterEmployementStatus>();
        List<FilterEmployeeType> emptypelist = new List<FilterEmployeeType>();
        List<FilterEmployeeContract> empcontractlist = new List<FilterEmployeeContract>();
        List<FilterDepartmentEmployee> empdeptlist = new List<FilterDepartmentEmployee>();

        public EmployeeStatistics()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var emptotal = db.Employees.Where(m => m.Archive == false);
            GetEmploymentStatus();
            GetEmployeeType();
            GetEmployeeContract();
            GetEmployeeDepartment();
            lblTotal.Content += emptotal.Count().ToString();
        }

        public void GetEmploymentStatus()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var employmentstat = db.Employees.Where(m => m.Archive == false).GroupBy(m => m.EmployeeStatusID);


                foreach (var x in employmentstat)
                {
                    var statname = db.EmployeeStatus.Where(m => m.EmployeeStatusID == x.Key).FirstOrDefault().EmployeeStatusName;

                    FilterEmployementStatus es = new FilterEmployementStatus();
                    es.EmployementStatusID = x.Key;
                    es.EmploymentStatusCount = x.Count();
                    es.EmploymentStatusDescription = statname;
                    empstatlist.Add(es);
                }
                dgEmployeeStatus.ItemsSource = empstatlist.OrderBy(m => m.EmploymentStatusDescription);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void GetEmployeeType()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var employmentstat = db.Employees.Where(m => m.Archive == false).GroupBy(m => m.EmployeeDesignation);


                foreach (var x in employmentstat)
                {
                    var typename = db.EmployeeDesignations.Where(m => m.EmployeeDesignationID == x.Key).FirstOrDefault().EmployeeDesignationName;

                    FilterEmployeeType et = new FilterEmployeeType();
                    et.EmployeeTypeID = x.Key;
                    et.EmployeeTypeCount = x.Count();
                    et.EmployeeTypeDescription = typename;
                    emptypelist.Add(et);
                }
                dgEmployeeType.ItemsSource = emptypelist.OrderBy(m => m.EmployeeTypeDescription);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void GetEmployeeContract()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var employeecontract = db.Employees.Where(m => m.Archive == false && m.DateEndContract != null && m.DateEndContract >= DateTime.Now);

                int no =1;
                foreach(var x in employeecontract)
                {
                    FilterEmployeeContract ec = new FilterEmployeeContract();
                    ec.EmployeeContractID = no;
                    ec.EmployeeName = x.FirstName + " " + x.LastName;
                    ec.EmployeeNumber = x.EmployeeNo;
                    ec.EmployeeDept = x.AcademicDepartment.AcaAcronym;
                    ec.DateEndContract = Convert.ToDateTime(x.DateEndContract);
                    no++;
                    empcontractlist.Add(ec);
                }
                dgExpiringContract.ItemsSource = empcontractlist.OrderByDescending(m => m.DateEndContract);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void GetEmployeeDepartment()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var empdept = db.Employees.Where(m => m.Archive == false).GroupBy(m => m.EmployeeDepartmentID);

                int no = 1;
                foreach(var x in empdept)
                {
                    var dept = db.AcademicDepartments.Where(m => m.AcaDeptID == x.Key).FirstOrDefault();
                    FilterDepartmentEmployee de = new FilterDepartmentEmployee();
                    de.DepartmentEmployeeID = no;
                    de.DepartmentID = x.Key;
                    de.DepartmentDescription = dept.AcaDepartmentName;
                    de.DepartmentEmployeeCount = x.Count();
                    no++;
                    empdeptlist.Add(de);

                }
                dgDepartment.ItemsSource = empdeptlist.OrderBy(m => m.DepartmentDescription);
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

   
        private void editEmp_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var x = ((FilterEmployeeContract)dgExpiringContract.SelectedItem);
                var emp = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNumber).FirstOrDefault();
                if (emp != null)
                {
                    UpdateEmployee ue = new UpdateEmployee();
                    ue.EmpID = emp.EmployeeID;
                    ue.Owner = this;
                    ue.ShowDialog();
                    

                }
                else
                {
                    MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void viewDept_Click(object sender, RoutedEventArgs e)
        {
            var x = ((FilterDepartmentEmployee)dgDepartment.SelectedItem);

            MessageBox.Show(x.DepartmentID.ToString());
        }

        #region Classes
        public class FilterEmployementStatus
        {
            public int? EmployementStatusID { get; set; }
            public string EmploymentStatusDescription { get; set; }
            public int EmploymentStatusCount { get; set; }
        }

        public class FilterEmployeeType
        {
            public int? EmployeeTypeID { get; set; }
            public string EmployeeTypeDescription { get; set; }
            public int EmployeeTypeCount { get; set; }
        }

        public class FilterEmployeeContract
        {
            public int EmployeeContractID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeDept { get; set; }
            public DateTime DateEndContract { get; set; }
        }

        public class FilterDepartmentEmployee
        {
            public int DepartmentEmployeeID { get; set; }
            public int? DepartmentID { get; set; }
            public int DepartmentEmployeeCount { get; set; }
            public string DepartmentDescription { get; set; }
        }
        #endregion

    }
}
