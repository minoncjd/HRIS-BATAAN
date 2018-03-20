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
using MahApps.Metro.Controls.Dialogs;
using HRiS.Model;
using System.IO;
using Microsoft.Win32;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for UpdateEmployee.xaml
    /// </summary>
    public partial class UpdateEmployee : MetroWindow
    {
        public int EmpID;
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmployeeEducationList> EEList = new List<EmployeeEducationList>();
        List<EmployeeSeminarList> ESList = new List<EmployeeSeminarList>();
        List<EmployeeWorkList> EWList = new List<EmployeeWorkList>();
        public UpdateEmployee()
        {
            InitializeComponent();

            LoadComboBox();
        }

        public void LoadComboBox()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();

                cbArea.ItemsSource = db.Areas.OrderBy(m => m.RegionID).ToList();
                cbArea.DisplayMemberPath = "Area1";
                cbArea.SelectedValuePath = "AreaID";

                cbProvArea.ItemsSource = db.Areas.OrderBy(m => m.RegionID).ToList();
                cbProvArea.DisplayMemberPath = "Area1";
                cbProvArea.SelectedValuePath = "AreaID";

                cbSalutation.Items.Add("Mr.");
                cbSalutation.Items.Add("Ms.");
                cbSalutation.Items.Add("Mrs.");
                cbSalutation.Items.Add("Bro.");
                cbSalutation.Items.Add("Fr.");
                cbSalutation.Items.Add("Dr.");
                cbSalutation.Items.Add("Engr.");
                cbSalutation.Items.Add("Sr.");
                cbSalutation.Items.Add("Atty.");

                cbCivilStatus.Items.Add("Single");
                cbCivilStatus.Items.Add("Married");
                cbCivilStatus.Items.Add("Widowed");
                cbCivilStatus.Items.Add("Separated");
                cbCivilStatus.Items.Add("Divorced");

                cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaDepartmentName).ToList();
                cbDepartment.DisplayMemberPath = "AcaDepartmentName";
                cbDepartment.SelectedValuePath = "AcaDeptID";

                cbEmpType.ItemsSource = db.EmploymentTypes.OrderBy(m => m.EmploymentTypeID).ToList();
                cbEmpType.DisplayMemberPath = "EmploymentTypeName";
                cbEmpType.SelectedValuePath = "EmploymentTypeID";

                cbStatus.ItemsSource = db.EmployeeStatus.OrderBy(m => m.EmployeeStatusID).ToList();
                cbStatus.DisplayMemberPath = "EmployeeStatusName";
                cbStatus.SelectedValuePath = "EmployeeStatusID";

                cbDesignation.ItemsSource = db.EmployeeDesignations.OrderBy(m => m.EmployeeDesignationID).ToList();
                cbDesignation.DisplayMemberPath = "EmployeeDesignationName";
                cbDesignation.SelectedValuePath = "EmployeeDesignationID";

                cbFacultyArea.ItemsSource = db.FacultyAreas.OrderBy(m => m.FacultyAreaID).ToList();
                cbFacultyArea.DisplayMemberPath = "FacultyAreaName";
                cbFacultyArea.SelectedValuePath = "FacultyAreaID";

                cbEmployeePosition.ItemsSource = db.EmployeePositions.OrderBy(m => m.EmployeePositionID).ToList();
                cbEmployeePosition.DisplayMemberPath = "EmployeePositionName";
                cbEmployeePosition.SelectedValuePath = "EmployeePositionID";

                cbLevel.ItemsSource = db.EmployeeLevels.OrderBy(m => m.EmployeeLevelID).ToList();
                cbLevel.DisplayMemberPath = "EmployeeLevel1";
                cbLevel.SelectedValuePath = "EmployeeLevelID";

                cbTax.Items.Add("Zero Tax Amount");
                cbTax.Items.Add("Zero Exemption");
                cbTax.Items.Add("Single");
                cbTax.Items.Add("Head of the Family");
                cbTax.Items.Add("Married");
                cbTax.Items.Add("Head of the Family with 1 Dependent");
                cbTax.Items.Add("Head of the Family with 2 Dependents");
                cbTax.Items.Add("Head of the Family with 3 Dependents");
                cbTax.Items.Add("Head of the Family with 4 Dependents");
                cbTax.Items.Add("Married with 1 Qualified Child");
                cbTax.Items.Add("Married with 2 Qualified Children");
                cbTax.Items.Add("Married with 3 Qualified Children");
                cbTax.Items.Add("Married with 4 Qualified Children");

                dpHired.SelectedDate = DateTime.Now;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public void LoadEmployee()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();

                var editemp = db.Employees.Find(EmpID);
                cbEmpType.SelectedValue = editemp.EmploymentTypeID;
                txtEmpNumber.Text = editemp.EmployeeNo;
                cbDepartment.SelectedValue = editemp.EmployeeDepartmentID;
                cbStatus.SelectedValue = editemp.EmployeeStatusID;
                cbDesignation.SelectedValue = editemp.EmployeeDesignation;
                cbFacultyArea.SelectedValue = editemp.FacultyAreaID;
                cbEmployeePosition.SelectedValue = editemp.EmployeePositionID;
                cbLevel.SelectedValue = editemp.EmployeeLevelID;
                cbTax.Text = editemp.TaxStatus;
                dpHired.SelectedDate = editemp.DateHired;
                dpPermanency.SelectedDate = editemp.DatePermanency;
                dpEndo.SelectedDate = editemp.DateEndContract;
                dpResigned.SelectedDate = editemp.DateResigned;
                dpRetirement.SelectedDate = editemp.DateRetired;
                txtReason.Text = editemp.ReasonForLeaving;
                if (editemp.Photo != null)
                    IDpic.Source = LoadImage(editemp.Photo);
                if (editemp.Archive == true)
                    chkActive.IsChecked = false;
                else
                    chkActive.IsChecked = true;

                cbSalutation.Text = editemp.Title;
                txtLastName.Text = editemp.LastName;
                txtFirstName.Text = editemp.FirstName;
                txtMiddleName.Text = editemp.MiddleName;
                txtNickName.Text = editemp.Nickname;
                if (editemp.Sex == "M")
                    rbMale.IsChecked = true;
                else
                    rbFemale.IsChecked = true;
                dpBirthday.SelectedDate = editemp.Birthday;
                txtPlaceBirth.Text = editemp.Birthplace;
                txtCitizenship.Text = editemp.Nationality;
                if (editemp.CivilStatus == "S")
                    cbCivilStatus.Text = "Single";
                else if (editemp.CivilStatus == "M")
                    cbCivilStatus.Text = "Married";
                else if (editemp.CivilStatus == "W")
                    cbCivilStatus.Text = "Widowed";
                else if (cbCivilStatus.Text == "D")
                    cbCivilStatus.Text = "Divorced";
                txtReligion.Text = editemp.Religion;
                txtCertificateNumber.Text = editemp.CedulaNo;
                txtTIN.Text = editemp.TIN;
                txtPagibig.Text = editemp.PAG_IBIG;
                txtSSS.Text = editemp.SSS;
                txtPhilhealth.Text = editemp.PhilHealth;
                txtRCPlace.Text = editemp.CedulaPlace;
                if (!String.IsNullOrEmpty(editemp.CedulaDate))
                    dpRC.SelectedDate = Convert.ToDateTime(editemp.CedulaDate);

                txtCity.Text = editemp.ResidentialAddress;

                cbArea.SelectedValue = editemp.ResidentialAreaID;
                cbArea_SelectionChanged(null, null);
                cbZipcode.SelectedValue = editemp.ResidentialZipCodeID;
                txtTelno.Text = editemp.TelephoneNo;
                txtMobileno.Text = editemp.MobileNo;
                txtLetranEmail.Text = editemp.PrimaryEmail;
                txtAlternateEmail.Text = editemp.SecondaryEmail;
                txtProvAddress.Text = editemp.ProvincialAddress;
                txtProvTelno.Text = editemp.ProvincialTelephoneNo;
                cbProvArea.SelectedValue = editemp.ProvincialAreaID;
                cbArea_SelectionChanged(null, null);
                cbProvZipcode.SelectedValue = editemp.ProvincialZipCodeID;


                GetEmployeeEducation();
                GetEmployeeSeminar();
                GetEmployeeWorkExp();

        }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
}

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            LoadEmployee();
            btnSaveSeminar.IsEnabled = false;
            btnSaveWorkExp.IsEnabled = false;
            btnSaveEducational.IsEnabled = false;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.InitialDirectory = @"\\192.168.252.253\images";
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";


            if (op.ShowDialog() == true)
            {
                txtPic.Text = op.FileName;

                ImageSource imageSource = new BitmapImage(new Uri(txtPic.Text));
                IDpic.Source = imageSource;

            }
        }

        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int x = Convert.ToInt32(cbArea.SelectedValue);
            cbZipcode.ItemsSource = db.Zipcodes.Where(m => m.AreaID == x).ToList();
            cbZipcode.DisplayMemberPath = "ZipCodeName";
            cbZipcode.SelectedValuePath = "ZipCodeID";
        }

        private void cbProvArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int x = Convert.ToInt32(cbProvArea.SelectedValue);
            cbProvZipcode.ItemsSource = db.Zipcodes.Where(m => m.AreaID == x).ToList();
            cbProvZipcode.DisplayMemberPath = "ZipCodeName";
            cbProvZipcode.SelectedValuePath = "ZipCodeID";
        }

        private void btnUPDATE_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                Mouse.OverrideCursor = Cursors.Wait;
                db = new LetranIntegratedSystemEntities();
                Employee emp = db.Employees.Find(EmpID);


                int deptid = Convert.ToInt32(cbDepartment.SelectedValue);
                int facareaid = Convert.ToInt32(cbFacultyArea.SelectedValue);
                int statid = Convert.ToInt32(cbStatus.SelectedValue);
                int desigid = Convert.ToInt32(cbDesignation.SelectedValue);
                int posid = Convert.ToInt32(cbEmployeePosition.SelectedValue);
                int lvlid = Convert.ToInt32(cbLevel.SelectedValue);
                int resareaid = Convert.ToInt32(cbArea.SelectedValue);
                int reszipcode = Convert.ToInt32(cbZipcode.SelectedValue);
                int provareaid = Convert.ToInt32(cbProvArea.SelectedValue);
                int provzipcode = Convert.ToInt32(cbProvZipcode.SelectedValue);
                int emptypeid = Convert.ToInt32(cbEmpType.SelectedValue);

                if (String.IsNullOrEmpty(txtEmpNumber.Text) || String.IsNullOrEmpty(cbDepartment.Text) || String.IsNullOrEmpty(cbStatus.Text) ||
                  String.IsNullOrEmpty(cbDesignation.Text) || String.IsNullOrEmpty(cbEmployeePosition.Text) || String.IsNullOrEmpty(dpHired.Text) ||
                  String.IsNullOrEmpty(cbSalutation.Text) || String.IsNullOrEmpty(txtLastName.Text) || String.IsNullOrEmpty(txtFirstName.Text) ||
                  String.IsNullOrEmpty(txtCitizenship.Text) || String.IsNullOrEmpty(cbStatus.Text) || String.IsNullOrEmpty(txtReligion.Text) || String.IsNullOrEmpty(txtCity.Text))
                {
                    MessageBox.Show("Please fill up necessary fields.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Mouse.OverrideCursor = null;
                    return;
                }
                emp.EmployeeNo = txtEmpNumber.Text;
                emp.Title = cbSalutation.Text;
                emp.LastName = txtLastName.Text;
                emp.FirstName = txtFirstName.Text;
                emp.MiddleName = String.IsNullOrEmpty(txtMiddleName.Text) ? " " : txtMiddleName.Text;
                emp.Nickname = txtNickName.Text;
                if (deptid != 0)
                    emp.EmployeeDepartmentID = deptid;
                if (emptypeid != 0)
                {
                    emp.EmploymentTypeID = emptypeid;
                }
                if (facareaid != 0)
                    emp.FacultyAreaID = facareaid;
                if (statid != 0)
                    emp.EmployeeStatusID = statid;
                if (desigid != 0)
                    emp.EmployeeDesignation = desigid;
                if (posid != 0)
                    emp.EmployeePositionID = posid;
                if (lvlid != 0)
                    emp.EmployeeLevelID = lvlid;
                if (!String.IsNullOrEmpty(dpHired.Text))
                    emp.DateHired = Convert.ToDateTime(dpHired.SelectedDate);
                if (!String.IsNullOrEmpty(dpPermanency.Text))
                    emp.DatePermanency = Convert.ToDateTime(dpPermanency.SelectedDate);
                if (!String.IsNullOrEmpty(dpEndo.Text))
                    emp.DateEndContract = Convert.ToDateTime(dpEndo.SelectedDate);
                emp.ReasonForLeaving = txtReason.Text;
                emp.OtherReasonForLeaving = txtReason.Text;
                if (!String.IsNullOrEmpty(dpResigned.Text))
                    emp.DateResigned = Convert.ToDateTime(dpResigned.SelectedDate);
                if (!String.IsNullOrEmpty(dpRetirement.Text))
                    emp.DateRetired = Convert.ToDateTime(dpRetirement.SelectedDate);
                emp.ResidentialAddress = txtCity.Text;
                if (resareaid != 0)
                    emp.ResidentialAreaID = resareaid;
                if (reszipcode != 0)
                    emp.ResidentialZipCodeID = reszipcode;
                emp.ProvincialAddress = txtProvAddress.Text;
                if (provareaid != 0)
                    emp.ProvincialAreaID = provareaid;
                if (provzipcode != 0)
                    emp.ProvincialZipCodeID = provzipcode;
                emp.TelephoneNo = txtTelno.Text;
                emp.ProvincialTelephoneNo = txtProvTelno.Text;
                emp.MobileNo = txtMobileno.Text;
                emp.PrimaryEmail = txtLetranEmail.Text;
                emp.SecondaryEmail = txtAlternateEmail.Text;
                emp.Sex = rbMale.IsChecked == true ? "M" : "F";
                if (!String.IsNullOrEmpty(dpBirthday.Text))
                    emp.Birthday = Convert.ToDateTime(dpBirthday.SelectedDate);
                emp.Birthplace = txtPlaceBirth.Text;
                emp.CivilStatus = cbCivilStatus.Text.Substring(0, 1);
                emp.Nationality = txtCitizenship.Text;
                emp.Religion = txtReligion.Text;
                emp.TaxStatus = cbTax.Text;
                emp.TIN = txtTIN.Text;
                emp.SSS = txtSSS.Text;
                emp.PAG_IBIG = txtPagibig.Text;
                emp.PhilHealth = txtPhilhealth.Text;
                emp.CedulaNo = txtCertificateNumber.Text;
                if (!String.IsNullOrEmpty(dpRC.Text))
                    emp.CedulaDate = Convert.ToDateTime(dpRC.SelectedDate).ToLongDateString();
                emp.CedulaPlace = txtRCPlace.Text;
                emp.DateEncoded = DateTime.Now;
                emp.Archive = chkActive.IsChecked == true ? false : true;
                if (!String.IsNullOrEmpty(txtPic.Text))
                {
                    emp.Photo = File.ReadAllBytes(txtPic.Text);
                }
                db.SaveChanges();
                MessageBox.Show("Update Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Mouse.OverrideCursor = null;
                this.Close();

            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Something went wrong!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void GetEmployeeEducation()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    //Educational Information
                    var editemp = db.Employees.Find(EmpID);
                    EEList = new List<EmployeeEducationList>();

                    var empeduc = db.EmployeeEducations.Where(m => m.EmployeeID == editemp.EmployeeID).ToList();

                    foreach (var x in empeduc)
                    {
                        EmployeeEducationList ee = new EmployeeEducationList();
                        ee.EducationType = x.EducationType;
                        ee.Award = x.Awards;
                        ee.Degree = x.Degree;
                        ee.Graduation = x.GraduationDate;
                        ee.Institution = x.Institution;
                        ee.id = x.EmployeeEducationID;
                        EEList.Add(ee);
                    }
                    dgEmpEduc.ItemsSource = EEList;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }
        private void GetEmployeeWorkExp()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {

                    //Work Experience

                    EWList = new List<EmployeeWorkList>();
                    var editemp = db.Employees.Find(EmpID);
                    var empwork = db.EmployeeWorks.Where(m => m.EmployeeID == editemp.EmployeeID).ToList();

                    foreach (var x in empwork)
                    {
                        EmployeeWorkList ew = new EmployeeWorkList();
                        ew.Company = x.Company;
                        ew.InclusiveDate = x.InclusiveDate;
                        ew.LastSalary = x.Salary;
                        ew.Position = x.WorkPosition;
                        ew.Reason = x.ReasonLeaving;
                        ew.id = x.EmployeeWorkID;
                        EWList.Add(ew);
                    }
                    dgWorkExp.ItemsSource = EWList;


                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
        private void GetEmployeeSeminar()
        {
            //Seminars
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    ESList = new List<EmployeeSeminarList>();
                    var editemp = db.Employees.Find(EmpID);
                    var empsem = db.EmployeeSeminars.Where(m => m.EmployeeID == editemp.EmployeeID).ToList();

                    foreach (var x in empsem)
                    {
                        EmployeeSeminarList es = new EmployeeSeminarList();
                        es.Seminar = x.Title;
                        es.Venue = x.Venue;
                        //x.DateStart.Value.ToString("MMMM dd,yyyy") + " - " + x.DateEnd.Value.ToString("MMMM dd,yyyy");
                        es.InclusiveDate = x.InclusiveDate;
                        es.id = x.EmployeeSeminarID;
                        ESList.Add(es);
                    }
                    dgSeminars.ItemsSource = ESList;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
           
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public class EmployeeEducationList
        {
            public string EducationType { get; set; }
            public string Institution { get; set; }
            public string Degree { get; set; }
            public string Graduation { get; set; }
            public string Award { get; set; }
            public int id { get; set; }
        }

        public class EmployeeSeminarList
        {
            public string Seminar { get; set; }
            public string Venue { get; set; }
            public string InclusiveDate { get; set; }
            public int id { get; set; }
        }

        public class EmployeeWorkList
        {
            public string Company { get; set; }
            public string Position { get; set; }
            public string InclusiveDate { get; set; }
            public string LastSalary { get; set; }
            public string Reason { get; set; }
            public int id { get; set; }
        }


        public void ClearEducationInfo()
        {
            txtEducationType.Text = "";
            txtInstitution.Text = "";
            txtDegree.Text = "";
            txtGraduationDate.Text = "";
            txtAwards.Text = "";
        }

        public void ClearWorkExp()
        {
            txtCompany.Text = "";
            txtPosition.Text = "";
            txtWorkExpInclusivedate.Text = "";
            txtSalary.Text = "";
            txtReasonForLeaving.Text = "";
        }

        public void ClearSeminar()
        {
            txtSeminar.Text = "";
            txtVenue.Text = "";
            txtSeminarInclusiveDate.Text = "";
        }

        private void btnAddWorkExp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (txtCompany.Text != "" && txtPosition.Text != "")
                    {
                        EmployeeWork ew = new EmployeeWork();
                        ew.EmployeeID = EmpID;
                        ew.Company = txtCompany.Text;
                        ew.WorkPosition = txtPosition.Text;
                        ew.InclusiveDate = txtWorkExpInclusivedate.Text;
                        ew.Salary = txtSalary.Text;
                        ew.ReasonLeaving = txtReasonForLeaving.Text;
                        db.EmployeeWorks.Add(ew);
                        db.SaveChanges();
                        MessageBox.Show("Add Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearWorkExp();
                        GetEmployeeWorkExp();

                    }
                    else
                    {
                        MessageBox.Show("Fill the required fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void btnAddSeminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (txtSeminar.Text != "" && txtVenue.Text != "" && txtSeminarInclusiveDate.Text != "")
                    {

                        EmployeeSeminar es = new EmployeeSeminar();
                        es.EmployeeID = EmpID;
                        es.Title = txtSeminar.Text;
                        es.Venue = txtVenue.Text;
                        es.InclusiveDate = txtSeminarInclusiveDate.Text;
                        db.EmployeeSeminars.Add(es);
                        db.SaveChanges();
                        MessageBox.Show("Add Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearSeminar();
                        GetEmployeeSeminar();
                    }
                    else
                    {
                        MessageBox.Show("Fill the required fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void btnAddEducational_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (txtEducationType.Text != "" && txtInstitution.Text != "" && txtDegree.Text != "" && txtGraduationDate.Text != "")
                    {

                        EmployeeEducation ed = new EmployeeEducation();
                        ed.EmployeeID = EmpID;
                        ed.EducationType = txtEducationType.Text;
                        ed.Institution = txtInstitution.Text;
                        ed.Degree = txtDegree.Text;
                        ed.Awards = txtAwards.Text;
                        ed.GraduationDate = txtGraduationDate.Text;
                        db.EmployeeEducations.Add(ed);
                        db.SaveChanges();
                        MessageBox.Show("Add Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearEducationInfo();
                        GetEmployeeEducation();
                    }
                    else
                    {
                        MessageBox.Show("Fill the required fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void dgWorkExp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (EmployeeWorkList)(dgWorkExp.SelectedItem);
            if (dgWorkExp.SelectedItem != null)
            {
                btnSaveWorkExp.IsEnabled = true;
                btnAddWorkExp.IsEnabled = false;
                btnRemoveWorkExp.IsEnabled = true;
                txtCompany.Text = selectedItem.Company;
                txtPosition.Text = selectedItem.Position;
                txtWorkExpInclusivedate.Text = selectedItem.InclusiveDate;
                txtSalary.Text = selectedItem.LastSalary;
                txtReasonForLeaving.Text = selectedItem.Reason;

            }
        }

        private void dgSeminars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (EmployeeSeminarList)(dgSeminars.SelectedItem);
            if (dgSeminars.SelectedItem != null)
            {
                btnSaveSeminar.IsEnabled = true;
                btnAddSeminar.IsEnabled = false;
                btnDeleteSeminar.IsEnabled = true;
                txtSeminar.Text = selectedItem.Seminar;
                txtVenue.Text = selectedItem.Venue;
                txtSeminarInclusiveDate.Text = selectedItem.InclusiveDate;
               
            }
        }

        private void dgEmpEduc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (EmployeeEducationList)(dgEmpEduc.SelectedItem);
            if (dgEmpEduc.SelectedItem != null)
            {
                btnSaveEducational.IsEnabled = true;
                btnAddEducational.IsEnabled = false;
                btbRemoveEducational.IsEnabled = true;
                txtEducationType.Text = selectedItem.EducationType;
                txtInstitution.Text = selectedItem.Institution;
                txtDegree.Text = selectedItem.Degree;
                txtGraduationDate.Text = selectedItem.Graduation;
                txtAwards.Text = selectedItem.Award;
            }


      }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveEducational_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {                  
                    var selectedItem = (EmployeeEducationList)(dgEmpEduc.SelectedItem);
                    if (selectedItem != null)
                    {
                        var empeduc = db.EmployeeEducations.Where(m => m.EmployeeEducationID == selectedItem.id).FirstOrDefault();
                        empeduc.EducationType = txtEducationType.Text;
                        empeduc.Institution = txtInstitution.Text;
                        empeduc.Degree = txtDegree.Text;
                        empeduc.GraduationDate = txtGraduationDate.Text;
                        empeduc.Awards = txtAwards.Text;

                        db.SaveChanges();
                        GetEmployeeEducation();
                        btnSaveEducational.IsEnabled = false;
                        MessageBox.Show("Update Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearEducationInfo();
                    }
                    else
                    {
                        MessageBox.Show("Select a record", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

            }


        }

        private void btnSaveWorkExp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
              {
      
                    var selectedItem = (EmployeeWorkList)(dgWorkExp.SelectedItem);
                    var empWork = db.EmployeeWorks.Where(m => m.EmployeeWorkID == selectedItem.id).FirstOrDefault();
                    if (empWork != null)
                    {
                       
                        empWork.Company = txtCompany.Text;
                        empWork.WorkPosition = txtPosition.Text;
                        empWork.InclusiveDate = txtWorkExpInclusivedate.Text;
                        empWork.Salary = txtSalary.Text;
                        empWork.ReasonLeaving = txtReasonForLeaving.Text;

                        db.SaveChanges();
                        GetEmployeeWorkExp();
                        btnSaveWorkExp.IsEnabled = false;
                        MessageBox.Show("Update Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearWorkExp();
                    }
                    else
                    {
                        MessageBox.Show("Select a record", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void btnSaveSeminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {

                    var selectedItem = (EmployeeSeminarList)(dgSeminars.SelectedItem);
                    if (selectedItem != null)
                    {
                        var empSeminar = db.EmployeeSeminars.Where(m => m.EmployeeSeminarID == selectedItem.id).FirstOrDefault();
                        empSeminar.Title = txtSeminar.Text;
                        empSeminar.Venue = txtVenue.Text;
                        empSeminar.InclusiveDate = txtSeminarInclusiveDate.Text;
               
                        db.SaveChanges();
                        GetEmployeeSeminar();
                        btnSaveSeminar.IsEnabled = false;
                        MessageBox.Show("Update Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearSeminar();
                    }
                    else
                    {
                        MessageBox.Show("Select a record", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
                
        private void bntClearEducational_Click(object sender, RoutedEventArgs e)
        {
            ClearEducationInfo();
            btnSaveEducational.IsEnabled = false;
            btnAddEducational.IsEnabled = true;
        
        }

        private void btnClearWorkExp_Click(object sender, RoutedEventArgs e)
        {
            ClearWorkExp();
            btnSaveWorkExp.IsEnabled = false;
            btnAddWorkExp.IsEnabled = true;
        }

        private void btnClearSeminar_Click(object sender, RoutedEventArgs e)
        {
            ClearSeminar();
            btnSaveSeminar.IsEnabled = false;
            btnAddSeminar.IsEnabled = true;
        }

        private void btbRemoveEducational_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var selectedItem = (EmployeeEducationList)(dgEmpEduc.SelectedItem);
                    var empeduc = db.EmployeeEducations.Where(m => m.EmployeeEducationID == selectedItem.id).FirstOrDefault();
                    if (empeduc != null)
                    {
                        db.EmployeeEducations.Remove(empeduc);
                        db.SaveChanges();
                        GetEmployeeEducation();
                        ClearEducationInfo();                
                        MessageBox.Show("Remove Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                       
                    }
                    else
                    {
                        MessageBox.Show("No record found.", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                 
                    btbRemoveEducational.IsEnabled = false;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRemoveWorkExp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var selectedItem = (EmployeeWorkList)(dgWorkExp.SelectedItem);
                    if (selectedItem != null)
                    {
                        var empworkexp = db.EmployeeWorks.Where(m => m.EmployeeWorkID == selectedItem.id).FirstOrDefault();
                        db.EmployeeWorks.Remove(empworkexp);
                        db.SaveChanges();
                        GetEmployeeWorkExp();
                        ClearWorkExp();
                        MessageBox.Show("Remove Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                   
                    }
                    else
                    {
                        MessageBox.Show("No record found.", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                   
                    btnRemoveWorkExp.IsEnabled = false;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteSeminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var selectedItem = (EmployeeSeminarList)(dgSeminars.SelectedItem);
                    var empseminar = db.EmployeeSeminars.Where(m => m.EmployeeSeminarID == selectedItem.id).FirstOrDefault();
                    if (empseminar != null)
                    {
                        db.EmployeeSeminars.Remove(empseminar);
                        db.SaveChanges();
                        GetEmployeeSeminar();
                        ClearSeminar();
                        MessageBox.Show("Remove Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                      
                    }
                    else
                    {
                        MessageBox.Show("No record found.", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                  
                    btnDeleteSeminar.IsEnabled = false;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void chkActive_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void chkActive_Unchecked(object sender, RoutedEventArgs e)
        {
        }
    }
}
