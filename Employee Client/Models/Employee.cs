

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Client.Models
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }

        [StringLength(100)]
        
        public string EmployeeName { get; set; }

        [StringLength(100)]
        
        public string EmployeeDesignation { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? EmployeeSalary { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        [StringLength(255)]
        
        public string EmployeeAddress { get; set; }

        [StringLength(15)]
        
        public string EmployeeMobileNo { get; set; }

        [ForeignKey("CityId")]
        [InverseProperty("Employees")]
        public virtual City City { get; set; }

        [ForeignKey("DistrictId")]
        [InverseProperty("Employees")]
        public virtual District District { get; set; }

        [ForeignKey("StateId")]
        [InverseProperty("Employees")]
        public virtual State State { get; set; }
    }
}
