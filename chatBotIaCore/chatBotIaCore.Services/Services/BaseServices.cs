using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Services.Interfaces.Base;

namespace chatBotIaCore.Services.Services
{
    public class BaseServices<T> : IBaseServices<T> where T : class
    {
        protected readonly IBaseInterface<T> _repository;

        public BaseServices(IBaseInterface<T> repository)
        {
            _repository = repository;
        }
        public async Task<List<T>> getAllAsync()
        {
            try
            {
                return await _repository.getAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("A error ocurrer while trying to get the items, error: " + ex.Message);
            }
        }
        public async Task<T?> getByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("The Id Informed is invalid");
                }

                return await _repository.getByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("A error ocurrer while trying to get the item, error: " + ex.Message);
            }
        }
        public async Task<T> createAsync(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new Exception("The Model Recaive to create was empty");
                }

                return await _repository.createAsync(model);
            }
            catch (Exception ex)
            {
                throw new Exception("A error ocurrer while trying to create the item, error: " + ex.Message);
            }
        }
        public async Task<T> updateAsync(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new Exception("The Model Recaive to update was empty");
                }

                return await _repository.updateAsync(model);
            }
            catch (Exception ex)
            {
                throw new Exception("A error ocurrer while trying to update the item, error: " + ex.Message);
            }
        }

        public async Task<bool> deleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("The Id Informed is invalid");
                }

                return await _repository.deleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("A error ocurrer while trying to delete the item, error: " + ex.Message);
            }
        }
    }
}
