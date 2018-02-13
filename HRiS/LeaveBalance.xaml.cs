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
    /// Interaction logic for LeaveBalance.xaml
    /// </summary>
    public partial class LeaveBalance : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<BalanceList> BList = new List<BalanceList>();

        public LeaveBalance()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetBalance();
            LoadComboBox();
        }

        public void LoadComboBox()
        {
            try
            {
                List<EmpCombo> EList = new List<EmpCombo>();

                using (var db = new LetranIntegratedSystemEntities())
                {
                    var emp = (from a in db.LeaveBalances
                               join b in db.Employees on a.EmployeeID equals b.EmployeeID
                               select new { b.FirstName, b.LastName, b.EmployeeNo, b.EmployeeID }).ToList();

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
            }
            catch (Exception)
            {

                throw;
            }
        }

        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }

        public void GetBalance()
        {
            try
            {
                //    db = new LetranIntegratedSystemEntities();
                BList = new List<BalanceList>();

                var x = db.GetHRiSLeaveBalances().OrderBy(m=>m.EmployeeName).ToList();

                foreach(var i in x)
                {
                    BalanceList bl = new BalanceList();
                    bl.Department = i.Department;
                    bl.Designation = i.Designation;
                    bl.EmployeeID = i.EmployeeID;
                    bl.EmployeeName = i.EmployeeName.ToUpper();
                    bl.EmployeeNumber = i.EmployeeNo;
                    bl.LeaveBalanceID = i.LeaveBalanceID;
                    bl.SickLeaveBalance = i.SickLeaveBalance.Value;
                    bl.VacationLeaveBalance = i.VacationLeaveBalance.Value;
                    bl.ServiceIncentiveLeave = i.ServiceIncentiveLeave.Value;
                    //el.NickName = x.Nickname == null ? " " : x.Nickname;
                BList.Add(bl);
                }
                dgBalances.ItemsSource = BList.OrderBy(m => m.EmployeeName);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var x = ((BalanceList)dgBalances.SelectedItem);
            UpdateLeaveBalance lb = new UpdateLeaveBalance();
            lb.leavebalanceid = x.LeaveBalanceID;
            lb.Owner = this;
            lb.ShowDialog();
            GetBalance();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddLeaveBalance ab = new AddLeaveBalance();
            ab.Owner = this;
            ab.ShowDialog();
            GetBalance();
        }

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (BList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 4;
                x.Report4 = BList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {               
                    BList = new List<BalanceList>();
                    var empid = Convert.ToInt32(cbEmployee.SelectedValue);
                    var x = db.GetHRiSLeaveBalances().Where(m => m.EmployeeID == empid).OrderBy(m => m.EmployeeName).FirstOrDefault();             
                    BalanceList bl = new BalanceList();

                    if (x != null)
                    {
                        bl.Department = x.Department;
                        bl.Designation = x.Designation;
                        bl.EmployeeID = x.EmployeeID;
                        bl.EmployeeName = x.EmployeeName.ToUpper();
                        bl.EmployeeNumber = x.EmployeeNo;
                        bl.LeaveBalanceID = x.LeaveBalanceID;
                        bl.SickLeaveBalance = x.SickLeaveBalance.Value;
                        bl.VacationLeaveBalance = x.VacationLeaveBalance.Value;
                        bl.ServiceIncentiveLeave = x.ServiceIncentiveLeave.Value;
                        //el.NickName = x.Nickname == null ? " " : x.Nickname;
                        BList.Add(bl);

                        dgBalances.ItemsSource = BList.OrderBy(m => m.EmployeeName);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class BalanceList
    {
        public int EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public int LeaveBalanceID { get; set; }
        public decimal SickLeaveBalance { get; set; }
        public decimal VacationLeaveBalance { get; set; }
        public decimal ServiceIncentiveLeave { get; set; }

    }
}
