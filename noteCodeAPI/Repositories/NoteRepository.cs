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
            return _dbContext.Notes.Include(n => n.Codetags).Include(n => n.Codes).ToList();        
        }

        public override async Task<Note> GetByIdAsync(int id)
        {
            return _dbContext.Notes.Include(n => n.Codetags).Include(n => n.Codes).FirstOrDefault(n => n.Id == id);
        }

        public async Task<List<Note>> GetAllByUserIdAsync(int userId)
        {
            return _dbContext.Notes.Include(n => n.Codetags).Include(n => n.Codes).Where(n => n.User.Id == userId).ToList();
        }

        public override async Task<bool> SaveAsync(Note element)
        {
            _dbContext.Notes.Add(element);
            return await UpdateAsync();
        }
    }
}
