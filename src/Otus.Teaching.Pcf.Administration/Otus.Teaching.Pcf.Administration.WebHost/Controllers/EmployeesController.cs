using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.Administration.WebHost.Models;
using Otus.Teaching.Pcf.Administration.Core.Employees;

namespace Otus.Teaching.Pcf.Administration.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController
        : ControllerBase
    {
        private readonly IEmployeesManager _employeesManager;

        public EmployeesController(IEmployeesManager employeesManager)
        {
            _employeesManager = employeesManager;
        }
        
        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeesManager.GetEmployeesAsync();

            var employeesModelList = employees.Select(x => 
                new EmployeeShortResponse()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FullName = x.FullName,
                    }).ToList();

            return employeesModelList;
        }
        
        /// <summary>
        /// Получить данные сотрудника по id
        /// </summary>
        /// <param name="id">Id сотрудника, например <example>451533d5-d8d5-4a11-9c7b-eb9f14e1a32f</example></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeesManager.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Role = new RoleItemResponse()
                {
                    Id = employee.Id,
                    Name = employee.Role.Name,
                    Description = employee.Role.Description
                },
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }
        
        /// <summary>
        /// Обновить количество выданных промокодов
        /// </summary>
        /// <param name="id">Id сотрудника, например <example>451533d5-d8d5-4a11-9c7b-eb9f14e1a32f</example></param>
        /// <returns></returns>
        [HttpPost("{id:guid}/appliedPromocodes")]
        
        public async Task<IActionResult> UpdateAppliedPromocodesAsync(Guid id)
        {
            try
            {
                await _employeesManager.UpdateAppliedPromocodesAsync(id);
            }
            catch(ArgumentException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}