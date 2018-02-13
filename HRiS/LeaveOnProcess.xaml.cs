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
    /// Interaction logic for LeaveOnProcess.xaml
    /// </summary>
    public partial class LeaveOnProcess : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        public List<OnProcessList> opl = new List<OnProcessList>();

        public LeaveOnProcess()
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
                opl = new List<OnProcessList>();

                var x = db.GetHRiSLeaveOnProcess().ToList();

                foreach (var i in x)
                {
                    OnProcessList Olist = new OnProcessList();

                    Olist.LeaveID = i.LeaveID;
                    Olist.EmployeeNo = i.EmployeeNo;
                    Olist.EmployeeName = i.EmployeeName.ToUpper();
                    Olist.DateFiled = i.FiledDate.Value;
                    Olist.StartDate = i.StartDate.Value;
                    Olist.EndDate = i.EndDate.Value;
                    Olist.Type = i.LeaveCode;
                    Olist.Days = i.Days.Value;
                    Olist.Status = i.Status;
                    Olist.Reason = i.Reason;
                    Olist.LeaveStatusID = i.LeaveApprovedStatusID.Value;
                    if (i.FiledDate.Value.Date.AddDays(3) <= DateTime.Now.Date)
                        Olist.IsOverdue = true;
                    else
                        Olist.IsOverdue = false;
                    opl.Add(Olist);
                }
                dgOnProcess.ItemsSource = opl.OrderByDescending(m => m.DateFiled);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((OnProcessList)dgOnProcess.SelectedItem);
                if (x != null)
                {
                    UpdateLeaveApplication ula = new UpdateLeaveApplication();
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

        private void btnApproved_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var x = ((OnProcessList)dgOnProcess.SelectedItem);


                Leave leaveapp = db.Leaves.Find(x.LeaveID);

                if(x.IsOverdue == true)
                {
                    if (leaveapp != null)
                    {
                        var empid = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNo).FirstOrDefault();
                        HRiS.Model.LeaveBalance leavebal = db.LeaveBalances.Where(m => m.EmployeeID == empid.EmployeeID).FirstOrDefault();

                        // APPROVED PART 
                        //leaveapp.LeaveApprovedStatusID = 2;
                        //leaveapp.ApprovedBy = db.Employees.Where(m => m.EmployeeNo == App.EmployeeNumber).FirstOrDefault().EmployeeID;
                        //leaveapp.ApprovedDate = DateTime.Now;
                      
                        // LEAVE DEDUCTION
                        //EL
                        if (leaveapp.LeaveType.LeaveTypeID == 1)
                        {
                            leavebal.VacationLeaveBalance = leavebal.VacationLeaveBalance - x.Days;
                            //leavebal.EmergencyLeaveBalance = leavebal.EmergencyLeaveBalance - x.Days;
                        }
                        //SL
                        else if (leaveapp.LeaveType.LeaveTypeID == 4)
                        {
                            //leavebal.SickLeaveBalance = leavebal.SickLeaveBalance - x.Days;
                        }
                        //VL
                        else if (leaveapp.LeaveType.LeaveTypeID == 5)
                        {
                            //leavebal.VacationLeaveBalance = leavebal.VacationLeaveBalance - x.Days;
                        }
                        //SPL
                        else if (leaveapp.LeaveType.LeaveTypeID == 7)
                        {
                            //leavebal.SoloParentLeaveBalance = leavebal.SoloParentLeaveBalance - x.Days;
                        }
                        //BL
                        else if (leaveapp.LeaveType.LeaveTypeID == 10)
                        {
                           // leavebal.BereavementLeaveBalance = leavebal.BereavementLeaveBalance - x.Days;
                        }
                        db.SaveChanges();

                        //Transfer leave to Softrak
                        using(var sof = new softrakEntities())
                        {
                            LEAf sofleav = new LEAf();
                            sofleav.USERNAME = App.EmployeeUserName;
                            sofleav.EMPN = leaveapp.Employee.EmployeeNo;
                            sofleav.DATE_FROM = leaveapp.StartDate.Value;
                            sofleav.DATE_TO = leaveapp.EndDate.Value;
                            //sofleav.TYPE
                            sofleav.CODE = leaveapp.LeaveType.LeaveCode;
                            //sofleav.ENTRYDATE = leaveapp.FiledDate.Value;
                            sofleav.CREDITS = Convert.ToDouble(leaveapp.Days.Value);
                            sof.LEAVES.Add(sofleav);
                            sof.SaveChanges();
                        }

                        MessageBox.Show("Leave Approved", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetLeave();
                    }
                    else
                    {
                        MessageBox.Show("Cannot find leave transaction.", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot approve leave.","System Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void btnDisapproved_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var x = ((OnProcessList)dgOnProcess.SelectedItem);


                Leave leaveapp = db.Leaves.Find(x.LeaveID);


                    if (leaveapp != null)
                    {
                        // DISAPPROVED PART 
                        //leaveapp.LeaveApprovedStatusID = 1;
                        //leaveapp.CancellationDate = DateTime.Now;

                        // LEAVE DEDUCTION
                        //if (leaveapp.LeaveTypeID == 5)
                        //{
                        //    var empid = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNo).FirstOrDefault();
                        //    HRiS.Model.LeaveBalance leavebal = db.LeaveBalances.Where(m => m.EmployeeID == empid.EmployeeID).FirstOrDefault();

                        //    leavebal.VacationLeaveBalance = leavebal.VacationLeaveBalance - x.Days;

                        //}

                        db.SaveChanges();
                        MessageBox.Show("Leave disapproved.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetLeave();
                }
                else
                {
                    MessageBox.Show("Cannot approve leave.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var x = ((OnProcessList)dgOnProcess.SelectedItem);


                Leave leaveapp = db.Leaves.Find(x.LeaveID);

                if (leaveapp != null)
                {
                    // DISAPPROVED PART 
                    //leaveapp.LeaveApprovedStatusID = 4;
                    //leaveapp.CancellationDate = DateTime.Now;

                    // LEAVE DEDUCTION
                    //if (leaveapp.LeaveTypeID == 5)
                    //{
                    //    var empid = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNo).FirstOrDefault();
                    //    HRiS.Model.LeaveBalance leavebal = db.LeaveBalances.Where(m => m.EmployeeID == empid.EmployeeID).FirstOrDefault();

                    //    leavebal.VacationLeaveBalance = leavebal.VacationLeaveBalance - x.Days;

                    //}

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

        private void rbEmpnum_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void rbEmpname_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                GetLeave();

                if (rbEmpname.IsChecked == true)
                {
                    opl = opl.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    opl = opl.Where(m => m.EmployeeNo == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    opl = opl.Where(m => m.StartDate >= datestart && m.EndDate <= dateend.AddDays(1)).ToList();
                }

                dgOnProcess.ItemsSource = opl.OrderByDescending(m => m.DateFiled);
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
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

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((OnProcessList)dgOnProcess.SelectedItem);
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

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if(opl.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 1;
                x.Report1 = opl;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.","System Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }
    }
    public class OnProcessList
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
        public bool IsOverdue { get; set; }
    }
}
