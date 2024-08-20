using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Exceptions;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IPromoCodesService _promoCodesService;

        public PromocodesController(IPromoCodesService promoCodesService)
        {
            _promoCodesService = promoCodesService;
        }
        
        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            var promocodes = await _promoCodesService.GetAllAsync();

            var response = promocodes.Select(PromoCodeMapper.MapToShortResponse);

            return Ok(response);
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            try
            {
                var promoCode = PromoCodeMapper.MapFromModel(request);
                await _promoCodesService.GivePromoCodesToCustomersWithPreferenceAsync(promoCode);

                return CreatedAtAction(nameof(GetPromocodesAsync), new { }, null);
            }
            catch(PreferenceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}