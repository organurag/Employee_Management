using Microsoft.AspNetCore.Mvc;
using Employee_Client.Models;
using Newtonsoft.Json;

namespace Employee_Client.Controllers
{
    public class EmployeeController : Controller
    {

        
        
            private readonly HttpClient _httpClient;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7187/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Employees");
            if (response.IsSuccessStatusCode)
            {
              string jsonResult = response.Content.ReadAsStringAsync().Result;
              var employees = JsonConvert.DeserializeObject<List<Employee>>(jsonResult);
              return View(employees);
            }
            else
            {
              return View("Error");
            }
        }

        public IActionResult Details(int id) 
        {
            var response = _httpClient.GetAsync($"Employees/{id}").Result;
            if(response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                var employee = JsonConvert.DeserializeObject<Employee>(jsonResult);
                return View(employee);
            }
            return View("Error");

        }

        [HttpGet]
        public IActionResult AddOrUpdate(int? id)
        {
            if (id == null)
            {
                // If id is null, it means we're adding a new employee, so create a new instance of the Employee model
                return View(new Employee());
            }
            else
            {
                var response = _httpClient.GetAsync($"Employees/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    var employee = JsonConvert.DeserializeObject<Employee>(jsonResult);
                    return View(employee);
                }
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult AddOrUpdate(Employee employee)
        {
            var response = _httpClient.PostAsJsonAsync<Employee>("employees", employee).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();



            //if (employee.EmployeeId == 0)
            //{
            //    var response = _httpClient.PostAsJsonAsync<Employee>("employees", employee).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    return View();
            //}
            //else
            //{
            //    int id = employee.EmployeeId;
            //    var response = _httpClient.GetAsync($"Product/{id}").Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string jsonr = response.Content.ReadAsStringAsync().Result;
            //        employee = JsonConvert.DeserializeObject<Employee>(jsonr);
            //        return View(employee);
            //    }
            //    return View(employee);

            //}




        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var response = _httpClient.GetAsync($"Employees/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                var employee = JsonConvert.DeserializeObject<Employee>(jsonResult);
                return View(employee);
            }
            return View("Error");
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id) 
        {
            
            var response = _httpClient.DeleteAsync($"Employees/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View() ;
        }



    }


}

