using System;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;

namespace TheCodeCamp.Controllers
{
    public class CampsController : ApiController
    {
        private ICampRepository _repository;

        public CampsController(ICampRepository repository)
        {
            _repository = repository;
        }
        // GET: Camps
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                Camp[] result = await _repository.GetAllCampsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}