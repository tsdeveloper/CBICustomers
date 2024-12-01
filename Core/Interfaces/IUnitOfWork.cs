using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
          IGenericRepository<TEntity> Repository<TEntity>() 
          where TEntity : IBaseEntity;
        Task<GenericResponse<bool>> Complete();
    }
}