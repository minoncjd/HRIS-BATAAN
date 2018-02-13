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
    /// Interaction logic for Overtime.xaml
    /// </summary>
    public partial class Overtime : MetroWindow
    {
        List<HRiSClass.OvertimeRequestList> OTRList = new List<HRiSClass.OvertimeRequestList>();

        public Overtime()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetOT();
        }

        public void GetOT()
        {
            try
            {
                OTRList = new List<HRiSClass.OvertimeRequestList>();
                using(var db = new LetranIntegratedSystemEntities())
                {
                    var otreq = db.HRISOvertimes.ToList();

                    foreach(var x in otreq)
                    {
                      
                            HRiSClass.OvertimeRequestList otr = new HRiSClass.OvertimeRequestList();

                            otr.OTID = x.EmployeeOvertimeID;
                            otr.EmployeeID = x.Employee.EmployeeID;
                            otr.EmployeeNumber = x.Employee.EmployeeNo;
                            otr.EmployeeName = x.Employee.LastName.ToUpper() + ", " + x.Employee.FirstName.ToUpper();
                            otr.DateFiled = x.DateFiled;
                            OTRList.Add(otr);
                    }
                    dgOTReq.ItemsSource = OTRList.OrderByDescending(m => m.DateFiled);

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
                GetOT();

                if (rbEmpname.IsChecked == true)
                {
                    OTRList = OTRList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    OTRList = OTRList.Where(m => m.EmployeeNumber == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    OTRList = OTRList.Where(m => m.DateFiled >= datestart && m.DateFiled <= dateend.AddDays(1)).ToList();
                }

                dgOTReq.ItemsSource = OTRList.OrderByDescending(m => m.DateFiled);
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
            GetOT();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HRiSClass.OvertimeRequestList)dgOTReq.SelectedItem);
            PrintWindow pw = new PrintWindow();
            pw.rptid = 21;
            pw.OvertimeID = x.OTID;
            pw.ShowDialog();
        }
    }
}
