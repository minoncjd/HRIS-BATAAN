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
    /// Interaction logic for CertificationOfEmployment.xaml
    /// </summary>
    public partial class CertificationOfEmployment : MetroWindow
    {
        List<HRiSClass.EmploymentCertificateList> ECList = new List<HRiSClass.EmploymentCertificateList>();


        public CertificationOfEmployment()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetCertificate();
        }

        public void GetCertificate()
        {
            try
            {
                ECList = new List<HRiSClass.EmploymentCertificateList>();
                using(var db = new LetranIntegratedSystemEntities())
                {
                    var appcert = db.HRISApplicationCertificateEmployments.ToList();

                    foreach(var x in appcert)
                    {
                        HRiSClass.EmploymentCertificateList ecl = new HRiSClass.EmploymentCertificateList();
                        ecl.ApplicantID = x.CetificateEmploymentID;
                        ecl.EmployeeName = x.LastName.ToUpper() + ", " + x.FirstName.ToUpper() + " " + x.MiddleName.ToUpper();
                        ecl.DepartmentID = x.DepartmentID;
                        ecl.Department = x.AcademicDepartment.AcaDepartmentName;
                        ecl.Sendee = x.Sendee;
                        ecl.SendeeAddress = x.SendeeAddress;
                        ecl.DateFile = x.DateFile;
                        ecl.DateNeeded = x.DateNeeded;
                        ecl.Purpose = x.Purpose;
                        ECList.Add(ecl);
                    }
                    dgEmpCert.ItemsSource = ECList.OrderByDescending(m => m.DateFile);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void rbEmpname_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void rbDepartment_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                GetCertificate();

                if (rbEmpname.IsChecked == true)
                {
                    ECList = ECList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbDepartment.IsChecked == true)
                {
                    ECList = ECList.Where(m => m.Department == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    ECList = ECList.Where(m => m.DateFile >= datestart && m.DateFile <= dateend.AddDays(1)).ToList();
                }

                dgEmpCert.ItemsSource = ECList.OrderByDescending(m => m.DateFile);
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
            GetCertificate();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void btnprint_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
