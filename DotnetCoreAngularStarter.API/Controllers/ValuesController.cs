using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using DotnetCoreAngularStarter.BusinessLogic.Services;
using DotnetCoreAngularStarter.BusinessLogic.Services.Abstract;
using DotnetCoreAngularStarter.Models.VM;
using DotnetCoreAngularStarter.Models.DTO;
using ShadowBox.Mapper.Abstract;

namespace DotnetCoreAngularStarter.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        private readonly INoteService _noteService;
        private readonly IManuallyRegisteredService _manuallyRegisteredService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<NoteService> _localizer;

        public ValuesController(INoteService noteService, IManuallyRegisteredService manuallyRegisteredService, IMapper mapper, IStringLocalizer<NoteService> localizer)
        {
            _noteService = noteService;
            _manuallyRegisteredService = manuallyRegisteredService;
            _mapper = mapper;
            _localizer = localizer;
        }
        // GET api/values
        [HttpGet]
        public async Task<NoteVM> GetNote()
        {
            var note = await _noteService.GetNote();
            var a = System.Globalization.CultureInfo.CurrentCulture;
            return await _mapper.Map<NoteDTO, NoteVM>(note);
        }

        [HttpGet]
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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
