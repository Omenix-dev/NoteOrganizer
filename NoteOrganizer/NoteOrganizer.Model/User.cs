

namespace NoteOrganizer.Model
{
    public class User : BaseModel
    {
        public string UserName { get; set; }          
        public string Password { get; set; }
        public string Role { get; set; }
        public string AccessCode { get; set; }
        public IEnumerable<Note> Notes { get; set; }    
    }
}