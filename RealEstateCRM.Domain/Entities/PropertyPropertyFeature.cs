namespace RealEstateCRM.Domain.Entities
{
    public class PropertyPropertyFeature
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int FeatureId { get; set; }
        public PropertyFeature Feature { get; set; }
    }
}