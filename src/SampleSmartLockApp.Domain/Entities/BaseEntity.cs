using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTimeOffset Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}