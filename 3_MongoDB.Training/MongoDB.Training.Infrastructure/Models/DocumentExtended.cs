using System.Collections.Generic;

namespace MongoDB.Training.Infrastructure.Models
{
    public class DocumentExtended : Document
    {
        public List<Tag> TagsEntities { get; set; } = new List<Tag>();
    }
}
