using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NoteOrganizer.Core.DTO;
using NoteOrganizer.Core.Interface;
using NoteOrganizer.DataAccess;
using NoteOrganizer.DataAccess.Repository;
using System.Drawing;
using System.Net;

namespace NoteOrganizer.Extensions
{
    public class AuthenticationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="next"></param>
       
        public AuthenticationExceptionMiddleware(RequestDelegate next)
        {

            var context = new NoteOrganizerDbContext(new DbContextOptions<NoteOrganizerDbContext>());
            _next = next;
            _unitOfWork = new UnitOfWork(context);
        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var response = context.Response;
                List<string> ListOfRestrictedEndpoint =
                    new() { "api/Note/CreateNoteById", 
                            "api/Note/UpdateNoteById",
                            "api/Note/DeleteNoteById", 
                            "api/Note/GetNotesById",
                            "api/Note/GetNotesByUserId"
                          };
                var endpointPath = context.Request.Path;
                if(ListOfRestrictedEndpoint.Where(x => x == endpointPath).Count() > 0)
                {
                    var accesstoken = context.Request.Headers["AccessToken"];
                    var UserId = context.Request.Headers["UserId"];
                    var Usermodel = await _unitOfWork.UserRepository.GetAsync(x => x.AccessCode == accesstoken && x.Id == UserId);
                    if (Usermodel is not null) 
                    {
                        bool isExpired = Convert.ToDateTime(Usermodel.AccessCode.Split(" ")[1]) < DateTime.Now;
                        if (isExpired)
                        {
                            var result = JsonConvert.SerializeObject(ResponseDTO<string>.Fail("you access token is expired sign in again"));
                            await response.WriteAsync(result);
                        }
                        await _next.Invoke(context);
                    }
                    else
                    {
                       
                        var result = JsonConvert.SerializeObject(ResponseDTO<string>.Fail("you are not signed in"));
                        await response.WriteAsync(result);
                    }

                }
                await _next.Invoke(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = ResponseDTO<string>.Fail(error.Message);
                switch (error)
                {
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        responseModel.Message = e.Message;
                        break;
                    case ArgumentOutOfRangeException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = e.Message;
                        break;
                    case ArgumentNullException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = e.Message;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = "Internal Server Error. Please Try Again Later.";
                        break;
                }
                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }
        }
        
    }
}
