using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MailerEntity
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Mailer { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}
