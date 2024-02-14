namespace Employee_Client.Models;


public partial class District
{
  
    public int DistrictId { get; set; }

 
    public string DistrictName { get; set; }

    public int? StateId { get; set; }

   
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

   
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

   
    public virtual State State { get; set; }
}