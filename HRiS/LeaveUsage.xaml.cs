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
    /// Interaction logic for LeaveUsage.xaml
    /// </summary>
    public partial class LeaveUsage : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<LeaveUsageList> LUList = new List<LeaveUsageList>();

        public LeaveUsage()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                db = new LetranIntegratedSystemEntities();
                LUList = new List<LeaveUsageList>();

                DateTime startDate = Convert.ToDateTime(dpStart.SelectedDate);
                DateTime endDate = Convert.ToDateTime(dpEnd.SelectedDate);

                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var query = db.GetHRiSLeaveUsage(startDate, endDate).ToList();

                    foreach (var x in query)
                    {
                        LeaveUsageList lul = new LeaveUsageList();
                        lul.EmployeeID = x.EmployeeID;
                        lul.EmployeeNumber = x.EmployeeNo;
                        lul.EmployeeName = x.EmployeeName.ToUpper();
                        lul.Department = x.Department;
                        lul.Designation = x.Designation;
                        lul.DesignationID = x.EmployeeDesignationID;
                        lul.SL = x.SL.HasValue ? x.SL.Value : 0;
                        lul.SIL = x.SIL.HasValue ? x.SIL.Value : 0;
                        lul.VL = x.VL.HasValue ? x.VL.Value : 0;
                        LUList.Add(lul);
                    }
                    if (rbAll.IsChecked == true)
                    {
                        LUList = LUList.ToList();
                    }
                    if (rbAdmin.IsChecked == true)
                    {
                        LUList = LUList.Where(m => m.DesignationID == 1).ToList();
                    }
                    if (rbFaculty.IsChecked == true)
                    {
                        LUList = LUList.Where(m => m.DesignationID == 2).ToList();
                    }
                    if (rbNT.IsChecked == true)
                    {
                        LUList = LUList.Where(m => m.DesignationID == 3).ToList();
                    }

                    dgLeaveUsage.ItemsSource = LUList;
                }
                else
                {
                    MessageBox.Show("Date/s cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

        }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
    Mouse.OverrideCursor = null;
        }



        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (LUList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 5;
                x.Report5 = LUList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class LeaveUsageList
    {
        public int EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public int DesignationID { get; set; }
        public string Designation { get; set; }
        public decimal SL { get; set; }
        public decimal VL { get; set; }
        public decimal SIL { get; set; }
    }
}
