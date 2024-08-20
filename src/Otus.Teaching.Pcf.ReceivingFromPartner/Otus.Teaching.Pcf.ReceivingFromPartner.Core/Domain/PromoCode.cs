using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain
{
    public class PromoCode
        : BaseEntity
    {
        public string Code { get; set; }

        public string ServiceInfo { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime BeginDate { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime EndDate { get; set; }

        public Guid PartnerId { get; set; }

        [JsonIgnore]
        public virtual Partner Partner { get; set; }
        
        public Guid? PartnerManagerId { get; set; }

        [JsonIgnore]
        public virtual Preference Preference { get; set; }

        public Guid PreferenceId { get; set; }
    }
}