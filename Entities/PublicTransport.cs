using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities
{
    public partial class PublicTransport
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public int? BusNumber { get; set; }
        public string BoardingStation { get; set; }
        public string DropStation { get; set; }
        public int LFId { get; set; }
        [JsonIgnore]
        public virtual LostFound LF { get; set; }
    }
}
