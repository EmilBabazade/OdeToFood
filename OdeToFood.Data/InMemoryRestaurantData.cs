using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private readonly List<Restaurant> _restaurants;
        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant()
                {
                    Cuisine = CuisineType.Indian,
                    Id = 1,
                    Location = "San Francisco",
                    Name = "Bob's food or some shit"
                },
                new Restaurant()
                {
                    Cuisine = CuisineType.Mexican,
                    Id = 2,
                    Location = "City dela Mexico",
                    Name = "Bob's food or some shit no 2",
                },
                new Restaurant()
                {
                    Cuisine = CuisineType.Italian,
                    Id = 3,
                    Location = "Duzeldorf",
                    Name = "Alehandra's food or some shit no 3",
                },
            };
        }

        public Restaurant GetById(int id)
        {
            return _restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAllByName(string name = null)
        {
            return from r in _restaurants
                   where string.IsNullOrWhiteSpace(name) || r.Name.ToLower().StartsWith(name.ToLower())
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var restaurantToUpdate = GetById(restaurant.Id);
            if (restaurantToUpdate != null)
            {
                restaurantToUpdate.Location = restaurant.Location;
                restaurantToUpdate.Name = restaurant.Name;
                restaurantToUpdate.Cuisine = restaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                _restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int GetRestaurantCount() => _restaurants.Count;
    }
}
