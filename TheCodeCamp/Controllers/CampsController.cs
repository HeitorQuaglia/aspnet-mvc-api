using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
    public class CampsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ICampRepository _repository;

        public CampsController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET: Camps
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                Camp[] result = await _repository.GetAllCampsAsync();

                //Mapping 
                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);
                
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}