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
    /// Interaction logic for MakeUpClass.xaml
    /// </summary>
    public partial class MakeUpClass : MetroWindow
    {
        List<HRiSClass.MakeUpClassList> MUCList = new List<HRiSClass.MakeUpClassList>();

        public MakeUpClass()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetMakeUpClass();
        }

        public void GetMakeUpClass()
        {
            try
            {
                MUCList = new List<HRiSClass.MakeUpClassList>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var makeup = db.HRISMakeUpClasses.ToList();

                    foreach (var x in makeup)
                    {
                        HRiSClass.MakeUpClassList muc = new HRiSClass.MakeUpClassList();
                        muc.MakeUpClassID = x.MakeUpClassID;
                        muc.EmployeeID = x.Employee.EmployeeID;
                        muc.EmployeeNumber = x.Employee.EmployeeNo;
                        muc.EmployeeName = x.Employee.LastName.ToUpper() + ", " + x.Employee.FirstName.ToUpper();
                        muc.DateFile = x.DateFiled;
                        muc.Reason = x.Reason;
                        MUCList.Add(muc);

                    }
                    dgMakeUp.ItemsSource = MUCList.OrderByDescending(m=>m.DateFile);

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
                GetMakeUpClass();

                if (rbEmpname.IsChecked == true)
                {
                    MUCList = MUCList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    MUCList = MUCList.Where(m => m.EmployeeNumber == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    MUCList = MUCList.Where(m => m.DateFile >= datestart && m.DateFile <= dateend.AddDays(1)).ToList();
                }

                dgMakeUp.ItemsSource = MUCList.OrderByDescending(m=>m.DateFile);
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
            GetMakeUpClass();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HRiSClass.MakeUpClassList)dgMakeUp.SelectedItem);
            PrintWindow pw = new PrintWindow();
            pw.rptid = 25;
            pw.MakeupID = x.MakeUpClassID;
            pw.ShowDialog();
        }


    }
}
