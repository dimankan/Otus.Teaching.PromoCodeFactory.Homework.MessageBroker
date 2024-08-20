﻿using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Services;
using Otus.Teaching.Pcf.Administration.Core.Exceptions;
using Otus.Teaching.Pcf.Administration.WebHost.Mappers;
using Otus.Teaching.Pcf.Administration.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }
        
        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeesService.GetAllAsync();

            var employeesModelList = employees.Select(EmployeeMapper.MapToShortResponse).ToList();

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
            var employee = await _employeesService.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return EmployeeMapper.MapToResponse(employee);
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
                await _employeesService.UpdateAppliedPromocodesAsync(id);

                return Ok();
            }
            catch(EmployeeNotFoundException)
            {
                return NotFound();
            }
            
        }
    }
}