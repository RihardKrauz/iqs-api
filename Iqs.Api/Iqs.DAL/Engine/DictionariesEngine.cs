using AutoMapper;
using Iqs.BL.Infrastructure;
using Iqs.BL.Interfaces;
using Iqs.DAL.Models;
using Iqs.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Iqs.DAL.Interfaces;

namespace Iqs.BL.Engine
{
    public class DictionariesEngine : IDictionariesEngine
    {
        private readonly IUnitOfWork _uow;
        public DictionariesEngine(IUnitOfWork uow) {
            _uow = uow;
        }

        private MethodResult<List<TEntityDto>> GetDictionaryDataItems<TEntityDto, TEntity>(Func<IQueryable<TEntity>> selector) 
            where TEntityDto : class 
            where TEntity : class
        {
            try
            {
                return Mapper.Map<List<TEntity>, List<TEntityDto>>(selector().ToList()).ToSuccessMethodResult();
            }
            catch (AutoMapperMappingException ex)
            {
                return $"Mapping error: {ex}".ToErrorMethodResult<List<TEntityDto>>();
            }
            catch (Exception ex)
            {
                return ex.ToErrorMethodResult<List<TEntityDto>>();
            }
        }

        public MethodResult<List<SpecializationDto>> GetSpecializations()
        {
            return GetDictionaryDataItems<SpecializationDto, Specialization>(_uow.Specializations.GetAll);
        }

        public MethodResult<List<DepartmentDto>> GetDepartments()
        {
            return GetDictionaryDataItems<DepartmentDto, Department>(_uow.Departments.GetAll);
        }
    }
}
