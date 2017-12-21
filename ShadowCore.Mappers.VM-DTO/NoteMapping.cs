using System;
using System.Threading.Tasks;
using ShadowCore.Models.VM;
using ShadowCore.Models.DTO;
using ShadowBox.Mapper.Abstract;

namespace MiUtility.Areas.Agent.Sap.Mappings
{
    public class NoteMappingDtoToVm : AsyncMapping<NoteDTO, NoteVM>
    {
        private readonly Func<IMapper> _mapper;
        public NoteMappingDtoToVm(Func<IMapper> mapper)
        {
            _mapper = mapper;
        }
        protected override async Task MapFieldsAsync(NoteDTO source, NoteVM destination)
        {
            var mapper = _mapper();
            destination.CreationDate = source.CreationDate;
            destination.ModificationDate = source.ModificationDate;
            destination.Id = source.Id;
            destination.Text = source.Text;
            destination.StringifiedNumber = source.Number.ToString();
            if (source.ParentNote != null)
            {
                destination.ParentNote = await mapper.Map<NoteDTO, NoteVM>(source.ParentNote);
            }
        }
    }

    public class NoteMappingVmToDto : AsyncMapping<NoteVM, NoteDTO>
    {
        protected override Task MapFieldsAsync(NoteVM source, NoteDTO destination)
        {
            destination.CreationDate = source.CreationDate;
            destination.ModificationDate = source.ModificationDate;
            destination.Id = source.Id;
            destination.Text = source.Text;
            destination.Number = Convert.ToInt32(source.StringifiedNumber);
            return Task.FromResult(0);
        }
    }
}
