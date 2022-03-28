using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NewLF
    {
        public LostFound LF { get; set; }
        public Address address { get; set; }
        public PublicTransport publicTransport { get; set; }

    }
    
}
