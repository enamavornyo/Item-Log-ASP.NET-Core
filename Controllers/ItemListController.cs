using System;
using ItemLog.Context;
using ItemLog.Models;
using ItemLog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ItemLog.Infrastructure;

namespace ItemLog.Controllers
{
	public class ItemListController : Controller
	{
		private readonly DataContext _context;

        public ItemListController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index() {

            List<CartItem> ItemList = HttpContext.Session.GetJson<List<CartItem>>("ItemList") ?? new List<CartItem>();

            ItemListViewModel ItemListVM = new()
            {
                CartItems = ItemList
            };
            return View(ItemListVM);
        }


        public async Task<IActionResult> Add(int id)
        {
            Item ? item = await _context.Items.FindAsync(id);

            List<CartItem> ItemList = HttpContext.Session.GetJson<List<CartItem>>("ItemList") ?? new List<CartItem>();

            CartItem? cartItem = ItemList.Where(c => c.ItemId == id).FirstOrDefault();

            if (cartItem == null)
            {
                ItemList.Add(new CartItem (item));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("ItemList", ItemList);

            TempData["Success"] = "The item has been added!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(long id)
        {
            List<CartItem> ItemList = HttpContext.Session.GetJson<List<CartItem>>("ItemList");

            CartItem? cartItem = ItemList.Where(c => c.ItemId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                ItemList.RemoveAll(p => p.ItemId == id);
            }

            if (ItemList.Count == 0)
            {
                HttpContext.Session.Remove("ItemList");
            }
            else
            {
                HttpContext.Session.SetJson("ItemList", ItemList);
            }

            TempData["Success"] = "The item has been removed!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(long id)
        {
            List<CartItem> ItemList = HttpContext.Session.GetJson<List<CartItem>>("ItemList");

            ItemList.RemoveAll(p => p.ItemId == id);

            if (ItemList.Count == 0)
            {
                HttpContext.Session.Remove("ItemList");
            }
            else
            {
                HttpContext.Session.SetJson("ItemList", ItemList);
            }

            TempData["Success"] = "The item has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("ItemList");

            return RedirectToAction("Index");
        }

    }
}

