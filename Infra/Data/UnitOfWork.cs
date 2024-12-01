using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infra.Repository;

namespace Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CBICustomersContext _context;
        private Hashtable _repositories;
        public UnitOfWork(CBICustomersContext context)
        {
            _context = context;
        }

        public async Task<GenericResponse<bool>> Complete()
        {
            var result = new GenericResponse<bool>();

            try
            {
                result.Data = await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                result.Error = new MessageResponse();
                result.Error.Message = "Error ao salvar os dados!";
                result.Error.Status = 500;
            }
            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : IBaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}