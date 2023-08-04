using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.OpenApi.Extensions;
using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using System.Runtime.InteropServices;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace noteCodeAPI.Services
{
    public class NoteService
    {
        private NoteRepository _noteRepos;
        private CodetagRepository _codetagRepos;
        private UserAppService _userService;

        public NoteService(NoteRepository noteRepos, CodetagRepository codetagRepos, UserAppService userService)
        {
            _noteRepos = noteRepos;
            _codetagRepos = codetagRepos;
            _userService = userService;
        }

        public async Task<NoteResponseDTO> AddNoteAsync(NoteRequestDTO noteRequest/*, IFormFile imageFile*/)
        {
            UserApp loggedUser = await _userService.GetLoggedUserAsync();
            
            if (loggedUser != null)
            {
                List<CodeSnippet> newCodes = new();
                Note newNote = new Note()
                {
                    Title = noteRequest.Title,
                    Description = noteRequest.Description,
                    CreationDate = DateTime.Now,
                    User = loggedUser,
                    Codes = new List<CodeSnippet>(),
                };

                if (noteRequest.Codes != null)
                {
                    foreach (var code in noteRequest.Codes)
                    {
                        CodeSnippet codeSnippet = new()
                        {
                            Code = code.Code,
                            Description = code.Description,
                            Language = code.Language
                        };
                        newNote.Codes.Add(codeSnippet);
                    }
                }

                if (noteRequest.Codetags != null)
                {
                    foreach (var t in noteRequest.Codetags)
                    {
                        var newCodetag = await _codetagRepos.GetByNameAsync(t);
                        if (newCodetag == null)
                        {
                            throw new TagsDontExistException();
                        }
                        else if (!newNote.Codetags.Contains(newCodetag))
                        {
                            newNote.Codetags.Add(newCodetag);
                        }
                    }
                }
                else throw new TagsDontExistException();



                //Upload image

                /*try
                {
                    newNote.Image = UploadNoteImage(imageFile);
                }
                catch (IOException IOEx)
                {
                    Console.WriteLine(IOEx.Message);
                    throw new UploadException(IOEx.Message);
                }

                if (newNote.Image == null)
                {
                    throw new UploadException();
                }*/


                if (await _noteRepos.SaveAsync(newNote))
                {
                    NoteResponseDTO noteResponse = new NoteResponseDTO()
                    {
                        Id = newNote.Id,
                        Title = newNote.Title,
                        Description = newNote.Description,
                        CreationDate = newNote.CreationDate,
                        Codes = newNote.Codes.Select(el => new CodeSnippetDTO { Code = el.Code, Description = el.Description, Language = el.Language }).ToList(),
                        Codetags = newNote.Codetags.Select(el => el.Name).ToList()

                    };

                    return noteResponse;
                }
                else throw new DatabaseException();

            }
            else throw new NotFoundUserException();
            
        }

        public async Task<bool> DeleteUserNoteAsync(int noteId)
        {
            var noteToDelete = await _noteRepos.GetByIdAsync(noteId);
            if(noteToDelete != null)
            {
                var userLogged = await _userService.GetLoggedUserAsync();
                var userNotes = await _noteRepos.GetAllByUserIdAsync(userLogged.Id);
                if (userNotes.Contains(noteToDelete))
                {
                    if (await _noteRepos.DeleteAsync(noteToDelete))
                        return true;
                }
            }
            throw new DatabaseException();
        }
        //public string UploadNoteImage(IFormFile imageFile)
        //{
        //    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "assets", imageFile.FileName);
        //    FileStream fileStream = new(filePath, FileMode.Create);
        //    imageFile.CopyTo(fileStream);
        //    fileStream.Close();
        //    if (filePath != null && imageFile != null)
        //    {
        //        return filePath;
        //    } else return null;
        //}

        public async Task<List<NoteResponseDTO>> GetUserNotesAsync() 
        {
            UserApp loggedUser = await _userService.GetLoggedUserAsync();
            if (loggedUser != null)
            {
                List<NoteResponseDTO> notesResponseList = new();
                var userNotes = await _noteRepos.GetAllByUserIdAsync(loggedUser.Id);
                foreach(var n in userNotes)
                {
                    NoteResponseDTO noteResponse = new()
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Description = n.Description,
                        CreationDate = n.CreationDate,
                        Codes = n.Codes.Select(el => new CodeSnippetDTO { Code = el.Code, Description = el.Description, Language = el.Language }).ToList(),
                        Codetags = n.Codetags.Select(ct => ct.Name).ToList()
                    };

                    notesResponseList.Add(noteResponse);
                }
                return notesResponseList.OrderByDescending(note => note.CreationDate).ToList();
            } throw new NotFoundUserException();
        }

        public async Task<NoteResponseDTO> GetSingleUserNoteAsync(int id)
        {
            UserApp loggedUser = await _userService.GetLoggedUserAsync();
            Note singleNote = await _noteRepos.GetByIdAsync(id);
            if(singleNote != null)
            {
                if (singleNote.User == loggedUser)
                {
                    NoteResponseDTO noteResponse = new()
                    {
                        Id = singleNote.Id,
                        Title = singleNote.Title,
                        Description = singleNote.Description,
                        CreationDate = singleNote.CreationDate,
                        Codes = singleNote.Codes.Select(el => new CodeSnippetDTO { Code = el.Code, Description = el.Description, Language = el.Language }).ToList(),
                        Codetags = singleNote.Codetags.Select(ct => ct.Name).ToList()
                    };
                    
                    return noteResponse;
                }
                else throw new NotFoundUserException("Actual logged user doesn't have access to this note because it's not his.");
            } else throw new DatabaseException("This note dosen't exist.");
        }

        public async Task<List<NoteResponseDTO>> GetAllNotesAsync()
        {
            List<NoteResponseDTO> notesResponseList = new();

            var allNotes = await _noteRepos.GetAllAsync();
            foreach(var n in allNotes)
            {
                NoteResponseDTO noteResponse = new()
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    CreationDate = n.CreationDate,
                    Codes = n.Codes.Select(el => new CodeSnippetDTO { Code = el.Code, Description = el.Description, Language = el.Language }).ToList(),
                    Codetags = n.Codetags.Select(ct => ct.Name).ToList()
                };

                notesResponseList.Add(noteResponse);
            }
            return notesResponseList;
        }
    }
}
