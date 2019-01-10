namespace FunctionalTests.Models
{
    public class Plane
    {
        public AirplaneType Type { get; set; }

        public string Description { get; set; }

        public Plane(AirplaneType airplaneType, string description)
        {
            Type = airplaneType;
            Description = description;
        }
    }
}