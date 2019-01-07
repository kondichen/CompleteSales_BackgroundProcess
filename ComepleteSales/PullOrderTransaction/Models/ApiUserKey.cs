using System;

namespace CompleteSales.Models
{
    public class ApiUserKey
    {
        public ApiKey apiKey { get; set; }
        public Guid apiUserId { get; set; }

        public ApiUserKey(Guid apiUserId, Guid apiId)
        {
            this.apiKey = new ApiKey()
            {
                apiId = apiId
            };
            this.apiUserId = apiUserId;
        }

        public ApiUserKey(Guid apiUserId, ApiKey apiKey) : this(apiUserId, apiKey.apiId)
        {
        }

        public override string ToString()
        {
            return $"{nameof(apiKey)}: {apiKey}, {nameof(apiUserId)}: {apiUserId}";
        }
    }

}
