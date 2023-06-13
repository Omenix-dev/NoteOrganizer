using NoteOrganizer.Core.Interface;
using NoteOrganizer.Model;


namespace NoteOrganizer.DataAccess.Repository
{
    public class UserRepository : GenericRepository<User> ,IUserRepository
    {
        public UserRepository(NoteOrganizerDbContext context) : base(context) 
        {
                
        }
    }
}
