﻿using DapperNightProject.Dtos.EmployeeDtos;
using DapperNightProject.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace DapperNightProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeService.GetAllEmployeesAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            await _employeeService.CreateEmployeeAsync(createEmployeeDto);
            return RedirectToAction("EmployeeList");
        }
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction("EmployeeList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var value = await _employeeService.GetEmployeeByIdAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            await _employeeService.UpdateEmployeeAsync(updateEmployeeDto);
            return RedirectToAction("EmployeeList");
        }
        public async Task<IActionResult> EmployeeListWithDepartment()
        {
            var values = await _employeeService.GetEmployeeWithDepartmentAsync();
            return View(values);
        }
    }
}
