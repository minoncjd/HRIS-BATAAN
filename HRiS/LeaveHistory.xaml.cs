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
    /// Interaction logic for LeaveHistory.xaml
    /// </summary>
    public partial class LeaveHistory : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmpCombo> EList = new List<EmpCombo>();
        List<HistoryList> HList = new List<HistoryList>();

        public LeaveHistory()
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

                cbType.ItemsSource = db.LeaveTypes.OrderBy(m => m.LeaveCode).ToList();
                cbType.DisplayMemberPath = "LeaveDescription";
                cbType.SelectedValuePath = "LeaveTypeID";


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
                db = new LetranIntegratedSystemEntities();
                HList = new List<HistoryList>();

                if (!String.IsNullOrEmpty(cbEmployee.Text))
                {
                    var empid = Convert.ToInt32(cbEmployee.SelectedValue);
                    var hist = db.GetHRiSLeaveEmployeeHistory(empid).OrderByDescending(m => m.EndDate).ToList();

                    if (hist != null)
                    {
                        foreach (var x in hist)
                        {
                            HistoryList hl = new HistoryList();
                            hl.Days = x.Days.Value;
                            hl.EndDate = x.EndDate.Value;
                            //hl.FiledDate = x.FiledDate.Value;
                            hl.LeaveID = x.LeaveID;
                            hl.LeaveType = x.LeaveType;
                            hl.LeaveTypeID = x.LeaveTypeID;
                            hl.Reason = x.Reason;
                            hl.StartDate = x.StartDate.Value;
                           // hl.Status = x.ApprovedStatus;
                            hl.EmployeeID = db.Employees.Find(x.EmployeeID).EmployeeID;
                            hl.EmployeeNo = db.Employees.Find(x.EmployeeID).EmployeeNo;
                            hl.EmployeeName = db.Employees.Find(x.EmployeeID).LastName.ToUpper() + ", " + db.Employees.Find(x.EmployeeID).FirstName.ToUpper();
                            HList.Add(hl);
                        }
                        if (String.IsNullOrEmpty(cbType.Text))
                        {
                            HList = HList.ToList();
                        }
                        else
                        {
                            var typeid = Convert.ToInt32(cbType.SelectedValue);
                            HList = HList.Where(m => m.LeaveTypeID == typeid).ToList();
                        }
                        dgLeaveHistory.ItemsSource = HList;
                    }
                }
                else
                {
                    MessageBox.Show("Employee cannot be empty!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            //}
            //catch
            //{
            //    MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }



        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbType.Text = "";
        }

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (HList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 7;
                x.Report7 = HList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class HistoryList
    {
        public int LeaveID { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveType { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public decimal Days { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }

    }
}
