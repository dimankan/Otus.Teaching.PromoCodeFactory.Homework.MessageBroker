using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Otus.Teaching.Pcf.Common.Dto;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;

namespace Otus.Teaching.Pcf.GivingToCustomer.Integration;

public class GivePromoCodeToCustomerConsumer: IConsumer<GivePromoCodeToCustomerDto>
{
    private IRepository<Preference> _preferenceRepository;
    private IRepository<Customer> _customerRepository;
    private IRepository<PromoCode> _promoCodeRepository;

    public GivePromoCodeToCustomerConsumer(
        IRepository<Preference> preferenceRepository, 
        IRepository<Customer> customerRepository,
        IRepository<PromoCode> promoCodeRepository)
    {
        _preferenceRepository = preferenceRepository;
        _customerRepository = customerRepository;
        _promoCodeRepository = promoCodeRepository;
    }
    
    public async Task Consume(ConsumeContext<GivePromoCodeToCustomerDto> context)
    {
        var preference = await _preferenceRepository.GetByIdAsync(context.Message.PreferenceId);
        
        if (preference is null) return;

        var customers = await _customerRepository
            .GetWhere(d => d.Preferences.Any(x =>
                x.Preference.Id == preference.Id));

        PromoCode promoCode = PromoCodeMapper.MapFromModel(context.Message, preference, customers);

        await _promoCodeRepository.AddAsync(promoCode);
    }
}