using System;
using System.Collections.Generic;
using Otus.Teaching.Pcf.Common;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers
{
	public class PromoCodeMapper
    {
        public static PromoCode MapFromModel(GivePromoCodeRequest request, Preference preference, IEnumerable<Customer> customers) {

            var promocode = new PromoCode();
            promocode.Id = request.PromoCodeId;
            
            promocode.PartnerId = request.PartnerId;
            promocode.Code = request.PromoCode;
            promocode.ServiceInfo = request.ServiceInfo;
           
            promocode.BeginDate = DateTime.Parse(request.BeginDate);
            promocode.EndDate = DateTime.Parse(request.EndDate);

            promocode.Preference = preference;
            promocode.PreferenceId = preference.Id;

            promocode.Customers = new List<PromoCodeCustomer>();

            foreach (var item in customers)
            {
                promocode.Customers.Add(new PromoCodeCustomer()
                {

                    CustomerId = item.Id,
                    Customer = item,
                    PromoCodeId = promocode.Id,
                    PromoCode = promocode
                });
            };

            return promocode;
        }
        public static PromoCode MapFromMessage(GivePromoCodeToCustomerMessage message, Preference preference, IEnumerable<Customer> customers)
        {
            var promocode = new PromoCode()
            {
                Id = message.PromoCodeId,

                PartnerId = message.PartnerId,
                Code = message.PromoCode,
                ServiceInfo = message.ServiceInfo,

                BeginDate = DateTime.Parse(message.BeginDate),
                EndDate = DateTime.Parse(message.EndDate),

                Preference = preference,
                PreferenceId = preference.Id,

                Customers = new List<PromoCodeCustomer>()
            };

            foreach (var item in customers)
            {
                promocode.Customers.Add(new PromoCodeCustomer()
                {

                    CustomerId = item.Id,
                    Customer = item,
                    PromoCodeId = promocode.Id,
                    PromoCode = promocode
                });
            };
            return promocode;
        }
    }
}
