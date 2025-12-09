using chatBotIaCore.API.Models.DTO;
using chatBotIaCore.Domain.Models;

namespace chatBotIaCore.API.Mapper.UserMapper
{
    public class UserMapper : IUserMapper
    {
        public List<UserDTO.UserDTOGetView> userToUserDTOGetView(List<User> models) => models.Select(x => new UserDTO.UserDTOGetView
        {
            Code = x.UseId,
            Name = x.UseName,
            Email = x.UseEmail,
            createDate = x.UseCreatedAt
        }).ToList();
    }
}
