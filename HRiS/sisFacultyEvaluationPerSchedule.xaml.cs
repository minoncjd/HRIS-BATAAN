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
    /// Interaction logic for sisFacultyEvaluationPerSchedule.xaml
    /// </summary>
    public partial class sisFacultyEvaluationPerSchedule : MetroWindow
    {
        public int PerID;
        public int SchdID;
        public int FacID;

        List<SisQuestionList> SQList = new List<SisQuestionList>();
        List<SisEvaluationPerSchedList> SEPSList = new List<SisEvaluationPerSchedList>();
        List<SisCommentList> SCList = new List<SisCommentList>();

        public sisFacultyEvaluationPerSchedule()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetSisFacultyEvaluationPerSchedule(PerID, SchdID);
        }

        public void GetSisFacultyEvaluationPerSchedule(int PeriodID, int ScheduleID)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    SQList = new List<SisQuestionList>();
                    SEPSList = new List<SisEvaluationPerSchedList>();

                    var result = db.GetHRiSsisPerSchedule(PeriodID, ScheduleID).ToList();
                    if (result.Count > 0)
                    {
                        var sisQuestion = db.sisItems.Where(m => (m.sisCatID == 1 || m.sisCatID == 2 || m.sisCatID == 3) && m.sisItemweight == null).OrderBy(m => m.sisCatID).ToList();

                        int id = 1;
                        foreach (var q in sisQuestion)
                        {
                            SisQuestionList ques = new SisQuestionList();
                            ques.SisQuestionID = id;
                            ques.SisQuestion = q.sisItem1;
                            ques.SisCategoryID = q.sisCatID.Value;
                            id++;
                            SQList.Add(ques);
                        }

                        foreach (var i in SQList)
                        {
                            SisEvaluationPerSchedList sis = new SisEvaluationPerSchedList();
                            sis.FacultyName = result.FirstOrDefault().FacultyName;
                            sis.SubjectDescription = result.FirstOrDefault().SubjectDescription;
                            sis.SectionName = result.FirstOrDefault().SectionName;
                            sis.Days = result.FirstOrDefault().Days;
                            sis.StartTime = result.FirstOrDefault().StartTime.Value;
                            sis.EndTime = result.FirstOrDefault().EndTime.Value;
                            sis.RoomName = result.FirstOrDefault().RoomName;
                            sis.Respondents = result.FirstOrDefault().Respondents.Value;

                            if (result.Where(m => m.QuestionNo == i.SisQuestionID.ToString()).Count() > 0)
                            {
                                sis.SisCategoryID = SQList.Single(m => m.SisQuestionID == i.SisQuestionID).SisCategoryID;
                                sis.QuestionID = SQList.Single(m => m.SisQuestionID == i.SisQuestionID).SisQuestionID;
                                sis.QuestionDescription = SQList.Single(m => m.SisQuestionID == i.SisQuestionID).SisQuestion;
                                sis.Result = result.Single(m => m.QuestionNo == i.SisQuestionID.ToString()).Result.Value;
                            }




                            SEPSList.Add(sis);
                        }

                        lblInstructor.Content = SEPSList.FirstOrDefault().FacultyName.ToUpper();
                        lblSubject.Content = SEPSList.FirstOrDefault().SubjectDescription;
                        lblSection.Content = SEPSList.FirstOrDefault().SectionName;
                        lblRespondents.Content = SEPSList.FirstOrDefault().Respondents.ToString();

                        lblDay.Content = SEPSList.FirstOrDefault().Days;
                        lblTime.Content = SEPSList.FirstOrDefault().StartTime.ToString("t") + " - " + SEPSList.FirstOrDefault().EndTime.ToString("t");
                        lblRoom.Content = SEPSList.FirstOrDefault().RoomName;

                        double cat1 = Math.Round((SEPSList.Where(m => m.SisCategoryID == 1 && m.Result != 0).Average(m => m.Result) * .50), 2);
                        double cat2 = Math.Round((SEPSList.Where(m => m.SisCategoryID == 2 && m.Result != 0).Average(m => m.Result) * .25), 2);
                        double cat3 = Math.Round((SEPSList.Where(m => m.SisCategoryID == 3 && m.Result != 0).Average(m => m.Result) * .25), 2);

                        lblOverallMean.Content = Math.Round(cat1 + cat2 + cat3, 2).ToString();

                        dgSkill.ItemsSource = SEPSList.Where(m => m.SisCategoryID == 1).ToList();
                        lblMeanSkill.Content = "MEAN: " + Math.Round(SEPSList.Where(m => m.SisCategoryID == 1 && m.Result != 0).Average(m => m.Result), 2).ToString();

                        dgWork.ItemsSource = SEPSList.Where(m => m.SisCategoryID == 2).ToList();
                        lblMeanWork.Content = "MEAN: " + Math.Round(SEPSList.Where(m => m.SisCategoryID == 2 && m.Result != 0).Average(m => m.Result), 2).ToString();

                        dgClassroom.ItemsSource = SEPSList.Where(m => m.SisCategoryID == 3).ToList();
                        lblMeanClassroom.Content = "MEAN: " + Math.Round(SEPSList.Where(m => m.SisCategoryID == 3 && m.Result != 0).Average(m => m.Result), 2).ToString();
                    }
                    else
                    {
                        MessageBox.Show("No respondent is this schedule.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void GetComment(int ScheduleID)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var com = db.sisSHS.Where(m => m.StudentSchedule.ScheduleID == ScheduleID).ToList();
                    SCList = new List<SisCommentList>();

                    if (com.Count > 0)
                    {
                        foreach (var x in com.Where(m => String.IsNullOrWhiteSpace(m.Comment1) == false))
                        {
                            SisCommentList siscom = new SisCommentList();
                            siscom.SisCategoryID = 1;
                            siscom.Comment = x.Comment1;
                            siscom.CategoryName = "INSTRUCTIONAL SKILL AND COMPETENCIES";
                            SCList.Add(siscom);
                        }

                        foreach (var x in com.Where(m => String.IsNullOrWhiteSpace(m.Comment2) == false))
                        {
                            SisCommentList siscom = new SisCommentList();
                            siscom.SisCategoryID = 2;
                            siscom.Comment = x.Comment2;
                            siscom.CategoryName = "WORK ATTITUDE";
                            SCList.Add(siscom);
                        }

                        foreach (var x in com.Where(m => String.IsNullOrWhiteSpace(m.Comment3) == false))
                        {
                            SisCommentList siscom = new SisCommentList();
                            siscom.SisCategoryID = 3;
                            siscom.Comment = x.Comment3;
                            siscom.CategoryName = "CLASSROOM MANAGEMENT";
                            SCList.Add(siscom);
                        }


                        SCList = SCList.OrderBy(m => m.SisCategoryID).ToList();
                        ListCollectionView sclcollection = new ListCollectionView(SCList);

                        sclcollection.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));


                        dgComments.ItemsSource = sclcollection;
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnComment_Click(object sender, RoutedEventArgs e)
        {
            GetComment(SchdID);
        }

        private void btnOverallEvalutaion_Click(object sender, RoutedEventArgs e)
        {
            sisFacultyEvaluationPerFaculty y = new sisFacultyEvaluationPerFaculty();
            y.PerID = PerID;
            y.FacID = FacID;
            y.ShowDialog();
        }

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (SEPSList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 16;
                x.SisPerSchedList = SEPSList;
                x.SisComList = SCList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
    public class SisEvaluationPerSchedList
    {
        public string FacultyName { get; set; }
        public string SubjectDescription { get; set; }
        public string SectionName { get; set; }
        public string Days { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string RoomName { get; set; }
        public int Respondents { get; set; }

        public int SisCategoryID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionDescription { get; set; }
        public double Result { get; set; }
    }

    public class SisQuestionList
    {
        public int SisQuestionID { get; set; }
        public int SisCategoryID { get; set; }
        public string SisQuestion { get; set; }
    }

    public class SisCommentList
    {
        public int SisCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Comment { get; set; }
    }
}
