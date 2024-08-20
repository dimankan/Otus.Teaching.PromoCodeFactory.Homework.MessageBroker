using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services
{
    public interface ICustomersService
    {
        Task<IEnumerable<Customer>> GetWhereAsync(System.Linq.Expressions.Expression<Func<Customer, bool>> predicate);
    }
}
