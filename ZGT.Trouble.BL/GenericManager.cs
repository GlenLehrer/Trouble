namespace ZGT.Trouble.BL
{
    public abstract class GenericManager<T> where T : class, IEntity
    {
        //A logger saves extraneous information about my program running, such as error messages.
        protected DbContextOptions<TroubleEntities> options;
        protected readonly ILogger logger;

        public GenericManager(ILogger logger, DbContextOptions<TroubleEntities> options)
        {
            this.logger = logger;
            this.options = options;
        }

        public GenericManager(DbContextOptions<TroubleEntities> options)
        {
            this.options = options;
        }

        public GenericManager()
        {

        }


        public async Task<int> UpdateAsync(T entity, bool rollback = false)
        {
            try
            {
                int results = 0;


                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    dc.Entry(entity).State = EntityState.Modified;

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {
                if (logger != null)
                    logger.LogWarning($"Update {typeof(T).Name}s - GenericManager");
                throw;
            }
        }
        public async Task<Guid> InsertAsync(T entity,
                                            Expression<Func<T, bool>> expression = null,
                                            bool rollback = false) //a void method, would like like: public async Task DoSomethingAsync()
        {
            try
            {
                Guid results = Guid.Empty;

                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    if ((expression == null) || ((expression != null) && (!dc.Set<T>().Any(expression))))
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        entity.Id = Guid.NewGuid();

                        dc.Set<T>().Add(entity);

                        dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        if (logger != null) logger.LogWarning("Row already exists. {UserId}", "gLehrer");
                    }
                }
                return entity.Id;
            }
            catch (Exception)
            {
                if (logger != null)
                    logger.LogWarning($"Insert {typeof(T).Name}s - GenericManager");
                throw new Exception("Row already exists.");
            }
        }
        public virtual async Task<int> DeleteAsync(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    T row = dc.Set<T>().FirstOrDefault(t => t.Id == id);

                    if (row != null)
                    {
                        dc.Set<T>().Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                }
                return results;
            }
            catch (Exception)
            {
                if (logger != null)
                    logger.LogWarning($"Delete {typeof(T).Name}s - GenericManager");
                throw;
            }
        }
        public async Task<List<T>> LoadAsync(Expression<Func<T, bool>> filter = null,
                                             Expression<Func<T, object>>[] includeProperties = null)
        {
            try
            {
                if (filter == null) filter = e => true;

                if (logger != null)
                    logger.LogWarning($"Get {typeof(T).Name}s - GenericManager");

                IQueryable<T> rows = new TroubleEntities(options)
                    .Set<T>()
                    .Where(filter);

                if (includeProperties != null)
                {
                    foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                    {
                        rows = rows.Include<T, object>(includeProperty);
                    }
                }


                return await rows.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public V Map<U, V>(U objfrom)
        {
            V objTo = (V)Activator.CreateInstance(typeof(V));

            var ToProperties = objTo.GetType().GetProperties();
            var FromProperties = objfrom.GetType().GetProperties();

            ToProperties.ToList().ForEach(o =>
            {
                var fromp = FromProperties.FirstOrDefault(x => x.Name == o.Name && x.PropertyType == o.PropertyType);
                if (fromp != null)
                    o.SetValue(objTo, fromp.GetValue(objfrom));
            });

            return objTo;
        }

    }
}
