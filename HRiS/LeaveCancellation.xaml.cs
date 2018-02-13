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
    /// Interaction logic for LeaveCancellation.xaml
    /// </summary>
    public partial class LeaveCancellation : MetroWindow
    {

        List<HRiSClass.LeaveCancellationList> LCList = new List<HRiSClass.LeaveCancellationList>(); 

        public LeaveCancellation()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetLeaveCancel();
        }

        public void GetLeaveCancel()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    LCList = new List<HRiSClass.LeaveCancellationList>();
                    var leavecancel = db.HRISLeaveCancellations.ToList();

                    foreach (var x in leavecancel)
                    {
                        HRiSClass.LeaveCancellationList lcl = new HRiSClass.LeaveCancellationList();
                        lcl.LeaveCancelID = x.LeaveCancellationID;
                        lcl.LeaveID = x.LeaveID.Value;
                        lcl.EmployeeID = x.Leave.EmployeeID.Value;
                        lcl.EmployeeNumber = x.Leave.Employee.EmployeeNo;
                        lcl.EmployeeName = x.Leave.Employee.LastName.ToUpper() + ", " + x.Leave.Employee.FirstName.ToUpper();
                        //lcl.DateFiled = x.Leave.FiledDate.Value;
                        lcl.StartDate = x.Leave.StartDate.Value;
                        lcl.EndDate = x.Leave.EndDate.Value;
                        lcl.Type = x.Leave.LeaveType.LeaveCode;
                        lcl.Reason = x.Leave.Reason;
                        lcl.Days = x.Leave.Days.Value;
                        //lcl.Status = x.Leave.LeaveApprovedStatu.ApprovedStatus;
                        lcl.LeaveCancelStatus = x.Reason;
                        lcl.CancelDate = x.DateFiled.Value;

                        //if (x.Leave.LeaveApprovedStatu.LeaveApprovedStatusID == 4 || x.Leave.LeaveApprovedStatu.LeaveApprovedStatusID == 5)
                        //    lcl.IsActive = false;
                        //else
                        //    lcl.IsActive = true;
                        LCList.Add(lcl);
                    }
                    dgLeaveCancel.ItemsSource = LCList.OrderByDescending(m => m.DateFiled);
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
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var x = ((HRiSClass.LeaveCancellationList)dgLeaveCancel.SelectedItem);

                    if(x.IsActive == true)
                    {
                        Leave leaveapp = db.Leaves.Find(x.LeaveID);
                        // var empid = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNo).FirstOrDefault();
                        HRiS.Model.LeaveBalance leavebal = db.LeaveBalances.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();

                        if (leaveapp != null)
                        {
                            // DISAPPROVED PART 
                            //leaveapp.LeaveApprovedStatusID = 5;
                            //leaveapp.CancellationDate = DateTime.Now;

                            //RETURN OF LEAVE CREDITS

                            //EL
                            if (leaveapp.LeaveType.LeaveTypeID == 1)
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
                            else if (leaveapp.LeaveType.LeaveTypeID == 5)
                            {
                                //leavebal.VacationLeaveBalance = leavebal.VacationLeaveBalance + x.Days;
                            }
                            //SPL
                            else if (leaveapp.LeaveType.LeaveTypeID == 7)
                            {
                                //leavebal.SoloParentLeaveBalance = leavebal.SoloParentLeaveBalance + x.Days;
                            }
                            //BL
                            else if (leaveapp.LeaveType.LeaveTypeID == 10)
                            {
                                //leavebal.BereavementLeaveBalance = leavebal.BereavementLeaveBalance + x.Days;
                            }
                            db.SaveChanges();
                            MessageBox.Show("Leave Cancelled", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                            GetLeaveCancel();
                        }
                        else
                        {
                            MessageBox.Show("Cannot find leave transaction.", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Leave already has been cancelled.", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                  
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                GetLeaveCancel();

                if (rbEmpname.IsChecked == true)
                {
                    LCList = LCList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    LCList = LCList.Where(m => m.EmployeeNumber == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    LCList = LCList.Where(m => m.StartDate >= datestart && m.EndDate <= dateend.AddDays(1)).ToList();
                }

                dgLeaveCancel.ItemsSource = LCList.OrderByDescending(m => m.DateFiled);
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetLeaveCancel();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HRiSClass.LeaveCancellationList)dgLeaveCancel.SelectedItem);
            PrintWindow pw = new PrintWindow();
            pw.rptid = 18;
            pw.LeaveCancellationID = x.LeaveCancelID;
            pw.ShowDialog();
        }
    }
}
