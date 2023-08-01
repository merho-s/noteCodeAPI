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
        private TagAliasRepository _tagAliasRepos;
        private UserAppService _userService;

        public NoteService(NoteRepository noteRepos, CodetagRepository codetagRepos, TagAliasRepository tagAliasRepos, UserAppService userService)
        {
            _noteRepos = noteRepos;
            _codetagRepos = codetagRepos;
            _tagAliasRepos = tagAliasRepos;
            _userService = userService;
        }

        public async Task<string> GetAliasAsync(string tag)
        {
            TagAlias alias = await _tagAliasRepos.GetAliasByNameAsync(tag);
            if (alias != null)
            {
                return alias.Name;
            }
            var allCodetags = await _codetagRepos.GetAllAsync();
            var codetag = allCodetags.FirstOrDefault(t => t.Name.ToLower() == tag.ToLower());
            if (codetag != null)
            {
                var tagAlias = await _tagAliasRepos.GetAliasByTagIdAsync(codetag.Id);
                return tagAlias.Name;
            }
            throw new TagsDontExistException();
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
                    User = loggedUser,
                    Codes = await Task.WhenAll(noteRequest.Codes.Select(async c => new CodeSnippet() { Code = c.Code, Description = c.Description, Language = await GetAliasAsync(c.Language) })),
                    Codetags = await Task.WhenAll(noteRequest.Codetags.Select(async c => await _codetagRepos.GetByNameAsync(c)).ToList())
                };

                // ADD LANGUAGE CODE TO TAGS (managed in front now)
/*
                foreach (var c in newNote.Codes)
                {
                    Codetag tag = await _codetagRepos.GetByAliasNameAsync(c.Language);
                    if (tag != null)
                    {
                        newNote.Codetags.Add(tag);
                    }
                }*/

                //NO NEED TO CHECK IF TAGS DONT EXIST ANYMORE BECAUSE UNKNOWN WILL BE ABLE TO BE ADDED TO USER TAGS
                //if (noteRequest.Codetags != null)
                //{
                //    foreach (var t in noteRequest.Codetags)
                //    {
                //        Codetag newCodetag = await _codetagRepos.GetByNameAsync(t.Name);
                //        if (newCodetag == null)
                //        {
                //            throw new TagsDontExistException();
                //        }
                //        else if (!newNote.Codetags.Contains(newCodetag))
                //        {
                //            newNote.Codetags.Add(newCodetag);
                //        }
                //    }
                //}
                //else throw new TagsDontExistException();



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
                        Codes = n.Codes.Select(el => new CodeSnippetDTO { Code = el.Code, Description = el.Description, Language = el.Language }).ToList(),
                        Codetags = n.Codetags.Select(ct => ct.Name).ToList()
                        //NO NEED TO CHECK IF TAGS DONT EXIST ANYMORE BECAUSE UNKNOWN WILL BE ABLE TO BE ADDED TO USER TAGS
                        //Codetags = await Task.WhenAll(n.Codetags.Select(async el =>
                        //{
                        //    Codetag codetag = await _codetagRepos.GetByIdAsync(el.Id);
                        //    if (codetag != null)
                        //    {
                        //        return new CodetagDTO { Name = codetag.Name };
                        //    }
                        //    throw new TagsDontExistException();
                        //}))
                    };

                    notesResponseList.Add(noteResponse);
                }
                return notesResponseList;
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
                        Codes = singleNote.Codes.Select(el => new CodeSnippetDTO { Code = el.Code, Description = el.Description, Language = el.Language }).ToList(),
                        Codetags = singleNote.Codetags.Select(ct => ct.Name).ToList()
                        //NO NEED TO CHECK IF TAGS DONT EXIST ANYMORE BECAUSE UNKNOWN WILL BE ABLE TO BE ADDED TO USER TAGS
                        //Codetags = await Task.WhenAll(singleNote.Codetags.Select(async el =>
                        //{
                        //    Codetag codetag = await _codetagRepos.GetByIdAsync(el.Id);
                        //    if (codetag != null)
                        //    {
                        //        return new CodetagDTO { Name = codetag.Name };
                        //    }
                        //    throw new TagsDontExistException();
                        //}))
                        //Image = singleNote.Image,
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
                    Codes = n.Codes.Select(el => new CodeSnippetDTO { Code = el.Code, Description = el.Description, Language = el.Language }).ToList(),
                    Codetags = n.Codetags.Select(ct => ct.Name).ToList()
                    //NO NEED TO CHECK IF TAGS DONT EXIST ANYMORE BECAUSE UNKNOWN WILL BE ABLE TO BE ADDED TO USER TAGS
                    //Codetags = await Task.WhenAll(n.Codetags.Select(async el =>
                    //{
                    //    Codetag codetag = await _codetagRepos.GetByIdAsync(el.Id);
                    //    if (codetag != null)
                    //    {
                    //        return new CodetagDTO { Name = codetag.Name };
                    //    }
                    //    throw new TagsDontExistException();
                    //}))
                    //Image = n.Image
                };

                notesResponseList.Add(noteResponse);
            }
            return notesResponseList;
        }
    }
}
