﻿using System.Net.Http;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Dto;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Http
{
    public class GivingPromoCodeToCustomerGateway
        : IGivingPromoCodeToCustomerGateway
    {
        private readonly HttpClient _httpClient;

        public GivingPromoCodeToCustomerGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task GivePromoCodeToCustomer(PromoCode promoCode)
        {
            var dto = new GivePromoCodeToCustomerDto()
            {
                PartnerId = promoCode.Partner.Id,
                BeginDate = promoCode.BeginDate.ToShortDateString(),
                EndDate = promoCode.EndDate.ToShortDateString(),
                PreferenceId = promoCode.PreferenceId,
                PromoCode = promoCode.Code,
                ServiceInfo = promoCode.ServiceInfo,
                PartnerManagerId = promoCode.PartnerManagerId
            };

            var response = await _httpClient.PostAsJsonAsync("api/v1/promocodes", dto);

            response.EnsureSuccessStatusCode();
        }
    }
}