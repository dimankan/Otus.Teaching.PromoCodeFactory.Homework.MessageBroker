using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.Core.UseCases;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {
        private readonly IPromocodesManager _promocodesManager;

        public PromocodesController(IPromocodesManager promocodesManager)
        {
            _promocodesManager = promocodesManager;
        }
        
        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            var promocodes = await _promocodesManager.GetPromocodesAsync();

            var response = promocodes.Select(x => new PromoCodeShortResponse()
            {
                Id = x.Id,
                Code = x.Code,
                BeginDate = x.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                PartnerId = x.PartnerId,
                ServiceInfo = x.ServiceInfo
            }).ToList();

            return Ok(response);
        }
        
        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            var promoCode = new PromoCode
            {
                Id = request.PromoCodeId,
                PartnerId = request.PartnerId,
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                BeginDate = DateTime.Parse(request.BeginDate),
                EndDate = DateTime.Parse(request.EndDate)
            };

            try
            {
                await _promocodesManager.GivePromoCodesToCustomersWithPreferenceAsync(request.PreferenceId, promoCode);
            }
            catch(ArgumentException)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetPromocodesAsync), new { }, null);
        }
    }
}