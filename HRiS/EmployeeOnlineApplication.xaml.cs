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
    /// Interaction logic for EmployeeOnlineApplication.xaml
    /// </summary>
    public partial class EmployeeOnlineApplication : MetroWindow
    {
        List<OnlineJobTitleList> OJTList = new List<OnlineJobTitleList>();
        List<OnlineJobApplicantList> OJAList = new List<OnlineJobApplicantList>();

        public EmployeeOnlineApplication()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetJobTitle();
        }
        public void GetJobTitle()
        {
            try
            {
                OJTList = new List<OnlineJobTitleList>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var jobtitle = db.EmployeeJobs.ToList();

                    foreach (var x in jobtitle)
                    {
                        OnlineJobTitleList ojtl = new OnlineJobTitleList();
                        ojtl.EmployeeJobID = x.EmployeeJobID;
                        ojtl.JobTitle = x.JobTitle;
                        ojtl.Qualifications = x.Qualifications;
                        ojtl.Description = x.Description;
                        ojtl.Type = x.Type;
                        ojtl.IsActive = x.Status.Value;
                        OJTList.Add(ojtl);
                    }
                    dgJobTitle.ItemsSource = OJTList.OrderBy(m => m.JobTitle);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (!String.IsNullOrEmpty(txtJobTitle.Text) || !String.IsNullOrEmpty(cbStatus.Text))
                    {
                        EmployeeJob ej = new EmployeeJob();
                        ej.JobTitle = txtJobTitle.Text;
                        ej.Qualifications = txtQualification.Text;
                        ej.Description = txtDescription.Text;
                        ej.Type = txtType.Text;
                        if (cbStatus.Text == "True")
                            ej.Status = true;
                        else
                            ej.Status = false;
                        db.EmployeeJobs.Add(ej);
                        db.SaveChanges();
                        MessageBox.Show("Add Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetJobTitle();
                    }
                    else
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgJobTitle.SelectedItem != null)
                    {
                        var selectedItem = (OnlineJobTitleList)(dgJobTitle.SelectedItem);

                        var findtit = db.EmployeeJobs.Find(selectedItem.EmployeeJobID);

                        findtit.JobTitle = txtJobTitle.Text;
                        findtit.Qualifications = txtQualification.Text;
                        findtit.Description = txtDescription.Text;
                        findtit.Type = txtType.Text;
                        if (cbStatus.Text == "True")
                            findtit.Status = true;
                        else
                            findtit.Status = false;
                        db.SaveChanges();
                        MessageBox.Show("Update Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetJobTitle();
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
            GetJobTitle();
            TextClear();
        }

        private void dgJobTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSave.IsEnabled = true;
                btnAdd.IsEnabled = false;
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgJobTitle.SelectedItem != null)
                    {
                        OJAList = new List<OnlineJobApplicantList>();
                        var selectedItem = (OnlineJobTitleList)(dgJobTitle.SelectedItem);

                        lblPos.Content = "List of Applicants in the Position of: " + selectedItem.JobTitle;

                        //Job Positions
                        txtJobTitle.Text = selectedItem.JobTitle;
                        txtDescription.Text = selectedItem.Description;
                        txtQualification.Text = selectedItem.Qualifications;
                        txtType.Text = selectedItem.Type;
                        if (selectedItem.IsActive == true)
                            cbStatus.Text = "True";
                        else
                            cbStatus.Text = "False";

                        //Applicant in that position

                        var appjob = db.HRISOnlineJobApplications.Where(m => m.EmployeeJobID == selectedItem.EmployeeJobID).ToList();

                        foreach (var x in appjob)
                        {
                            OnlineJobApplicantList ojal = new OnlineJobApplicantList();
                            ojal.ApplicationID = x.ApplicationID;
                            ojal.EmployeeJobID = x.EmployeeJobID.Value;
                            ojal.ApplicantName = x.LastName + ", " + x.FirstName + " " + x.MiddleName;
                            ojal.ApplicantAddress = x.CityAddress;
                            ojal.TelNo = x.TelephoneNo;
                            ojal.MobileNo = x.MobileNo;
                            ojal.StatusID = x.ApplicationStatusID.Value;
                            ojal.StatusDescription = x.HRISOnlineJobApplicationStatu.ApplicationStatus;
                            OJAList.Add(ojal);
                        }
                        dgAppJob.ItemsSource = OJAList.OrderBy(m => m.ApplicantName);

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TextClear()
        {
            dgJobTitle.SelectedItem = null;
            txtJobTitle.Text = "";
            txtDescription.Text = "";
            txtQualification.Text = "";
            txtType.Text = "";
            cbStatus.Text = "";
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;

        }

        public class OnlineJobTitleList
        {
            public int EmployeeJobID { get; set; }
            public string JobTitle { get; set; }
            public string Qualifications { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public bool IsActive { get; set; }
        }

        public class OnlineJobApplicantList
        {
            public int ApplicationID { get; set; }
            public int EmployeeJobID { get; set; }
            public string ApplicantName { get; set; }
            public string ApplicantAddress { get; set; }
            public string TelNo { get; set; }
            public string MobileNo { get; set; }
            public int StatusID { get; set; }
            public string StatusDescription { get; set; }
        }

        private void viewApplication_Click(object sender, RoutedEventArgs e)
        {
            if (dgAppJob.SelectedItem != null)
            {

                try
                {
                    using (var db = new LetranIntegratedSystemEntities())
                    {
                        var selectedItem = (OnlineJobApplicantList)(dgAppJob.SelectedItem);
                        UpdateOnlineApplication uoa = new UpdateOnlineApplication();
                        uoa.lblPos.Content += db.EmployeeJobs.Find(selectedItem.EmployeeJobID).JobTitle.ToUpper();
                        uoa.ApplicationID = selectedItem.ApplicationID;
                        uoa.Owner = this;
                        uoa.ShowDialog();
                        GetJobTitle();
                        TextClear();
                        dgAppJob.ItemsSource = null;
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
