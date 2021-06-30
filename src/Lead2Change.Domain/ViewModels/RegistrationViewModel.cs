using System;
using Lead2Change.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class RegistrationViewModel
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
        public string CollegesList { get; set; } // List
        public bool CollegeEssayStatus { get; set; }
        public bool CollegeEssayHelp { get; set; }
        public string FirstChoiceCollege { get; set; } // Used to be CollegeChoice List
        public string SecondChoiceCollege { get; set; } // Used to be CollegeChoice List
        public string ThirdChoiceCollege { get; set; } // Used to be CollegeChoice List
        public bool TradeSchoolStatus { get; set; }
        public string TradeSchoolsList { get; set; } // List
        public bool ArmedForcesStatus { get; set; }
        public string ArmedForcesBranch { get; set; } // List
        public bool WorkStatus { get; set; }
        public string CareerPathList { get; set; } // List
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
