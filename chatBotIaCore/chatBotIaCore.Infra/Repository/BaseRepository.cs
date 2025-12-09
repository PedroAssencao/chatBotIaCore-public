using chatBotIaCore.Infra.DAL;
using chatBotIaCore.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace chatBotIaCore.Infra.Repository
{
    public class BaseRepository<T> : IBaseInterface<T> where T : class
    {
        protected readonly ChatBotIaCoreContext _context;

        public BaseRepository(ChatBotIaCoreContext context)
        {
            _context = context;
        }

        public async Task<List<T>> getAllAsync()
        {
            IQueryable<T> iQueryAble = _context.Set<T>();

            var navigationProperties = _context.Model.FindEntityType(typeof(T))?
                .GetNavigations()
                .Select(e => e.Name);

            foreach (var item in navigationProperties!)
            {
                iQueryAble = iQueryAble.Include(item);
            }

            return await iQueryAble.ToListAsync();
        }
        public async Task<T?> getByIdAsync(int id)
        {
            IQueryable<T> query = _context.Set<T>();

            var navigationProperties = _context.Model.FindEntityType(typeof(T))!
                .GetNavigations()
                .Select(n => n.Name);

            foreach (var property in navigationProperties)
            {
                query = query.Include(property);
            }

            var primaryKeyName = _context?.Model?.FindEntityType(typeof(T))
                ?.FindPrimaryKey()
                ?.Properties
                ?.Select(p => p.Name)
                ?.FirstOrDefault();

            if (primaryKeyName == null)
            {
                throw new InvalidOperationException($"A entidade {typeof(T).Name} não possui uma chave primária definida.");
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, primaryKeyName) == id);
        }
        public async Task<T> createAsync(T model)
        {
            var result = await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<T> updateAsync(T model)
        {
            var result = _context.Set<T>().Update(model);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> deleteAsync(int id)
        {
            try
            {
                _context.Set<T>().Remove(await _context.Set<T>().FindAsync(id) ?? throw new NullReferenceException($"the element of {_context.Set<T>().EntityType.Name} with the id {id} were not found"));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
