using System;

namespace Otus.Teaching.Pcf.Administration.Core.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException() : base("Employee not found") { }
    }
}
