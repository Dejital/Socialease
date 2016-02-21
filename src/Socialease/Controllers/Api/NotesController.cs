using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Socialease.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authorization;
using Socialease.ViewModels;

namespace Socialease.Controllers.Api
{
    [Authorize]
    [Route("api/people/{personId}/notes")]
    public class NotesController : Controller
    {
        private readonly ISocialRepository _repository;
        private readonly ILogger<NotesController> _logger;

        public NotesController(ISocialRepository repository, ILogger<NotesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get(int personId)
        {
            var notes = _repository.GetUserNotes(personId, User.Identity.Name);
            var results = Mapper.Map<IEnumerable<NoteViewModel>>(notes);

            return Json(results);
        }

        [HttpGet("{noteId}")]
        public JsonResult Get(int personId, int noteId)
        {
            try
            {
                var results = _repository.GetNoteById(noteId, User.Identity.Name);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<NoteViewModel>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get note with id {noteId}.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { ex.Message });
            }
        }

        [HttpPost("")]
        public JsonResult Post(int personId, [FromBody] NoteViewModel vm)
        {
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            if (!ModelState.IsValid || personId == 0)
            {
                return Json(new {Message = "Failed", ModelState = ModelState});
            }
            try
            {
                var note = Mapper.Map<Note>(vm);
                note.Created = DateTime.Now;
                note.PersonId = personId;
                note.UserName = User.Identity.Name;
                _logger.LogInformation("Attempting to save a new note.");
                _repository.AddNote(note);
                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int) HttpStatusCode.Created;
                    return Json(Mapper.Map<Note>(note));
                }
            }
            catch (Exception ex)
            {
                var message = "Failed to save new note.";
                _logger.LogError(message, ex);
                return Json(message);
            }
            return Json(new {Message = "Failed", ModelState = ModelState});
        }

        [HttpPost("{noteId}")]
        public JsonResult Post(int personId, int noteId, [FromBody] NoteViewModel vm)
        {
            var errorMessage = "Failed to update a note.";
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            if (!ModelState.IsValid || noteId != vm.Id)
            {
                _logger.LogError(errorMessage);
                return Json(errorMessage);
            }
            try
            {
                var note = Mapper.Map<Note>(vm);
                note.UserName = User.Identity.Name;
                _logger.LogInformation("Attempting to update an existing note.");
                _repository.UpdateNote(note);
                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int) HttpStatusCode.OK;
                    return Json(Mapper.Map<Note>(note));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(errorMessage, ex);
                return Json(errorMessage);
            }
            _logger.LogError(errorMessage);
            return Json(errorMessage);
        }
    }
}
