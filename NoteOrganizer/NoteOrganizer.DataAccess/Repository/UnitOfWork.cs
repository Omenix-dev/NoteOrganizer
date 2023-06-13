using Microsoft.EntityFrameworkCore.Storage;
using NoteOrganizer.Core.Interface;

namespace NoteOrganizer.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposedValue;
        private readonly NoteOrganizerDbContext _context;
        private IDbContextTransaction _objTransaction;
        private INoteRepository _noteRepository;
        private IUserRepository _userRepository;
        public UnitOfWork(NoteOrganizerDbContext context)
        {
            _context = context;

        }

        public INoteRepository NoteRepository => _noteRepository ??= new NoteRepository(_context);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public async Task CreateTransaction()
        {
            _objTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _objTransaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _objTransaction?.RollbackAsync();
            await _objTransaction.DisposeAsync();
        }


        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
