using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Models
{
    /// <summary>
    /// Represents a service response payload.
    /// </summary>
    /// <typeparam name="T">The specified response type</typeparam>
    public class ServiceResponse<T>
    {
        public T Payload { get; set; }
        public object AdditionalData { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
