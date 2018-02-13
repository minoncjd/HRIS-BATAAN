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
    /// Interaction logic for EmployeeJobPosition.xaml
    /// </summary>
    public partial class EmployeeJobPosition : MetroWindow
    {
        List<EmployeePositionList> EPList = new List<EmployeePositionList>();
        List<EmployeeJobPositionList> EJPList = new List<EmployeeJobPositionList>();

        public EmployeeJobPosition()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetJobPosition();
            using (var db = new LetranIntegratedSystemEntities())
            {
                cbEmpRank.ItemsSource = db.EmployeeRanks.ToList();
                cbEmpRank.DisplayMemberPath = "EmployeeRankName";
                cbEmpRank.SelectedValuePath = "EmployeeRankID";
            }
        }

        public void GetJobPosition()
        {
            try
            {
                EPList = new List<EmployeePositionList>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var jobpos = db.EmployeePositions.ToList();

                    foreach (var x in jobpos)
                    {
                        EmployeePositionList epl = new EmployeePositionList();
                        epl.PositionID = x.EmployeePositionID;
                        if (x.EmployeeRank != null)
                        {
                            epl.PositionRankID = x.EmployeeRank.EmployeeRankID;
                            epl.PositionRank = x.EmployeeRank.EmployeeRankName;
                        }
                        epl.Description = x.Description;
                        epl.IsActive = x.Active;
                        epl.PositionName = x.EmployeePositionName;
                        EPList.Add(epl);
                    }
                    dgJobPosition.ItemsSource = EPList.OrderBy(m=>m.PositionName);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void dgJobPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSave.IsEnabled = true;
                btnAdd.IsEnabled = false;
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgJobPosition.SelectedItem != null)
                    {
                        EJPList = new List<EmployeeJobPositionList>();
                        var selectedItem = (EmployeePositionList)(dgJobPosition.SelectedItem);

                        lblPos.Content = "List of Current Employees in Position of: " + selectedItem.PositionName;

                        //Job Positions
                        txtPosName.Text = selectedItem.PositionName;
                        txtDescription.Text = selectedItem.Description;
                        cbEmpRank.SelectedValue = selectedItem.PositionRankID;
                        if (selectedItem.IsActive == true)
                            cbIsActive.Text = "True";
                        else
                            cbIsActive.Text = "False";

                        //Employee in that position

                        var emppos = db.Employees.Where(m => m.EmployeePositionID == selectedItem.PositionID && m.Archive == false).ToList();

                        foreach (var x in emppos)
                        {
                            EmployeeJobPositionList ejpl = new EmployeeJobPositionList();
                            ejpl.EmployeeID = x.EmployeeID;
                            ejpl.EmployeeNo = x.EmployeeNo;
                            ejpl.EmployeeName = x.LastName.ToUpper() + ", " + x.FirstName.ToUpper();
                            ejpl.EmployeeDepartment = x.AcademicDepartment.AcaDepartmentName;
                            ejpl.EmployeeDesignation = x.EmployeeDesignation1.EmployeeDesignationName;
                            ejpl.EmployeeStatus = x.EmployeeStatu.EmployeeStatusName;
                            EJPList.Add(ejpl);
                        }
                        dgEmpPos.ItemsSource = EJPList.OrderBy(m => m.EmployeeName);

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
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgJobPosition.SelectedItem != null)
                    {
                        var selectedItem = (EmployeePositionList)(dgJobPosition.SelectedItem);

                        var findpos = db.EmployeePositions.Find(selectedItem.PositionID);

                        findpos.EmployeePositionName = txtPosName.Text;
                        findpos.Description = txtDescription.Text;
                        if (!String.IsNullOrEmpty(cbEmpRank.Text))
                            findpos.EmployeeRankID = Convert.ToInt32(cbEmpRank.SelectedValue);
                        if (cbIsActive.Text == "True")
                            findpos.Active = true;
                        else
                            findpos.Active = false;

                        db.SaveChanges();
                        MessageBox.Show("Update Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetJobPosition();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TextClear()
        {
            dgJobPosition.SelectedItem = null;
            txtPosName.Text = "";
            txtDescription.Text = "";
            cbEmpRank.Text = "";
            cbIsActive.Text = "";
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetJobPosition();
            TextClear();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (!String.IsNullOrEmpty(txtPosName.Text) || !String.IsNullOrEmpty(cbIsActive.Text))
                    {
                        EmployeePosition ep = new EmployeePosition();
                        ep.EmployeePositionName = txtPosName.Text;
                        ep.Description = txtDescription.Text;
                        if (!String.IsNullOrEmpty(cbEmpRank.Text))
                            ep.EmployeeRankID = Convert.ToInt32(cbEmpRank.SelectedValue);
                        if (cbIsActive.Text == "True")
                            ep.Active = true;
                        else
                            ep.Active = false;
                        db.EmployeePositions.Add(ep);
                        db.SaveChanges();
                        MessageBox.Show("Add Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetJobPosition();
                    }
                    else
                    {
                        MessageBox.Show("Required fields cannot be empty.","System Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class EmployeePositionList
        {
            public int PositionID { get; set; }
            public int? PositionRankID { get; set; }
            public string PositionRank { get; set; }
            public string Description { get; set; }
            public bool? IsActive { get; set; }
            public string PositionName { get; set; }
        }

        public class EmployeeJobPositionList
        {
            public int EmployeeID { get; set; }
            public string EmployeeNo { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeDepartment { get; set; }
            public string EmployeeDesignation { get; set; }
            public string EmployeeStatus { get; set; }
        }

    }
}
