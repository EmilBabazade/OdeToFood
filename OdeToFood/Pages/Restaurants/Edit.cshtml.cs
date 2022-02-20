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
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

            if (restaurantId == null)
            {
                Restaurant = new Restaurant();
                return Page();
            }

            Restaurant = _restaurantData.GetById((int)restaurantId);
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                Restaurant = _restaurantData.Update(Restaurant);
                TempData["Message"] = "Restaurant Updated!";
            }
            else
            {
                Restaurant = _restaurantData.Add(Restaurant);
                TempData["Message"] = "Restaurant Created!";
            }

            _restaurantData.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
