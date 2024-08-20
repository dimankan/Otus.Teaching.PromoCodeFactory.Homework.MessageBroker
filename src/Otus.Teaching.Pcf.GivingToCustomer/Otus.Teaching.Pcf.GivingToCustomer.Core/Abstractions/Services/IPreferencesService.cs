using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services
{
    public interface IPreferencesService
    {
        Task<Preference> GetByIdAsync(Guid id);
    }
}
