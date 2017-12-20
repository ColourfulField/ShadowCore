using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetCoreAngularStarter.API.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using DotnetCoreAngularStarter.BusinessLogic.Services.Abstract;
using DotnetCoreAngularStarter.Models.VM;
using DotnetCoreAngularStarter.Models.DTO;
using Microsoft.Extensions.Logging;
using ShadowBox.Mapper.Abstract;
using ShadowBox.Mapper.Extensions;

namespace DotnetCoreAngularStarter.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    {
        private readonly INoteService _noteService;
        private readonly IManuallyRegisteredService _manuallyRegisteredService;
        private readonly IStringLocalizer<ValuesController> _localizer;

        public ValuesController(
            IMapper mapper,
            ILogger<ValuesController> logger,
            INoteService noteService,
            IManuallyRegisteredService manuallyRegisteredService,
            IStringLocalizer<ValuesController> localizer
        ) :base(logger, mapper)
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
            return await _mapper.Map<NoteDTO, NoteVM>(note);
        }

        [HttpGet]
        [Route("text/{text}")]
        public async Task<List<NoteVM>> GetByText(string text)
        {
            var notes = await _noteService.GetNotesByText("q");
            return await _mapper.Map<NoteDTO, NoteVM>(notes).ToListAsync();
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

        // POST api/values
        [HttpPost]
        public async Task<Guid> Post(string noteText)
        {
            return await _noteService.AddNote(noteText ?? "qwe");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
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