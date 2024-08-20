using System;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Exceptions
{
    public class PreferenceNotFoundException : Exception
    {
        public PreferenceNotFoundException() : base("Preference not found") { }
    }
}
