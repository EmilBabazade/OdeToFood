namespace OdeToFood.DBL
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Location { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
