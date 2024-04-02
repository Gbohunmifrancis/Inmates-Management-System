using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Inmate : BaseEntity
    {
        public string? CivilId { get; set; }
        public string? FileNumber { get; set; }
        public string? FullName { get; set; }
        public string? Offence { get; set; }
        public string? Address { get; set; }
        public string? YearsToSpend { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Mugshot { get; set; }    
        public string? Other { get; set; }




        //Relationship many to one

        public GeneralDepartment? GeneralDepartment { get; set; }
        public int GeneralDepartmentId { get; set; }

        public Department? Department { get; set; }
        public int DepartmentId { get; set; }


    }
}
