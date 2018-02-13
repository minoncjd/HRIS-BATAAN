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
    /// Interaction logic for LeaveTransaction.xaml
    /// </summary>
    public partial class LeaveTransaction : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<TransactionList> TList = new List<TransactionList>();
        public LeaveTransaction()
        {
            InitializeComponent();
            rbSpecific.IsChecked = true;
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            dpEnd.IsEnabled = true;
            dpStart.IsEnabled = true;
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                TList = new List<TransactionList>();


                var query = db.GetHRiSLeaveTransactionReport().ToList();

                foreach(var x in query)
                {
                    TransactionList tl = new TransactionList();
                    tl.Days = x.Days.Value;
                    tl.Designation = x.Designation;
                    tl.EmployeeName = x.EmployeeName;
                    tl.EmployeeNo = x.EmployeeNo;
                    tl.EndDate = x.EndDate.Value;
                    tl.FiledDate = x.FiledDate.Value;
                    tl.RangeDate = x.RangeDate;
                    tl.Reason = x.Reason;
                    tl.StartDate = x.StartDate.Value;
                    tl.Type = x.Type;
                    TList.Add(tl);
                }

                if(rbSpecific.IsChecked == true)
                {
                    if(!String.IsNullOrEmpty(dpEnd.Text) && !String.IsNullOrEmpty(dpStart.Text))
                    {
                        DateTime StartDate = Convert.ToDateTime(dpStart.SelectedDate);
                        DateTime EndDate = Convert.ToDateTime(dpEnd.SelectedDate);

                        TList = TList.Where(m=>m.StartDate >= StartDate && m.StartDate <= EndDate).ToList();
                    }
                    else
                    {
                        MessageBox.Show("Date/s cannot be empty","System Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
                        return;
                    }
                }
                if(rbToday.IsChecked == true)
                {
                    TList = TList.Where(m => m.StartDate == DateTime.Now.Date).ToList();
                }
                if(rbMonth.IsChecked == true)
                {
                    TList = TList.Where(m => m.StartDate.Month == DateTime.Now.Month && m.StartDate.Year == DateTime.Now.Year).ToList();
                }
                if(rbYear.IsChecked == true)
                {
                    TList = TList.Where(m => m.StartDate.Year == DateTime.Now.Year).ToList();
                }

                dgLeaveTransaction.ItemsSource = TList;
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void rbYear_Checked(object sender, RoutedEventArgs e)
        {
            dpEnd.IsEnabled = false;
            dpStart.IsEnabled = false;
        }

        private void rbMonth_Checked(object sender, RoutedEventArgs e)
        {
            dpEnd.IsEnabled = false;
            dpStart.IsEnabled = false;
        }

        private void rbToday_Checked(object sender, RoutedEventArgs e)
        {
            dpEnd.IsEnabled = false;
            dpStart.IsEnabled = false;
        }

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (TList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 6;
                x.Report6 = TList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class TransactionList
    {
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public decimal Days { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime FiledDate { get; set; }
        public string RangeDate { get; set; }
    }
}
