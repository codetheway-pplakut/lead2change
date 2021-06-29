using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public DateTime StudentDateOfBirth { get; set; }
        public string StudentAddress { get; set; }
        public string StudentApartmentNumber { get; set; }
        public string StudentCity { get; set; }
        public int StudentZipCode { get; set; }
        public int StudentHomePhone { get; set; }
        public int StudentCellPhone { get; set; }
        public string StudentEmail { get; set; }
        public string StudentCareerPath { get; set; }
        public string StudentCareerInterest { get; set; }


        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string Address { get; set; }

        public string ParentCity { get; set; }
        public string ParentState { get; set; }
        public int ParentZipCode { get; set; }
        public int ParentHomePhone { get; set; }
        public int ParentCellPhone { get; set; }
        public string ParentEmail { get; set; }

        public bool KnowGuidanceCounselor { get; set; }
        public string GuidanceCounselorName { get; set; }
        public bool MeetWithGuidanceCounselor { get; set; }
        public string HowOftenMeetWithGuidanceCounselor { get; set; }
        public string DiscussWithGuidanceCounselor { get; set; }

        public string PlanAfterHighSchool { get; set; }
        public bool CollegeApplicationStatus { get; set; }
        public List<string> CollegesList { get; set; }
        public bool CollegeEssayStatus { get; set; }
        public bool CollegeEssayHelp { get; set; }
        public Array RankedColleges { get; set; }
        public bool TradeSchoolStatus { get; set; }
        public List<string> TradeSchoolsList { get; set; }
        public bool ArmedForcesStatus { get; set; }
        public List<string> ArmedForcesBranch { get; set; }
        public bool WorkStatus { get; set; }
        public List<string> CareerPathList { get; set; }
        public string OtherPlans { get; set; }

        public DateTime PACTTestDate { get; set; }
        public int PACTTestScore { get; set; }

        public DateTime SATTestDate { get; set; }
        public int SATTestScore { get; set; }

        public DateTime ACTTestDate { get; set; }
        public int ACTTestScore { get; set; }


        public bool PrepClassRequired { get; set; }
        public bool AssistanceForForms { get; set; }
        public bool FinancialAidProcessComplete { get; set; }
        public string StudentSignature { get; set; }
        public DateTime StudentSignatureDate { get; set; }
        public string ParentSignature { get; set; }
        public DateTime ParentSignatureDate { get; set; }

    }
}
