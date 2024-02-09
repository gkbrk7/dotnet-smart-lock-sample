using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Domain.Entities
{
    public class Office : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<Lock> Locks { get; set; } = [];
    }
}