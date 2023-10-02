namespace WebApi.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }   
        public string Code { get; set; }
        public string Name { get; set; }   
        // can be a null value
        public string? RegionImageUrl { get; set; }
    }
}
