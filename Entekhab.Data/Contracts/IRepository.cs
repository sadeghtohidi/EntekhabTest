using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entekhab.Domain.Common;

namespace Data.Repositories
{/// <summary>
/// base repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        void Add(TEntity entity, bool saveNow = true);
        Task AddAsync(TEntity entity, bool saveNow = true);
        void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        void Attach(TEntity entity);
        void Delete(TEntity entity, bool saveNow = true);
        Task DeleteAsync(TEntity entity, bool saveNow = true);
        void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        void Detach(TEntity entity);
        TEntity GetById(params object[] ids);
        ValueTask<TEntity> GetByIdAsync( params object[] ids);
       
       
        void Update(TEntity entity, bool saveNow = true);
        Task UpdateAsync(TEntity entity, bool saveNow = true);
        void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);



        #region learning

        Task<IEnumerable<TEntity>> GetAllAsync( Expression<Func<TEntity, bool>> where = null);

        Task<IEnumerable<TEntity>> GetAllAsync( Expression<Func<TEntity, bool>> where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string StrIncludes = null, params Expression<Func<TEntity, object>>[] includes);

        Task<ICollection<T>> GetColumns<T>( Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, T>> select) where T : class;

        Task<bool> UpdateFiledAsync( TEntity entity, params Expression<Func<TEntity, object>>[] properties);

        bool UpdateFiled(TEntity entity, params Expression<Func<TEntity, object>>[] properties);


            #endregion learning


    }
        
}