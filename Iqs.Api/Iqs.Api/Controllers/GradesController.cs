﻿using System;
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
    public class GradesController : ControllerBase
    {

        private readonly IGradesEngine _gradesEngine;

        public GradesController(IGradesEngine gradesEngine)
        {
            _gradesEngine = gradesEngine;
        }

        [HttpGet("/specializations/{specId}/grades")]
        public MethodResult<List<GradeDto>> GetGradesBySpecialization(long specId)
        {
            return _gradesEngine.GetGradesBySpecialization(specId);
        }
    }
}