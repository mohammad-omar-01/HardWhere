using Application.DTOs;
using Application.Services___Repositores;
using Domain.UserNs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService sService)
        {
            _searchService = sService;
        }

        [HttpPost]
        public ActionResult<UserSearch> AddSearchRequest([FromBody] SerachQuery serachQuery)
        {
            var response = _searchService.AddSearchRecord(serachQuery);
            if (response == null)
            {
                return BadRequest();
            }
            var json = JsonConvert.SerializeObject(
                response.Result,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }
    }
}
