using Azure;
using Azure.Data.Tables;

namespace RvdB.UrlShortener.API.Entities
{
    public class RedirectEntity : ITableEntity
    {
        public string RedirectUrl { get; set; }

        #region ITableEntity implementation

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        #endregion

        public RedirectEntity()
        {
            PartitionKey = "redir";
        }
    }
}
