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
    /// Interaction logic for EmployeeByDepartment.xaml
    /// </summary>
    public partial class EmployeeByDepartment : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmployeeDepartmentList> EDList = new List<EmployeeDepartmentList>();

        public EmployeeByDepartment()
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

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                db = new LetranIntegratedSystemEntities();
                EDList = new List<EmployeeDepartmentList>();
                var dept = cbDepartment.SelectedValue.ToString();

                var emp = db.GetHRiSEmployeebyDepartment().ToList();

                foreach (var x in emp)
                {
                    EmployeeDepartmentList edl = new EmployeeDepartmentList();
                    edl.Department = x.AcaDepartmentName;
                    edl.EmployeeDesignation = x.EmployeeStatusName;
                    edl.EmployeeLevel = x.EmployeeLevel;
                    edl.EmployeeName = x.EmployeeName;
                    edl.EmployeePosition = x.EmployeePositionName;
                    EDList.Add(edl);
                }
                EDList = EDList.OrderBy(m => m.Department).Where(m=>m.Department == dept).ToList();
                ListCollectionView edlcollection = new ListCollectionView(EDList);
                edlcollection.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
                dgEmployeeDepartment.ItemsSource = edlcollection;
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GridRefresh();
        }
        
        public void GridRefresh()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                EDList = new List<EmployeeDepartmentList>();

                var emp = db.GetHRiSEmployeebyDepartment().ToList();

                foreach(var x in emp)
                {
                    EmployeeDepartmentList edl = new EmployeeDepartmentList();
                    edl.Department = x.AcaDepartmentName;
                    edl.EmployeeDesignation = x.EmployeeStatusName;
                    edl.EmployeeLevel = x.EmployeeLevel;
                    edl.EmployeeName = x.EmployeeName.ToUpper();
                    edl.EmployeePosition = x.EmployeePositionName;
                    EDList.Add(edl);
                }
                EDList = EDList.OrderBy(m => m.Department).ToList();
                ListCollectionView edlcollection = new ListCollectionView(EDList);
                edlcollection.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
                dgEmployeeDepartment.ItemsSource = edlcollection;
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btnPrintRpt_Click(object sender, RoutedEventArgs e)
        {
            if (EDList.Count > 0)
            {
                PrintWindow x = new PrintWindow();
                x.rptid = 11;
                x.Report11 = EDList;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("List cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    public class EmployeeDepartmentList
    {
        public string Department{ get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeDesignation { get; set; }
        public string EmployeeLevel { get; set; }
    }
}
