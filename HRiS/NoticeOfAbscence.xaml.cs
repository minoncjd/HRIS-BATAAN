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
    /// Interaction logic for NoticeOfAbscence.xaml
    /// </summary>
    public partial class NoticeOfAbscence : MetroWindow
    {
        List<HRiSClass.NoticeOfAbscenceList> NOAList = new List<HRiSClass.NoticeOfAbscenceList>();

        public NoticeOfAbscence()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetNOA();
        }

        public void GetNOA()
        {
            try
            {
                NOAList = new List<HRiSClass.NoticeOfAbscenceList>();

                using(var db = new LetranIntegratedSystemEntities())
                {
                    var noa = db.HRISNoticeofAbsences.ToList();

                    foreach(var x in noa)
                    {
                        HRiSClass.NoticeOfAbscenceList noal = new HRiSClass.NoticeOfAbscenceList();
                        noal.NoticeAbsenceID = x.NoticeAbsenceID;
                        noal.EmployeeID = x.Employee.EmployeeID;
                        noal.EmployeeNumber = x.Employee.EmployeeNo;
                        noal.EmployeeName = x.Employee.LastName.ToUpper() + ", " + x.Employee.FirstName.ToUpper();
                        noal.DateFiled = x.DateFiled.Value;
                        noal.StartDate = x.DateStart.Value;
                        noal.EndDate = x.DateEnd.Value;
                        noal.Reason = x.Reason;
                        noal.Days = x.NumberDays;
                        
                        NOAList.Add(noal);
                    }
                    dgNoticeOfAbscence.ItemsSource = NOAList.OrderByDescending(m => m.DateFiled);
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
                GetNOA();

                if (rbEmpname.IsChecked == true)
                {
                    NOAList = NOAList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    NOAList = NOAList.Where(m => m.EmployeeNumber == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    NOAList = NOAList.Where(m => m.StartDate >= datestart && m.EndDate <= dateend.AddDays(1)).ToList();
                }

                dgNoticeOfAbscence.ItemsSource = NOAList.OrderByDescending(m => m.DateFiled);
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
            GetNOA();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";
        }

        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HRiSClass.NoticeOfAbscenceList)dgNoticeOfAbscence.SelectedItem);
            PrintWindow pw = new PrintWindow();
            pw.rptid = 20;
            pw.NOAID = x.NoticeAbsenceID;
            pw.ShowDialog();
        }


    }
}
