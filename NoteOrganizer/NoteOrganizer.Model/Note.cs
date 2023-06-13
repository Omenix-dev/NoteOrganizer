using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoteOrganizer.Model
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string NoteHeader { get; set; }
        public string NoteBody { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
