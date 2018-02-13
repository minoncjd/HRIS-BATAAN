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
    /// Interaction logic for LeaveApproved.xaml
    /// </summary>
    public partial class LeaveApproved : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<ApprovedList> al = new List<ApprovedList>();

        public LeaveApproved()
        {
            InitializeComponent();
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
                al = new List<ApprovedList>();

                var x = db.GetHRiSLeaveApproved().ToList();

                foreach (var i in x)
                {
                    ApprovedList Alist = new ApprovedList();

                    Alist.LeaveID = i.LeaveID;
                    Alist.EmployeeNo = i.EmployeeNo;
                    Alist.EmployeeName = i.EmployeeName.ToUpper();
                    Alist.DateFiled = i.FiledDate.Value;
                    Alist.StartDate = i.StartDate.Value;
                    Alist.EndDate = i.EndDate.Value;
                    Alist.Type = i.LeaveCode;
                    Alist.Days = i.Days.Value;
                    Alist.Status = i.Status;
                    Alist.Reason = i.Reason;
                    Alist.ApprovedDate = i.ApprovedDate.Value;
                    Alist.ApprovedBy = i.Approved;
                    Alist.LeaveStatusID = i.LeaveApprovedStatusID.Value;
                       
                    al.Add(Alist);
                }
                dgApproved.ItemsSource = al.OrderByDescending(m => m.DateFiled);

            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong!","System Warning.",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var x = ((ApprovedList)dgApproved.SelectedItem);


                Leave leaveapp = db.Leaves.Find(x.LeaveID);
                var empid = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNo).FirstOrDefault();
                HRiS.Model.LeaveBalance leavebal = db.LeaveBalances.Where(m => m.EmployeeID == empid.EmployeeID).FirstOrDefault();

                if (leaveapp != null)
                {
                    //// DISAPPROVED PART 
                    //leaveapp.LeaveApprovedStatusID = 5;
                    //leaveapp.CancellationDate = DateTime.Now;

                    //RETURN OF LEAVE CREDITS

                    //EL
                    if(leaveapp.LeaveType.LeaveTypeID ==  1)
                    {
                        leavebal.VacationLeaveBalance = leavebal.VacationLeaveBalance + x.Days;
                        //leavebal.EmergencyLeaveBalance = leavebal.EmergencyLeaveBalance + x.Days;
                    }
                    //SL
                    else if (leaveapp.LeaveType.LeaveTypeID == 4)
                    {
                        leavebal.SickLeaveBalance = leavebal.SickLeaveBalance + x.Days;
                    }
                    //VL
                    else if(leaveapp.LeaveType.LeaveTypeID == 5)
                    {
                        leavebal.VacationLeaveBalance = leavebal.VacationLeaveBalance + x.Days;
                    }
                    //SPL
                    else if(leaveapp.LeaveType.LeaveTypeID == 7)
                    {
                        //leavebal.SoloParentLeaveBalance = leavebal.SoloParentLeaveBalance + x.Days;
                    }
                    //BL
                    else if(leaveapp.LeaveType.LeaveTypeID == 10)
                    {
                        //leavebal.BereavementLeaveBalance = leavebal.BereavementLeaveBalance + x.Days;
                    }
                    db.SaveChanges();
                    MessageBox.Show("Leave Cancelled", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetLeave();
                }
                else
                {
                    MessageBox.Show("Cannot find leave transaction.", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((ApprovedList)dgApproved.SelectedItem);
                if (x != null)
                {
                    PrintLeaveApplication ula = new PrintLeaveApplication();
                    ula.LeaveID = x.LeaveID;
                    ula.Owner = this;
                    ula.ShowDialog();
                    GetLeave();
                }
                else
                {
                    MessageBox.Show("Selected item not found.", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

       

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                GetLeave();

                if(rbEmpname.IsChecked == true)
                {
                    al = al.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if(rbEmpnum.IsChecked == true)
                {
                    al = al.Where(m => m.EmployeeNo == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    al = al.Where(m => m.StartDate >= datestart && m.EndDate <= dateend.AddDays(1)).ToList();
                }
                dgApproved.ItemsSource = al.OrderByDescending(m => m.DateFiled);
                Mouse.OverrideCursor = null;
            }
            catch(Exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GetLeave();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void rbEmpnum_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void rbEmpname_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (al.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 2;
                x.Report2 = al;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
    public class ApprovedList
    {
        public int LeaveID { get; set; }
        public int LeaveStatusID { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateFiled { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string Type { get; set; }
        public decimal Days { get; set; }
        public string Status { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
    }
}
