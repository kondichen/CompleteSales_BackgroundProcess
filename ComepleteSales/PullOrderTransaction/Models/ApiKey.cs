using System;

namespace CompleteSales.Models
{
    public class ApiKey
    {
        public Guid apiId { get; set; }

        public override string ToString()
        {
            return $"{nameof(apiId)}: {apiId}";
        }
    }
}
