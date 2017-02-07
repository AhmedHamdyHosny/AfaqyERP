using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static GenericApiController.Utilities.GenericDataFormat;

namespace GenericApiController.Utilities
{
    public partial class Repository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
            //context.Database.CommandTimeout = 180;
        }

        public Repository() {
        }

        // The code Expression<Func<TEntity, bool>> filter means 
        //the caller will provide a lambda expression based on the TEntity type,
        //and this expression will return a Boolean value.
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            
            if(!string.IsNullOrEmpty(includeProperties))
            {
                // applies the eager-loading expressions after parsing the comma-delimited list
                foreach (var includeProperty in includeProperties.Split
                    (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            

            return orderBy != null
                ? orderBy(query)
                : query;

        }

        public virtual dynamic GetWithOptions(
            IEnumerable<string> includeProperties = null,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<SortItems> thenByOrders = null,
            string includeReferences = "",
            int? pageNumber = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (!string.IsNullOrEmpty(includeReferences))
            {
                // applies the eager-loading expressions after parsing the comma-delimited list
                foreach (var includeProperty in includeReferences.Split
                    (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            query = orderBy != null ? orderBy(query) : query;

            if (thenByOrders != null )
            {
                var orderdQuery = ((IOrderedQueryable<TEntity>)query);
                foreach (var thenByItem in thenByOrders)
                {
                    orderdQuery = thenByItem.SortType == SortType.Asc ?
                        orderdQuery.ThenBy(GetSelector(thenByItem.Property)) :
                        orderdQuery.ThenByDescending(GetSelector(thenByItem.Property));
                }
                query = orderdQuery;
            }

            if(orderBy != null && pageNumber != null && pageSize != null)
            {
                int? skpItmsCount = (pageNumber - 1) * pageSize;
                query = query.Skip((int)skpItmsCount).Take((int)pageSize);
            }

            if(includeProperties != null)
            {
                return query.SelectProperties(includeProperties).ToList<object>();
            }

            return query;
        }
        public virtual dynamic GetReferenceForImport(
            IEnumerable<string> includeProperties = null,
            dynamic filter = null)
        {
            var result = GetWithOptions(includeProperties: includeProperties, filter: (Expression<Func<TEntity, bool>>)filter);
            if(!(result is List<object>))
            {
                return ((IQueryable<TEntity>)result).ToList<TEntity>();
            }
            return result;
        }
        public virtual async Task<ICollection<TResult>> GetAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            if (selector == null)
                throw new ArgumentNullException("selector");
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            // applies the eager-loading expressions after parsing the comma-delimited list
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            // applies the eager-loading expressions after parsing the comma-delimited list

            return await (orderBy != null
                ? orderBy(query).Select(selector).ToListAsync()
                : query.Select(selector).ToListAsync());
        }
        public virtual TEntity GetByID(int id, Expression<Func<TEntity, bool>> filter = null)
        {
           
            var item = DbSet.Find(id);
            if (filter != null && item != null)
            {
                List<TEntity> items = new List<TEntity>() ;
                items.Add(item);
                item = items.AsQueryable<TEntity>().Where(filter).SingleOrDefault();
            }
           
            return item;
        }
        public virtual async Task<TEntity> GetByAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual TEntity Insert(TEntity entity)
        {
            return DbSet.Add(entity);
        }
        public virtual IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            return DbSet.AddRange(entities);
        }
        public virtual IEnumerable<TEntity> InserBulk(IEnumerable<TEntity> entities)
        {
            List<TEntity> result = null;
            try
            {
                _context.BulkInsert<TEntity>(entities);
                //DbSet.AddRange(entities);
                //_context.BulkSaveChanges(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            /*
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                DbContext context = null;
                result = new List<TEntity>();
                try
                {
                    
                    //context = new DbContext("name=AfaqyStoreEntities");
                    var entityCnxStringBuilder = 
                        new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings[_context.GetType().Name].ConnectionString);

                    context = new DbContext(entityCnxStringBuilder.ConnectionString);
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    context.Configuration.AutoDetectChangesEnabled = false;
                    //context.Database.Connection.Open();
                    const int bulkCount = 100;
                    int index = 0;
                    int count = index * bulkCount;
                    while (count < entities.Count())
                    {
                        var bulkEntities = entities.Skip(count).Take(bulkCount);
                        //insert to context 
                        //result.AddRange(context.Set<TEntity>().AddRange(bulkEntities));
                        context.Set<TEntity>().AddRange(bulkEntities);
                        context.SaveChanges();
                        context.Dispose();
                        entityCnxStringBuilder =
                        new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings[_context.GetType().Name].ConnectionString);
                        context = new DbContext(entityCnxStringBuilder.ConnectionString);
                        context.Configuration.LazyLoadingEnabled = false;
                        context.Configuration.ProxyCreationEnabled = false;
                        context.Configuration.AutoDetectChangesEnabled = false;

                        ++index;
                        count = index * bulkCount;
                    }
                    
                    //foreach (var entity in entities)
                    //{
                    //    ++count;
                    //    context = AddToContext(context, entity,ref result, count, 100, true);
                    //}

                    context.SaveChanges();
                }
                finally
                {
                    if (context != null)
                        context.Dispose();
                }

                scope.Complete();
            }
            */
            return result;
        }
        //private DbContext AddToContext(DbContext context,TEntity entity,ref List<TEntity> result, int count, int commitCount, bool recreateContext)
        //{
        //    try
        //    {
        //        result.Add(context.Set<TEntity>().Add(entity));
        //        if (count % commitCount == 0)
        //        {
        //            context.SaveChanges();
        //            if (recreateContext)
        //            {
        //                context.Dispose();
        //                var entityCnxStringBuilder =
        //                new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings[_context.GetType().Name].ConnectionString);
        //                context = new DbContext(entityCnxStringBuilder.ConnectionString);
        //                context.Configuration.LazyLoadingEnabled = false;
        //                context.Configuration.ProxyCreationEnabled = false;
        //                context.Configuration.AutoDetectChangesEnabled = false;
        //            }
        //        }
        //        return context;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public virtual void Delete(int id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
        public virtual void Detach(TEntity entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Detached;
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Update(TEntity entityToUpdate, List<string> excluded)
        {
            DbSet.Attach(entityToUpdate);
            var entry = _context.Entry(entityToUpdate);
            entry.State = EntityState.Modified;

            if (excluded != null)
            {
                foreach (var name in excluded)
                {
                    entry.Property(name).IsModified = false;
                }
            }
        }
        public virtual void DeleteRange(IQueryable<TEntity> entitiesToDelete)
        {
            foreach (var entity in entitiesToDelete)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }
            DbSet.RemoveRange(entitiesToDelete.AsEnumerable());
        }
        public virtual IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters) as IQueryable<TEntity>; //DbSet.SqlQuery to connect directly to the database
        }
    }
}