using System;

namespace ECommerce.Models.Domain
{
    public class BaseObject
    {
        public long Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
