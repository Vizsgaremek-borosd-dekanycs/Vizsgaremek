using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Exceptions;
using vetcms.ServerApplication.Domain.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace vetcms.ServerApplication.Common.Abstractions.Data
{
    public abstract class RepositoryBase<T>(DbContext context) : IRepositoryBase<T>
        where T : AuditedEntity
    {
        protected readonly DbSet<T> Entities = context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false)
        {
            return await WhereAsync(e => true);
        }


        public async Task<T> GetByIdAsync(int id, bool includeDeleted = false)
        {

            try
            {
                var result = await IncludeAll(Entities).Where(e => (includeDeleted || !e.Deleted) && e.Id == id).FirstAsync();
                return result;
            }
            catch(InvalidOperationException)
            {
                throw new NotFoundException(nameof(T), id);
            }
        }

        public IEnumerable<T> Where(Func<T, bool> predicate, bool includeDeleted = false)
        {
            return IncludeAll(Entities).Where(predicate).Where(e => includeDeleted || !e.Deleted);
        }

        public async Task<IEnumerable<T>> WhereAsync(Func<T, bool> predicate, bool includeDeleted = false)
        {
            return await IncludeAll(Entities).Where(e => includeDeleted || !e.Deleted).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await Entities.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Entities.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await Entities.Where(e => !e.Deleted && e.Id == id).FirstAsync();
            if (entity == null)
            {
                throw new NotFoundException(nameof(T), id);
            }

            Entities.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ExistAsync(int id, bool includeDeleted = false)
        {
            return await Entities.AnyAsync(e => (e.Id == id && (includeDeleted || !e.Deleted)) );
        }

        public void LoadReferencedCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> propertyExpression)
            where TProperty : class
        {
            Entities.Entry(entity).Collection(propertyExpression).Load();
        }


        public IQueryable<T> Search(string searchTerm = "", bool includeDeleted = false)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return IncludeAll(Entities); // Return paginated original query if search term is empty

            var parameter = Expression.Parameter(typeof(T), "e");
            var properties = typeof(T)
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string)) // Only string properties
                .ToList();

            if (!properties.Any())
                return IncludeAll(Entities);

            Expression combinedExpression = null;

            foreach (var property in properties)
            {
                var propertyAccess = Expression.Property(parameter, property);
                var searchExpression = Expression.Call(
                    propertyAccess,
                    nameof(string.Contains),
                    Type.EmptyTypes,
                    Expression.Constant(searchTerm)
                );

                combinedExpression = combinedExpression == null
                    ? searchExpression
                    : Expression.OrElse(combinedExpression, searchExpression);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            return IncludeAll(Entities).Where(e => includeDeleted || !e.Deleted).Where(lambda);
        }

        public async Task<List<T>> SearchAsync(string searchTerm = "", int skip = 0, int take = 10, bool includeDeleted = false)
        {
            return await Search(searchTerm).Skip(skip).Take(take).ToListAsync();
        }

        protected IQueryable<T> IncludeAll(IQueryable<T> dataset)
        {
            foreach (var property in context.Model.FindEntityType(typeof(T)).GetNavigations())
            {
                dataset = dataset.Include(property.Name);
            }
            return dataset;
        }
    }
}
