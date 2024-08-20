using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.WebHost.Models;

namespace Otus.Teaching.Pcf.Administration.WebHost.Mappers
{
    public class EmployeeMapper
    {
        public static EmployeeShortResponse MapToShortResponse(Employee employee)
            => new()
            {
                Id = employee.Id,
                Email = employee.Email,
                FullName = employee.FullName
            };

        public static EmployeeResponse MapToResponse(Employee employee)
            => new()
            {
                Id = employee.Id,
                Email = employee.Email,
                Role = new RoleItemResponse()
                {
                    Id = employee.Id,
                    Name = employee.Role.Name,
                    Description = employee.Role.Description
                },
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };
    }
}
