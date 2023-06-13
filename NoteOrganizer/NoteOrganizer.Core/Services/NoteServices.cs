using NoteOrganizer.Core.DTO;
using NoteOrganizer.Core.Interface;
using NoteOrganizer.Model;

namespace NoteOrganizer.Core.Services
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork _unitOfWork;   
        public NoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<ResponseDTO<string>> CreateNoteById(NoteDto note)
        {
            try
            {
                var noteObj = new Note
                {
                    UserId = note.UserId,
                    NoteBody = note.NoteBody,
                    NoteHeader = note.NoteHeader
                };
                _unitOfWork.NoteRepository.InsertAsync(noteObj);
                _unitOfWork.Save();
                return Task.Run(()=>ResponseDTO<string>.Success("note has been created", "Successfull", 201));
            }
            catch (Exception ex)
            {
                return Task.Run(()=> ResponseDTO<string>.Fail(ex.Message));
            }
        }

        public Task<ResponseDTO<string>> DeleteNoteById(string noteId)
        {
            try
            {
                _unitOfWork.NoteRepository.DeleteAsync(noteId);
                _unitOfWork.Save();
                return Task.Run(() => ResponseDTO<string>.Success("note has been deleted", "Successfull", 201));
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _unitOfWork.Dispose();
                return Task.Run(() => ResponseDTO<string>.Fail(ex.Message));
            }
        }

        public Task<ResponseDTO<List<NoteDto>>> GetAllNote(string UserId)
        {
            try
            {
               var result = _unitOfWork.NoteRepository.GetAllAsync().Where(x => x.UserId == UserId)
                            .Select(x => new NoteDto{ NoteBody = x.NoteBody,NoteHeader = x.NoteHeader}).ToList();
                return Task.Run(() => ResponseDTO<List<NoteDto>>.Success("successfully gotten the value from the database",result, 200));
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _unitOfWork.Dispose();
                return Task.Run(() => ResponseDTO<List<NoteDto>>.Fail(ex.Message));
            }       
        }

        public async Task<ResponseDTO<NoteDto>> GetNotesById(string noteId)
        {
            try
            {
               var result = await _unitOfWork.NoteRepository.GetAsync(x=> x.Id == noteId);
                return await Task.Run(() => ResponseDTO<NoteDto>.Success("Successfully gotten note",
                    new NoteDto { NoteBody = result.NoteBody,NoteHeader = result.NoteHeader,UserId = result.UserId},200));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => ResponseDTO<NoteDto>.Fail(ex.Message));
            }
        }

        public Task<ResponseDTO<bool>> UpdateNoteById(NoteDto note)
        {
            try
            {
                var notes = new Note
                {
                    UserId = note.UserId,
                    Id = note.NoteId,
                    NoteHeader = note.NoteHeader,
                    NoteBody = note.NoteBody,
                    UpdateAt = DateTimeOffset.Now
                };
                _unitOfWork.NoteRepository.Update(notes);
                _unitOfWork.Save();
                return Task.Run(() => ResponseDTO<bool>.Success("Successfully updated the note in the database", true, 202));
            }
            catch (Exception ex)
            {
                return Task.Run(() => ResponseDTO<bool>.Fail(ex.Message));
            }
        }
    }
}
