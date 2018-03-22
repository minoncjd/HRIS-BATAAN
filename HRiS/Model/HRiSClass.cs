using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRiS.Model
{
    public class HRiSClass
    {
        public class EmployeeAddEducationList
        {
            public string EducationType { get; set; }
            public string Institution { get; set; }
            public string Degree { get; set; }
            public string Graduation { get; set; }
            public string Award { get; set; }
        }

        public class EmployeeAddSeminarList
        {
            public string Seminar { get; set; }
            public string Venue { get; set; }
            public string InclusiveDate { get; set; }
        }

        public class EmployeeShiftList
        {
            public int EmployeeID { get; set; }
            public string EmployeeNo { get; set; }
            public string EmployeeName { get; set; }
            public string ShiftCode { get; set; }
            public string Department{ get; set; }

        }

        public class EmployeeAddWorkList
        {
            public string Company { get; set; }
            public string Position { get; set; }
            public string InclusiveDate { get; set; }
            public string LastSalary { get; set; }
            public string Reason { get; set; }
        }

        public class LeaveCancellationList
        {
            public int LeaveCancelID { get; set; }
            public int LeaveID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public DateTime DateFiled { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Type { get; set; }
            public string Reason { get; set; }
            public decimal Days { get; set; }
            public string Status { get; set; }
            public string LeaveCancelStatus { get; set; }
            public DateTime CancelDate { get; set; }
            public bool IsActive { get; set; }
        }

        public class ChangeVacationLeaveList
        {
            public int VLID { get; set; }
            public int VLDetailsID { get; set; }
            public int LeaveID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal Days { get; set; }
            public string Status { get; set; }
            public DateTime DateFiled { get; set; }
            public DateTime NewStartDate { get; set; }
            public DateTime NewEndDate { get; set; }
            public string Reason { get; set; }
        }

        public class NoticeOfAbscenceList
        {
            public int NoticeAbsenceID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public DateTime DateFiled { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Reason { get; set; }
            public string Days { get; set; }
        }

        public class OvertimeRequestList
        {
            public int OTID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public DateTime DateFiled { get; set; }

            public DateTime OTDate { get; set; }
            public string Reason { get; set; }
            public string Time  { get; set; }
          

        }

        public class AccountManagementList
        {
            public string AccountID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public string Department { get; set; }
            public string EmployeePosition { get; set; }
            public string Email { get; set; }
        }
        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }

        public class EmploymentCertificateList
        {
            public int ApplicantID { get; set; }
            public string EmployeeName { get; set; }
            public int DepartmentID { get; set; }
            public string Department { get; set; }
            public string Sendee { get; set; }
            public string SendeeAddress { get; set; }
            public DateTime DateFile { get; set; }
            public DateTime DateNeeded { get; set; }
            public string Purpose { get; set; }
        }

        public class OffCampusActivityList
        {
            public int OffCampusActivityID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public string Reason { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
            public DateTime DateFiled { get; set; }
        }
        public class PostTrainingList
        {
            public int PostTrainingID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public int ActivityID { get; set; }
            public string ActivityType { get; set; }
            public string Title { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
            public string Venue { get; set; }
            public string Others { get; set; }
            public string Background { get; set; }
            public string Expectation { get; set; }
            public string ResourcePerson { get; set; }
            public string Approach { get; set; }
            public string KeyLearning { get; set; }
            public string Issues { get; set; }
            public string Generalization { get; set; }
            public DateTime DateFile { get; set; }
        }

        public class  MakeUpClassList
        {
            public int MakeUpClassID { get; set; }
            public int MakeUpClassDetailsID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public DateTime DateFile { get; set; }
            public string Reason { get; set; }
        }

        public class SubstituteClassList
        {
            public int SubstituteClassID { get; set; }
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public int SubEmployeeID { get; set; }
            public string SubEmployeeNumber { get; set; }
            public string SubEmployeeName { get; set; }
            public DateTime DateFile { get; set; }
            public string Reason { get; set; }
        }
    }
}
