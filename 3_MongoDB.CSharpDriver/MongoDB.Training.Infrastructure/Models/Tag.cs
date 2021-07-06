using System;

namespace MongoDB.Training.Infrastructure.Models
{
    public class Tag : IdentityCollection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
