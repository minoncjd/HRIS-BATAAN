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
using static HRiS.EmployeeAbsent;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : MetroWindow
    {
        public int rptid;
        ReportDocument report;
        public List<OnProcessList> Report1 = new List<OnProcessList>();
        public List<ApprovedList> Report2 = new List<ApprovedList>();
        public List<DisapprovedList> Report3 = new List<DisapprovedList>();
        public List<BalanceList> Report4 = new List<BalanceList>();
        public List<AbsentList> Report28 = new List<AbsentList>();
        public List<GetEmployeeDTR_Result> Report29 = new List<GetEmployeeDTR_Result>();
        public List<GetEmployeeDTR_Result> Report30 = new List<GetEmployeeDTR_Result>();
        public List<LeaveUsageList> Report5 = new List<LeaveUsageList>();
        public List<TransactionList> Report6 = new List<TransactionList>();
        public List<HistoryList> Report7 = new List<HistoryList>();
        public List<BiometricList> Report8 = new List<BiometricList>();
        public List<TurnstileList> Report9 = new List<TurnstileList>();
        public List<EmployeeStatusList> Report10 = new List<EmployeeStatusList>();
        public List<EmployeeDepartmentList> Report11 = new List<EmployeeDepartmentList>();
        public List<BirthdayList> Report12 = new List<BirthdayList>();
        public List<ServiceAwardeeList> Report13 = new List<ServiceAwardeeList>();
        public List<EmployeeAgeList> Report14 = new List<EmployeeAgeList>();
        public List<EmployeeGenderList> Report15 = new List<EmployeeGenderList>();
        public List<SisEvaluationPerSchedList> SisPerSchedList = new List<SisEvaluationPerSchedList>();
        public List<SisEvaluationPerFacultyList> SisPerFacultyList = new List<SisEvaluationPerFacultyList>();
        public List<SisCommentList> SisComList = new List<SisCommentList>();
        public string startDate;
        public string endDate;
 
        public int LeaveCancellationID;
        public int ChangeVLID;
        public int NOAID;
        public int OvertimeID;
        public int OffID;
        public int PostID;
        public int MakeupID;
        public int SubID;
        public int ApplicationID;
        public string department;
        public PrintWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            crViewer1.Owner = Window.GetWindow(this);
            LoadReport(rptid);
        }

        public void LoadReport(int ReportID)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (rptid != 0)
                    {

                        if (ReportID == 1)       //Leave On Process Report
                        {
                            if (Report1.Count > 0)
                            {
                                report = new Reports.LeaveOnProcessRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report1);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 2)  //Leave Approved Report
                        {
                            if (Report2.Count > 0)
                            {
                                report = new Reports.LeaveApprovedRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report2);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 3)  //Leave Disapproved Report
                        {
                            if (Report3.Count > 0)
                            {
                                report = new Reports.LeaveDisapprovedRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report3);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 4)  //Leave Balance Report
                        {
                            if (Report4.Count > 0)
                            {
                                report = new Reports.LeaveBalanceRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report4);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 5)  //Leave Usage Report 
                        {
                            if (Report5.Count > 0)
                            {
                                report = new Reports.LeaveUsageRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report5);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 6)  //Leave Transaction Report
                        {
                            if (Report6.Count > 0)
                            {
                                report = new Reports.LeaveTransactionRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report6);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 7)  //Leave History Report
                        {
                            if (Report7.Count > 0)
                            {
                                report = new Reports.LeaveHistoryRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report7);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 8)  //Attendance Biometrics Report
                        {
                            if (Report8.Count > 0)
                            {
                                report = new Reports.AttendanceBiometricRPT();
                                report.SetDatabaseLogon("softrack", "softrack");
                                report.SetDataSource(Report8);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 9)  //Attendance Turnstile Report
                        {
                            if (Report9.Count > 0)
                            {
                                report = new Reports.AttendanceTurnstileRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report9);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 10) //Employee by Status Report
                        {
                            if (Report10.Count > 0)
                            {
                                report = new Reports.EmployeeStatusRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report10);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 11) //Employee by Department Report
                        {
                            if (Report11.Count > 0)
                            {
                                report = new Reports.EmployeeDepartmentRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report11);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 12) //Employee by Birthday Report
                        {
                            if (Report12.Count > 0)
                            {
                                report = new Reports.EmployeeBirthdayRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report12);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 13) //Employee Service Awardees Report
                        {
                            if (Report13.Count > 0)
                            {
                                report = new Reports.EmployeeServiceAwardRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report13);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 14) //Employee Age Report
                        {
                            if (Report14.Count > 0)
                            {
                                report = new Reports.EmployeeAgeRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report14);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 15) //Employee by Sex Report
                        {
                            if (Report15.Count > 0)
                            {
                                report = new Reports.EmployeeGenderRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report15);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 16) //SIS Evaluation Per Schedule Report
                        {
                            if (SisPerSchedList.Count > 0)
                            {
                                report = new Reports.SISPerSchedRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(SisPerSchedList);
                                report.Subreports[0].SetDataSource(SisComList);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 17) //SIS Evaluation Per Faculty Report
                        {
                            if (SisPerFacultyList.Count > 0)
                            {
                                report = new Reports.SISPerFacultyRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(SisPerFacultyList);
                                report.Subreports[0].SetDataSource(SisComList);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 18) //Leave Cancellation Report
                        {
                            report = new Reports.LeaveCancellationRPT();
                            report.SetDatabaseLogon("nikko","letran1620");
                            report.SetParameterValue("@LeaveCancellationID", LeaveCancellationID);
                            crViewer1.ViewerCore.ReportSource = report;
                        }
                        else if (ReportID == 19) //Change Vacation Leave Report
                        {
                            if (ChangeVLID != 0)
                            {
                                report = new Reports.ChangeVacationLeaveRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetParameterValue("@VacationLeaveChangeID",ChangeVLID);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 20) //Notice of Absence Report
                        {
                            if (NOAID != 0)
                            {
                                report = new Reports.NoticeOfAbsenceRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetParameterValue("@NoticeAbsenceID",NOAID);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 21) //Overtime Request Report
                        {
                            if (OvertimeID != 0)
                            {
                                report = new Reports.OvertimeRequestFormRPT();
                               
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetParameterValue("@EmployeeOvertimeID",OvertimeID);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 22) //Certificate of Employment Report
                        {
                            
                        }
                        else if (ReportID == 23) //Off Campus Activity Report
                        {
                            if (OffID != 0)
                            {
                                report = new Reports.OffCampusActivityRPT();
                                
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetParameterValue("@OffCampusActivityID",OffID);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 24) //Post Training Report
                        {
                            if (PostID != 0)
                            {
                                report = new Reports.PostTrainingRPT();

                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetParameterValue("@PostTrainingID",PostID);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 25) //Make-Up Class Report
                        {
                            if (MakeupID != 0)
                            {
                                report = new Reports.MakeUpClassFormRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetParameterValue("@MakeUpClassID",MakeupID);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 26) //Substitution Report
                        {
                            if (SubID != 0)
                            {
                                report = new Reports.SubstitutionFormRPT();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetParameterValue("@SubstitutionClassID",SubID);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 27) //Application Form
                        {
                            if(ApplicationID !=0)
                            {
                                report = new Reports.ApplicationForm();
                                report.SetDatabaseLogon("nikko","letran1620");
                                report.SetParameterValue("@ApplicationID",ApplicationID);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }

                        else if (ReportID == 28) //Absents Report
                        {
                            if (Report28.Count > 0)
                            {
                                report = new Reports.EmployeeAbsent();
                                report.SetDatabaseLogon("nikko", "letran1620");
                                report.SetDataSource(Report28);
                                crViewer1.ViewerCore.ReportSource = report;
                            }
                        }
                        else if (ReportID == 29)
                        {
                            if (Report29.Count > 0)
                            {
                                report = new Reports.EmployeeDTR();
                                report.SetDatabaseLogon("softrack", "softrack");                      
                                report.SetDataSource(Report29);
                                report.SetParameterValue("StartDate", startDate);
                                report.SetParameterValue("EndDate", endDate);
                                report.SetParameterValue("regular", "08:00");
                                crViewer1.ViewerCore.ReportSource = report;
                            }

                        }

                        else if (ReportID == 30)
                        {
                            if (Report30.Count > 0)
                            {
                                report = new Reports.AttendanceSummary();
                                report.SetDatabaseLogon("softrack", "softrack");
                                report.SetDataSource(Report30);
                                report.SetParameterValue("StartDate", startDate);
                                report.SetParameterValue("EndDate", endDate);
                                report.SetParameterValue("Department", department);

                                crViewer1.ViewerCore.ReportSource = report;
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Report cannot be loaded.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

        }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong " + ex.Message, "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}
    }
}
