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
    /// Interaction logic for AddEmployeeLeave.xaml
    /// </summary>
    public partial class AddEmployeeLeave : MetroWindow
    {
        public AddEmployeeLeave()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (txtDays.Text != null && startDate.SelectedDate != null && endDate.SelectedDate != null && cbEmp.SelectedItem != null && cbLeaveType.SelectedItem != null)
                    {
                        int empID = Convert.ToInt32(cbEmp.SelectedValue);
                        int leavetypeid = Convert.ToInt32(cbLeaveType.SelectedValue);


                        if (leavetypeid == 1)
                        {
                            if (Convert.ToDecimal(txtDays.Text) > Convert.ToDecimal(txtSL.Text))
                            {
                                MessageBox.Show("Not enough leave balance", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }
                        else if (leavetypeid == 2)
                        {
                            if (Convert.ToDecimal(txtDays.Text) > Convert.ToDecimal(txtVL.Text))
                            {
                                MessageBox.Show("Not enough leave balance", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }
                        else if (leavetypeid == 3)
                        {
                            if (Convert.ToDecimal(txtDays.Text) > Convert.ToDecimal(txtSIL.Text))
                            {
                                MessageBox.Show("Not enough leave balance", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }

                        Leave leave = new Leave();
                        leave.EmployeeID = empID;
                        leave.LeaveTypeID = leavetypeid;
                        leave.Reason = txtReason.Text;
                        leave.StartDate = startDate.SelectedDate;
                        leave.EndDate = endDate.SelectedDate;
                        leave.Days = Convert.ToDecimal(txtDays.Text);
                        leave.FiledDate = dateFiled.SelectedDate;
                        db.Leaves.Add(leave);
                        db.SaveChanges();

                        UpdateLeaveBalance();

                        MessageBox.Show("Added Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Complete the required details.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                   
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddtoLeaveHistory()
        {

        }

        private void clear()
        {
            txtVL.Text = "";
            txtSL.Text = "";
            txtSIL.Text = "";
            txtDays.Text = "";
            txtReason.Text = "";
            cbEmp.Text = "";
            cbLeaveType.Text = "";
            startDate.Text = "";
            endDate.Text = "";
            
        }
        private void LoadLeaveBalance()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                  
                    int empid = Convert.ToInt32(cbEmp.SelectedValue);
                    var leaveBalance = db.LeaveBalances.Where(m => m.EmployeeID == empid).FirstOrDefault();

                    if (leaveBalance != null)
                    {
                        txtVL.Text = leaveBalance.VacationLeaveBalance.ToString();
                        txtSL.Text = leaveBalance.SickLeaveBalance.ToString();
                        txtSIL.Text = leaveBalance.ServiceIncentiveLeave.ToString();

                    }
                    else
                    {
                        txtVL.Text = "0";
                        txtSL.Text = "0";
                        txtSIL.Text = "0";
                    }
                   
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LoadComboBox()
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

                    cbEmp.ItemsSource = EList.OrderBy(m => m.EmployeeName);
                    cbEmp.DisplayMemberPath = "EmployeeName";
                    cbEmp.SelectedValuePath = "EmployeeID";

                    cbLeaveType.ItemsSource = db.LeaveTypes.OrderBy(m => m.LeaveDescription).ToList();
                    cbLeaveType.DisplayMemberPath = "LeaveDescription";
                    cbLeaveType.SelectedValuePath = "LeaveTypeID";
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

        private void UpdateLeaveBalance()
        {
            try
            {
              
                using (var db = new LetranIntegratedSystemEntities())
                {
                    int empid = Convert.ToInt32(cbEmp.SelectedValue);
                    int leavetypeid = Convert.ToInt32(cbLeaveType.SelectedValue);

                    var leavebalance = db.LeaveBalances.Where(m => m.EmployeeID == empid).FirstOrDefault();

                    if (leavetypeid == 1)
                    {
                        leavebalance.SickLeaveBalance = leavebalance.SickLeaveBalance - Convert.ToDecimal(txtDays.Text);                  
                        db.SaveChanges();

                    }
                    else if (leavetypeid == 2)
                    {
                        leavebalance.VacationLeaveBalance = leavebalance.VacationLeaveBalance - Convert.ToDecimal(txtDays.Text);
                        db.SaveChanges();
                    }
                    else if (leavetypeid == 3)
                    {
                        leavebalance.ServiceIncentiveLeave = leavebalance.ServiceIncentiveLeave - Convert.ToDecimal(txtDays.Text);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }

        private void cbEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadLeaveBalance();
        }
    }
}
