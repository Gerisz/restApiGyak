using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ELTE.TodoList.Persistence.Services;
using AutoMapper;
using ELTE.TodoList.DTO;

namespace ELTE.TodoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly ITodoListService _service;
        private readonly IMapper _mapper;

        public ListsController(ITodoListService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Lists
        [HttpGet]
        public ActionResult<IEnumerable<ListDto>> GetLists()
        {
            return _service
                .GetLists()
                .Select(list => _mapper.Map<ListDto>(list))
                .ToList();
        }
    }
}
