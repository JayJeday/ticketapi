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
    
    public partial class sp_select_all_tickets_Result
    {
        public Nullable<int> TotalCount { get; set; }
        public int TicketId { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDesc { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> TechId { get; set; }
    }
}
