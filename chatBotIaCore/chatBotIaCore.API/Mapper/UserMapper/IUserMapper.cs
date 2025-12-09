using chatBotIaCore.API.Models.DTO;
using chatBotIaCore.Domain.Models;

namespace chatBotIaCore.API.Mapper.UserMapper
{
    public interface IUserMapper
    {
        public List<UserDTO.UserDTOGetView> userToUserDTOGetView(List<User> models);
    }
}
