using Application.Services___Repositores.NotficationNS;
using Domain.NotficationNS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotficationController : ControllerBase
    {
        private readonly INotficationService _notficationService;

        public NotficationController(INotficationService notficationService)
        {
            this._notficationService = notficationService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Notfication>> GetUserNotfications(int userId)
        {
            var response = await _notficationService.GetAllNotfications(userId);
            if (response == null)
            {
                return BadRequest();
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }
    }
}
