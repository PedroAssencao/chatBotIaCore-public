namespace chatBotIaCore.API.Models.DTO
{
    public class UserDTO
    {
        public class UserDTOGetView
        {
            public int Code { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public DateTime? createDate { get; set; }
        }
    }
}
