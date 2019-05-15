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
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int id)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            Restaurant = restaurantData.GetById(id);

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            if (ModelState.IsValid)
            {
                restaurantData.Update(Restaurant);
                restaurantData.Commit();

                return RedirectToPage("./Detail", new { id = Restaurant.Id});
            }

            return Page();
        }
    }
}