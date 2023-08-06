using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class NoteRepository : BaseRepository<Note>
    {
        public NoteRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<bool> DeleteAsync(Note element)
        {
            _dbContext.Notes.Remove(element);
            return await UpdateAsync();
        }

        public override async Task<List<Note>> GetAllAsync()
        {
            return await _dbContext.Notes.Include(n => n.Codetags).Include(n => n.Codes).ToListAsync();        
        }

        public override async Task<Note> GetByGuidAsync(Guid id)
        {
            return await _dbContext.Notes.Include(n => n.Codetags).Include(n => n.Codes).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Note>> GetAllByUserIdAsync(int userId)
        {
            return await _dbContext.Notes.Include(n => n.Codetags).Include(n => n.Codes).Where(n => n.User.Id == userId).ToListAsync();
        }

        public override async Task<bool> SaveAsync(Note element)
        {
            _dbContext.Notes.Add(element);
            return await UpdateAsync();
        }

        public override Task<Note> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
