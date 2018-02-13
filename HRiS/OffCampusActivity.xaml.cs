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
    /// Interaction logic for OffCampusActivity.xaml
    /// </summary>
    public partial class OffCampusActivity : MetroWindow
    {
        List<HRiSClass.OffCampusActivityList> OCAList = new List<HRiSClass.OffCampusActivityList>();
        public OffCampusActivity()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetOffCampus();
        }

        public void GetOffCampus()
        {
            try
            {
                OCAList = new List<HRiSClass.OffCampusActivityList>();
                using(var db = new LetranIntegratedSystemEntities())
                {
                    var offcampus = db.HRISOffCampusActivities.ToList();

                    foreach(var x in offcampus)
                    {
                        HRiSClass.OffCampusActivityList oca = new HRiSClass.OffCampusActivityList();
                        oca.OffCampusActivityID = x.OffCampusActivityID;
                        oca.EmployeeID = x.Employee.EmployeeID;
                        oca.EmployeeNumber = x.Employee.EmployeeNo;
                        oca.EmployeeName = x.Employee.LastName.ToUpper() + ", " + x.Employee.FirstName.ToUpper();
                        oca.Reason = x.Reason;
                        oca.DateStart = x.DateStart.Value;
                        oca.DateEnd = x.DateEnd.Value;
                        oca.DateFiled = x.DateFiled.Value;
                        OCAList.Add(oca);
                    }
                    dgOffCampus.ItemsSource = OCAList.OrderByDescending(m => m.DateFiled);
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
                GetOffCampus();

                if (rbEmpname.IsChecked == true)
                {
                    OCAList = OCAList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    OCAList = OCAList.Where(m => m.EmployeeNumber == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    OCAList = OCAList.Where(m => m.DateStart >= datestart && m.DateEnd <= dateend.AddDays(1)).ToList();
                }

                dgOffCampus.ItemsSource = OCAList.OrderByDescending(m => m.DateFiled);
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
            GetOffCampus();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HRiSClass.OffCampusActivityList)dgOffCampus.SelectedItem);
            PrintWindow pw = new PrintWindow();
            pw.rptid = 23;
            pw.OffID = x.OffCampusActivityID;
            pw.ShowDialog();
        }
    }
}
