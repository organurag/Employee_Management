using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.Json.Serialization;
using System.Text.Json;
using NuGet.Protocol;

namespace Employee_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
         EmployeeDbContext _db;

        public EmployeesController(EmployeeDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var employees = _db.Employees.Include(e => e.City).Include(e => e.District).Include(e => e.State).ToList();
            var jsonResult = JsonSerializer.Serialize(employees, options);
            return Content(jsonResult, "application/json");
           // var employees = _db.Employees.Include(e => e.City).Include(e => e.District).Include(e => e.State).ToList();
            //return Ok(employees);
        }

        [HttpGet]
        [Route("{id?}")]
        public IActionResult GetById(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var employee = _db.Employees.Include(e => e.City).Include(e => e.District).Include(e => e.State)
                .FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            var jsonResult = JsonSerializer.Serialize(employee, options);
            return Content(jsonResult, "application/json");

            

           
        }

        
        //[HttpPost]

        //public IActionResult Create([Bind("EmployeeName,EmployeeDesignation,EmployeeSalary,StateId,DistrictId,CityId,EmployeeAddress,EmployeeMobileNo")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Add(employee);
        //        _db.SaveChanges();
        //        return Created("defaultApi", employee);
        //    }
        //    // If ModelState is not valid, return a bad request or validation error response
        //    return BadRequest(ModelState);
        //}

        [HttpPost]
        public IActionResult AddOrUpdate(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.EmployeeId == 0)
                {

                    // If the EmployeeId is 0, it means it's a new employee, so add it
                    _db.Employees.Add(employee);
                }
                else
                {
                    // If the EmployeeId is not 0, it means it's an existing employee, so update it
                    _db.Employees.Update(employee);
                }

                _db.SaveChanges();
                return Created("defaultApi", employee); // Redirect to the index page after the operation
            }
            else
            {
                // Model is not valid, so return the same view with validation errors
                return BadRequest(ModelState);
            }
        }



        //[HttpPut]
        
        //public IActionResult Edit(int id, Employee employee)
        //{
        //    if (employee != null && id == employee.EmployeeId)
        //    {
        //    Employee dbemployee = _db.Employees.Find(employee.EmployeeId);

        //        if (dbemployee == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            //_db.Employees.Update(employee);
        //            dbemployee.EmployeeName = employee.EmployeeName;
        //            dbemployee.EmployeeDesignation = employee.EmployeeDesignation;
        //            dbemployee.EmployeeSalary = employee.EmployeeSalary;
        //            dbemployee.EmployeeMobileNo = employee.EmployeeMobileNo;
        //            dbemployee.EmployeeAddress = employee.EmployeeAddress;
        //            dbemployee.State = employee.State;
        //            dbemployee.City = employee.City;
        //            dbemployee.District = employee.District;

        //            _db.SaveChanges();
        //            return Ok(dbemployee);
        //        }
        //    }
        //    else
        //    { 
        //        return BadRequest();
        //    }
        //}

        

        
        [HttpDelete("{id}")]
        
        public IActionResult Delete(int id)
        {
            if (_db.Employees == null)
            {
                return Problem("Entity set 'EmployeeDbContext.Employees'  is null.");
            }
            var employee = _db.Employees.Find(id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
            }
            
            _db.SaveChanges();
            return Ok();
        }

        [HttpGet("States")]
        public IActionResult GetStates()
        {
            var states = _db.States.ToList();
            return Ok(states);
        }
        [HttpGet("districts/{stateId}")]
        public IActionResult GetDistrictsByState(int stateId)
        {
            var districts = _db.Districts
                .Where(d => d.StateId == stateId)
                .Select(d => new { value = d.DistrictId, text = d.DistrictName })
                .ToList();
            return Ok(districts);
        }

        [HttpGet("cities/{districtId}")]
        public IActionResult GetCitiesByDistrict(int districtId)
        {
            var cities = _db.Cities
                .Where(c => c.DistrictId == districtId)
                .Select(c => new { value = c.CityId, text = c.CityName })
                .ToList();
            return Ok(cities);
        }
    }
}
