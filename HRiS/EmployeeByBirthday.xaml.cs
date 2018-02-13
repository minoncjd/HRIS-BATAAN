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
    /// Interaction logic for EmployeeByBirthday.xaml
    /// </summary>
    public partial class EmployeeByBirthday : MetroWindow
    {
        List<Month> MList = new List<Month>();
        List<BirthdayList> EBList = new List<BirthdayList>();

        public EmployeeByBirthday()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MList = new List<Month>();
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

                cbMonth.SelectedIndex = DateTime.Now.Month - 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public class Month
        {
            public int MonthID { get; set; }
            public string MonthDescription { get; set; }
        }


        private void cbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                EBList = new List<BirthdayList>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (cbMonth.SelectedItem != null)
                    {
                        int xmonth = Convert.ToInt32(cbMonth.SelectedValue) + 1;
                        var getbday = db.GetHRiSBirthdayCelebrants(xmonth).OrderByDescending(m => m.Day).ToList();

                        foreach (var x in getbday)
                        {
                            BirthdayList eb = new BirthdayList();
                            eb.EmployeeName = x.EmployeeName.ToUpper();
                            eb.Department = x.Department;
                            eb.Day = x.Day.Value;
                            if (x.Day.Value == DateTime.Now.Day)
                            {
                                eb.IsToday = true;
                            }
                            else
                            {
                                eb.IsToday = false;
                            }
                            eb.Month = cbMonth.Text;
                            EBList.Add(eb);
                        }

                        dgBirthday.ItemsSource = EBList;
                    }
                    else
                    {
                        MessageBox.Show("Select an item.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (EBList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 12;
                x.Report12 = EBList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    public class BirthdayList
    {
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public int Day { get; set; }
        public bool IsToday { get; set; }
        public string Month { get; set; }
    }
}
 
