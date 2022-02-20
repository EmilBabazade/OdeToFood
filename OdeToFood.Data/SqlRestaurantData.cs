using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Restaurant Add(Restaurant restaurant)
        {
            _dbContext.Add(restaurant);
            return restaurant;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                _dbContext.Remove(restaurant);
            }
            return restaurant;
        }

        public IEnumerable<Restaurant> GetAllByName(string name = null)
        {
            return from r in _dbContext.Restaurants
                   where string.IsNullOrWhiteSpace(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant GetById(int id)
        {
            return _dbContext.Restaurants.SingleOrDefault(r => r.Id == id);
        }

        public int GetRestaurantCount() => _dbContext.Restaurants.Count();

        public Restaurant Update(Restaurant restaurant)
        {
            _dbContext.Update(restaurant);
            return restaurant;
        }
    }
}
