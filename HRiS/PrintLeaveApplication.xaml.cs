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
using CrystalDecisions.CrystalReports.Engine;
using HRiS.Reports;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for PrintLeaveApplication.xaml
    /// </summary>
    public partial class PrintLeaveApplication : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        public int LeaveID;
        List<PrintLeaveList> plist = new List<PrintLeaveList>();
        ReportDocument report;

        public PrintLeaveApplication()
        {
            InitializeComponent();

            LoadComboBox();
        }

        public void LoadComboBox()
        {
            db = new LetranIntegratedSystemEntities();

            cbLeaveType.ItemsSource = db.LeaveTypes.OrderBy(m => m.LeaveTypeID).ToList();
            cbLeaveType.DisplayMemberPath = "LeaveDescription";
            cbLeaveType.SelectedValuePath = "LeaveTypeID";

            cbSickLeaveType.ItemsSource = db.SickLeaveTypes.OrderBy(m => m.SickLeaveReasonID).ToList();
            cbSickLeaveType.DisplayMemberPath = "SickLeaveReasonType";
            cbSickLeaveType.SelectedValuePath = "SickLeaveReasonID";
        }

        public void GetLeave()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();

                var leave = db.Leaves.Find(LeaveID);

                if (leave != null)
                {
                    lblName.Content = leave.Employee.LastName.ToUpper() + ", " + leave.Employee.FirstName.ToUpper();
                    lblDepartment.Content = leave.Employee.AcademicDepartment.AcaAcronym.ToUpper();
                    lblPosition.Content = leave.Employee.EmployeePosition.EmployeePositionName.ToUpper();
                    lblDesignation.Content = leave.Employee.EmployeeDesignation1.EmployeeDesignationName.ToUpper();

                    //lblDateFiled.Content = leave.FiledDate.Value.ToString("MMMM dd, yyyy");
                    cbLeaveType.SelectedValue = leave.LeaveTypeID;
                    dpStart.SelectedDate = leave.StartDate;
                    dpEnd.SelectedDate = leave.EndDate;
                    txtDays.Text = leave.Days.Value.ToString();

                    lblSL.Content = leave.Employee.LeaveBalances.FirstOrDefault().SickLeaveBalance.Value.ToString() + " (SL)";
                    lblVL.Content = leave.Employee.LeaveBalances.FirstOrDefault().VacationLeaveBalance.Value.ToString() + " (VL)";

                    //if (leave.SickLeaveReasonID != null || leave.SickLeaveReasonID != 0)
                    //    cbSickLeaveType.SelectedValue = leave.SickLeaveReasonID;

                    txtReason.Text = leave.Reason;
                }
                else
                {
                    MessageBox.Show("Leave not found", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                plist = new List<PrintLeaveList>();
                var leave = db.Leaves.Find(LeaveID);
                if(leave != null)
                {
                    PrintLeaveList pl = new PrintLeaveList();
                    pl.LeaveID = leave.LeaveID;
                    pl.EmployeeName = leave.Employee.LastName.ToUpper() + ", " + leave.Employee.FirstName.ToUpper();
                    pl.EmployeeDepartment = leave.Employee.AcademicDepartment.AcaAcronym.ToUpper();
                    pl.EmployeePosition = leave.Employee.EmployeePosition.EmployeePositionName.ToUpper();
                    pl.EmployeeDesignation = leave.Employee.EmployeeDesignation1.EmployeeDesignationName.ToUpper();
                    //pl.DateFiled = leave.FiledDate.Value;
                    pl.LeaveType = leave.LeaveType.LeaveDescription;
                    pl.StartDate = leave.StartDate.Value;
                    pl.EndDate = leave.EndDate.Value;
                    pl.Days = leave.Days.Value;
                    if(leave.LeaveTypeID == 4)
                    {
                        pl.CreditsAvailable = leave.Employee.LeaveBalances.FirstOrDefault().SickLeaveBalance.Value;
                    
                    }
                    else if(leave.LeaveTypeID == 5)
                    {
                        pl.CreditsAvailable = leave.Employee.LeaveBalances.FirstOrDefault().VacationLeaveBalance.Value;
                    }
                    else if(leave.LeaveTypeID == 1)
                    {
                        //pl.CreditsAvailable = leave.Employee.LeaveBalances.FirstOrDefault().EmergencyLeaveBalance.Value;
                    }
                    else if(leave.LeaveTypeID == 7)
                    {
                        //pl.CreditsAvailable = leave.Employee.LeaveBalances.FirstOrDefault().SoloParentLeaveBalance.Value;
                    }
                    else if(leave.LeaveTypeID == 10)
                    {
                        //pl.CreditsAvailable = leave.Employee.LeaveBalances.FirstOrDefault().BereavementLeaveBalance.Value;
                    }
                    else
                    {
                        pl.CreditsAvailable = 0;
                    }
                    string process = App.EmployeeUserName;
                    pl.ProcessedBy = process;
                    pl.Reason = leave.Reason;
                    plist.Add(pl);

                    report = new Reports.PrintLeave();
                    report.SetDataSource(plist);
                    report.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    System.Drawing.Printing.PrinterSettings ps = new System.Drawing.Printing.PrinterSettings();
                    ps.PrinterName = ps.PrinterName;
                    ps.Copies = 1;
                    ps.Collate = false;
                    ps.FromPage = 0;
                    ps.ToPage = 0;
                    System.Drawing.Printing.PageSettings pgs = new System.Drawing.Printing.PageSettings(ps);
                    pgs.Landscape = false;
                    pgs.Margins = new System.Drawing.Printing.Margins(250, 250, 250, 250);
                    pgs.PaperSize = new System.Drawing.Printing.PaperSize("CUSTOM REPORT SIZE", 850, 550);
                    report.PrintToPrinter(ps, pgs, false);
                    MessageBox.Show("Print Successful","System Success",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Leave not found", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.Close();
                }

                
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetLeave();
        }

    }

    public class PrintLeaveList
    {
        public int LeaveID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDepartment { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeDesignation { get; set; }
        public DateTime DateFiled { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Days { get; set; }
        public decimal CreditsAvailable { get; set; }
        public string ProcessedBy { get; set; }
        public string Reason { get; set; }
    }
}
