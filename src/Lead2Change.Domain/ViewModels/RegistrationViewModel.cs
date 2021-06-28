using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// This is an example and not necessarily how a Goal should be used.
        /// </summary>
        [Required]
        public string Goal1 { get; set; }
    }
}
