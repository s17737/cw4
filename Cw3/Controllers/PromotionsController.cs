using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DTOs.Requests;
using Cw3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [Route("api/enrollments/promotions")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private IStudentsDbService _dbService;

        public PromotionsController(IStudentsDbService dbService)
        {
            _dbService = dbService;
        }

        /*[HttpPost]
        public IActionResult Studies(StudiesRequest modelStudies)
        {
            return Ok(_dbService.GetStudies(modelStudies));
        }*/
    }
}