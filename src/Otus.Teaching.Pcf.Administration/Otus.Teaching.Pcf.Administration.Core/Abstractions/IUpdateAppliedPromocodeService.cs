using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Core.Logic
{
    public interface IUpdateAppliedPromocodeService
    {
        Task<IActionResult> UpdateAppliedPromocodesAsync(Guid id);
    }
}