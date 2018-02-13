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
    /// Interaction logic for sisFacultyEvaluationPerFaculty.xaml
    /// </summary>
    public partial class sisFacultyEvaluationPerFaculty : MetroWindow
    {

        public int FacID;
        public int PerID;
        List<SisQuestionList> SQList = new List<SisQuestionList>();
        List<SisCommentList> SCList = new List<SisCommentList>();
        List<SisEvaluationPerFacultyList> SEPFList = new List<SisEvaluationPerFacultyList>();

        public sisFacultyEvaluationPerFaculty()
        {
            InitializeComponent();
        }

        private void btnComment_Click(object sender, RoutedEventArgs e)
        {
            GetComment(FacID);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetSisFaculty(FacID, PerID);
        }

        public void GetSisFaculty(int FacultyID, int PeriodID)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    SQList = new List<SisQuestionList>();
                    SEPFList = new List<SisEvaluationPerFacultyList>();

                    var result = db.GetHRiSsisPerFaculty(PeriodID, FacultyID).ToList();
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
                            SisEvaluationPerFacultyList sis = new SisEvaluationPerFacultyList();
                            sis.FacultyName = result.FirstOrDefault().FacultyName;

                            sis.Department = db.Employees.Find(db.Faculties.Find(FacultyID).Employee.EmployeeID).AcademicDepartment.AcaDepartmentName;
                            sis.Designation = db.Employees.Find(db.Faculties.Find(FacultyID).Employee.EmployeeID).EmployeeDesignation1.EmployeeDesignationName;
                            sis.Position = db.Employees.Find(db.Faculties.Find(FacultyID).Employee.EmployeeID).EmployeePosition.EmployeePositionName;
                            sis.Respondents = result.FirstOrDefault().Respondents.Value;
                          
                            if (result.Where(m => m.QuestionNo == i.SisQuestionID.ToString()).Count() > 0)
                            {
                                sis.SisCategoryID = SQList.Single(m => m.SisQuestionID == i.SisQuestionID).SisCategoryID;
                                sis.QuestionID = SQList.Single(m => m.SisQuestionID == i.SisQuestionID).SisQuestionID;
                                sis.QuestionDescription = SQList.Single(m => m.SisQuestionID == i.SisQuestionID).SisQuestion;
                                sis.Result = result.Single(m => m.QuestionNo == i.SisQuestionID.ToString()).Result.Value;
                            }

                            SEPFList.Add(sis);
                        }

                        lblInstructor.Content = SEPFList.FirstOrDefault().FacultyName.ToUpper();
                        lblDepartment.Content = SEPFList.FirstOrDefault().Department;
                        lblDesignation.Content = SEPFList.FirstOrDefault().Designation;
                        lblPosition.Content = SEPFList.FirstOrDefault().Position;
                        lblRespondents.Content = SEPFList.FirstOrDefault().Respondents.ToString();


                        double cat1 = Math.Round((SEPFList.Where(m => m.SisCategoryID == 1 && m.Result != 0).Average(m => m.Result) * .50), 2);
                        double cat2 = Math.Round((SEPFList.Where(m => m.SisCategoryID == 2 && m.Result != 0).Average(m => m.Result) * .25), 2);
                        double cat3 = Math.Round((SEPFList.Where(m => m.SisCategoryID == 3 && m.Result != 0).Average(m => m.Result) * .25), 2);

                        lblOverallMean.Content = Math.Round(cat1 + cat2 + cat3, 2).ToString();

                        dgSkill.ItemsSource = SEPFList.Where(m => m.SisCategoryID == 1).ToList();
                        lblMeanSkill.Content = "MEAN: " + Math.Round(SEPFList.Where(m => m.SisCategoryID == 1 && m.Result != 0).Average(m => m.Result), 2).ToString();

                        dgWork.ItemsSource = SEPFList.Where(m => m.SisCategoryID == 2).ToList();
                        lblMeanWork.Content = "MEAN: " + Math.Round(SEPFList.Where(m => m.SisCategoryID == 2 && m.Result != 0).Average(m => m.Result), 2).ToString();

                        dgClassroom.ItemsSource = SEPFList.Where(m => m.SisCategoryID == 3).ToList();
                        lblMeanClassroom.Content = "MEAN: " + Math.Round(SEPFList.Where(m => m.SisCategoryID == 3 && m.Result != 0).Average(m => m.Result), 2).ToString();
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

        public void GetComment(int FacultyID)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var com = db.sisSHS.Where(m => m.StudentSchedule.Schedule.Faculty.FacultyID == FacultyID).ToList();
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

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (SEPFList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 17;
                x.SisPerFacultyList = SEPFList;
                x.SisComList = SCList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    public class SisEvaluationPerFacultyList
    {
        public string FacultyName { get; set; }
        public string Position { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }

        public int Respondents { get; set; }

        public int SisCategoryID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionDescription { get; set; }
        public double Result { get; set; }
    }
}
