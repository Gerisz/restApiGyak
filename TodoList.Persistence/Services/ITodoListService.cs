using System;
using System.Collections.Generic;

namespace ELTE.TodoList.Persistence.Services
{
    public interface ITodoListService
    {
        List<List> GetLists(String? name = null);
        List GetListByID(int id);
        List<Item> GetItemsByListID(int id);
        List GetListDetails(int id);
        Item? GetItem(int id);
        bool CreateItem(Item item);
        bool DeleteItem(int id);
        bool UpdateItem(Item item);
        bool CreateList(List list);
        bool UpdateList(List list);
        bool DeleteList(int id);
    }
}
