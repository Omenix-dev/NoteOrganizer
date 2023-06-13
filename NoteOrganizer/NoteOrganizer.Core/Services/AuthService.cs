using NoteOrganizer.Core.DTO;
using NoteOrganizer.Core.Interface;
using NoteOrganizer.Model;
using EasyEncryption;
using Newtonsoft.Json.Linq;

namespace NoteOrganizer.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<ResponseDTO<string>> Login(UserDto model)
        {
            try
            {
                var value = _unitOfWork.UserRepository.GetAllAsync()
                            .Where(x => x.Password.Equals(SHA.ComputeSHA256Hash(model.Password))
                            && x.UserName.Equals(model.UserName)).First();
                if (value is not null)
                {
                    var accessToken = GenerateRandomString();
                    var Usermodel = new User
                    {
                        Id = value.Id,
                        UserName = value.UserName,
                        Password = value.Password,
                        AccessCode = accessToken
                    };
                    _unitOfWork.UserRepository.Update(Usermodel);
                    _unitOfWork.Save();
                     return Task.Run(()=>ResponseDTO<string>.Success("Login successfully", accessToken,200 ));
                }
                 return Task.Run(()=>ResponseDTO<string>.Fail("Login Failed", 400));

            }catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _unitOfWork.Dispose();
               return Task.Run(() => ResponseDTO<string>.Fail(ex.Message, 400));
            }
        }

        public Task<ResponseDTO<string>> Register(UserDto model)
        {
            try
            {
                var Usermodel = new User
                {
                    UserName = model.UserName,
                    Password = SHA.ComputeSHA256Hash(model.Password),
                    AccessCode = null,
                    Role = "User"
                };
                _unitOfWork.UserRepository.InsertAsync(Usermodel);
                _unitOfWork.Save();
                return Task.Run(() => ResponseDTO<string>.Success("Successfull registered User", "Successfull", 201));
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _unitOfWork.Dispose();
                return Task.Run(() => ResponseDTO<string>.Fail(ex.Message, 404));
            }
        }

        string GenerateRandomString()
        {
            Random random = new Random();
            string randomChars = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4)
                                                      .Select(s => s[random.Next(s.Length)]).ToArray());

            DateTime currentTime = DateTime.Now;
            DateTime appendedTime = currentTime.AddMinutes(5);
            string formattedTime = appendedTime.ToString("yyyy-MM-dd HH:mm:ss");

            string result = randomChars + "/" + formattedTime;
            return result;
        }
    }
}
