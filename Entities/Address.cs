using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities
{
    public partial class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }

        public int LFId { get; set; }

        [JsonIgnore]
        public virtual LostFound LF { get; set; }
        

    }
}
