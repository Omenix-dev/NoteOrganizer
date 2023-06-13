

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoteOrganizer.Model
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }  
        public DateTimeOffset AddedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdateAt { get; set; } = default(DateTimeOffset);

    }
}
