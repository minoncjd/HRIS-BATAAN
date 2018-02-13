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
    /// Interaction logic for EmployeeAnnouncementFeedback.xaml
    /// </summary>
    public partial class EmployeeAnnouncementFeedback : MetroWindow
    {

        List<FeeedbackList> FBList = new List<FeeedbackList>();
        public EmployeeAnnouncementFeedback()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAnnouncementFeedback();
        }

        public void GetAnnouncementFeedback()
        {
            try
            {
                using(var db = new LetranIntegratedSystemEntities())
                {
                    FBList = new List<FeeedbackList>();
                    //Announcement
                    var ann = db.EmployeeAnnouncements.OrderBy(m=>m.EmployeeAnnoucementID).ToList();

                    if(ann != null)
                    {
                        dgAnnouncement.ItemsSource = ann;
                    }

                    //Feedback

                    var feed = db.EmployeeFeedbacks.OrderBy(m => m.EmployeeFeedbackID).ToList();

                    foreach(var x in feed)
                    {
                        FeeedbackList fbl = new FeeedbackList();
                        fbl.FeedBackID = x.EmployeeFeedbackID;
                        fbl.EmployeeID = x.Employee.EmployeeID;
                        fbl.EmployeeNo = x.Employee.EmployeeNo;
                        fbl.EmployeeName = x.Employee.LastName.ToUpper() + ", " + x.Employee.FirstName.ToUpper();
                        fbl.Department = x.Employee.AcademicDepartment.AcaDepartmentName;
                        fbl.Feedback = x.Feedback;
                        fbl.DatePosted = x.DatePosted.Value;
                        FBList.Add(fbl);
                    }
                    dgFeedBack.ItemsSource = FBList;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        public void TextClear()
        {
            dgAnnouncement.SelectedItem = null;
            dgFeedBack.SelectedItem = null;
            txtTitle.Text = "";
            txtCreatedBy.Text = "";
            txtAnnouncement.Text = "";
            dpDate.Text = "";
            cbIsActive.Text = "";
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db = new LetranIntegratedSystemEntities())
                {
                    if (!String.IsNullOrEmpty(txtTitle.Text) || !String.IsNullOrEmpty(cbIsActive.Text))
                    {
                        EmployeeAnnouncement ea = new EmployeeAnnouncement();

                        ea.Title = txtTitle.Text;
                        ea.Annoucement = txtTitle.Text;
                        ea.CreatedBy = txtCreatedBy.Text;
                        ea.DateCreated = Convert.ToDateTime(dpDate.SelectedDate);
                        if (cbIsActive.Text == "True")
                            ea.Active = true;
                        else
                            ea.Active = false;
                        db.EmployeeAnnouncements.Add(ea);
                        db.SaveChanges();
                        MessageBox.Show("Add Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetAnnouncementFeedback();
                    }
                    else
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if(dgAnnouncement.SelectedItem != null)
                    {
                        var selectedItem = (HRiS.Model.EmployeeAnnouncement)(dgAnnouncement.SelectedItem);
                        var ann = db.EmployeeAnnouncements.Find(selectedItem.EmployeeAnnoucementID);

                        ann.Title = txtTitle.Text;
                        ann.Annoucement = txtAnnouncement.Text;
                        ann.CreatedBy = txtCreatedBy.Text;
                        ann.DateCreated = dpDate.SelectedDate;
                        if (cbIsActive.Text == "True")
                            ann.Active = true;
                        else
                            ann.Active = false;
                        db.SaveChanges();
                        MessageBox.Show("Update Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetAnnouncementFeedback();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetAnnouncementFeedback();
            TextClear();
        }

        private void dgAnnouncement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSave.IsEnabled = true;
                btnAdd.IsEnabled = false;
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgAnnouncement.SelectedItem != null)
                    {
                      
                        var selectedItem = (HRiS.Model.EmployeeAnnouncement)(dgAnnouncement.SelectedItem);
                        var ann = db.EmployeeAnnouncements.Find(selectedItem.EmployeeAnnoucementID);

                        txtTitle.Text = ann.Title;
                        txtAnnouncement.Text = ann.Annoucement;
                        txtCreatedBy.Text = ann.CreatedBy;
                        dpDate.SelectedDate = ann.DateCreated;
                        if (ann.Active == true)
                            cbIsActive.Text = "True";
                        else
                            cbIsActive.Text = "False";
                       

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        class FeeedbackList
        {
            public int FeedBackID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNo { get; set; }
            public string EmployeeName { get; set; }
            public string Department { get; set; }
            public string Feedback { get; set; }
            public DateTime DatePosted { get; set; }
        }
    }
}
