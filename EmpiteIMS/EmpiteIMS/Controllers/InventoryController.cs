using EmpiteIMS.Models.Domain;
using EmpiteIMS.Models.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpiteIMS.Controllers
{
    public class InventoryController : Controller
    {

        private readonly EmpiteIMSDBContext empiteIMSDBContext;
        public InventoryController(EmpiteIMSDBContext empiteIMSDBContext)
        {
            this.empiteIMSDBContext = empiteIMSDBContext;
        }

        [Authorize(Roles = "viewer, admin, manager")]
        [HttpGet]
        public async Task<IActionResult> ShowInventory()
        {
            var inventory = await empiteIMSDBContext.Inventory.ToListAsync();
            return View(inventory);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<IActionResult> AddItem(GetInventoryViewModel addItemViewModel)
        {
            var item = new InventoryModel()
            {
                ItemCode = addItemViewModel.ItemCode,
                ItemName = addItemViewModel.ItemName,
                Quantity = addItemViewModel.Quantity
            };

            if (item != null)
            {
                await empiteIMSDBContext.Inventory.AddAsync(item);
                await empiteIMSDBContext.SaveChangesAsync();
            }
            
            return RedirectToAction("GetInventory");
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        [Route("Inventory/ViewItem/{Item:int}")]
        public async Task<IActionResult> ViewItem(int Item)
        {
            var item = await empiteIMSDBContext.Inventory.FirstOrDefaultAsync(x => x.ItemCode == Item);

            if (item != null)
            {
                var viewInventoryModel = new GetInventoryViewModel()
                {
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    Quantity = item.Quantity
                };

                return await Task.Run(() => View("ViewItem", viewInventoryModel));
            }

            return RedirectToAction("ShowInventory", "Inventory");
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<IActionResult> ViewItem(GetInventoryViewModel model)
        {
            var item = await empiteIMSDBContext.Inventory.FirstOrDefaultAsync(x => x.ItemCode == model.ItemCode);

            if (item != null)
            {
                item.ItemCode = model.ItemCode;
                item.ItemName = model.ItemName;
                item.Quantity = model.Quantity;
                

                await empiteIMSDBContext.SaveChangesAsync();

                return RedirectToAction("ShowInventory", "Inventory");
            }
            return RedirectToAction("ShowInventory", "Inventory");
        }

        [Authorize(Roles = "admin, manager")]
        [HttpDelete]
        public async Task<IActionResult> Delete(GetInventoryViewModel model)
        {
            var item = await empiteIMSDBContext.Inventory.FirstOrDefaultAsync(x => x.ItemCode == model.ItemCode);

            if (item != null)
            {
                empiteIMSDBContext.Inventory.Remove(item);
                await empiteIMSDBContext.SaveChangesAsync();

                return RedirectToAction("ShowInventory", "Inventory");
            }
            return RedirectToAction("ShowInventory", "Inventory");
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public async Task<IActionResult> GoBack()
        {
            return RedirectToAction("ShowInventory", "Inventory");
        }
    }
}
