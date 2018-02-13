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
    /// Interaction logic for EmployeeByAge.xaml
    /// </summary>
    public partial class EmployeeByAge : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmployeeAgeList> EAList = new List<EmployeeAgeList>();
        public EmployeeByAge()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db = new LetranIntegratedSystemEntities();
            GridRefresh();
            cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaDepartmentName).ToList();
            cbDepartment.DisplayMemberPath = "AcaDepartmentName";
            cbDepartment.SelectedValuePath = "AcaDepartmentName";
        }

        public void GridRefresh()
        {
            try
            {
                
             var db   = new LetranIntegratedSystemEntities();
                EAList = new List<EmployeeAgeList>();

                var emp = db.GetHRiSListbyAge().ToList();

                foreach (var x in emp)
                {
                    EmployeeAgeList eal = new EmployeeAgeList();
                if (x.Age > 0)
                {
                    eal.Age = x.Age.Value;
                }
                else
                {
                    eal.Age = 0;
                }

                if (x.Birthday != null)
                {
                    eal.Birthday = x.Birthday.Value;
                }
                else
                {
                    eal.Birthday = DateTime.Now;
                }

       
                    eal.Department = x.AcaDepartmentName;
                    eal.EmployeeName = x.EmployeeName;
                    EAList.Add(eal);

                }
                EAList = EAList.OrderBy(m => m.Department).ToList();
                ListCollectionView ealcollection = new ListCollectionView(EAList);
                ealcollection.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
                dgEmployeeAge.ItemsSource = ealcollection;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                EAList = new List<EmployeeAgeList>();

                var emp = db.GetHRiSListbyAge().ToList();

                foreach (var x in emp)
                {
                    EmployeeAgeList eal = new EmployeeAgeList();
                    if (x.Age > 0)
                    {
                        eal.Age = x.Age.Value;
                    }
                    else
                    {
                        eal.Age = 0;
                    }

                    if (x.Birthday != null)
                    {
                        eal.Birthday = x.Birthday.Value;
                    }
                    else
                    {
                        eal.Birthday = DateTime.Now;
                    }
                    eal.Department = x.AcaDepartmentName;
                    eal.EmployeeName = x.EmployeeName;
                    EAList.Add(eal);

                }
                if (!String.IsNullOrEmpty(cbDepartment.Text))
                {
                    string dept = cbDepartment.SelectedValue.ToString();

                    EAList = EAList.Where(m => m.Department == dept).ToList();
                }
                EAList = EAList.OrderBy(m => m.Department).ToList();
                ListCollectionView ealcollection = new ListCollectionView(EAList);
                ealcollection.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
                dgEmployeeAge.ItemsSource = ealcollection;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GridRefresh();
        }
      

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (EAList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 14;
                x.Report14 = EAList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    public class EmployeeAgeList
    {
        public string Department { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Birthday { get; set; } 
        public int Age { get; set; }
    }
}
