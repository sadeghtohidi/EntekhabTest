using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using Entekhab.Data.Database;
using Entekhab.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); // City => Cities
        }

        #region Async Method
        public virtual ValueTask<TEntity> GetByIdAsync( params object[] ids)
        {
            return Entities.FindAsync(ids);
        }

        public virtual async Task AddAsync(TEntity entity,  bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
           
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities,  bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity,  bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities,  bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity,  bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities,  bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }
        #endregion

        #region Sync Methods
        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            DbContext.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }
        #endregion

        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);

            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }
        #endregion


        #region learning

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync( Expression<Func<TEntity, bool>> where = null)
        {
            if (where != null)
                return await TableNoTracking.Where(where).ToListAsync();
            return await TableNoTracking.ToListAsync();

        }

        //public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)

        //{
        //    IQueryable<TEntity> query = null;

        //    foreach (var include in includes)
        //    {
        //        query = Entities.Include(include);// _dbset.Include(include);
        //    }

        //    return query;
        //}
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync( Expression<Func<TEntity, bool>> where = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string StrIncludes = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Entities;

            if (where != null)
            {
                query = query.Where(where);

            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (StrIncludes != null)
            {
                foreach (string include in StrIncludes.Split(','))
                {
                    query = (await query.Include(include).ToListAsync()).AsQueryable();
                }
            }

            if (includes != null)
            {
                foreach (var include in includes)

                    query = query.Include(include);
            }

            var result = await query.ToListAsync();

            return result.AsEnumerable();
        }



        public virtual async Task<ICollection<T>> GetColumns<T>(
            Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, T>> select) where T : class
        {

            var result = await Table.Where(where).Select(select).ToListAsync();
            return result;


        }
       


        public virtual async Task<bool> UpdateFiledAsync( TEntity entity,
            params Expression<Func<TEntity, object>>[] properties)
        {

            //if (! await TableNoTracking.AnyAsync(c => c.Id == entity.Id))
            //if (entity.Id == 0)
            //    return false;
            var entry = Entities.Attach(entity);
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
       
            if (entry.State == EntityState.Added)
                return false;

            await DbContext.SaveChangesAsync();
            Detach(entity);
            return true;
        }

        public virtual bool UpdateFiled( TEntity entity,
           params Expression<Func<TEntity, object>>[] properties)
        {

            //if (! await TableNoTracking.AnyAsync(c => c.Id == entity.Id))
            //if (entity.Id == 0)
            //    return false;
            var entry = Entities.Attach(entity);
            foreach (var property in properties)
                entry.Property(property).IsModified = true;

            if (entry.State == EntityState.Added)
                return false;

            DbContext.SaveChanges();
            Detach(entity);
            return true;
        }



        #endregion learning



    }
}
