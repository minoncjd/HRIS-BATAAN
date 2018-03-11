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
using System.Globalization;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for AttendanceBiometric.xaml
    /// </summary>
    public partial class AttendanceBiometric : MetroWindow
    {

        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();

        List<EmpCombo> EList = new List<EmpCombo>();
        List<Month> MList = new List<Month>();
        List<BiometricList> BList = new List<BiometricList>();

        softrakEntities sof = new softrakEntities();


        public AttendanceBiometric()
        {
            InitializeComponent();        
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
      
        }

        public void LoadComboBox()
        {
            try
            {
                EList = new List<EmpCombo>();
                MList = new List<Month>();
                db = new LetranIntegratedSystemEntities();

                var emp = db.Employees.Where(m => m.Archive == false).ToList();

                foreach (var i in emp)
                {
                    EmpCombo ec = new EmpCombo();
                    ec.EmployeeID = i.EmployeeID;
                    ec.EmployeeNumber = i.EmployeeNo;
                    ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                    EList.Add(ec);
                }

                cbEmployee.ItemsSource = EList.OrderBy(m => m.EmployeeName);
                cbEmployee.DisplayMemberPath = "EmployeeName";
                cbEmployee.SelectedValuePath = "EmployeeNumber";           

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                if (sdate.SelectedDate != null && edate.SelectedDate != null && cbEmployee.SelectedItem != null)
                {
                    GetBiometrics();
                }
                else
                {
                    MessageBox.Show("Employee and date fields cannot boe empty", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
           
            
        }
 
        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public void GetAllBiometrics()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                sof = new softrakEntities();
                BList = new List<BiometricList>();


                if (!String.IsNullOrEmpty(cbEmployee.Text))
                {

                    var biometricLogData = (from a in db.BiometricsLogDatas
                                            join b in db.Employees on a.EMPN equals b.bioid
                                            select new { a.Id, b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), a.DTIME, a.MODE }).ToList();

                    foreach (var x in biometricLogData)
                    {
                        BiometricList bl = new BiometricList();
                        bl.AttendanceID = x.Id;
                        bl.EmployeeNumber = x.EmployeeNo;
                        bl.EmployeeName = x.EmployeeName;
                        bl.BiometricDate = x.DTIME.Date;
                        bl.DayOfWeek = x.DTIME.Date.DayOfWeek.ToString().ToUpper();
                        bl.Month = x.DTIME.Date.ToString("MMMM").ToUpper();


                        ////bl.BiometricTime = x.DTIME.
                        ;
                        //if (x.MODE == "I")
                        //{
                        //    bl.Type = "IN";
                        //}
                        //else if (x.MODE == "BO")
                        //{
                        //    bl.Type = "BREAK OUT";
                        //}
                        //else if (x.MODE == "BI")
                        //{
                        //    bl.Type = "BREAK IN";
                        //}
                        //else if (x.MODE == "O")
                        //{
                        //    bl.Type = "OUT";
                        //}


                        BList.Add(bl);
                    }
                   
                    dgTurnstile.ItemsSource = BList.OrderByDescending(m => m.BiometricDate);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
           
        public void GetBiometrics()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                sof = new softrakEntities();
                BList = new List<BiometricList>();


                if (!String.IsNullOrEmpty(cbEmployee.Text))
                {
                    string empno = Convert.ToString(cbEmployee.SelectedValue);
                
                    DateTime startdate = Convert.ToDateTime(sdate.SelectedDate);
                    DateTime enddate = Convert.ToDateTime(edate.SelectedDate);
                    BList = new List<BiometricList>();

                    var attendance = db.GetEmployeeAttendanceByEmployeeNumber(Convert.ToString(sdate), Convert.ToString(edate), empno).OrderBy(m => m.DATE);
                    var employee = db.Employees.Where(m => m.EmployeeNo == empno).FirstOrDefault();
                    foreach (var x in attendance)
                    {
                        BiometricList bl = new BiometricList();

                        bl.BiometricDate = Convert.ToDateTime(x.DATE);
                        bl.DayOfWeek = x.DATE.Value.ToString("ddd");
                        bl.TimeIn = x.TIMEIN;
                        bl.BreakOut = x.BREAKOUT;
                        bl.BreakIn = x.BREAKIN;
                        bl.TimeOut = x.TIMEOUT;
                        bl.Remarks = x.REMARK;
                        bl.EmployeeName = employee.LastName + ", " + employee.FirstName;
                        bl.EmployeeNumber = Convert.ToString(empno);
                        bl.StarDate = Convert.ToDateTime(sdate.SelectedDate);
                        bl.EndDate = Convert.ToDateTime(edate.SelectedDate);


                        bl.TimeinID = Convert.ToInt32(x.TimeinID);
                        bl.BreakOutID = Convert.ToInt32(x.BreakoutID);
                        bl.BreakInID = Convert.ToInt32(x.BreakinID);
                        bl.TimeOutID = Convert.ToInt32(x.TimeoutID);
                        BList.Add(bl);
                    }

                    dgTurnstile.ItemsSource = BList.OrderByDescending(m => m.BiometricDate);

                }
                else
                {
                    MessageBox.Show("Employee cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }

        public class Month
        {
            public int MonthID { get; set; }
            public string MonthDescription { get; set; }
        }

        

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (BList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 8;
                x.Report8 = BList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        
        private void btnAddAttendance_Click(object sender, RoutedEventArgs e)
        {

            AddAttendance x = new AddAttendance();
            x.Owner = this;
            x.ShowDialog();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
        
            var selectedItem = (BiometricList)(dgTurnstile.SelectedItem);
            string empno = Convert.ToString(cbEmployee.SelectedValue);       
            var employee = db.Employees.Where(m => m.EmployeeNo == empno).FirstOrDefault();
            
            UpdateAttendanceType x = new UpdateAttendanceType();
            x.empid = employee.EmployeeID;
            //x.timein = selectedItem.TimeIn;
            //x.breakin = selectedItem.BreakIn;
            //x.breakout = selectedItem.BreakOut;
            //x.timeout = Convert.ToDateTime(selectedItem.TimeOut);
            x.remark = selectedItem.Remarks;
            x.Date = Convert.ToDateTime(selectedItem.BiometricDate);

            x.timeinID = selectedItem.TimeinID;
            x.breakoutID = selectedItem.BreakOutID;
            x.breakinID = selectedItem.BreakInID;
            x.timeoutID = selectedItem.TimeOutID;
            x.Owner = this;
            x.ShowDialog();
    

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var selectedItem = (BiometricList)(dgTurnstile.SelectedItem);
                    var id = selectedItem.AttendanceID;
                    var attendance = db.BiometricsLogDatas.Where(m => m.Id == id).FirstOrDefault();
                    if (attendance!=null)
                    {
                        db.BiometricsLogDatas.Remove(attendance);
                        db.SaveChanges();
                        MessageBox.Show("Deleted Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetBiometrics();
                    }
                    else
                    {
                        MessageBox.Show("No record found!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetBiometrics();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (BiometricList)(dgTurnstile.SelectedItem);
            string empno = Convert.ToString(cbEmployee.SelectedValue);
            var employee = db.Employees.Where(m => m.EmployeeNo == empno).FirstOrDefault();

            AddHoursRenderedLateUnderTime x = new AddHoursRenderedLateUnderTime();
            x.empid = employee.EmployeeID;
            x.empname = cbEmployee.Text;
            x.curentdate = selectedItem.BiometricDate.ToShortDateString();
            //x.timein = selectedItem.TimeIn;
            //x.breakin = selectedItem.BreakIn;
            //x.breakout = selectedItem.BreakOut;
            //x.timeout = Convert.ToDateTime(selectedItem.TimeOut);
            x.remark = selectedItem.Remarks;
            x.Date = Convert.ToDateTime(selectedItem.BiometricDate);

            x.timeinID = selectedItem.TimeinID;
            x.breakoutID = selectedItem.BreakOutID;
            x.breakinID = selectedItem.BreakInID;
            x.timeoutID = selectedItem.TimeOutID;
            x.Owner = this;
            x.ShowDialog();
        }
    }

    public class BiometricList
    {
        public long AttendanceID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime BiometricDate { get; set; }
        public string Month { get; set; }
        public string DayOfWeek { get; set; } 
        public string TimeIn { get; set; }
        public string BreakOut { get; set; }
        public string BreakIn { get; set; }
        public string TimeOut { get; set; }
        public string Remarks { get; set; }

        public int TimeinID { get; set; }

        public int BreakInID { get; set; }
        public int BreakOutID { get; set; }

        public int TimeOutID { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
