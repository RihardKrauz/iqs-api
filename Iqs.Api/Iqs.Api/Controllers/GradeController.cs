using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iqs.BL.Infrastructure;
using Iqs.BL.Interfaces;
using Iqs.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iqs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GradeController : ControllerBase
    {

        private readonly IGradesEngine _gradesEngine;

        public GradeController(IGradesEngine gradesEngine)
        {
            _gradesEngine = gradesEngine;
        }

        [HttpGet("/specialization/{specId}/grades")]
        public MethodResult<List<GradeDto>> GetGradesBySpecialization(long specId)
        {
            return _gradesEngine.GetGradesBySpecialization(specId);
        }
    }
}