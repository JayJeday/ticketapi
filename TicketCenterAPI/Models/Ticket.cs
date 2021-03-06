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
    
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> TechId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
        public virtual Ticket Tickets1 { get; set; }
        public virtual Ticket Ticket1 { get; set; }
    }
}
