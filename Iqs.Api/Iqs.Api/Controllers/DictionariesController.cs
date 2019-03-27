using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iqs.BL.Infrastructure;
using Iqs.BL.Interfaces;
using Iqs.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iqs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IDictionariesEngine _dictionariesEngine;

        public DictionariesController(IDictionariesEngine dictionariesEngine)
        {
            _dictionariesEngine = dictionariesEngine;
        }

        [HttpGet("/specializations")]
        public MethodResult<List<SpecializationDto>> GetAllSpecializations()
        {
            return _dictionariesEngine.GetSpecializations();
        }

        [HttpGet("/departments")]
        public MethodResult<List<DepartmentDto>> GetAllDepartments()
        {
            return _dictionariesEngine.GetDepartments();
        }

    }
}