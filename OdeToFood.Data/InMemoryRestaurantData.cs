using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant{Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian},
                new Restaurant{Id = 2, Name = "Big Truck Tacos", Location = "Oklahoma", Cuisine = CuisineType.Mexican},
                new Restaurant{Id = 3, Name = "Naan Issues", Location = "Indiana", Cuisine = CuisineType.Indian},
                new Restaurant{Id = 4, Name = "Pho Geddabouit", Location = "New York", Cuisine = CuisineType.Vietnamese}
            };
        }

        public int Commit()
        {
            // Really only needed when we start using a real data source
            return 0;
        }

        public Restaurant GetById(int id)
        {
            //var results = from r in restaurants
            //       where r.Id.Equals(id)
            //       select r;

            //return results.First();
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = GetById(updatedRestaurant.Id);

            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }

            return restaurant;
        }
    }
}