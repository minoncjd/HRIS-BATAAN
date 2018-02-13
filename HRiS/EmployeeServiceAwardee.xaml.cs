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
    /// Interaction logic for EmployeeServiceAwardee.xaml
    /// </summary>
    public partial class EmployeeServiceAwardee : MetroWindow
    {

        List<ServiceAwardeeList> SAList = new List<ServiceAwardeeList>();

        public EmployeeServiceAwardee()
        {
            InitializeComponent();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SAList = new List<ServiceAwardeeList>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (!String.IsNullOrEmpty(txtYear.Text))
                    {
                        int y;
                        if (Int32.TryParse(txtYear.Text, out y))
                        {
                            var getservice = db.GetHRiSServiceAwardees(y).ToList();

                            foreach (var x in getservice)
                            {
                                ServiceAwardeeList sal = new ServiceAwardeeList();
                                sal.EmployeeID = x.EmployeeID;
                                sal.EmployeeNo = x.EmployeeNo;
                                sal.Title = x.Title;
                                sal.EmployeeName = x.EmployeeName;
                                sal.Department = x.Department;
                                sal.DateHired = x.DateHired.Value;
                                sal.YearEntry = y;
                                sal.Year = x.YR.ToString() + " Years";
                                SAList.Add(sal);
                            }
                            dgServiceAwardee.ItemsSource = SAList;
                        }
                        else
                        {
                            MessageBox.Show("Not a valid number.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Year cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            if (SAList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 13;
                x.Report13 = SAList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    public class ServiceAwardeeList
    {
        public int EmployeeID { get; set; }
        public string EmployeeNo { get; set; }
        public string Title { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public DateTime DateHired { get; set; }
        public string Year { get; set; }
        public int YearEntry { get; set; }
    }
}
