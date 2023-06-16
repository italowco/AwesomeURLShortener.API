using AutoMapper;
using AweSomeURLShortener.Application.Interfaces;
using AweSomeURLShortener.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TDomain, TDbContext> :  ICommandRepository<TDomain>, IQueryRepository<TDomain>
        where TEntity : class, IDbEntity
        where TDbContext : DbContext
         where TDomain : class

    {
        private readonly TDbContext _context;
        public readonly DbSet<TEntity> _entities;
        private readonly IMapper _mapper;

        public BaseRepository(TDbContext context,  IMapper mapper)
        {
            _context = context;
            _entities = context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TDomain> AddAsync(TDomain entity)
        {
            var dbEnttiy = _mapper.Map<TEntity>(entity);
            var newEntry = await _context.AddAsync(dbEnttiy);
            await _context.SaveChangesAsync();

            return _mapper.Map<TDomain>(newEntry.Entity);
        }

        public async void Delete(TDomain entity)
        {
            var dbEnttiy = _mapper.Map<TEntity>(entity);
            _context.Entry(dbEnttiy).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            var result = await _entities.ToListAsync();
            return _mapper.Map<IEnumerable<TDomain>>(result);
        }

        public async Task<TDomain> GetAsync(int id)
        {
            return _mapper.Map<TDomain>(await _entities.FindAsync(id));
        }

        public async Task<TDomain> UpdateAsync(TDomain entity)
        {
            var dbEnttiy = _mapper.Map<TEntity>(entity);
            _context.Entry(dbEnttiy).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
