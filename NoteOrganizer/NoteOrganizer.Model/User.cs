using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoteOrganizer.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserName { get; set; }          
        public string Password { get; set; }   
        public string Role { get; set; }
        public IEnumerable<Note> Notes { get; set; }    
    }
}