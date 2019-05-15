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
        private readonly IHtmlHelper htmlHelper;
        private readonly IRestaurantData restaurantData;

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IActionResult OnGet(int? id)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            if (id.HasValue)
            {
                Restaurant = restaurantData.GetById(id.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

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
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id == 0)
            {
                restaurantData.Add(Restaurant);
            }
            else
            {
                restaurantData.Update(Restaurant);
            }
            restaurantData.Commit();

            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { id = Restaurant.Id });
        }
    }
}