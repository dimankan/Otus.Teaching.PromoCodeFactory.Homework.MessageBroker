using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.UseCases
{
    public class PromocodesManager : IPromocodesManager
    {
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;

        public PromocodesManager(IRepository<PromoCode> promoCodesRepository, IRepository<Preference> preferencesRepository, IRepository<Customer> customersRepository)
        {
            _promoCodesRepository = promoCodesRepository;
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;
        }

        public async Task<IEnumerable<PromoCode>> GetPromocodesAsync() => await _promoCodesRepository.GetAllAsync();

        public async Task GivePromoCodesToCustomersWithPreferenceAsync(Guid preferenceId, PromoCode promoCode)
        {
            //Получаем предпочтение по имени
            var preference = await _preferencesRepository.GetByIdAsync(preferenceId);

            if (preference == null)
            {
                throw new ArgumentException("Не найдено предпочтение", nameof(preferenceId));
            }

            //  Получаем клиентов с этим предпочтением:
            var customers = await _customersRepository
                .GetWhere(d => d.Preferences.Any(x =>
                    x.Preference.Id == preference.Id));

           var enrichedPromoCode = EnrichModel(promoCode, preference, customers);

            await _promoCodesRepository.AddAsync(enrichedPromoCode);
        }

        private PromoCode EnrichModel(PromoCode request, Preference preference, IEnumerable<Customer> customers)
        {

            var promocode = new PromoCode();
            promocode.Id = request.Id;

            promocode.PartnerId = request.PartnerId;
            promocode.Code = request.Code;
            promocode.ServiceInfo = request.ServiceInfo;

            promocode.BeginDate = request.BeginDate;
            promocode.EndDate = request.EndDate;

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
    }
}
