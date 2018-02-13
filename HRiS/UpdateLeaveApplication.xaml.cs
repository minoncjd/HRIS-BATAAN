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
    /// Interaction logic for UpdateLeaveApplication.xaml
    /// </summary>
    public partial class UpdateLeaveApplication : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        public int LeaveID;
        public UpdateLeaveApplication()
        {
            InitializeComponent();

            LoadComboBox();
        }

        public void LoadComboBox()
        {
            cbLeaveType.ItemsSource = db.LeaveTypes.OrderBy(m => m.LeaveTypeID).ToList();
            cbLeaveType.DisplayMemberPath = "LeaveDescription";
            cbLeaveType.SelectedValuePath = "LeaveTypeID";

            cbSickLeaveType.ItemsSource = db.SickLeaveTypes.OrderBy(m => m.SickLeaveReasonID).ToList();
            cbSickLeaveType.DisplayMemberPath = "SickLeaveReasonType";
            cbSickLeaveType.SelectedValuePath = "SickLeaveReasonID";
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetLeave();
        }

        public void GetLeave()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();

                var leave = db.Leaves.Find(LeaveID);

                if (leave != null)
                {
                    lblName.Content = leave.Employee.LastName.ToUpper() + ", " + leave.Employee.FirstName.ToUpper();
                    lblDepartment.Content = leave.Employee.AcademicDepartment.AcaAcronym.ToUpper();
                    lblPosition.Content = leave.Employee.EmployeePosition.EmployeePositionName.ToUpper();
                    lblDesignation.Content = leave.Employee.EmployeeDesignation1.EmployeeDesignationName.ToUpper();

                    //lblDateFiled.Content = leave.FiledDate.Value.ToString("MMMM dd, yyyy");
                    cbLeaveType.SelectedValue = leave.LeaveTypeID;
                    dpStart.SelectedDate = leave.StartDate;
                    dpEnd.SelectedDate = leave.EndDate;
                    txtDays.Text = leave.Days.Value.ToString();

                    lblSL.Content = leave.Employee.LeaveBalances.FirstOrDefault().SickLeaveBalance.Value.ToString() + " (SL)";
                    lblVL.Content = leave.Employee.LeaveBalances.FirstOrDefault().VacationLeaveBalance.Value.ToString() + " (VL)";

                    //if (leave.SickLeaveReasonID != null || leave.SickLeaveReasonID != 0)
                    //    cbSickLeaveType.SelectedValue = leave.SickLeaveReasonID;

                    txtReason.Text = leave.Reason;
                }
                else
                {
                    MessageBox.Show("Leave not found", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var L = db.Leaves.Find(LeaveID);

                if(L != null)
                {
                    var typeid = Convert.ToInt32(cbLeaveType.SelectedValue);
                    L.StartDate = Convert.ToDateTime(dpStart.SelectedDate);
                    L.EndDate = Convert.ToDateTime(dpEnd.SelectedDate);
                    L.LeaveTypeID = typeid;
                    L.Reason = txtReason.Text;
                    L.Days = Convert.ToDecimal(txtDays.Text);
                    //if (!String.IsNullOrEmpty(cbSickLeaveType.Text))
                    //    L.SickLeaveReasonID = Convert.ToInt32(cbSickLeaveType.SelectedValue);
                    //L.LeaveApprovedStatusID = 3;
                    db.SaveChanges();
                    MessageBox.Show("Save Successful","System Sucess!",MessageBoxButton.OK,MessageBoxImage.Information);
                    this.Close();
                    
                }
                else
                {
                    MessageBox.Show("Leave not found", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.Close();

                }

            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
