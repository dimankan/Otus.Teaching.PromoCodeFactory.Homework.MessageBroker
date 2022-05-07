using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain
{
	public class PartnerManagerId: IPartnerManager
	{
		public Guid? Id { get; set; }

		public Task NotifyAdminAboutPartnerManagerPromoCode(Guid partnerManagerId)
		{
			throw new NotImplementedException();
		}
	}
}
