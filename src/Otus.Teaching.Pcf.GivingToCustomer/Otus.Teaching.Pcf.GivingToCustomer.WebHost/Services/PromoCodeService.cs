using Otus.Teaching.Pcf.Common;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;

        public PromoCodeService(IRepository<PromoCode> promoCodesRepository,
            IRepository<Preference> preferencesRepository, IRepository<Customer> customersRepository)
        {
            _promoCodesRepository = promoCodesRepository;
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;

        }
        public async Task GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeToCustomerMessage message)
        {
            
            //Получаем предпочтение по имени
            var preference = await _preferencesRepository.GetByIdAsync(message.PreferenceId);

            if (preference == null)
                throw new System.Exception("Предпочтение не найдено");

            //  Получаем клиентов с этим предпочтением:
            var customers = await _customersRepository
                .GetWhere(d => d.Preferences.Any(x =>
                    x.Preference.Id == preference.Id));

            PromoCode promoCode = PromoCodeMapper.MapFromMessage(message, preference, customers);

            await _promoCodesRepository.AddAsync(promoCode);

        }
    }
}
