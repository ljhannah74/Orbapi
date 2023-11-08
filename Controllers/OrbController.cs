using System.Net;
using Microsoft.AspNetCore.Mvc;
using orbapi.DAL;
using orbapi.DTOs;
using orbapi.Models;

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
        public async Task<ApiResponse<List<StateDTO>>> GetStates()
        {
            ApiResponse<List<StateDTO>> response = ApiResponseUtil<List<StateDTO>>.GetApiResponse(HttpStatusCode.OK);
            List<StateDTO> stateData = await Task.Run(() => OrbIndexDAL.GetStates()); 
            response.data = stateData;
      
            return response;
        }

        /// <summary>
        /// GET api/orb/PA
        /// 
        /// Retrieves the complete list of counties for the selected state in the system.
        /// </summary>
        /// <param name="stateName">The selected state</param>
        /// <returns></returns>
        [HttpGet("{stateName}")]
        public async Task<ApiResponse<List<CountyDTO>>> GetCountiesByState(string stateName) 
        {
            ApiResponse<List<CountyDTO>> response = ApiResponseUtil<List<CountyDTO>>.GetApiResponse(HttpStatusCode.OK);
            List<CountyDTO> countyData = await Task.Run(() => OrbIndexDAL.GetCountiesByState(stateName));
            
            if(countyData.Count == 0) 
                response = ApiResponseUtil<List<CountyDTO>>.GetApiResponse(HttpStatusCode.NotFound);
            else
                response.data = countyData;

            return response;
        }

        // GET api/orb/PA/ALLEGHENY
        [HttpGet("{stateName}/{countyName}")]
        public async Task<ApiResponse<List<OrbIndexDTO>>> GetOrbIndexes(string stateName, string countyName)
        {
            ApiResponse<List<OrbIndexDTO>> response = ApiResponseUtil<List<OrbIndexDTO>>.GetApiResponse(HttpStatusCode.OK);
            List<OrbIndexDTO> orbIndexes = await Task.Run(() => OrbIndexDAL.GetOrbIndexes(stateName, countyName));

            if(orbIndexes.Count == 0) 
                response = ApiResponseUtil<List<OrbIndexDTO>>.GetApiResponse(HttpStatusCode.NotFound);
            else
                response.data = orbIndexes;
            
            return response;
        }

        // Get api/orb/PA/WASHINGTON/localities
        [HttpGet("{stateName}/{countyName}/localities")]
        public async Task<ApiResponse<List<LocalityDTO>>> GetLocalities(string stateName, string countyName)
        {
            ApiResponse<List<LocalityDTO>> response = ApiResponseUtil<List<LocalityDTO>>.GetApiResponse(HttpStatusCode.OK);
            List<LocalityDTO> localitiesList = await Task.Run(() => OrbIndexDAL.GetLocalities(stateName, countyName));
            
            if(localitiesList.Count == 0)
                response = ApiResponseUtil<List<LocalityDTO>>.GetApiResponse(HttpStatusCode.OK);
            else
                response.data = localitiesList;

            return response;
        }
    }
}
