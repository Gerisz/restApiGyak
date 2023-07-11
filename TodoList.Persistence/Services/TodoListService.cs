using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ELTE.TodoList.Persistence.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly TodoListDbContext _context;

        public TodoListService(TodoListDbContext context)
        {
            _context = context;
        }

        public List<List> GetLists(String? name = null)
        {
            return _context.Lists
                .Where(l => l.Name.Contains(name ?? ""))
                .OrderBy(l => l.Name)
                .ToList();
        }

        public List GetListByID(int id)
        {
            return _context.Lists
                .Single(l => l.Id == id); // throws InvalidOperationException if id not found
        }

        public List<Item> GetItemsByListID(int id)
        {
            return _context.Items
                 .Where(i => i.ListId == id)
                 .ToList();
        }

        public List GetListDetails(int id)
        {
            return _context.Lists
                .Include(l => l.Items)
                .Single(l => l.Id == id);
        }

        public Item? GetItem(int id)
        {
            return _context.Items
                 .FirstOrDefault(i => i.Id == id);
        }

        public bool CreateList(List list)
        {
            try
            {
                _context.Add(list);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool UpdateList(List list)
        {
            try
            {
                _context.Update(list);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool DeleteList(int id)
        {
            var list = _context.Lists.Find(id);
            if (list == null)
            {
                return false;
            }

            try
            {
                _context.Remove(list);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool CreateItem(Item item)
        {
            try
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool UpdateItem(Item item)
        {
            try
            {
                _context.Update(item);
                if (item.Image == null)
                {
                    _context.Entry(item).Property("Image").IsModified = false;
                }
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool DeleteItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return false;
            }

            try
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }
    }
}
