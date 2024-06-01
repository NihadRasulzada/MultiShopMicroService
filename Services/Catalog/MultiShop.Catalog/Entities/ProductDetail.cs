using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class ProductDetail : BaseEntity
    {
        public string ProductDescription { get; set; }
        public string ProductInfo { get; set; }
        public string ProductId { get; set; }
        [BsonIgnore]
        public Product Product { get; set; }
    }
}
