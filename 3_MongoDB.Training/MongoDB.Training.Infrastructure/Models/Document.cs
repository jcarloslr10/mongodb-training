using System;
using System.Collections.Generic;

namespace MongoDB.Training.Infrastructure.Models
{
    public class Document : IdentityCollection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Priority { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
