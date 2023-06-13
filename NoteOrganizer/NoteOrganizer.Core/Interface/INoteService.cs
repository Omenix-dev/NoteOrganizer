using NoteOrganizer.Core.DTO;

namespace NoteOrganizer.Core.Interface
{
    public interface INoteService
    {
        Task<ResponseDTO<string>> CreateNoteById(NoteDto note);
        Task<ResponseDTO<bool>> UpdateNoteById(NoteDto note);
        Task<ResponseDTO<string>> DeleteNoteById(string noteId);
        Task<ResponseDTO<NoteDto>>  GetNotesById(string noteId);
        Task<ResponseDTO<List<NoteDto>>> GetAllNote(string userId);
        
    }
}