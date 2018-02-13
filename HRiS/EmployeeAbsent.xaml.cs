using HRiS.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for EmployeeAbsent.xaml
    /// </summary>
    public partial class EmployeeAbsent : MetroWindow
    {
        List<AbsentList> lAbsentList = new List<AbsentList>();
        public EmployeeAbsent()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }

        private void btnAddAbsent_Click(object sender, RoutedEventArgs e)
        {
            AddAbsent x = new AddAbsent();
            x.ShowDialog();
        }

        private void LoadComboBox()
        {
            var db = new LetranIntegratedSystemEntities();
            var emp = db.Employees.Where(m => m.Archive == false).ToList();
            List<EmpCombo> EList = new List<EmpCombo>();
            foreach (var i in emp)
            {
                EmpCombo ec = new EmpCombo();
                ec.EmployeeID = i.EmployeeID;
                ec.EmployeeNumber = i.EmployeeNo;
                ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                EList.Add(ec);
            }

            cbEmployee.ItemsSource = EList.OrderBy(m => m.EmployeeName);
            cbEmployee.DisplayMemberPath = "EmployeeName";
            cbEmployee.SelectedValuePath = "EmployeeID";
        }

        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }

        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           



        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

           
        }

        private void cbEmployee_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (lAbsentList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 28;
                x.Report28 = lAbsentList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


       

        private void btnViewResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lAbsentList = new List<AbsentList>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dpEndDate.SelectedDate != null && dpStartDate.SelectedDate != null && cbEmployee.SelectedItem != null)
                    {


                        int empid = Convert.ToInt32(cbEmployee.SelectedValue);
                        var empabsentlist = db.HRISEmployeeAbsents.Where(m => m.EmployeeID == empid && m.Date >= dpStartDate.SelectedDate && m.Date <= dpEndDate.SelectedDate).ToList();
                      
                        foreach (var x in empabsentlist)
                        {


                            AbsentList al = new AbsentList();
                            var employee = db.Employees.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();
                            
                            al.AbsentDate = dpEndDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                            al.Reason = x.Reason;
                            al.EmployeeNumber = employee.EmployeeNo;
                            al.EmployeeName = employee.LastName + ", " + employee.FirstName;
                            al.startDate = dpStartDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                            al.endDate = dpEndDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                            lAbsentList.Add(al);

                        }

                        dgEmployeeAbsentList.ItemsSource = lAbsentList.OrderByDescending(m => m.AbsentDate);
                    }
                    else
                    {
                        MessageBox.Show("Date fiedls and employee cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    public class AbsentList
    {
        public int EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Reason { get; set; }
        public string AbsentDate { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }

    }
}
