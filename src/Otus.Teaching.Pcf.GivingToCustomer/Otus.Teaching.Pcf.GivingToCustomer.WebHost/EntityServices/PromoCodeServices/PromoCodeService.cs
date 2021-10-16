using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.Integration.EntityServices.MessageServices;
using Otus.Teaching.Pcf.GivingToCustomer.Integration.Messages;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.ModelOperationsResult;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.EntityServices.PromoCodeServices
{
    public class PromoCodeService : IRabbitMqMsgService, IPromoCodeService
    {
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;
        public PromoCodeService(IRepository<PromoCode> promoCodeRepository,
            IRepository<Preference> preferencesRepository,
            IRepository<Customer> customersRepository)
        {
            _promoCodesRepository = promoCodeRepository;
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;
        }

        public async Task ProcessRabbitMQMessage(IRabbitMQMessage message)
        {
            GivePromoCodeMessage givePromoCodeMessage = message as GivePromoCodeMessage;
            GivePromoCodeRequest request = GivePromoCodeRequestMapper.MapMessageToRequest(givePromoCodeMessage);

            await GivePromoCodesToCustomersWithPreferenceAsync(request);
        }

        public async Task<PromoCodeServiceOperationResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            PromoCodeServiceOperationResult opResult = new PromoCodeServiceOperationResult();

            var preference = await _preferencesRepository.GetByIdAsync(request.PreferenceId);

            opResult.IsPreferenceFound = preference != null;

            if (!opResult.IsPreferenceFound)
            {
                return opResult;
            }

            //  Получаем клиентов с этим предпочтением:
            var customers = await _customersRepository
                .GetWhere(d => d.Preferences.Any(x =>
                    x.Preference.Id == preference.Id));

            PromoCode promoCode = PromoCodeMapper.MapFromModel(request, preference, customers);

            await _promoCodesRepository.AddAsync(promoCode);

            opResult.Ok = true;

            return opResult;
        }
    }
}
