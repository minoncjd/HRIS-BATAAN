﻿using HRiS.Model;
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
    /// Interaction logic for AddEmployeeHistory.xaml
    /// </summary>
    public partial class AddEmployeeHistory : MetroWindow
    {
        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();

        public AddEmployeeHistory()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var empid = Convert.ToInt32(cbEmployee.SelectedValue);
                    var employee = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var dept = db.AcademicDepartments.Where(m => m.AcaDeptID == employee.EmployeeDepartmentID).FirstOrDefault();
                    var pos = db.EmployeePositions.Where(m => m.EmployeePositionID == employee.EmployeePositionID).FirstOrDefault();
                    EmployeeHistory eh = new EmployeeHistory();
                    eh.EmployeeID = empid;
                    eh.DepartmentID = dept.AcaDeptID;
                    eh.EmployeePositionID = pos.EmployeePositionID;
                    eh.StartDate = startDate.SelectedDate;
                    eh.Remark = txtRemark.Text;
                    eh.EndDate = endDate.SelectedDate;
                    db.EmployeeHistories.Add(eh);
                    db.SaveChanges();
                    Clear();
                    MessageBox.Show("Successfully added.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadComboBox()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    EList = new List<HRiSClass.EmpCombo>();

                    var emp = db.Employees.Where(m => m.Archive == false).ToList();

                    foreach (var i in emp)
                    {
                        HRiSClass.EmpCombo ec = new HRiSClass.EmpCombo();
                        ec.EmployeeID = i.EmployeeID;
                        ec.EmployeeNumber = i.EmployeeNo;
                        ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                        EList.Add(ec);
                    }

                    cbEmployee.ItemsSource = EList.OrderBy(m => m.EmployeeName);
                    cbEmployee.DisplayMemberPath = "EmployeeName";
                    cbEmployee.SelectedValuePath = "EmployeeID";

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void Clear()
        {
           
            cbEmployee.Text = "";
            startDate.SelectedDate = null;
            endDate.SelectedDate = null;
        }

     

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }
    }
}
