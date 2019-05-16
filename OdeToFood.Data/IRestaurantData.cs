using OdeToFood.Core;
using System.Collections.Generic;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        Restaurant Add(Restaurant newRestaurant);

        int Commit();

        Restaurant Delete(int id);

        Restaurant GetById(int id);

        int GetCountOfRestaurants();

        IEnumerable<Restaurant> GetRestaurantsByName(string name);

        Restaurant Update(Restaurant updatedRestaurant);
    }
}