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
    /// Interaction logic for AddLeave.xaml
    /// </summary>
    public partial class AddLeave : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmpCombo> EList = new List<EmpCombo>();

        public AddLeave()
        {
            InitializeComponent();
            lblDateFiled.Content = DateTime.Now.ToShortDateString();
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
                         where listbal.Select(m => m.EmployeeID).Contains(a.EmployeeID)
                         select a).ToList();

                foreach (var i in x)
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


                cbLeaveType.ItemsSource = db.LeaveTypes.OrderBy(m => m.LeaveDescription).ToList();
                cbLeaveType.DisplayMemberPath = "LeaveDescription";
                cbLeaveType.SelectedValuePath = "LeaveTypeID";

                cbShift.Items.Add("Morning");
                cbShift.Items.Add("Afternoon");

                for (double i = 0.5; i <= 20; i = i + 0.5)
                {
                    txtDays.Items.Add(i.ToString());
                }

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

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (String.IsNullOrEmpty(cbEmployee.Text))
                    {
                            MessageBox.Show("Employee cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                    }
                    if (String.IsNullOrEmpty(cbLeaveType.Text))
                    {
                        MessageBox.Show("Leave type cannot be empty..", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (String.IsNullOrEmpty(dpStart.Text) || String.IsNullOrEmpty(dpEnd.Text))
                    {
                        MessageBox.Show("Start and/or End date cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (String.IsNullOrEmpty(txtDays.Text))
                    {
                        MessageBox.Show("Day/s date cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    int empid = Convert.ToInt32(cbEmployee.SelectedValue);
                    var employee = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    DateTime startdate = Convert.ToDateTime(dpStart.SelectedDate);
                    DateTime enddate = Convert.ToDateTime(dpEnd.SelectedDate);

                    if (employee != null)
                    {
                        int leavetypeid = Convert.ToInt32(cbLeaveType.SelectedValue);

                        //EL
                        if (leavetypeid == 1)
                        {
                            //if (employee.LeaveBalances.FirstOrDefault().EmergencyLeaveBalance < Convert.ToDecimal(txtDays.Text))
                            //{
                            //    MessageBox.Show("Leave Balance is not enough", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            //    return;
                            //}
                        }
                        //SL
                        if (leavetypeid == 4)
                        {
                            if (employee.LeaveBalances.FirstOrDefault().SickLeaveBalance < Convert.ToDecimal(txtDays.Text))
                            {
                                MessageBox.Show("Leave Balance is not enough", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }
                        //VL
                        if (leavetypeid == 5)
                        {
                            if (employee.LeaveBalances.FirstOrDefault().VacationLeaveBalance < Convert.ToDecimal(txtDays.Text))
                            {
                                MessageBox.Show("Leave Balance is not enough", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }
                        //SPL
                        if (leavetypeid == 7)
                        {
                            //if (employee.LeaveBalances.FirstOrDefault().SoloParentLeaveBalance < Convert.ToDecimal(txtDays.Text))
                            //{
                            //    MessageBox.Show("Leave Balance is not enough", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            //    return;
                            //}
                        }
                        //BL
                        if (leavetypeid == 10)
                        {
                            //if (employee.LeaveBalances.FirstOrDefault().BereavementLeaveBalance < Convert.ToDecimal(txtDays.Text))
                            //{
                            //    MessageBox.Show("Leave Balance is not enough", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            //    return;
                            //}
                        }

                        Leave leave = new Leave();

                        leave.EmployeeID = employee.EmployeeID;
                        leave.LeaveTypeID = leavetypeid;
                        leave.Days = Convert.ToDecimal(txtDays.Text);
                        leave.StartDate = startdate;
                        leave.EndDate = enddate;
                        //leave.FiledDate = DateTime.Now;
                        //leave.FiledBy = App.EmployeeID;

                        if (!String.IsNullOrEmpty(cbShift.Text))
                        {
                            if (cbShift.Text == "Morning")
                            {
                                leave.Reason = "[AM Half] " + txtReason.Text;

                                if (!String.IsNullOrEmpty(cbSickLeaveType.Text))
                                {
                                    int sickleavetypeid = Convert.ToInt32(cbSickLeaveType.SelectedValue);
                                    //leave.SickLeaveReasonID = sickleavetypeid;
                                    leave.Reason = "[AM Half] " + cbSickLeaveType.Text + " " + txtReason.Text;
                                }
                            }
                            else
                            {
                                leave.Reason = "[PM Half] " + txtReason.Text;

                                if (!String.IsNullOrEmpty(cbSickLeaveType.Text))
                                {
                                    int sickleavetypeid = Convert.ToInt32(cbSickLeaveType.SelectedValue);
                                    //leave.SickLeaveReasonID = sickleavetypeid;
                                    leave.Reason = "[PM Half] " + cbSickLeaveType.Text + " " + txtReason.Text;
                                }
                            }

                        }
                        else
                        {
                            leave.Reason = txtReason.Text;

                            if (!String.IsNullOrEmpty(cbSickLeaveType.Text))
                            {
                                int sickleavetypeid = Convert.ToInt32(cbSickLeaveType.SelectedValue);
                                //leave.SickLeaveReasonID = sickleavetypeid;
                                leave.Reason = cbSickLeaveType.Text + " " + txtReason.Text;
                            }
                        }
                        //leave.LeaveApprovedStatusID = 3;
                        db.Leaves.Add(leave);
                        db.SaveChanges();
                        MessageBox.Show("Leave filed.", "System Sucess!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Employee not found.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
           }
            catch (Exception)
            {
                //MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEmployee.SelectedItem != null)
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var empid = Convert.ToInt32(cbEmployee.SelectedValue);

                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();

                    lblDepartment.Content = emp.AcademicDepartment.AcaAcronym;
                    lblPosition.Content = emp.EmployeePosition.EmployeePositionName;
                    lblDesignation.Content = emp.EmployeeDesignation1.EmployeeDesignationName;
                    lblSL.Content = emp.LeaveBalances.FirstOrDefault().SickLeaveBalance.ToString();
                    lblVL.Content = emp.LeaveBalances.FirstOrDefault().VacationLeaveBalance.ToString();

                }
            }
            else
            {
                cbEmployee.Text = "";
                cbEmployee.Focus();
                MessageBox.Show("Select an employee.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void cbLeaveType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLeaveType.SelectedItem != null)
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var leavetypeid = Convert.ToInt32(cbLeaveType.SelectedValue);
                    if (leavetypeid == 4)
                    {
                        cbSickLeaveType.IsEnabled = true;
                        cbSickLeaveType.ItemsSource = db.SickLeaveTypes.OrderBy(m => m.SickLeaveReasonType).ToList();
                        cbSickLeaveType.DisplayMemberPath = "SickLeaveReasonType";
                        cbSickLeaveType.SelectedValuePath = "SickLeaveReasonID";
                    }
                    else
                    {
                        cbSickLeaveType.ItemsSource = null;
                        cbSickLeaveType.IsEnabled = false;
                    }
                }

            }
            else
            {
                MessageBox.Show("Select a leave type.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void txtDays_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtDays.Text == "0.5")
            {
                cbShift.IsEnabled = true;
            }
            else
            {
                cbShift.IsEnabled = false;
            }
        }

    }
}
