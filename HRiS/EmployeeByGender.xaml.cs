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
using MahApps.Metro.Controls;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for EmployeeByGender.xaml
    /// </summary>
    public partial class EmployeeByGender : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmployeeGenderList> EGList = new List<EmployeeGenderList>();
        public EmployeeByGender()
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
                db = new LetranIntegratedSystemEntities();
                EGList = new List<EmployeeGenderList>();

                var emp = db.GetHRiSListbyGender().ToList();

                foreach (var x in emp)
                {
                    EmployeeGenderList egl = new EmployeeGenderList();
                    egl.Department = x.AcaDepartmentName;
                    egl.EmployeeName = x.EmployeeName;
                    egl.Gender = x.Gender;
                    EGList.Add(egl);
                }
                EGList = EGList.OrderBy(m => m.Department).ToList();
                ListCollectionView eglcollection = new ListCollectionView(EGList);
                eglcollection.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
                dgEmployeeGender.ItemsSource = eglcollection;
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
                EGList = new List<EmployeeGenderList>();

                var emp = db.GetHRiSListbyGender().ToList();

                foreach (var x in emp)
                {
                    EmployeeGenderList egl = new EmployeeGenderList();
                    egl.Department = x.AcaDepartmentName;
                    egl.EmployeeName = x.EmployeeName;
                    egl.Gender = x.Gender;
                    EGList.Add(egl);
                }
                if (!String.IsNullOrEmpty(cbDepartment.Text))
                {
                    string dept = cbDepartment.SelectedValue.ToString();

                    EGList = EGList.Where(m => m.Department == dept).ToList();
                }
                EGList = EGList.OrderBy(m => m.Department).ToList();
                ListCollectionView eglcollection = new ListCollectionView(EGList);
                eglcollection.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
                dgEmployeeGender.ItemsSource = eglcollection;
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
            if (EGList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 15;
                x.Report15 = EGList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
    public class EmployeeGenderList
    {
        public string Department { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
    }
}
