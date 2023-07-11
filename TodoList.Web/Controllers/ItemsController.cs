using ELTE.TodoList.Web.Models;
using ELTE.TodoList.Persistence;
using ELTE.TodoList.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELTE.TodoList.Web.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly ITodoListService _service;

        public ItemsController(ITodoListService service)
        {
            _service = service;
        }

        public IActionResult? DisplayImage(int id)
        {
            var item = _service.GetItem(id);
            if (item != null && item.Image != null)
            {
                return File(item.Image, "image/png");
            }
            return null;
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.Lists = new SelectList(_service.GetLists(), "Id", "Name", TempData["ListId"]);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemViewModel vm, IFormFile? image)
        {
            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    image.CopyTo(stream);
                    vm.Image = stream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                var item = (Item)vm;
                _service.CreateItem(item);
                return RedirectToAction("Details", "Lists", new { id = item.ListId });
            }

            ViewBag.Lists = new SelectList(_service.GetLists(), "Id", "Name", vm.ListId);
            return View(vm);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _service.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }
            
            var vm = (ItemViewModel)item;
            ViewBag.Lists = new SelectList(_service.GetLists(), "Id", "Name", vm.ListId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ItemViewModel vm, IFormFile? image)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }


            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    image.CopyTo(stream);
                    vm.Image = stream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                var item = (Item)vm;
                bool result = _service.UpdateItem(item);

                if (result)
                {
                    return RedirectToAction("Details", "Lists", new { id = vm.ListId });
                }
                else
                {
                    ModelState.AddModelError("", "Hiba történt a mentés során!");
                }
            }

            ViewBag.Lists = new SelectList(_service.GetLists(), "Id", "Name", vm.ListId);
            return View(vm);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _service.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _service.GetItem(id);
            if (item != null)
            {
                _service.DeleteItem(id);
                return RedirectToAction("Details", "Lists", new { id = item.ListId });
            }
            else
            {
                return NotFound();
            }
        }
    }
}