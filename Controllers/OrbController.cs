using Microsoft.AspNetCore.Mvc;
using orbapi.DAL;
using orbapi.DTOs;

namespace orbapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrbController : ControllerBase
    {
        public OrbController() 
        {
        }

        /// <summary>
        /// GET api/orb
        /// 
        /// Retrieves the complete list of states in the system.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<List<StateDTO>>> GetStates()
        {
            List<StateDTO> stateData = await Task.Run(() => OrbIndexDAL.GetStates()); 
            return Ok(stateData);
        }

        /// <summary>
        /// GET api/orb/PA
        /// 
        /// Retrieves the complete list of counties for the selected state in the system.
        /// </summary>
        /// <param name="stateName">The selected state</param>
        /// <returns></returns>
        [HttpGet("{stateName}")]
        public async Task<ActionResult<List<CountyDTO>>> GetCountiesByState(string stateName) 
        {
            List<CountyDTO> countyData = await Task.Run(() => OrbIndexDAL.GetCountiesByState(stateName));
            
            if(countyData.Count == 0)
                return NotFound();

            return Ok(countyData);
        }

        // GET api/orb/PA/ALLEGHENY
        [HttpGet("{stateName}/{countyName}")]
        public async Task<ActionResult<List<OrbIndexDTO>>> GetOrbIndexes(string stateName, string countyName)
        {
            List<OrbIndexDTO> orbIndexes = await Task.Run(() => OrbIndexDAL.GetOrbIndexes(stateName, countyName));

            if(orbIndexes.Count == 0)
                return NotFound();

            return Ok(orbIndexes);
        }

        // Get api/orb/PA/WASHINGTON/localities
        [HttpGet("{stateName}/{countyName}/localities")]
        public async Task<ActionResult<List<LocalityDTO>>> GetLocalities(string stateName, string countyName)
        {
            List<LocalityDTO> localitiesList = await Task.Run(() => OrbIndexDAL.GetLocalities(stateName, countyName));
            
            if(localitiesList.Count == 0)
                return NotFound();

            return Ok(localitiesList);
        }
    }
}
