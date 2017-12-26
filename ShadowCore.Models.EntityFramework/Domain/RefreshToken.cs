using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowCore.Models.EntityFramework.Domain
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public string ClientApp { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public virtual User User { get; set; }
    }
}
