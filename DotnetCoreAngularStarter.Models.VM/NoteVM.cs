using System;

namespace DotnetCoreAngularStarter.Models.VM
{
    public class NoteVM
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string StringifiedNumber { get; set; }
        public NoteVM ParentNote { get; set; }
    }
}
