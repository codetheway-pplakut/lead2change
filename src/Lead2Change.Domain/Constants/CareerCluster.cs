using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lead2Change.Domain.Constants
{
    public enum CareerCluster
    {
        [Description("Agriculture, Food & Natural Resources")]
        Agriculture = 1,
        [Description("Architecture & Construction")]
        Architecture = 2,
        [Description("Arts, Audio, Video Technology")]
        MediaTechnology = 3,
        [Description("Business Management & Administration")]
        Management = 4,
        [Description("Education & Training")]
        Education = 5,
        [Description("Finance")]
        Finance = 6,
        [Description("Government & Public Administration")]
        GovernmentAdministration = 7,
        [Description("Health Science")]
        HealthScience = 8,
        [Description("Hospitality & Tourism")]
        Tourism = 9,
        [Description("Human Services")]
        HumanServices = 10,
        [Description("Information Technology")]
        InfoTechnology = 11,
        [Description("Law, Public Safety, Correction & Security")]
        LawSecurity = 12,
        [Description("Manufacturing")]
        Manufacturing = 13,
        [Description("Marketing")]
        Marketing = 14,
        [Description("Science, Technology, Engineering, Mathematics")]
        Engineering = 15,
        [Description("Transportation, Distribution & Logistics")]
        Distribution = 16
    }
}
