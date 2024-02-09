using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Domain.Entities
{
    public class AccessPermission : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid LockId { get; set; }
        public Lock? Lock { get; set; }
        public DateTimeOffset? ValidUntil { get; set; } = null;
    }
}