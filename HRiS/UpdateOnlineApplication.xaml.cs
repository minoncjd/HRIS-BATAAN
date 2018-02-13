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
using MahApps.Metro.Controls;
using HRiS.Reports;
using CrystalDecisions.CrystalReports.Engine;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for UpdateOnlineApplication.xaml
    /// </summary>
    public partial class UpdateOnlineApplication : MetroWindow
    {
        public int ApplicationID;
        List<ApplicationEducation> AEList = new List<ApplicationEducation>();
        List<ApplicationProfessionalLicense> APList = new List<ApplicationProfessionalLicense>();
        List<ApplicationWorkExperience> AWEList = new List<ApplicationWorkExperience>();
        List<ApplicationSeminar> ASList = new List<ApplicationSeminar>();

        List<HRiSClass.EmployeeAddWorkList> EAWList = new List<HRiSClass.EmployeeAddWorkList>();
        List<HRiSClass.EmployeeAddEducationList> EAEList = new List<HRiSClass.EmployeeAddEducationList>();
        List<HRiSClass.EmployeeAddSeminarList> EASList = new List<HRiSClass.EmployeeAddSeminarList>();



        ReportDocument report;

        public UpdateOnlineApplication()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new LetranIntegratedSystemEntities())
            {
                cbStatus.ItemsSource = db.HRISOnlineJobApplicationStatus.OrderBy(m => m.ApplicationStatusID).ToList();
                cbStatus.DisplayMemberPath = "ApplicationStatus";
                cbStatus.SelectedValuePath = "ApplicationStatusID";
            }
            cbCivilStatus.Items.Add("Single");
            cbCivilStatus.Items.Add("Married");
            cbCivilStatus.Items.Add("Widowed");
            cbCivilStatus.Items.Add("Separated");
            cbCivilStatus.Items.Add("Divorced");

            LoadApplicant(ApplicationID);
        }

        public void LoadApplicant(int appid)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var findapp = db.HRISOnlineJobApplications.Find(appid);

                    if (findapp != null)
                    {
                        //Personal Information
                        txtFirstName.Text = findapp.FirstName;
                        txtMiddleName.Text = findapp.MiddleName;
                        txtLastName.Text = findapp.LastName;
                        txtAddress.Text = findapp.CityAddress;
                        txtTelNo.Text = findapp.TelephoneNo;
                        txtMobileNo.Text = findapp.MobileNo;
                        txtAge.Text = findapp.Age;
                        if (findapp.Gender == "Male")
                            rbMale.IsChecked = true;
                        else
                            rbFemale.IsChecked = false;
                        dpBirthday.SelectedDate = findapp.Birthday;
                        txtPlaceBirth.Text = findapp.Birthplace;
                        cbCivilStatus.Text = findapp.CivilStatus;
                        txtCitizenship.Text = findapp.Citizenship;
                        txtReligion.Text = findapp.Religion;
                        txtTIN.Text = findapp.TIN;
                        txtSSS.Text = findapp.SSS;
                        txtPagibig.Text = findapp.PAGIBIG;
                        txtPhilhealth.Text = findapp.PHILHEALTH;

                        //Educational Information
                        AEList = new List<ApplicationEducation>();
                        cbEduc.ItemsSource = db.EducationalLevels.ToList();
                        cbEduc.DisplayMemberPath = "EducLevelName";
                        cbEduc.SelectedValuePath = "EducLevelID";

                        var appeduc = db.HRISOnlineJobApplicationEducations.Where(m => m.ApplicationID == findapp.ApplicationID).ToList();
                        foreach (var x in appeduc)
                        {
                            ApplicationEducation ae = new ApplicationEducation();
                            ae.EducLevelID = x.EducationType.Value;
                            ae.Course = x.Course;
                            ae.Honor = x.Honors;
                            ae.InclusiveDate = x.InclusiveDates;
                            ae.SchoolName = x.SchoolAddress;
                            AEList.Add(ae);
                        }
                        dgEduc.ItemsSource = AEList;

                        //Professional Licenses
                        APList = new List<ApplicationProfessionalLicense>();
                        var applicense = db.HRISOnlineJobApplicationLicenses.Where(m => m.ApplicationID == findapp.ApplicationID).ToList();
                        foreach (var x in applicense)
                        {
                            ApplicationProfessionalLicense apl = new ApplicationProfessionalLicense();
                            apl.ExamDate = x.ExamDate.Value;
                            apl.ExamPassed = x.ExamPassed;
                            apl.ExamPlace = x.ExamPlace;
                            apl.Rating = x.Rating;
                            APList.Add(apl);
                        }
                        dgProf.ItemsSource = APList;

                        //Work Experience
                        AWEList = new List<ApplicationWorkExperience>();
                        var appwork = db.HRISOnlineJobApplicationWorkExperiences.Where(m => m.ApplicationID == findapp.ApplicationID).ToList();
                        foreach (var x in appwork)
                        {
                            ApplicationWorkExperience awe = new ApplicationWorkExperience();
                            awe.Company = x.CompanyName;
                            awe.InclusiveDate = x.InclusiveDate;
                            awe.LastSalary = x.LastSalary;
                            awe.Position = x.Position;
                            awe.ReasonforLeaving = x.ReasonLeaving;
                            AWEList.Add(awe);
                        }
                        dgWorkExp.ItemsSource = AWEList;

                        //Seminars / Training Attended
                        ASList = new List<ApplicationSeminar>();
                        var appsem = db.HRISOnlineJobApplicationSeminars.Where(m => m.ApplicationID == findapp.ApplicationID).ToList();
                        foreach (var x in appsem)
                        {
                            ApplicationSeminar asem = new ApplicationSeminar();
                            asem.InclusiveDate = x.InclusiveDate;
                            asem.Seminar = x.Title;
                            asem.Venue = x.Venue;
                            ASList.Add(asem);
                        }
                        dgSem.ItemsSource = ASList;

                        //Additional Qualifications
                        txtSpecialskill.Text = findapp.SpecialSkills;
                        txtLanguage.Text = findapp.LanguagesSpeak;
                        txtMachine.Text = findapp.MachinesOperate;

                        txtKnowledge.Text = findapp.AdditionalKnowledge;
                        txtImpairment.Text = findapp.PhyschologicalImpairments;

                        cbStatus.SelectedValue = Convert.ToInt32(findapp.ApplicationStatusID);


                    }
                    else
                    {
                        MessageBox.Show("Applicant not found.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void PrintApplicant(int appid)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    PrintWindow pw = new PrintWindow();
                    pw.rptid = 27;
                    pw.ApplicationID = appid;
                    pw.ShowDialog();             
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveApplicant(int appid)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var findapp = db.HRISOnlineJobApplications.Find(appid);

                    if (findapp != null)
                    {
                        //Personal Information
                        findapp.FirstName = txtFirstName.Text;
                        findapp.MiddleName = txtMiddleName.Text;
                        findapp.LastName = txtLastName.Text;
                        findapp.CityAddress = txtAddress.Text;
                        findapp.TelephoneNo = txtTelNo.Text;
                        findapp.MobileNo = txtMobileNo.Text;
                        findapp.Age = txtAge.Text;
                        if (rbMale.IsChecked == true)
                            findapp.Gender = "Male";
                        else
                            findapp.Gender = "Female";
                        findapp.Birthday = Convert.ToDateTime(dpBirthday.SelectedDate);
                        findapp.Birthplace = txtPlaceBirth.Text;
                        findapp.CivilStatus = cbCivilStatus.Text;
                        findapp.Citizenship = txtCitizenship.Text;
                        findapp.Religion = txtReligion.Text;
                        findapp.TIN = txtTIN.Text;
                        findapp.SSS = txtSSS.Text;
                        findapp.PAGIBIG = txtPagibig.Text;
                        findapp.PHILHEALTH = txtPhilhealth.Text;

                        //Educational Information

                        var educlist = db.HRISOnlineJobApplicationEducations.Where(m => m.ApplicationID == findapp.ApplicationID).ToList();

                        foreach (var x in AEList)
                        {
                            if (educlist.Where(m => m.EducationType == x.EducLevelID && m.SchoolAddress == x.SchoolName).Count() == 0)
                            {
                                HRISOnlineJobApplicationEducation ae = new HRISOnlineJobApplicationEducation();
                                ae.ApplicationID = findapp.ApplicationID;
                                ae.EducationType = x.EducLevelID;
                                ae.SchoolAddress = x.SchoolName;
                                ae.Course = x.Course;
                                ae.InclusiveDates = x.InclusiveDate;
                                db.HRISOnlineJobApplicationEducations.Add(ae);
                            }
                        }

                        //Professional Licenses
                        var licenselist = db.HRISOnlineJobApplicationLicenses.Where(m => m.ApplicationID == findapp.ApplicationID).ToList();

                        foreach (var x in APList)
                        {
                            if (licenselist.Where(m => m.ExamPassed == x.ExamPassed && m.ExamDate == x.ExamDate).Count() == 0)
                            {
                                HRISOnlineJobApplicationLicens al = new HRISOnlineJobApplicationLicens();
                                al.ApplicationID = findapp.ApplicationID;
                                al.ExamPassed = x.ExamPassed;
                                al.ExamDate = x.ExamDate;
                                al.Rating = x.Rating;
                                al.ExamPlace = x.ExamPlace;
                                db.HRISOnlineJobApplicationLicenses.Add(al);
                            }
                        }

                        //Work Experiences

                        var worklist = db.HRISOnlineJobApplicationWorkExperiences.Where(m => m.ApplicationID == findapp.ApplicationID).ToList();

                        foreach (var x in AWEList)
                        {
                            if (worklist.Where(m => m.CompanyName == x.Company && m.Position == x.Position).Count() == 0)
                            {
                                HRISOnlineJobApplicationWorkExperience we = new HRISOnlineJobApplicationWorkExperience();
                                we.ApplicationID = findapp.ApplicationID;
                                we.CompanyName = x.Company;
                                we.Position = x.Position;
                                we.InclusiveDate = x.InclusiveDate;
                                we.LastSalary = x.LastSalary;
                                we.ReasonLeaving = x.ReasonforLeaving;
                                db.HRISOnlineJobApplicationWorkExperiences.Add(we);
                            }
                        }

                        //Seminars

                        var seminarlist = db.HRISOnlineJobApplicationSeminars.Where(m => m.ApplicationID == findapp.ApplicationID).ToList();

                        foreach (var x in ASList)
                        {
                            if (seminarlist.Where(m => m.Title == x.Seminar && m.InclusiveDate == x.InclusiveDate).Count() == 0)
                            {
                                HRISOnlineJobApplicationSeminar sem = new HRISOnlineJobApplicationSeminar();
                                sem.ApplicationID = findapp.ApplicationID;
                                sem.Title = x.Seminar;
                                sem.InclusiveDate = x.InclusiveDate;
                                sem.Venue = x.Venue;
                                db.HRISOnlineJobApplicationSeminars.Add(sem);
                            }
                        }
                        //Additional Qualification

                        findapp.SpecialSkills = txtSpecialskill.Text;
                        findapp.LanguagesSpeak = txtLanguage.Text;
                        findapp.MachinesOperate = txtMachine.Text;

                        findapp.AdditionalKnowledge = txtKnowledge.Text;
                        findapp.PhyschologicalImpairments = txtImpairment.Text;
                        findapp.ApplicationStatusID = Convert.ToInt32(cbStatus.SelectedValue);

                        db.SaveChanges();
                        MessageBox.Show("Save Successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Applicant not found.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TransferApplicant(int appid)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var appfind = db.HRISOnlineJobApplications.Find(appid);
                    if (appfind != null)
                    {
                        AddNewEmployee ae = new AddNewEmployee();
                        ae.Owner = this;

                        //Personal Information
                        ae.txtFirstName.Text = appfind.FirstName;
                        ae.txtMiddleName.Text = appfind.MiddleName;
                        ae.txtLastName.Text = appfind.LastName;
                        ae.txtCity.Text = appfind.CityAddress;
                        ae.txtTelno.Text = appfind.TelephoneNo;
                        ae.txtMobileno.Text = appfind.MobileNo;
                        if (appfind.Gender == "Male")
                            ae.rbMale.IsChecked = true;
                        else
                            ae.rbFemale.IsChecked = true;
                        ae.dpBirthday.SelectedDate = appfind.Birthday.Value;
                        ae.cbCivilStatus.Text = appfind.CivilStatus;
                        ae.txtCitizenship.Text = appfind.Citizenship;
                        ae.txtReligion.Text = appfind.Religion;
                        ae.txtTIN.Text = appfind.TIN;
                        ae.txtSSS.Text = appfind.SSS;
                        ae.txtPagibig.Text = appfind.PAGIBIG;
                        ae.txtPhilhealth.Text = appfind.PHILHEALTH;

                        //Educational Information
                        EAEList = new List<HRiSClass.EmployeeAddEducationList>();

                        var appeduc = db.HRISOnlineJobApplicationEducations.Where(m => m.ApplicationID == appfind.ApplicationID).ToList();

                        foreach (var x in appeduc)
                        {
                            HRiSClass.EmployeeAddEducationList edu = new HRiSClass.EmployeeAddEducationList();
                            edu.Award = x.Honors;
                            edu.Degree = x.Course;
                            edu.EducationType = db.EducationalLevels.Find(x.EducationType).EducLevelName;
                            edu.Graduation = x.InclusiveDates;
                            edu.Institution = x.SchoolAddress;
                            EAEList.Add(edu);
                        }

                        ae.dgEmpEduc.ItemsSource = EAEList;

                        //Work Experience
                        EAWList = new List<HRiSClass.EmployeeAddWorkList>();
                        var appwork = db.HRISOnlineJobApplicationWorkExperiences.Where(m => m.ApplicationID == appfind.ApplicationID).ToList();

                        foreach (var x in appwork)
                        {
                            HRiSClass.EmployeeAddWorkList eaw = new HRiSClass.EmployeeAddWorkList();
                            eaw.Company = x.CompanyName;
                            eaw.InclusiveDate = x.InclusiveDate;
                            eaw.LastSalary = x.LastSalary;
                            eaw.Position = x.Position;
                            eaw.Reason = x.Position;
                            EAWList.Add(eaw);
                        }
                        ae.dgWorkExp.ItemsSource = EAWList;

                        //Seminars
                        EASList = new List<HRiSClass.EmployeeAddSeminarList>();

                        var appsem = db.HRISOnlineJobApplicationSeminars.Where(m => m.ApplicationID == appfind.ApplicationID).ToList();

                        foreach (var x in appsem)
                        {
                            HRiSClass.EmployeeAddSeminarList eas = new HRiSClass.EmployeeAddSeminarList();
                            eas.InclusiveDate = x.InclusiveDate;
                            eas.Seminar = x.Title;
                            eas.Venue = x.Venue;
                            EASList.Add(eas);
                        }
                        ae.dgSeminars.ItemsSource = EASList;

                        //to be continued.. plus CSS plus INfo Ticketing


                        ae.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Applicant not found.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintApplicant(ApplicationID);
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            TransferApplicant(ApplicationID);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveApplicant(ApplicationID);
        }

        public class ApplicationEducation
        {
            public int EducLevelID { get; set; }
            public string SchoolName { get; set; }
            public string Course { get; set; }
            public string InclusiveDate { get; set; }
            public string Honor { get; set; }
        }
        public class ApplicationProfessionalLicense
        {
            public string ExamPassed { get; set; }
            public DateTime ExamDate { get; set; }
            public string Rating { get; set; }
            public string ExamPlace { get; set; }
        }
        public class ApplicationWorkExperience
        {
            public string Company { get; set; }
            public string Position { get; set; }
            public string InclusiveDate { get; set; }
            public string LastSalary { get; set; }
            public string ReasonforLeaving { get; set; }
        }
        public class ApplicationSeminar
        {
            public string Seminar { get; set; }
            public string Venue { get; set; }
            public string InclusiveDate { get; set; }
        }





    }
}
