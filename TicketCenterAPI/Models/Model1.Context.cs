﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ticketcenterdbEntities : DbContext
    {
        public ticketcenterdbEntities()
            : base("name=ticketcenterdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TechCategoryRel> TechCategoryRels { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTicket> UserTickets { get; set; }
    
        public virtual int ins_tickets(string description, string comment, Nullable<int> categoryId, Nullable<int> statusId)
        {
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var commentParameter = comment != null ?
                new ObjectParameter("Comment", comment) :
                new ObjectParameter("Comment", typeof(string));
    
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("StatusId", statusId) :
                new ObjectParameter("StatusId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_tickets", descriptionParameter, commentParameter, categoryIdParameter, statusIdParameter);
        }
    
        public virtual int ins_user(string firstName, string lastName, string email, string password, Nullable<bool> isActivate, Nullable<bool> isLocked, Nullable<int> rolesId)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var isActivateParameter = isActivate.HasValue ?
                new ObjectParameter("IsActivate", isActivate) :
                new ObjectParameter("IsActivate", typeof(bool));
    
            var isLockedParameter = isLocked.HasValue ?
                new ObjectParameter("IsLocked", isLocked) :
                new ObjectParameter("IsLocked", typeof(bool));
    
            var rolesIdParameter = rolesId.HasValue ?
                new ObjectParameter("RolesId", rolesId) :
                new ObjectParameter("RolesId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_user", firstNameParameter, lastNameParameter, emailParameter, passwordParameter, isActivateParameter, isLockedParameter, rolesIdParameter);
        }
    
        public virtual ObjectResult<sp_select_all_categories_Result> sp_select_all_categories()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_select_all_categories_Result>("sp_select_all_categories");
        }
    
        public virtual ObjectResult<sp_select_all_roles_Result> sp_select_all_roles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_select_all_roles_Result>("sp_select_all_roles");
        }
    
        public virtual ObjectResult<sp_select_all_tickets_Result> sp_select_all_tickets()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_select_all_tickets_Result>("sp_select_all_tickets");
        }
    
        public virtual ObjectResult<sp_selectAllStatus_Result> sp_selectAllStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_selectAllStatus_Result>("sp_selectAllStatus");
        }
    
        public virtual int usp_techician_ticket(Nullable<int> ticketId, Nullable<int> statusId, string comments)
        {
            var ticketIdParameter = ticketId.HasValue ?
                new ObjectParameter("ticketId", ticketId) :
                new ObjectParameter("ticketId", typeof(int));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(int));
    
            var commentsParameter = comments != null ?
                new ObjectParameter("comments", comments) :
                new ObjectParameter("comments", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_techician_ticket", ticketIdParameter, statusIdParameter, commentsParameter);
        }
    
        public virtual int usp_users_role(Nullable<int> roleId, Nullable<int> userId)
        {
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("roleId", roleId) :
                new ObjectParameter("roleId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_users_role", roleIdParameter, userIdParameter);
        }
    }
}
