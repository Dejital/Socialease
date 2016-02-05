using System;
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
    [Route("api/notes")]
    public class NotesController : Controller
    {
        private readonly ISocialRepository _repository;
        private readonly ILogger<NotesController> _logger;

        public NotesController(ISocialRepository repository, ILogger<NotesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                var results = _repository.GetNoteById(id, User.Identity.Name);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<NoteViewModel>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get note with id {id}.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { ex.Message });
            }
        }

        [HttpPost("")]
        public JsonResult Post([FromBody] NoteViewModel vm)
        {
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            if (!ModelState.IsValid || (vm.PersonId == 0 && vm.PingId == 0))
            {
                return Json(new {Message = "Failed", ModelState = ModelState});
            }
            try
            {
                var note = Mapper.Map<Note>(vm);
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

        [HttpPost("{id}")]
        public JsonResult Post(int id, [FromBody] NoteViewModel vm)
        {
            var errorMessage = "Failed to update a note.";
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            if (!ModelState.IsValid || id != vm.Id)
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
