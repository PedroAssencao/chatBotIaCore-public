using chatBotIaCore.Domain.Models;

namespace chatBotIaCore.Infra.Interfaces
{
    public interface IMessageInterface : IBaseInterface<Message>
    {
        Task<bool> messageAlreadyExist(string externalId);
    }
}
