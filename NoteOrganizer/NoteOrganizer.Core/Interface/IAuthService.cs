using NoteOrganizer.Core.DTO;

namespace NoteOrganizer.Core.Interface
{
    public interface IAuthService
    {
       public Task<ResponseDTO<string>> Register(UserDto model);
       public Task<ResponseDTO<string>> Login(UserDto model);
    }
}
