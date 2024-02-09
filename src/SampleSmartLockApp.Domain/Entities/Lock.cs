using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Domain.Entities
{
    public class Lock : BaseEntity
    {
        public required string Name { get; set; }
        public Guid OfficeId { get; set; }
        public Office? Office { get; set; }
        public ICollection<AccessPermission> AccessPermissions { get; set; } = [];
    }
}