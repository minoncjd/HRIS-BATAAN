//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRiS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.AttendanceElementaries = new HashSet<AttendanceElementary>();
            this.AttendanceKinders = new HashSet<AttendanceKinder>();
            this.AttendanceSHS = new HashSet<AttendanceSH>();
            this.BackAccounts = new HashSet<BackAccount>();
            this.BasicEducationOthers = new HashSet<BasicEducationOther>();
            this.ChangeOfGradeDoctorals = new HashSet<ChangeOfGradeDoctoral>();
            this.ChangeOfGradeMasterals = new HashSet<ChangeOfGradeMasteral>();
            this.CounselingRecords = new HashSet<CounselingRecord>();
            this.Discounts = new HashSet<Discount>();
            this.DMCMs = new HashSet<DMCM>();
            this.GradeColleges = new HashSet<GradeCollege>();
            this.GradeCredits = new HashSet<GradeCredit>();
            this.GradeDoctorals = new HashSet<GradeDoctoral>();
            this.GradeElementaries = new HashSet<GradeElementary>();
            this.GradeJHS = new HashSet<GradeJH>();
            this.GradeKinders = new HashSet<GradeKinder>();
            this.GradeKinderRemarks = new HashSet<GradeKinderRemark>();
            this.GradeMasterals = new HashSet<GradeMasteral>();
            this.GradeSHS = new HashSet<GradeSH>();
            this.GuidanceImportantPersons = new HashSet<GuidanceImportantPerson>();
            this.GuidanceOtherPeoples = new HashSet<GuidanceOtherPeople>();
            this.GuidancePersonalDescriptions = new HashSet<GuidancePersonalDescription>();
            this.GuidanceProblemChecklists = new HashSet<GuidanceProblemChecklist>();
            this.GuidanceReferrals = new HashSet<GuidanceReferral>();
            this.GuidanceSiblings = new HashSet<GuidanceSibling>();
            this.GuidanceStudentEvents = new HashSet<GuidanceStudentEvent>();
            this.GuidanceStudentSeminars = new HashSet<GuidanceStudentSeminar>();
            this.GuidanceStudentTests = new HashSet<GuidanceStudentTest>();
            this.GuidanceSurveyAnswers = new HashSet<GuidanceSurveyAnswer>();
            this.Payments = new HashSet<Payment>();
            this.RegistrarLogs = new HashSet<RegistrarLog>();
            this.Student_Requirement = new HashSet<Student_Requirement>();
            this.Student_Curriculum = new HashSet<Student_Curriculum>();
            this.Student_Section = new HashSet<Student_Section>();
            this.StudentClearances = new HashSet<StudentClearance>();
            this.StudentMaxUnits = new HashSet<StudentMaxUnit>();
            this.StudentWithQualifyings = new HashSet<StudentWithQualifying>();
        }
    
        public int StudentID { get; set; }
        public string StudentNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public Nullable<int> YearEntry { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public byte[] Photo { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string LandlineNo { get; set; }
        public string MobileNo { get; set; }
        public string CivilStatus { get; set; }
        public string Religion { get; set; }
        public string FatherName { get; set; }
        public string FatherContact { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherEmployer { get; set; }
        public string FatherEmail { get; set; }
        public string MotherName { get; set; }
        public string MotherContact { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherEmployer { get; set; }
        public string MotherEmail { get; set; }
        public string GuardianName { get; set; }
        public string GuardianAddress { get; set; }
        public string RelationshipToGuardian { get; set; }
        public string GuardianContactNo { get; set; }
        public string FormerSchool { get; set; }
        public string FormerSchoolAddress { get; set; }
        public string GraduationYear { get; set; }
        public Nullable<double> GPA { get; set; }
        public string HonorsReceived { get; set; }
        public string SchoolLastAttended { get; set; }
        public string SchoolLastAttendedCourse { get; set; }
        public string Encoder { get; set; }
        public Nullable<System.DateTime> DateEnconded { get; set; }
        public string ContactNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BDORefNo { get; set; }
        public string IDCode { get; set; }
        public string IDCode2 { get; set; }
        public string EntranceData { get; set; }
        public string SO_Number { get; set; }
        public Nullable<System.DateTime> SO_Date { get; set; }
        public string SO_Serial { get; set; }
        public string Birthplace { get; set; }
        public string EmailAddress { get; set; }
        public string Age { get; set; }
        public string IntermediateSchool { get; set; }
        public Nullable<System.DateTime> IntermediateSchoolGradYear { get; set; }
        public string LRN { get; set; }
        public string ElementarySchool { get; set; }
        public Nullable<System.DateTime> ElementarySchoolGradYear { get; set; }
    
        public virtual ARMessage ARMessage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceElementary> AttendanceElementaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceKinder> AttendanceKinders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceSH> AttendanceSHS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BackAccount> BackAccounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasicEducationOther> BasicEducationOthers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeOfGradeDoctoral> ChangeOfGradeDoctorals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeOfGradeMasteral> ChangeOfGradeMasterals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CounselingRecord> CounselingRecords { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discount> Discounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DMCM> DMCMs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeCollege> GradeColleges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeCredit> GradeCredits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeDoctoral> GradeDoctorals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeElementary> GradeElementaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeJH> GradeJHS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeKinder> GradeKinders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeKinderRemark> GradeKinderRemarks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeMasteral> GradeMasterals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeSH> GradeSHS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceImportantPerson> GuidanceImportantPersons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceOtherPeople> GuidanceOtherPeoples { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidancePersonalDescription> GuidancePersonalDescriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceProblemChecklist> GuidanceProblemChecklists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceReferral> GuidanceReferrals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceSibling> GuidanceSiblings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceStudentEvent> GuidanceStudentEvents { get; set; }
        public virtual GuidanceStudentInfo GuidanceStudentInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceStudentSeminar> GuidanceStudentSeminars { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceStudentTest> GuidanceStudentTests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceSurveyAnswer> GuidanceSurveyAnswers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual Period Period { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrarLog> RegistrarLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Requirement> Student_Requirement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Curriculum> Student_Curriculum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Section> Student_Section { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentClearance> StudentClearances { get; set; }
        public virtual StudentDemographic StudentDemographic { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentMaxUnit> StudentMaxUnits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentWithQualifying> StudentWithQualifyings { get; set; }
    }
}