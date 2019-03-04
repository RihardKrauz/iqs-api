using AutoMapper;
using Iqs.BL.Infrastructure;
using Iqs.BL.Interfaces;
using Iqs.DAL.Interfaces;
using Iqs.DAL.Models;
using Iqs.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.BL.Engine
{
    public class GradesEngine : IGradesEngine
    {
        private readonly IUnitOfWork _uow;
        public GradesEngine(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public MethodResult<List<GradeDto>> GetGradesBySpecialization(long specId)
        {
            try
            {
                var grades = _uow.Grades.GetBySpecializationId(specId);
                return Mapper.Map<List<GradeDto>>(grades).ToSuccessMethodResult();
            }
            catch (AutoMapperMappingException ex)
            {
                return $"Mapping error on 'User' object: {ex}".ToErrorMethodResult<List<GradeDto>>();
            }
            catch (Exception ex)
            {
                return ex.ToErrorMethodResult<List<GradeDto>>();
            }
        }
    }
}
