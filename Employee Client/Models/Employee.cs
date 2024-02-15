

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Client.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [Required]

        [StringLength(100)]        
        public string EmployeeDesignation { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? EmployeeSalary { get; set; }
        [Required]
        public int? StateId { get; set; }
        [Required]
        public int? DistrictId { get; set; }
        [Required]
        public int? CityId { get; set; }

        [StringLength(255)]
        [Required]
        public string EmployeeAddress { get; set; }

        [StringLength(15)]
        [Required]
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
