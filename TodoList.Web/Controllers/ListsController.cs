using ELTE.TodoList.Persistence;
using ELTE.TodoList.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ELTE.TodoList.Web.Controllers
{
    public enum SortOrder { NAME_DESC, NAME_ASC, DEADLINE_DESC, DEADLINE_ASC }

    [Authorize]
    public class ListsController : Controller
    {
        private readonly ITodoListService _service;

        public ListsController(ITodoListService service)
        {
            _service = service;
        }

        // GET: Lists
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_service.GetLists());
        }

        // GET: Lists/Details/5
        [AllowAnonymous]
        public IActionResult Details(int id, SortOrder sortOrder = SortOrder.NAME_ASC)
        {
            try
            {
                ViewData["NameSortParam"] = sortOrder == SortOrder.NAME_DESC ? SortOrder.NAME_ASC : SortOrder.NAME_DESC;
                ViewData["DeadLineSortParam"] = sortOrder == SortOrder.DEADLINE_DESC ? SortOrder.DEADLINE_ASC : SortOrder.DEADLINE_DESC;

                var list = _service.GetListDetails(id);

                switch (sortOrder)
                {
                    case SortOrder.NAME_DESC:
                        list.Items = list.Items.OrderByDescending(i => i.Name).ToList();
                        break;
                    case SortOrder.NAME_ASC:
                        list.Items = list.Items.OrderBy(i => i.Name).ToList();
                        break;
                    case SortOrder.DEADLINE_DESC:
                        list.Items = list.Items.OrderByDescending(i => i.Deadline).ToList();
                        break;
                    case SortOrder.DEADLINE_ASC:
                        list.Items = list.Items.OrderBy(i => i.Deadline).ToList();
                        break;
                    default:
                        break;
                }

                return View(list);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List list)
        {
            if (ModelState.IsValid)
            {
                bool result = _service.CreateList(list);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(list);
            }
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var list = _service.GetListByID((int)id);
                return View(list);
            } 
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, List list)
        {
            if (id != list.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool result = _service.UpdateList(list);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Hiba történt a mentés során!");
                }
            }

            return View(list);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var list = _service.GetListByID((int)id);
                return View(list);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.GetListByID(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            _service.DeleteList(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateItem(int id)
        {
            TempData["ListId"] = id;
            return RedirectToAction("Create", "Items");
        }
    }
}
