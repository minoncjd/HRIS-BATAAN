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
    /// Interaction logic for PostTraining.xaml
    /// </summary>
    public partial class PostTraining : MetroWindow
    {
        List<HRiSClass.PostTrainingList> PTList = new List<HRiSClass.PostTrainingList>();
        public PostTraining()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetPostTraining();
        }
        public void GetPostTraining()
        {
            try
            {
                PTList = new List<HRiSClass.PostTrainingList>();
                using(var db = new LetranIntegratedSystemEntities())
                {
                    var post = db.HRISPostTrainings.ToList();

                    foreach(var x in post)
                    {
                        HRiSClass.PostTrainingList pt = new HRiSClass.PostTrainingList();
                        pt.PostTrainingID = x.PostTrainingID;
                        pt.EmployeeID = x.Employee.EmployeeID;
                        pt.EmployeeNumber = x.Employee.EmployeeNo;
                        pt.EmployeeName = x.Employee.LastName.ToUpper() + ", " + x.Employee.FirstName.ToUpper();
                        pt.ActivityID = x.HRISActivityType.HRISActivityTypeID;
                        pt.ActivityType = x.HRISActivityType.ActivityName;
                        pt.Title = x.Titile;
                        pt.DateStart = Convert.ToDateTime(x.DateStart);
                        pt.DateEnd = Convert.ToDateTime(x.DateEnd);
                        pt.Venue = x.Venue;
                        pt.Others = x.Others;
                        pt.Background = x.Background;
                        pt.Expectation = x.Expectation;
                        pt.ResourcePerson = x.ResourcePerson;
                        pt.Approach = x.Approach;
                        pt.KeyLearning = x.KeyLearning;
                        pt.Issues = x.Issues;
                        pt.Generalization = x.Generalization;
                        pt.DateFile = x.DateFile.Value;
                        PTList.Add(pt);
                    }
                    dgPostTraining.ItemsSource = PTList.OrderByDescending(m=>m.DateFile);
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
                GetPostTraining();

                if (rbEmpname.IsChecked == true)
                {
                    PTList = PTList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    PTList = PTList.Where(m => m.EmployeeNumber == txtSearch.Text).ToList();
                }
                if (!String.IsNullOrEmpty(dpStart.Text) && !String.IsNullOrEmpty(dpEnd.Text))
                {
                    var datestart = Convert.ToDateTime(dpStart.SelectedDate);
                    var dateend = Convert.ToDateTime(dpEnd.SelectedDate);

                    PTList = PTList.Where(m => m.DateFile >= datestart && m.DateFile <= dateend.AddDays(1)).ToList();
                }

                dgPostTraining.ItemsSource = PTList.OrderByDescending(m=>m.DateFile);
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
            GetPostTraining();
            txtSearch.Text = "";
            dpEnd.Text = "";
            dpStart.Text = "";

        }

        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HRiSClass.PostTrainingList)dgPostTraining.SelectedItem);
            PrintWindow pw = new PrintWindow();
            pw.rptid = 24;
            pw.PostID = x.PostTrainingID;
            pw.ShowDialog();
        }
    }
}
