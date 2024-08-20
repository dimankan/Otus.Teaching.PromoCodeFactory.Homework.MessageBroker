using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Services
{
    internal class PromoCodesService : IPromoCodesService
    {
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IPreferencesService _preferencesService;
        private readonly ICustomersService _customersService;

        public PromoCodesService(IRepository<PromoCode> promoCodesRepository,
            IPreferencesService preferencesService,
            ICustomersService customersService)
        {
            _promoCodesRepository = promoCodesRepository;
            _preferencesService = preferencesService;
            _customersService = customersService;
        }

        public async Task<IEnumerable<PromoCode>> GetAllAsync()
            => await _promoCodesRepository.GetAllAsync();

        public async Task GivePromoCodesToCustomersWithPreferenceAsync(PromoCode promoCode)
        {
            var preference = await _preferencesService.GetByIdAsync(promoCode.PreferenceId) ?? throw new PreferenceNotFoundException();

            var customers = await _customersService.GetWhereAsync(d => d.Preferences.Any(x => x.Preference.Id == preference.Id));

            promoCode.Preference = preference;

            promoCode.Customers = customers.Select(c =>
                new PromoCodeCustomer
                {
                    CustomerId = c.Id,
                    Customer = c,
                    PromoCodeId = promoCode.Id,
                    PromoCode = promoCode
                }
            ).ToList();

            await _promoCodesRepository.AddAsync(promoCode);
        }
    }
}
