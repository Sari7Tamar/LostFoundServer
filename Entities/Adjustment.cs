using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities
{
    public partial class Adjustment
    {
        public int Id { get; set; }
        public int LostId { get; set; }
        public int FoundId { get; set; }
        public int AdjustmentPercentage { get; set; }
        public int StatusEmail { get; set; }
        public int StatusPhone { get; set; }
       
        [JsonIgnore]
        public virtual LostFound Found { get; set; }
        [JsonIgnore]
        public virtual LostFound Lost { get; set; }
    }
}
