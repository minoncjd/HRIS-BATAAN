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


namespace HRiS
{
    /// <summary>
    /// Interaction logic for AddLeaveBalance.xaml
    /// </summary>
    public partial class AddLeaveBalance : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmpCombo> EList = new List<EmpCombo>();

        public AddLeaveBalance()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }
        public void LoadComboBox()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                EList = new List<EmpCombo>();

                var listbal = db.LeaveBalances.ToList();

                var listemp = db.Employees.Where(m => m.EmployeeStatusID == 3 && m.Archive == false).ToList();

                var x = (from a in listemp
                         where !listbal.Select(m => m.EmployeeID).Contains(a.EmployeeID)
                         select a).ToList();

                foreach(var i in x )
                {
                    EmpCombo ec = new EmpCombo();
                    ec.EmployeeID = i.EmployeeID;
                    ec.EmployeeNumber = i.EmployeeNo;
                    ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                    EList.Add(ec);
                }

                cbEmp.ItemsSource = EList.OrderBy(m=>m.EmployeeName); 
                cbEmp.DisplayMemberPath = "EmployeeName";
                cbEmp.SelectedValuePath = "EmployeeID";

            }

            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                HRiS.Model.LeaveBalance lb = new Model.LeaveBalance();
                int EmpID = Convert.ToInt32(cbEmp.SelectedValue);

                lb.EmployeeID = EmpID;
                lb.SickLeaveBalance = Convert.ToDecimal(txtSL.Text);
                lb.VacationLeaveBalance = Convert.ToDecimal(txtVL.Text);
               lb.ServiceIncentiveLeave = Convert.ToDecimal(txtSIL.Text);

                db.LeaveBalances.Add(lb);
                db.SaveChanges();
                MessageBox.Show("Added Succesful","System Succes!",MessageBoxButton.OK,MessageBoxImage.Information);
                this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LeaveBalance lb = new LeaveBalance();
            lb.GetBalance();
        }
    }
}
