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
    /// Interaction logic for EmployeesHistory.xaml
    /// </summary>
    public partial class EmployeesHistory : MetroWindow
    {
        List<EmployeHistoryList> lEmployeeHistoryList = new List<EmployeHistoryList>();
        public EmployeesHistory()
        {
            InitializeComponent();
        }

        public void GetEmployeeHistory()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    lEmployeeHistoryList = new List<EmployeHistoryList>();
                    var emphistory = db.EmployeeHistories.ToList();

                    foreach (var x in emphistory)
                    {

                        EmployeHistoryList eh = new EmployeHistoryList();
                        var emp = db.Employees.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();
                        var dep = db.AcademicDepartments.Where(m => m.AcaDeptID == x.DepartmentID).FirstOrDefault();
                        var pos = db.EmployeePositions.Where(m => m.EmployeePositionID == x.EmployeePositionID).FirstOrDefault();

                        eh.EmployeeName = emp.LastName + ", " + emp.FirstName;
                        eh.EmployeeNo = emp.EmployeeNo;
                        eh.Remark = x.Remark;
                        eh.Department = dep.AcaAcronym;
                        eh.Position = pos.EmployeePositionName;
                        eh.StartDate = x.StartDate.Value.Date;
                        eh.EndDate = x.EndDate.Value.Date;

                        lEmployeeHistoryList.Add(eh);


                    }
                    dgEmployeeHistory.ItemsSource = lEmployeeHistoryList.OrderBy(m => m.EmployeeName);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public class EmployeHistoryList {
            public string EmployeeNo { get; set; }

            public string EmployeeName { get; set; }
            public string Department { get; set; }
            public string Position { get; set; }
            public string Remark { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetEmployeeHistory();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    
                    lEmployeeHistoryList = new List<EmployeHistoryList>();
                    var emphistory = db.EmployeeHistories.ToList();

                    foreach (var x in emphistory)
                    {

                        EmployeHistoryList eh = new EmployeHistoryList();
                        var emp = db.Employees.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();
                        var dep = db.AcademicDepartments.Where(m => m.AcaDeptID == x.DepartmentID).FirstOrDefault();
                        var pos = db.EmployeePositions.Where(m => m.EmployeePositionID == x.EmployeePositionID).FirstOrDefault();

                        eh.EmployeeName = emp.LastName + ", " + emp.FirstName;
                        eh.EmployeeNo = emp.EmployeeNo;
                        eh.Remark = x.Remark;
                        eh.Department = dep.AcaAcronym;
                        eh.Position = pos.EmployeePositionName;
                        eh.StartDate = x.StartDate.Value.Date;
                        eh.EndDate = x.EndDate.Value.Date;

                        lEmployeeHistoryList.Add(eh);


                    }
                

                    if (rbEmpname.IsChecked == true)
                    {
                        lEmployeeHistoryList = lEmployeeHistoryList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                    }
                    else if (rbEmpn.IsChecked == true)
                    {
                        lEmployeeHistoryList = lEmployeeHistoryList.Where(m => m.EmployeeNo.Contains(txtSearch.Text)).ToList();
                    }

                    dgEmployeeHistory.ItemsSource = lEmployeeHistoryList.OrderBy(m => m.EmployeeName);

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void rbEmpn_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbEmpname_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {      
            AddEmployeeHistory ae = new AddEmployeeHistory();
            ae.ShowDialog();

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeeHistory();
        }
    }
}
