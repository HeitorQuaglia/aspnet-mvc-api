using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
    [RoutePrefix("api/camps")]
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
        [Route()]
        public async Task<IHttpActionResult> Get(bool includeTalks = false)
        {
            try
            {
                Camp[] result = await _repository.GetAllCampsAsync(includeTalks);

                //Mapping 
                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //Get: Camp
        [Route("{Moniker}", Name = "GetCamp")]
        public async Task<IHttpActionResult> Get(string moniker, bool includeTalks = false)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker, includeTalks);
                if (result == null) return NotFound();
                //Mapping
                var mappedResult = _mapper.Map<CampModel>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //
        public async Task<IHttpActionResult> Post(CampModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var camp = _mapper.Map<Camp>(model);
                    _repository.AddCamp(camp);
                    if (await _repository.SaveChangesAsync())
                    {
                        var newModel = _mapper.Map<CampModel>(model);

                        return CreatedAtRoute("GetCamp", new { moniker = newModel.Moniker } , newModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest();
        }
    }
}