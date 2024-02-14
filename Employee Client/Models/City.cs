namespace Employee_Client.Models;



public partial class City
{
   
    public int CityId { get; set; }

    
    public string CityName { get; set; }

    
    public string CityDesc { get; set; }

    public int? DistrictId { get; set; }

   
    public virtual District District { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}