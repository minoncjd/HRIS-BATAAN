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
    /// Interaction logic for ChangeVacationLeave.xaml
    /// </summary>
    public partial class ChangeVacationLeave : MetroWindow
    {
        List<HRiSClass.ChangeVacationLeaveList> CVLList = new List<HRiSClass.ChangeVacationLeaveList>();
       
        public ChangeVacationLeave()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetChangeVL();
        }

        public void GetChangeVL()
        {
            try
            {
                CVLList = new List<HRiSClass.ChangeVacationLeaveList>();

                using(var db = new LetranIntegratedSystemEntities())
                {
                    var changevl = db.HRISChangeVacationLeaves.ToList();
                    
                    foreach(var x in changevl)
                    {
                        var changevldet = db.HRISChangeVacationLeaveDetails.Where(m => m.VacationLeaveChangeID == x.VacationLeaveChangeID).ToList();
                        foreach(var y in changevldet)
                        {
                            HRiSClass.ChangeVacationLeaveList cvl = new HRiSClass.ChangeVacationLeaveList();

                            cvl.VLID = x.VacationLeaveChangeID;
                            cvl.VLDetailsID = y.VacationLeaveChangeDetailsID;
                            cvl.LeaveID = y.LeaveID.Value;
                            cvl.EmployeeID = y.Leave.Employee.EmployeeID;
                            cvl.EmployeeNumber = y.Leave.Employee.EmployeeNo;
                            cvl.EmployeeName = y.Leave.Employee.LastName.ToUpper() + ", " + y.Leave.Employee.FirstName.ToUpper();
                            cvl.StartDate = y.Leave.StartDate.Value;
                            cvl.EndDate = y.Leave.EndDate.Value;
                            cvl.Days = y.Leave.Days.Value;
                            //cvl.Status = y.Leave.LeaveApprovedStatu.ApprovedStatus;
                            cvl.DateFiled = x.DateFiled.Value;
                            cvl.NewStartDate = y.NewDateStart.Value;
                            cvl.NewEndDate = y.NewDateEnd.Value;
                            cvl.Reason = y.Reason;

                            CVLList.Add(cvl);
                        }
                    }
                    dgChangeVL.ItemsSource = CVLList.OrderByDescending(m => m.DateFiled);
                }
            }
           catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
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
                GetChangeVL();

                if (rbEmpname.IsChecked == true)
                {
                    CVLList = CVLList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    CVLList = CVLList.Where(m => m.EmployeeNumber == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    CVLList = CVLList.Where(m => m.StartDate >= datestart && m.EndDate <= dateend.AddDays(1)).ToList();
                }

                dgChangeVL.ItemsSource = CVLList.OrderByDescending(m => m.DateFiled);
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
            GetChangeVL();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HRiSClass.ChangeVacationLeaveList)dgChangeVL.SelectedItem);
            PrintWindow pw = new PrintWindow();
            pw.rptid = 19;
            pw.ChangeVLID = x.VLID;
            pw.ShowDialog();
        }

    }
}
