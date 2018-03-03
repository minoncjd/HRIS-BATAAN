using HRiS.Model;
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

namespace HRiS
{
    /// <summary>
    /// Interaction logic for PrintSISResult.xaml
    /// </summary>
    public partial class PrintSISResult : MetroWindow
    {
        public PrintSISResult()
        {
            InitializeComponent();

            using (var db = new LetranIntegratedSystemEntities())
            {
                cbEducLevel.ItemsSource = db.EducationalLevels.Where(m => m.EducLevelID != 1).OrderBy(m => m.EducLevelID).ToList();
                cbEducLevel.DisplayMemberPath = "EducLevelName";
                cbEducLevel.SelectedValuePath = "EducLevelID";     
            }

        }

        private void LoadComboBox()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var db= new LetranIntegratedSystemEntities())
            {

          
                var period = Convert.ToInt32(cbPeriod.SelectedValue);
                var sem = Convert.ToInt32(cbSemester.SelectedValue);
                var rt = Convert.ToInt32(cbReportType.SelectedValue);
                var educlevel = Convert.ToInt32(cbEducLevel.SelectedValue);
                var sy = Convert.ToInt32(cbSchoolYear.SelectedValue);
                var reportID = Convert.ToInt32(cbReportType.SelectedValue);
                
                //var periodid = db.Periods.Where(m => m.SchoolYearID == sy && m.EducLevelID == educlevel).FirstOrDefault();
               // var periodkey = periodid.PeriodID;
                if (reportID == 1)
                {
                    PrintWindow x = new PrintWindow();

                    x.rptid = 32;
                    x.period = period;
                    x.sem = sem;
                    x.ShowDialog();

                }
                else if (reportID == 2 || reportID == 3)
                {
                    PrintWindow x = new PrintWindow();

                    x.rptid = 31;
                    x.period = period;
                 
                    x.ShowDialog();
                }

            }
        }

        private void cbEducLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new LetranIntegratedSystemEntities())
            {
                var educlevel = Convert.ToInt32(cbEducLevel.SelectedValue);
                cbSchoolYear.IsEnabled = true;
                cbSchoolYear.ItemsSource = db.SchoolYears.OrderByDescending(m => m.SchoolYearName).ToList();
                cbSchoolYear.DisplayMemberPath = "SchoolYearName";
                cbSchoolYear.SelectedValuePath = "SYID";

                List<ReportList> RList = new List<ReportList>();
                RList.Add(new ReportList { ReportID = 1, Report = "SIS Result Per Faculty (SHS)" });
                RList.Add(new ReportList { ReportID = 2, Report = "SIS Result Per Faculty (JHS)" });
                RList.Add(new ReportList { ReportID = 3, Report = "SIS Result Per Faculty (COLL)" });
                RList.Add(new ReportList { ReportID = 4 , Report = "SIS Summary Result Per Faculty (COLL)" });

                if (educlevel == 4)
                {
                    cbReportType.ItemsSource = RList.Where(m => m.ReportID == 3 || m.ReportID ==4).ToList();
                }
                else if (educlevel == 3)
                {
                    cbReportType.ItemsSource = RList.Where(m => m.ReportID == 1).ToList();
                }
                else if (educlevel == 2)
                {
                    cbReportType.ItemsSource = RList.Where(m => m.ReportID == 2).ToList();
                }


                cbReportType.DisplayMemberPath = "Report";
                cbReportType.SelectedValuePath = "ReportID";

            }
        }

        private void cbPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new LetranIntegratedSystemEntities())
            {
                int y = Convert.ToInt32(cbEducLevel.SelectedValue);
                if (y == 3)
                {

                    List<SemesterList> SList = new List<SemesterList>();
                    SList.Add(new SemesterList { SemesterID = 1, Semester = "1st Semester" });
                    SList.Add(new SemesterList { SemesterID = 2, Semester = "2nd Semester" });
                    cbSemester.ItemsSource = SList;
                    cbSemester.DisplayMemberPath = "Semester";
                    cbSemester.SelectedValuePath = "SemesterID";


                }
                else
                {
                    //Load faculties
                   // GetSisFaculty();
                }
            }
        }


        private void cbSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new LetranIntegratedSystemEntities())
            {
                int x = Convert.ToInt32(cbSchoolYear.SelectedValue);
                int y = Convert.ToInt32(cbEducLevel.SelectedValue);

                cbPeriod.IsEnabled = true;
                cbPeriod.ItemsSource = db.Periods.Where(m => m.SchoolYearID == x && m.EducLevelID == y).ToList();
                cbPeriod.DisplayMemberPath = "Period1";
                cbPeriod.SelectedValuePath = "PeriodID";

                if (y == 3)
                    cbSemester.IsEnabled = true;
                else
                {
                    cbSemester.IsEnabled = false;
                    cbSemester.Text = "";
                }

            }
        }


        class SemesterList
        {
            public int SemesterID { get; set; }
            public string Semester { get; set; }
        }


        class ReportList
        {
            public int ReportID { get; set; }
            public string Report { get; set; }
        }
    }
}
