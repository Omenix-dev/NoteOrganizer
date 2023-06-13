

namespace NoteOrganizer.Core.Interface
{
    public interface IUnitOfWork
    {
        INoteRepository NoteRepository { get; }
        IUserRepository UserRepository { get; }

        Task Commit();
        Task CreateTransaction();
        void Dispose();
        Task Rollback();
        Task Save();
    }
}
