using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

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

        public NoteResponseDTO AddNote(NoteRequestDTO noteRequest, IFormFile imageFile)
        {
            UserApp loggedUser = _userService.GetLoggedUser();
            if (loggedUser != null)
            {
                Note newNote = new Note()
                {
                    Title = noteRequest.Title,
                    Description = noteRequest.Description,
                    Code = noteRequest.Code,
                    User = loggedUser
                };

                if (noteRequest.Codetags != null)
                {
                    noteRequest.Codetags.ForEach(t =>
                    {
                        Codetag newCodetag = _codetagRepos.GetByName(t.Name);
                        if (newCodetag != null)
                        {
                            newNote.Codetags.Add(new NotesTags() { Note = newNote, Tag = newCodetag });
                        }
                        else throw new TagsDontExistException();
                    });   
                }
                else throw new TagsDontExistException();

                try
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
                }


                if (_noteRepos.Save(newNote))
                {
                    NoteResponseDTO noteResponse = new NoteResponseDTO()
                    {
                        Id = newNote.Id,
                        Title = newNote.Title,
                        Description = newNote.Description,
                        Code = newNote.Code,
                        Image = newNote.Image
                    };

                    newNote.Codetags.ForEach(t =>
                    {
                        noteResponse.Codetags.Add(new CodetagDTO() { Name = t.Tag.Name });
                    });

                    return noteResponse;
                }
                else throw new DatabaseException();

            }
            else throw new NotLoggedUserException();
            
        }

        public string UploadNoteImage(IFormFile imageFile)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "assets", imageFile.FileName);
            FileStream fileStream = new(filePath, FileMode.Create);
            imageFile.CopyTo(fileStream);
            fileStream.Close();
            if (filePath != null && imageFile != null)
            {
                return filePath;
            } else return null;
        }

        public List<NoteResponseDTO> GetNotesList() 
        {
            UserApp loggedUser = _userService.GetLoggedUser();
            if (loggedUser != null)
            {
                List<NoteResponseDTO> notesResponseList = new();
                _noteRepos.GetAllByUserId(loggedUser.Id).ForEach(n =>
                {
                    NoteResponseDTO noteResponse = new()
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Description = n.Description,
                        Code = n.Code,
                        Image = n.Image
                    };

                    n.Codetags.ForEach(t =>
                    {
                        if (t.TagId != null)
                        {
                            noteResponse.Codetags.Add(new CodetagDTO { Name = _codetagRepos.GetById(t.TagId).Name });
                        }
                    });

                    notesResponseList.Add(noteResponse);
                });
                return notesResponseList;
            } throw new NotLoggedUserException();
        }

        public NoteResponseDTO GetSingleNote(int id)
        {
            UserApp loggedUser = _userService.GetLoggedUser();
            Note singleNote = _noteRepos.GetById(id);
            if(singleNote != null)
            {
                if (singleNote.User == loggedUser)
                {
                    NoteResponseDTO noteResponse = new()
                    {
                        Id = singleNote.Id,
                        Title = singleNote.Title,
                        Description = singleNote.Description,
                        Image = singleNote.Image,
                        Code = singleNote.Code
                    };

                    singleNote.Codetags.ForEach(t =>
                    {
                        if (t.TagId != null)
                        {
                            noteResponse.Codetags.Add(new CodetagDTO { Name = _codetagRepos.GetById(t.TagId).Name });
                        }
                    });
                    
                    return noteResponse;
                }
                else throw new NotLoggedUserException("Actual logged user doesn't have access to this note because it's not his.");
            } else throw new DatabaseException("This note dosen't exist.");
        }

        public List<NoteResponseDTO> GetAllNotesTest()
        {
            List<NoteResponseDTO> notesResponseList = new();
            
            _noteRepos.GetAll().ForEach(n =>
            {
                NoteResponseDTO noteResponse = new()
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    Code = n.Code,
                    Image = n.Image
                };
                
                n.Codetags.ForEach(t =>
                {
                    if (t.TagId != null)
                    {
                        noteResponse.Codetags.Add(new CodetagDTO { Name = _codetagRepos.GetById(t.TagId).Name });
                    }
                });

                notesResponseList.Add(noteResponse);
            });
            return notesResponseList;
        }
    }
}
