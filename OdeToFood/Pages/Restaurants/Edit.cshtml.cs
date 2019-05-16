using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;
using System.Collections.Generic;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly IRestaurantData _restaurantData;

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
        }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IActionResult OnGet(int? id)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

            Restaurant = id.HasValue ? _restaurantData.GetById(id.Value) : new Restaurant();

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id == 0)
            {
                _restaurantData.Add(Restaurant);
            }
            else
            {
                _restaurantData.Update(Restaurant);
            }
            _restaurantData.Commit();

            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { id = Restaurant.Id });
        }
    }
}