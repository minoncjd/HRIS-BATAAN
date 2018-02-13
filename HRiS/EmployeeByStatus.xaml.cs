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
    /// Interaction logic for EmployeeByStatus.xaml
    /// </summary>
    public partial class EmployeeByStatus : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmployeeStatusList> ESList = new List<EmployeeStatusList>();

        public EmployeeByStatus()
        {
            InitializeComponent();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeeStatus();
        }

        public void GetEmployeeStatus()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                ESList = new List<EmployeeStatusList>();


                var empstat = db.GetHRiSEmployeebyStatus().ToList();

                foreach(var x in empstat)
                {
                    EmployeeStatusList e = new EmployeeStatusList();

                    if (x.DateHired.HasValue)
                    {
                        e.DateHired = x.DateHired.Value;
                    }
                    else
                    {

                    }
                    if (x.Archive.HasValue)
                    {
                        e.Archive = x.Archive.Value;
                    }
                    else
                    {
                        e.Archive = true;
                     }
                    
                    e.EmployeeID = x.EmployeeID;
                    e.EmployeeNumber = Convert.ToInt32(x.EmployeeNo);
                    e.EmployeeName = x.EmployeeName;
                    e.Department = x.Department;
                    e.DepartmentID = x.AcaDeptID;
                    e.Designation = x.Designation;
                    e.DesignationID = x.EmployeeDesignationID;
           
                     e.SeparationDate = x.SeparationDate.ToString();
                    e.EmployeeStatusID = x.EmployeeStatusID;
                    e.EmployeeStatusCode = x.EmployeeStatusCode;
                    e.TIN = x.TIN;
                    e.SSS = x.SSS;
                    e.PAGIBIG = x.PAG_IBIG;
                    e.PHILHEALTH = x.PhilHealth;
                  
                    ESList.Add(e);
                }
                if(rbAll.IsChecked == true)
                {
                    ESList = ESList.OrderBy(m => m.EmployeeName).ToList();
                }
                else if (rbPermanent.IsChecked == true)
                {
                    ESList = ESList.Where(m => m.EmployeeStatusID == 3).OrderBy(m => m.EmployeeName).ToList();
                }
                else if (rbProbitionary.IsChecked == true)
                {
                    ESList = ESList.Where(m => m.EmployeeStatusID == 1).OrderBy(m => m.EmployeeName).ToList();
                }
                else if (rbContractual.IsChecked == true)
                {
                    ESList = ESList.Where(m => m.EmployeeStatusID == 2).OrderBy(m => m.EmployeeName).ToList();
                }
                if(chkArchive.IsChecked == true)
                {
                    ESList = ESList.Where(m => m.Archive == true).OrderBy(m => m.EmployeeName).ToList();
                }
                else
                {
                    ESList = ESList.Where(m => m.Archive == false).OrderBy(m => m.EmployeeName).ToList();
                }
                if(!String.IsNullOrEmpty(cbDesignation.Text))
                {
                    int desid = Convert.ToInt32(cbDesignation.SelectedValue);
                    ESList = ESList.Where(m => m.DesignationID == desid).OrderBy(m => m.EmployeeName).ToList();
                }

                dgEmpStatus.ItemsSource = ESList.OrderBy(m => m.EmployeeName);
                lblCount.Content = "Count: " + ESList.Count.ToString();
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong!","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }

       

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();

                cbDesignation.ItemsSource = db.EmployeeDesignations.OrderBy(m => m.EmployeeDesignationName).ToList();
                cbDesignation.DisplayMemberPath = "EmployeeDesignationName";
                cbDesignation.SelectedValuePath = "EmployeeDesignationID";
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (ESList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 10;
                x.Report10 = ESList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    public class EmployeeStatusList
    {

        public EmployeeStatusList()
        {
            DateHired = DateTime.Now;
            Archive = false;
        }

        public int EmployeeID { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public int DepartmentID { get; set; }
        public string Designation { get; set; }
        public int DesignationID { get; set; }
        public DateTime DateHired { get; set; }
        public string SeparationDate { get; set; }
        public int EmployeeStatusID { get; set; }
        public string EmployeeStatusCode { get; set; }
        public string TIN { get; set; }
        public string SSS { get; set; }
        public string PAGIBIG { get; set; }
        public string PHILHEALTH { get; set; }
        public bool Archive { get; set; }

       
    }
}
