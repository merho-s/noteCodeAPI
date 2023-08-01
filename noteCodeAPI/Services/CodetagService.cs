using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using System.Runtime.CompilerServices;

namespace noteCodeAPI.Services
{
    public class CodetagService
    {
        private CodetagRepository _codetagRepos;

        public CodetagService(CodetagRepository codetagRepos)
        {
            _codetagRepos = codetagRepos;
        }

        public async Task<List<string>> GetCodetagsAsync()
        {
            var codetags = await _codetagRepos.GetAllAsync();
            if (codetags != null)
            {
                return codetags.Select(c => c.Name).ToList();
            }
            else throw new DatabaseException();
 
        }
    }
}
