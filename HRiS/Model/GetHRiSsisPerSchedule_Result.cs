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
    
    public partial class GetHRiSsisPerSchedule_Result
    {
        public string FacultyName { get; set; }
        public string SubjectDescription { get; set; }
        public string SectionName { get; set; }
        public string Days { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public string RoomName { get; set; }
        public Nullable<int> Respondents { get; set; }
        public Nullable<double> Result { get; set; }
        public string QuestionNo { get; set; }
    }
}
