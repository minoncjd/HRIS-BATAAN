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


using MahApps.Metro.Controls;namespace HRiS
{
    /// <summary>
    /// Interaction logic for SubstituteClass.xaml
    /// </summary>
    public partial class SubstituteClass : MetroWindow
    {
        List<HRiSClass.SubstituteClassList> SCList = new List<HRiSClass.SubstituteClassList>();
        public SubstituteClass()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetSubstitute();
        }

        public void GetSubstitute()
        {
            try
            {
                SCList = new List<HRiSClass.SubstituteClassList>();
                using(var db = new LetranIntegratedSystemEntities())
                {
                    var sub = db.HRISSubstituteClasses.ToList();

                    foreach(var x in sub)
                    {
                        HRiSClass.SubstituteClassList sc = new HRiSClass.SubstituteClassList();
                        sc.SubstituteClassID = x.SubstitutionClassID;
                        sc.EmployeeID = x.Employee.EmployeeID;
                        sc.EmployeeNumber = x.Employee.EmployeeNo;
                        sc.EmployeeName = x.Employee.LastName.ToUpper() + ", " + x.Employee.FirstName.ToUpper();
                        sc.SubEmployeeID = x.Employee1.EmployeeID;
                        sc.SubEmployeeNumber = x.Employee1.EmployeeNo;
                        sc.SubEmployeeName = x.Employee1.LastName.ToUpper() + ", " + x.Employee1.FirstName.ToUpper();
                        sc.DateFile = x.DateFiled;
                        sc.Reason = x.Reason;
                        SCList.Add(sc);
                    }
                    dgSubstitute.ItemsSource = SCList.OrderByDescending(m => m.DateFile);
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
                GetSubstitute();

                if (rbEmpname.IsChecked == true)
                {
                    SCList = SCList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    SCList = SCList.Where(m => m.EmployeeNumber == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    SCList = SCList.Where(m => m.DateFile >= datestart && m.DateFile <= dateend.AddDays(1)).ToList();
                }

                dgSubstitute.ItemsSource = SCList.OrderByDescending(m => m.DateFile);
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
            GetSubstitute();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HRiSClass.SubstituteClassList)dgSubstitute.SelectedItem);
            PrintWindow pw = new PrintWindow();
            pw.rptid = 26;
            pw.SubID = x.SubstituteClassID;
            pw.ShowDialog();

        }
    }
}
