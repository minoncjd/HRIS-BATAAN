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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;
using HRiS.Model;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using System.Data;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for AddNewEmployee.xaml
    /// </summary>
    public partial class AddNewEmployee : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<HRiSClass.EmployeeAddWorkList> ew = new List<HRiSClass.EmployeeAddWorkList>();
        List<HRiSClass.EmployeeAddEducationList> ee = new List<HRiSClass.EmployeeAddEducationList>();
        List<HRiSClass.EmployeeAddSeminarList> es = new List<HRiSClass.EmployeeAddSeminarList>();

        public AddNewEmployee()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
            chkActive.IsChecked = true;
            btnRemoveEducational.IsEnabled = false;
            btnRemoveSeminar.IsEnabled = false;
            btnRemoveWorkExp.IsEnabled = false;
            db = new LetranIntegratedSystemEntities();

            string year1 = DateTime.Now.Year.ToString().Substring(0, 1);
            string year2 = DateTime.Now.Year.ToString().Substring(2, 2);
            string year = year1 + year2;
            string month = Convert.ToInt32(DateTime.Now.Month).ToString("00");
            string day = Convert.ToInt64(DateTime.Now.Day).ToString("00");

            var searchno = db.Employees.Where(m => m.EmployeeNo.StartsWith(year + month + day)).OrderByDescending(m => m.EmployeeNo).FirstOrDefault();

            if (searchno != null)
            {
                txtEmpNumber.Text = (Convert.ToInt32(searchno.EmployeeNo) + 1).ToString();
            }
            else
            {
                txtEmpNumber.Text = year + month + day + "01";
            }


          
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

                cbEmpType.ItemsSource = db.EmploymentTypes.OrderBy(m => m.EmploymentTypeID).ToList();
                cbEmpType.DisplayMemberPath = "EmploymentTypeName";
                cbEmpType.SelectedValuePath = "EmploymentTypeID";

                //cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaDepartmentName).ToList();
                cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaDepartmentName).ToList();
                cbDepartment.DisplayMemberPath = "AcaDepartmentName";
                cbDepartment.SelectedValuePath = "AcaDeptID";

                cbStatus.ItemsSource = db.EmployeeStatus.Where(m => m.IsActive == true).OrderBy(m => m.EmployeeStatusName).ToList();
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


        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                db = new LetranIntegratedSystemEntities();
                Employee emp = new Employee();
                AspNetUser aspuser = new AspNetUser();
                AspNetUserRole asproleuser = new AspNetUserRole();
                string roleid = "";


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
                    MessageBox.Show("Please fill up necessary fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (db.Employees.Where(m => m.EmployeeNo == txtEmpNumber.Text).FirstOrDefault() != null)
                {
                    MessageBox.Show("Employee number already exists!", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (db.Employees.Where(m => m.FirstName.Trim().ToUpper() == txtFirstName.Text.Trim().ToUpper() && m.LastName.Trim().ToUpper() == txtLastName.Text.Trim().ToUpper() && m.MiddleName.Trim().ToUpper() == (String.IsNullOrEmpty(txtMiddleName.Text) ? " " : txtMiddleName.Text.Trim().ToUpper())).FirstOrDefault() != null)
                {
                    MessageBox.Show("Employee already exists!!", "System Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                    Mouse.OverrideCursor = null;
                    return;
                }
                emp.EmployeeNo = txtEmpNumber.Text;
                emp.Title = cbSalutation.Text;
                emp.LastName = txtLastName.Text;
                emp.FirstName = txtFirstName.Text;
                emp.MiddleName = String.IsNullOrEmpty(txtMiddleName.Text) ? " " : txtMiddleName.Text;
                emp.Nickname = txtNickName.Text;
                if (emptypeid != 0)
                {
                    emp.EmploymentTypeID = emptypeid;
                }
                if (deptid != 0)
                    emp.EmployeeDepartmentID = deptid;
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
                var newemp = db.Employees.Add(emp);
                db.SaveChanges();

                if (newemp.EmployeeDesignation == 1 || newemp.EmployeeDepartmentID == 19)
                {
                    roleid = "2";
                }
                if (newemp.EmployeeDesignation == 2)
                {
                    roleid = "5";
                }
                if (newemp.EmployeeDepartmentID == 34)
                {
                    roleid = "9";
                }
                else if (newemp.EmployeeDepartmentID == 35)
                {
                    roleid = "10";
                }
                else if (newemp.EmployeeDepartmentID == 18)
                {
                    roleid = "8";
                }
                else
                {
                    roleid = "4";
                }

                if (db.AspNetUsers.Where(m => m.UserName == newemp.EmployeeNo).FirstOrDefault() != null)
                {
                    MessageBox.Show("User account for this person already exists!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (db.AspNetUsers.Where(m => m.Email == newemp.PrimaryEmail && m.Email != "").FirstOrDefault() != null)
                {
                    MessageBox.Show("Email already exists!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (String.IsNullOrEmpty(roleid))
                    {
                        MessageBox.Show("Role is not specified", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        aspuser.Id = Guid.NewGuid().ToString();
                        aspuser.UserName = newemp.EmployeeNo;
                        aspuser.Email = newemp.PrimaryEmail;
                        aspuser.EmailConfirmed = true;
                        aspuser.PhoneNumberConfirmed = false;
                        aspuser.TwoFactorEnabled = false;
                        aspuser.LockoutEnabled = true;
                        aspuser.AccessFailedCount = 0;
                        aspuser.SecurityStamp = Guid.NewGuid().ToString();
                        var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
                        aspuser.PasswordHash = passwordHasher.HashPassword("letran1620");
                        var adduser = db.AspNetUsers.Add(aspuser);
                        asproleuser.UserId = adduser.Id;
                        asproleuser.RoleId = roleid;
                        db.AspNetUserRoles.Add(asproleuser);
                    }
                }

                if (dgEmpEduc.Items.Count > 0)
                {
                   
                    foreach (HRiSClass.EmployeeAddEducationList x in dgEmpEduc.ItemsSource)
                    {
                        EmployeeEducation ee = new EmployeeEducation();
                        ee.EmployeeID = newemp.EmployeeID;
                        ee.EducationType = x.EducationType;
                        ee.Institution = x.Institution;
                        ee.Degree = x.Degree;
                        ee.GraduationDate = x.Graduation;
                        ee.Awards = x.Award;
                        db.EmployeeEducations.Add(ee);
                    }
                }

                //Work Experience
                if (dgWorkExp.Items.Count > 0)
                {
                    foreach (HRiSClass.EmployeeAddWorkList x in dgWorkExp.ItemsSource)
                    {
                        EmployeeWork ew = new EmployeeWork();
                        ew.EmployeeID = newemp.EmployeeID;
                        ew.Company = x.Company;
                        ew.WorkPosition = x.Position;
                        ew.InclusiveDate = x.InclusiveDate;
                        ew.Salary = x.LastSalary;
                        ew.ReasonLeaving = x.Reason;
                        db.EmployeeWorks.Add(ew);
                    }
                }

                //Semniars
                if (dgSeminars.Items.Count > 0)
                {
                    foreach (HRiSClass.EmployeeAddSeminarList x in dgSeminars.ItemsSource)
                    {
                        EmployeeSeminar es = new EmployeeSeminar();
                        es.EmployeeID = newemp.EmployeeID;
                        es.Title = x.Seminar;
                        es.InclusiveDate = x.InclusiveDate;
                        es.Venue = x.Venue;
                        db.EmployeeSeminars.Add(es);
                    }
                }

                db.SaveChanges();
                TextClear();
                MessageBox.Show("Successfully Addedd.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        public void TextClear()
        {
            txtEmpNumber.Text = "";
            cbDepartment.Text = "";
            cbStatus.Text = "";
            cbEmpType.Text = "";
            cbDesignation.Text = "";
            cbFacultyArea.Text = "";
            cbEmployeePosition.Text = "";
            cbLevel.Text = "";
            cbTax.Text = "";
            dpHired.SelectedDate = DateTime.Now;
            dpPermanency.Text = "";
            dpEndo.Text = "";
            dpResigned.Text = "";
            dpRetirement.Text = "";
            txtReason.Text = "";

            cbSalutation.Text = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtNickName.Text = "";
            dpBirthday.Text = "";
            txtPlaceBirth.Text = "";
            txtCitizenship.Text = "";
            cbCivilStatus.Text = "";
            txtReligion.Text = "";
            txtCertificateNumber.Text = "";
            txtTIN.Text = "";
            txtPagibig.Text = "";
            txtSSS.Text = "";
            txtPhilhealth.Text = "";
            txtRCPlace.Text = "";
            dpRC.Text = "";

            txtCity.Text = "";
            cbArea.Text = "";
            cbZipcode.Text = "";
            txtTelno.Text = "";
            txtMobileno.Text = "";
            txtLetranEmail.Text = "";
            txtAlternateEmail.Text = "";
            txtProvAddress.Text = "";
            txtProvTelno.Text = "";
            cbProvArea.Text = "";
            cbProvZipcode.Text = "";

            dgEmpEduc.ItemsSource = null;
            dgSeminars.ItemsSource = null;
            dgWorkExp.ItemsSource = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbEducationalLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       

      

        private void xx_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWorkExp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddEducational_Click(object sender, RoutedEventArgs e)
        {
            if (txtEducationType.Text != "" && txtInstitution.Text != "" && txtDegree.Text != "" && txtGraduationDate.Text != "")
            {
                ee.Add(new HRiSClass.EmployeeAddEducationList
                {
                    EducationType = txtEducationType.Text,
                    Institution = txtInstitution.Text,
                    Degree = txtDegree.Text,
                    Graduation = txtGraduationDate.Text,
                    Award = txtAwards.Text
                });

                dgEmpEduc.ItemsSource = ee;
                dgEmpEduc.Items.Refresh();
                ClearEducationInfo();
            }
            else
            {
                MessageBox.Show("Fill the required fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
                          
        }

        private void btnAddWorkExp_Click(object sender, RoutedEventArgs e)
        {
            if (txtCompany.Text != "" && txtPosition.Text != "")
            {

                ew.Add(new HRiSClass.EmployeeAddWorkList
                {
                    Company = txtCompany.Text,
                    Position = txtPosition.Text,
                    InclusiveDate = txtWorkExpInclusivedate.Text,
                    LastSalary = txtLastSalary.Text,
                    Reason = txtReasonForLeaving.Text
                });

                dgWorkExp.ItemsSource = ew;
                dgWorkExp.Items.Refresh();               
                ClearWorkExp();

            }
            else
            {
                MessageBox.Show("Fill the required fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
          

        }

        private void btnAddSeminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtSeminar.Text != "" && txtVenue.Text != "" && txtSeminarInclusiveDate.Text != "")
            {

                es.Add(new HRiSClass.EmployeeAddSeminarList
                {
                    Seminar = txtSeminar.Text,
                    Venue = txtVenue.Text,
                    InclusiveDate = txtSeminarInclusiveDate.Text
                });

                dgSeminars.ItemsSource = es;
                dgSeminars.Items.Refresh();
                ClearSeminar();
            }
            else
            {
                MessageBox.Show("Fill the required fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            }


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
            txtLastSalary.Text = "";
            txtReasonForLeaving.Text = "";
        }

        public void ClearSeminar()
        {
            txtSeminar.Text = "";
            txtVenue.Text = "";
            txtSeminarInclusiveDate.Text = "";
        }

        private void btnRemoveWorkExp_Click(object sender, RoutedEventArgs e)
        {
            ew.RemoveAt(dgWorkExp.SelectedIndex);
            dgWorkExp.ItemsSource = ew;
            dgWorkExp.Items.Refresh();
            btnRemoveWorkExp.IsEnabled = false;

        }

        private void btnRemoveEducational_Click(object sender, RoutedEventArgs e)
        {

            ee.RemoveAt(dgEmpEduc.SelectedIndex);
            dgEmpEduc.ItemsSource = ee;
            dgEmpEduc.Items.Refresh();
            btnRemoveEducational.IsEnabled = false;
        }

        private void btnRemoveSeminar_Click(object sender, RoutedEventArgs e)
        {

            es.RemoveAt(dgSeminars.SelectedIndex);
            dgSeminars.ItemsSource = es;
            dgSeminars.Items.Refresh();
            btnRemoveSeminar.IsEnabled = false;    
      
        }

        private void dgEmpEduc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemoveEducational.IsEnabled = true;
        }

        private void dgWorkExp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemoveWorkExp.IsEnabled = true;
        }

        private void dgSeminars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemoveSeminar.IsEnabled = true;
        }
    }
}
