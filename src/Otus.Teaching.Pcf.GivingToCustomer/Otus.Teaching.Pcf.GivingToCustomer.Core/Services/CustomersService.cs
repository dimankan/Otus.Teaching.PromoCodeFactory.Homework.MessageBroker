using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Services
{
    internal class CustomersService : ICustomersService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomersService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetWhereAsync(Expression<Func<Customer, bool>> predicate)
            => await _customerRepository.GetWhere(predicate);
    }
}
