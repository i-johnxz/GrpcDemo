using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class CatalogType : BaseEntity, IAggregateRoot
    {
        public string Type { get; set; }
        
    }
}