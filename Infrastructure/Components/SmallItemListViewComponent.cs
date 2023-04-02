using System;
using ItemLog.Context;
using ItemLog.Models;
using ItemLog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemLog.Infrastructure;

namespace ItemLog.Infrastructure.Components
{
	public class SmallItemListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() {

            List<CartItem> ItemList = HttpContext.Session.GetJson<List<CartItem>>("ItemList");
            SmallItemListViewModel smallItemListVM;

            if (ItemList == null || ItemList.Count == 0) {

                smallItemListVM = null;
            }
            else {
                smallItemListVM = new()
                {
                    TotalNum = ItemList.Sum(x => x.Quantity)
                };
            }

            return View(smallItemListVM);

        }
    }
}

