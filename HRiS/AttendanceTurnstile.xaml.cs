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
    /// Interaction logic for AttendanceTurnstile.xaml
    /// </summary>
    public partial class AttendanceTurnstile : MetroWindow
    {
        TurnstileEntities tb = new TurnstileEntities();
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();

        List<EmpCombo> EList = new List<EmpCombo>();
        List<Month> MList = new List<Month>();
        List<TurnstileList> TList = new List<TurnstileList>();

        public AttendanceTurnstile()
        {
            InitializeComponent();
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
                cbEmployee.SelectedValuePath = "EmployeeID";


                for (int x = Convert.ToInt32(DateTime.Now.Year); x < (Convert.ToInt32(DateTime.Now.Year) + 10); x = x + 1)
                {
                    cbYear.Items.Add(x);
                }
                for (int x = 0; x < 12; x++)
                {
                    Month m = new Month();
                    m.MonthID = x;
                    m.MonthDescription = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[x];
                    MList.Add(m);
                }
                cbMonth.ItemsSource = MList.OrderBy(m => m.MonthID);
                cbMonth.DisplayMemberPath = "MonthDescription";
                cbMonth.SelectedValuePath = "MonthID";

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }

        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            GetTurnstile();
        }

        public void GetTurnstile()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                tb = new TurnstileEntities();
                TList = new List<TurnstileList>();


                if (!String.IsNullOrEmpty(cbEmployee.Text))
                {
                    int empid = Convert.ToInt32(cbEmployee.SelectedValue);
                    var employee = db.Employees.Find(empid);

                    var turntran = tb.tbltimeins.Where(m => m.IDnumber == employee.EmployeeNo).ToList();

                    foreach (var x in turntran)
                    {
                        TurnstileList tl = new TurnstileList();
                        tl.TurnID = x.IDlog;
                        tl.EmployeeNumber = x.IDnumber;
                        tl.TurnDate = x.TimeIn.Value.Date;
                        tl.EmployeeID = empid;
                        tl.EmployeeName = employee.LastName.ToUpper() + ", " + employee.FirstName.ToUpper();
                        tl.Month = x.TimeIn.Value.Date.ToString("MMMM");
                        tl.DayOfWeek = x.TimeIn.Value.Date.DayOfWeek.ToString().ToUpper();
                        tl.TurnTime = x.TimeIn.Value.TimeOfDay;
                        tl.Type = x.Status.ToUpper();
                        TList.Add(tl);
                    }
                    if (!String.IsNullOrEmpty(cbYear.Text) && !String.IsNullOrEmpty(cbMonth.Text))
                    {
                        TList = TList.Where(m => m.TurnDate.Year == Convert.ToInt32(cbYear.Text) && m.TurnDate.Month == (Convert.ToInt32(cbMonth.SelectedValue) + 1)).ToList();
                    }

                    dgTurnstile.ItemsSource = TList.OrderByDescending(m => m.TurnDate);
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
            if (TList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 9;
                x.Report9 = TList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
    public class TurnstileList
    {
        public long TurnID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Month { get; set; }
        public DateTime TurnDate { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan TurnTime { get; set; }
        public string Type { get; set; }
    }
}
