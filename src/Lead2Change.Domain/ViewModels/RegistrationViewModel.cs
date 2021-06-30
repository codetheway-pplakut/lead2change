using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class RegistrationViewModel
    {
        /*[Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }*/

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

        /// <summary>
        /// This is an example and not necessarily how a Goal should be used.
        /// </summary>
        //[Required]
        //public string Goal1 { get; set; }
    }
}
