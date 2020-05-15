using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DTOs.Requests;
using Cw3.Models;
using Cw3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        //private const string ConString = "Data Source=db-mssql;Initial Catalog=s17737;Integrated Security=True";

        private IStudentsDbService _dbService;

        public StudentsController(IStudentsDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            return Ok(_dbService.GetStudent());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
             return Ok(_dbService.GetStudent(id));
            //dodac że jak nie ma studenta to błąd. ALe jak?
                
            
            //return NotFound("Nie znaleziono studenta");
        }
    }
}
