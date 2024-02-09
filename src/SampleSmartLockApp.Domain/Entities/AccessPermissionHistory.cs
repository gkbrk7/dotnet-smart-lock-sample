using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Domain.Entities
{
    public class AccessPermissionHistory : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid LockId { get; set; }
        public virtual Lock Lock { get; set; } = null!;
        public DateTimeOffset Timestamp { get; set; } = new();
        public bool IsConfirmed { get; set; }
        public string? Message { get; set; }
    }
}