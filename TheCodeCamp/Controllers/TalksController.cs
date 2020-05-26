using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
    [Route("api/camps/{Moniker}/talks")]
    public class TalksController : ApiController
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public TalksController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET: Talks
        [Route()]
        public async Task<IHttpActionResult> Get(string moniker, bool includeSpeakers = false)
        {
            try
            {
                var results = await _repository.GetTalksByMonikerAsync(moniker, includeSpeakers);

                return Ok(_mapper.Map<IEnumerable<TalkModel>>(results));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        // GET: Talk
        [Route("{id:int}", Name = "GetTalk")]
        public async Task<IHttpActionResult> Get(string moniker, int id, bool includeSpeakers = false)
        {
            try
            {
                var result = await _repository.GetTalkByMonikerAsync(moniker, id, includeSpeakers);
                if (result == null) return NotFound();

                return Ok(_mapper.Map<TalkModel>(result));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        //POST: Talk
        [Route()]
        public async Task<IHttpActionResult> Post(string moniker, TalkModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var camp = await _repository.GetCampAsync(moniker);
                    var talk = _mapper.Map<Talk>(model);
                    talk.Camp = camp;

                    if(await _repository.SaveChangesAsync())
                    {
                        return CreatedAtRoute("GetTalk",
                            new { moniker = moniker, id = talk.TalkId},
                            _mapper.Map<TalkModel>(talk));
                    }
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return BadRequest();
        }
    }
}