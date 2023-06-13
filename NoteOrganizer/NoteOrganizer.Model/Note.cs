

namespace NoteOrganizer.Model
{
    public class Note : BaseModel
    {
        public string NoteHeader { get; set; }
        public string NoteBody { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
