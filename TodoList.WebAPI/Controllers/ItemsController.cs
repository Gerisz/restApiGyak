using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ELTE.TodoList.Persistence.Services;
using ELTE.TodoList.DTO;
using AutoMapper;

namespace ELTE.TodoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ITodoListService _service;
        private readonly IMapper _mapper;

        public ItemsController(ITodoListService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Items
        [HttpGet("{listId}")]
        public ActionResult<IEnumerable<ItemDto>> GetItems(int listId)
        {
            try
            {
                return _service
                    .GetListByID(listId)
                    .Items
                    .Select(item => _mapper.Map<ItemDto>(item))
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }
    }
}
