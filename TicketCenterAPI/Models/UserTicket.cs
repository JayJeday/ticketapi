//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketCenterAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTicket
    {
        public int id { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> ticketId { get; set; }
    
        public virtual Ticket Ticket { get; set; }
        public virtual User User { get; set; }
    }
}
