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
    /// Interaction logic for AddEmployeeHistory.xaml
    /// </summary>
    public partial class AddEmployeeHistory : MetroWindow
    {
        public int deptid;
        public int positionid;
        public int empid;
        public AddEmployeeHistory()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    
                    EmployeeHistory eh = new EmployeeHistory();
                    eh.EmployeeID = empid;
                    eh.DepartmentID = deptid;
                    eh.EmployeePositionID = positionid;
                    eh.StartDate = startDate.SelectedDate;
                    eh.Remark = txtRemark.Text;
                    eh.EndDate = endDate.SelectedDate;
                    db.EmployeeHistories.Add(eh);
                    db.SaveChanges();
                    Clear();
                    MessageBox.Show("Successfully added.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Clear()
        {
           
            txtEmployee.Text = "";
            startDate.SelectedDate = null;
            endDate.SelectedDate = null;
        }

        
        public void EmployeeDetails()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var employee = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    txtEmployee.Text = employee.LastName + ", " + employee.FirstName;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeDetails();
        }
    }
}
