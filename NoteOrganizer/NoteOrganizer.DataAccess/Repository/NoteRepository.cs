using NoteOrganizer.Core.Interface;
using NoteOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteOrganizer.DataAccess.Repository
{
    public class NoteRepository : GenericRepository<Note> , INoteRepository
    {
        public NoteRepository(NoteOrganizerDbContext content) : base(content) { }
        
    }
}
