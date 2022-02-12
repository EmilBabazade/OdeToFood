using OdeToFood.Core;
using System.Collections.Generic;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAllByName(string name = null);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant restaurant);
        int Commit();
    }
}
