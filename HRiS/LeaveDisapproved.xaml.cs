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
    /// Interaction logic for LeaveDisapproved.xaml
    /// </summary>
    public partial class LeaveDisapproved : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<DisapprovedList> dl = new List<DisapprovedList>();
        public LeaveDisapproved()
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
                dl = new List<DisapprovedList>();

                var x = db.GetHRiSLeaveCancelled().ToList();

                foreach(var i in x)
                {
                    DisapprovedList Dlist = new DisapprovedList();
                    Dlist.LeaveID = i.LeaveID;
                    Dlist.EmployeeNo = i.EmployeeNo;
                    Dlist.EmployeeName = i.EmployeeName.ToUpper();
                    Dlist.DateFiled = i.FiledDate.Value;
                    Dlist.StartDate = i.StartDate.Value;
                    Dlist.EndDate = i.EndDate.Value;
                    Dlist.Type = i.LeaveCode;
                    Dlist.Days = i.Days.Value;
                    Dlist.Status = i.Status;
                    Dlist.Reason = i.Reason;
                    Dlist.LeaveStatusID = i.LeaveApprovedStatusID.Value;
                    dl.Add(Dlist);
                }
                dgDisapproved.ItemsSource = dl.OrderByDescending(m => m.DateFiled);
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong!","System Warning.",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((DisapprovedList)dgDisapproved.SelectedItem);
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

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((DisapprovedList)dgDisapproved.SelectedItem);
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var x = ((DisapprovedList)dgDisapproved.SelectedItem);


                Leave leaveapp = db.Leaves.Find(x.LeaveID);
                var empid = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNo).FirstOrDefault();
               // HRiS.Model.LeaveBalance leavebal = db.LeaveBalances.Where(m => m.EmployeeID == empid.EmployeeID).FirstOrDefault();

                if (leaveapp != null)
                {
                    //if cancelled already
                    //if(leaveapp.LeaveApprovedStatusID == 4 || leaveapp.LeaveApprovedStatusID == 5)
                    //{
                    //    MessageBox.Show("Leave already cancelled.","System Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
                    //    GetLeave();
                    //    return;
                    //}
                    
                    //leaveapp.LeaveApprovedStatusID = 4;
                    //leaveapp.CancellationDate = DateTime.Now;

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
                    dl = dl.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    dl = dl.Where(m => m.EmployeeNo == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    dl = dl.Where(m => m.StartDate >= datestart && m.EndDate <= dateend.AddDays(1)).ToList();
                }

                dgDisapproved.ItemsSource = dl.OrderByDescending(m => m.DateFiled);
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

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (dl.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 3;
                x.Report3 = dl;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
    public class DisapprovedList
    {
        public int LeaveID { get; set; }
        public int LeaveStatusID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNo { get; set; }
        public DateTime DateFiled { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string Type { get; set; }
        public decimal Days { get; set; }
        public string Status { get; set; }
    }
}
