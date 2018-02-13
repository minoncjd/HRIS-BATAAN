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
    /// Interaction logic for EmployeeLevelRank.xaml
    /// </summary>
    public partial class EmployeeLevelRank : MetroWindow
    {
        List<LevelList> LList = new List<LevelList>();
        List<EmployeeLevelList> ELList = new List<EmployeeLevelList>();

        public EmployeeLevelRank()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetLevel();
        }

        public void GetLevel()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    LList = new List<LevelList>();

                    var lvl = db.EmployeeLevels.ToList();

                    foreach (var x in lvl)
                    {
                        LevelList l = new LevelList();
                        l.LevelID = x.EmployeeLevelID;
                        l.LevelDescription = x.EmployeeLevel1;
                        LList.Add(l);
                    }
                    dgLvl.ItemsSource = LList.OrderBy(m => m.LevelDescription);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgEmpLvl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSave.IsEnabled = true;
                btnAdd.IsEnabled = false;
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgLvl.SelectedItem != null)
                    {
                        ELList = new List<EmployeeLevelList>();
                        var selectedItem = (LevelList)(dgLvl.SelectedItem);

                        lblPos.Content = "List of Current Employees in the Level of: " + selectedItem.LevelDescription;
                        //Level
                        var lvl = db.EmployeeLevels.Find(selectedItem.LevelID);

                        txtEmpLvl.Text = lvl.EmployeeLevel1;
                        //Employee in the Level

                        var empdept = db.Employees.Where(m => m.EmployeeLevel.EmployeeLevelID == selectedItem.LevelID && m.Archive == false).ToList();

                        foreach (var x in empdept)
                        {
                            EmployeeLevelList edl = new EmployeeLevelList();
                            edl.EmployeeID = x.EmployeeID;
                            edl.EmployeeNo = x.EmployeeNo;
                            edl.EmployeeName = x.LastName.ToUpper() + ", " + x.FirstName.ToUpper();
                            edl.EmployeePosition = x.EmployeePosition.EmployeePositionName;
                            edl.EmployeeDesignation = x.EmployeeDesignation1.EmployeeDesignationName;
                            edl.EmployeeStatus = x.EmployeeStatu.EmployeeStatusName;
                            ELList.Add(edl);
                        }
                        dgEmpLvl.ItemsSource = ELList.OrderBy(m => m.EmployeeName);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (!String.IsNullOrEmpty(txtEmpLvl.Text))
                    {
                        EmployeeLevel el = new EmployeeLevel();
                        el.EmployeeLevel1 = txtEmpLvl.Text;
                        db.EmployeeLevels.Add(el);
                        db.SaveChanges();
                        MessageBox.Show("Add Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetLevel();
                    }
                    else
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgLvl.SelectedItem != null)
                {
                    using (var db = new LetranIntegratedSystemEntities())
                    {
                        var selectedItem = (LevelList)(dgLvl.SelectedItem);

                        var findlvl = db.EmployeeLevels.Find(selectedItem.LevelID);

                        findlvl.EmployeeLevel1 = txtEmpLvl.Text;
                        db.SaveChanges();
                        MessageBox.Show("Update Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetLevel();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetLevel();
            TextClear();
        }

        public void TextClear()
        {
            dgLvl.SelectedItem = null;
            txtEmpLvl.Text = "";
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;
        }

        public class LevelList
        {
            public int LevelID { get; set; }
            public string LevelDescription { get; set; }
        }

        public class EmployeeLevelList
        {
            public int EmployeeID { get; set; }
            public string EmployeeNo { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeePosition { get; set; }
            public string EmployeeDesignation { get; set; }
            public string EmployeeStatus { get; set; }
        }
    }
}
