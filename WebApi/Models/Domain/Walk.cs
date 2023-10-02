namespace WebApi.Models.Domain
{
    public class Walk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }


        //Navigation properties this tells entity framework core that walk has a difficulty
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }

    
    }

}
