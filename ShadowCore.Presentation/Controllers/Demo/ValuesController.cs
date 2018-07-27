using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ShadowCore.BusinessLogic.Services.Abstract;
using ShadowCore.Models.DTO;
using ShadowCore.Models.VM;
using ShadowCore.Presentation.Controllers.Abstract;
using ShadowTools.Mapper.Abstract;
using ShadowTools.Mapper.Extensions;

namespace ShadowCore.Presentation.Controllers.Demo
{
    /// <summary>
    ///     Demo controller showcasing different features of the project
    /// </summary>
    [AllowAnonymous]
    public class ValuesController : BaseController
    {
        private readonly IStringLocalizer<ValuesController> _localizer;
        private readonly IManuallyRegisteredService _manuallyRegisteredService;
        private readonly INoteService _noteService;

        public ValuesController(
            IMapper mapper,
            ILogger<ValuesController> logger,
            INoteService noteService,
            IManuallyRegisteredService manuallyRegisteredService,
            IStringLocalizer<ValuesController> localizer
        ) : base(logger, mapper)
        {
            _noteService = noteService;
            _manuallyRegisteredService = manuallyRegisteredService;
            _localizer = localizer;
        }

        // GET api/values
        [HttpGet]
        public async Task<NoteVM> Get()
        {
            var note = await _noteService.GetNote();
            return await Mapper.Map<NoteDTO, NoteVM>(note);
        }

        [HttpGet]
        [Route("text/{text}")]
        public async Task<List<NoteVM>> GetByText(string text)
        {
            var notes = await _noteService.GetNotesByText("q");
            return await Mapper.Map<NoteDTO, NoteVM>(notes).ToListAsync();
        }

        [HttpGet]
        [Route("value")]
        public async Task<string> GetValue()
        {
            return _manuallyRegisteredService.GetValue();
        }

        // GET api/values/5
        [HttpGet("{text}")]
        public async Task<string> GetLocalizerHello(string text)
        {
            return _localizer["Hello"];
        }

        [HttpPost]
        public async Task<string> PostWithGuids([FromQuery] Guid[] guids)
        {
            return _localizer["Hello"];
        }

        // POST api/values
        [HttpPost]
        public async Task<Guid> Post([FromBody] string noteText)
        {
            return await _noteService.AddNote(noteText ?? "qwe");
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromQuery] int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            NoContentResponse();
        }
    }
}