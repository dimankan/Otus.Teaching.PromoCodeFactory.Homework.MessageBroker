using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Services
{
    internal class PreferencesService : IPreferencesService
    {
        private readonly IRepository<Preference> _preferencesRepository;

        public PreferencesService(IRepository<Preference> preferencesRepository)
        {
            _preferencesRepository = preferencesRepository;
        }

        public async Task<Preference> GetByIdAsync(Guid id)
            => await _preferencesRepository.GetByIdAsync(id);
    }
}
