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
using MahApps.Metro.SimpleChildWindow;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using HRiS.Model;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        System.Windows.Threading.DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        public MainMenu()
        {
            this.Cursor = Cursors.AppStarting;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
            Status1.Content = "Today is " + DateTime.Now.ToLongDateString();
            empname.Text = App.EmployeeName.ToUpper();

            dispatcherTimer1.Tick += new EventHandler(dispatcherTimer_Tick1);
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer1.Start();
        }

        private void dispatcherTimer_Tick1(object sender, EventArgs e)
        {
            thedate.Content = DateTime.Now.ToLongDateString();
            time.Content = DateTime.Now.ToString("h:mm:ss tt");
        }
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyboardStatus();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CheckKeyboardStatus();
        }

        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow x = new MainWindow();
            x.Show();
            this.Close();
        }
        public void CheckKeyboardStatus()
        {
            Status1.Content = Keyboard.IsKeyToggled(Key.NumLock) ? "NUM" : "";
            Status3.Content = Keyboard.IsKeyToggled(Key.CapsLock) ? "CAPS" : "";
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            CWChangePass.IsOpen = true;
            this.Cursor = Cursors.Arrow;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            db = new LetranIntegratedSystemEntities();
            var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();

            AspNetUser search = db.AspNetUsers.Where(m => m.UserName == App.EmployeeNumber).FirstOrDefault();
            string compare = passwordHasher.VerifyHashedPassword(search.PasswordHash, pbCurrent.Password).ToString();
            if (compare != "Success")
            {
                MessageBox.Show("Incorrect current password.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (pbNew.Password != pbVerify.Password)
            {
                MessageBox.Show("Verify password does not match.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (String.IsNullOrEmpty(pbCurrent.Password) || String.IsNullOrEmpty(pbNew.Password) || String.IsNullOrEmpty(pbVerify.Password))
            {
                MessageBox.Show("Password cannot be empty.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            search.PasswordHash = passwordHasher.HashPassword(pbVerify.Password);
            db.SaveChanges();
            MessageBox.Show("Password has been changed.", "Successful", MessageBoxButton.OK);
            pbCurrent.Clear();
            pbNew.Clear();
            pbVerify.Clear();
            this.Cursor = Cursors.Arrow;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            CWChangePass.Close();
            this.Cursor = Cursors.Arrow;
        }

        private void TileEmployeeStatistics_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeStatistics x = new EmployeeStatistics();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void TileEmployeeList_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Employees x = new Employees();
              x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void TileLeaveManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveMenu x = new LeaveMenu();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }

        private void TileAttendance_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AttendanceMenu x = new AttendanceMenu();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }

        private void TileReport_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ReportMenu x = new ReportMenu();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void TileSisEvaluation_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            sisViewingMain x = new sisViewingMain();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void TileMaintenance_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            MaintenanceMenu x = new MaintenanceMenu();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miFileLeave_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AddLeave x = new AddLeave();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveOP_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveOnProcess x = new LeaveOnProcess();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveA_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveApproved x = new LeaveApproved();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveD_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveDisapproved x = new LeaveDisapproved();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveBal_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveBalance x = new LeaveBalance();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveUsage_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveUsage x = new LeaveUsage();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveTransaction_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveTransaction x = new LeaveTransaction();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveHistory x = new LeaveHistory();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miTurn_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AttendanceTurnstile x = new AttendanceTurnstile();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }

        private void miBio_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AttendanceBiometric x = new AttendanceBiometric();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miEmployeeStat_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByStatus x = new EmployeeByStatus();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miEmployeeDept_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByDepartment x = new EmployeeByDepartment();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miBirthday_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByBirthday x = new EmployeeByBirthday();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miServiceAwardee_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeServiceAwardee x = new EmployeeServiceAwardee();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miEmployeeAge_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByAge x = new EmployeeByAge();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miEmployeeGender_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByGender x = new EmployeeByGender();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miJobPos_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeJobPosition x = new EmployeeJobPosition();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miEmpDept_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AcademicDepartmentModule x = new AcademicDepartmentModule();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miEmpRank_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeLevelRank x = new EmployeeLevelRank();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miJobApplication_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeOnlineApplication x = new EmployeeOnlineApplication();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveCancellation x = new LeaveCancellation();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miChangeVL_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ChangeVacationLeave x = new ChangeVacationLeave();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miNotice_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            NoticeOfAbscence x = new NoticeOfAbscence();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miOTReq_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Overtime x = new Overtime();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AccountManagement x = new AccountManagement();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miCertificate_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            CertificationOfEmployment x = new CertificationOfEmployment();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miOffCampus_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            OffCampusActivity x = new OffCampusActivity();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }

        private void miPostTraining_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            PostTraining x = new PostTraining();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }

        private void miMakeUp_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            MakeUpClass x = new MakeUpClass();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miSubstitution_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            SubstituteClass x = new SubstituteClass();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miLeaveType_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveTypeModule x = new LeaveTypeModule();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void TileFeedback_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeAnnouncementFeedback x = new EmployeeAnnouncementFeedback();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void mLogsData_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            BiometricsLogs x = new BiometricsLogs();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void mEmployeeList_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //EmployeeList x = new EmployeeList();
            //x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void mEmployeeSchedule_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeSchedule x = new EmployeeSchedule();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miEmpShifts_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeShift x = new EmployeeShift();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miEmployeesDTR_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            PrintDTR x = new PrintDTR();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }

        private void mEmployeeAbsent_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeAbsent x = new EmployeeAbsent();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void miAddLeave_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AddEmployeeLeave x = new AddEmployeeLeave();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void TileEmployeeDTR_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            PrintDTR x = new PrintDTR();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void mEmployeeHistoryList_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeesHistory x = new EmployeesHistory();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void TileOT_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AddEmployeeOvertime x = new AddEmployeeOvertime();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void mOverTime_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Overtime x = new Overtime();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }

        private void TileSIS_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            PrintSISResult x = new PrintSISResult();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void TileFileOffCampusAct_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AddOffCampusActivity x = new AddOffCampusActivity();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }

        private void TileFileLeave_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AddEmployeeLeave x = new AddEmployeeLeave();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }



        //private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        //{
        //    // set the content
        //    this.HamburgerMenuControl.Content = e.ClickedItem;

        //}
    }
}
