using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities
{
    public partial class LostFound
    {
        public LostFound()
        {
            Addresses = new HashSet<Address>();
            AdjustmentFounds = new HashSet<Adjustment>();
            AdjustmentLosts = new HashSet<Adjustment>();
            PublicTransports = new HashSet<PublicTransport>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; }
        public int Type { get; set; }
        public string LocationType { get; set; }
        public int UserId { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        
        public virtual ICollection<Adjustment> AdjustmentFounds { get; set; }
       
        public virtual ICollection<Adjustment> AdjustmentLosts { get; set; }
     
        public virtual ICollection<PublicTransport> PublicTransports { get; set; }
    }
}
