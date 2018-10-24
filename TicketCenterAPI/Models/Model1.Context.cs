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
    
    public partial class ticketcenterdbEntities1 : DbContext
    {
        public ticketcenterdbEntities1()
            : base("name=ticketcenterdbEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TechCategory> TechCategories { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTicket> UserTickets { get; set; }
    
        public virtual int ins_category(string category)
        {
            var categoryParameter = category != null ?
                new ObjectParameter("category", category) :
                new ObjectParameter("category", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_category", categoryParameter);
        }
    
        public virtual int ins_status(string status)
        {
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_status", statusParameter);
        }
    
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
    
        public virtual int ins_user(string firstName, string lastName, string email, Nullable<int> rolesId, Nullable<int> categoryId)
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
    
            var rolesIdParameter = rolesId.HasValue ?
                new ObjectParameter("RolesId", rolesId) :
                new ObjectParameter("RolesId", typeof(int));
    
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_user", firstNameParameter, lastNameParameter, emailParameter, rolesIdParameter, categoryIdParameter);
        }
    
        public virtual ObjectResult<sp_get_all_technician_Result> sp_get_all_technician()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_all_technician_Result>("sp_get_all_technician");
        }
    
        public virtual ObjectResult<sp_get_category_by_id_Result> sp_get_category_by_id(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_category_by_id_Result>("sp_get_category_by_id", idParameter);
        }
    
        public virtual ObjectResult<sp_get_login_Result> sp_get_login(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_login_Result>("sp_get_login", emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<sp_get_status_by_id_Result> sp_get_status_by_id(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_status_by_id_Result>("sp_get_status_by_id", idParameter);
        }
    
        public virtual ObjectResult<sp_get_ticket_by_id_Result> sp_get_ticket_by_id(Nullable<int> ticketId)
        {
            var ticketIdParameter = ticketId.HasValue ?
                new ObjectParameter("TicketId", ticketId) :
                new ObjectParameter("TicketId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_ticket_by_id_Result>("sp_get_ticket_by_id", ticketIdParameter);
        }
    
        public virtual ObjectResult<sp_get_user_by_id_Result> sp_get_user_by_id(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_user_by_id_Result>("sp_get_user_by_id", userIdParameter);
        }
    
        public virtual ObjectResult<sp_get_user_ticket_by_id_Result> sp_get_user_ticket_by_id(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_user_ticket_by_id_Result>("sp_get_user_ticket_by_id", userIdParameter);
        }
    
        public virtual ObjectResult<sp_get_user_ticket_by_id_pagi_Result> sp_get_user_ticket_by_id_pagi(Nullable<int> pageIndex, Nullable<int> pageSize, Nullable<int> userId)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("pageIndex", pageIndex) :
                new ObjectParameter("pageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("pageSize", pageSize) :
                new ObjectParameter("pageSize", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_user_ticket_by_id_pagi_Result>("sp_get_user_ticket_by_id_pagi", pageIndexParameter, pageSizeParameter, userIdParameter);
        }
    
        public virtual ObjectResult<sp_select_all_categories_Result> sp_select_all_categories()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_select_all_categories_Result>("sp_select_all_categories");
        }
    
        public virtual ObjectResult<sp_select_all_roles_Result> sp_select_all_roles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_select_all_roles_Result>("sp_select_all_roles");
        }
    
        public virtual ObjectResult<sp_select_all_status_Result> sp_select_all_status()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_select_all_status_Result>("sp_select_all_status");
        }
    
        public virtual ObjectResult<sp_select_all_tickets_Result> sp_select_all_tickets()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_select_all_tickets_Result>("sp_select_all_tickets");
        }
    
        public virtual ObjectResult<sp_select_all_users_Result> sp_select_all_users()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_select_all_users_Result>("sp_select_all_users");
        }
    
        public virtual ObjectResult<sp_selectAllStatus_Result> sp_selectAllStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_selectAllStatus_Result>("sp_selectAllStatus");
        }
    
        public virtual ObjectResult<sp_summary_category_Result> sp_summary_category()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_summary_category_Result>("sp_summary_category");
        }
    
        public virtual ObjectResult<sp_summary_status_Result> sp_summary_status()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_summary_status_Result>("sp_summary_status");
        }
    
        public virtual int usp_category(Nullable<int> categoryId, string category)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("categoryId", categoryId) :
                new ObjectParameter("categoryId", typeof(int));
    
            var categoryParameter = category != null ?
                new ObjectParameter("category", category) :
                new ObjectParameter("category", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_category", categoryIdParameter, categoryParameter);
        }
    
        public virtual int usp_status(Nullable<int> statusId, string status)
        {
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_status", statusIdParameter, statusParameter);
        }
    
        public virtual int usp_tech_cat(Nullable<int> categoryId, Nullable<int> userId)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("categoryId", categoryId) :
                new ObjectParameter("categoryId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_tech_cat", categoryIdParameter, userIdParameter);
        }
    
        public virtual int usp_ticket(Nullable<int> ticketId, Nullable<int> userId, Nullable<int> statusId, string comments)
        {
            var ticketIdParameter = ticketId.HasValue ?
                new ObjectParameter("ticketId", ticketId) :
                new ObjectParameter("ticketId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(int));
    
            var commentsParameter = comments != null ?
                new ObjectParameter("comments", comments) :
                new ObjectParameter("comments", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_ticket", ticketIdParameter, userIdParameter, statusIdParameter, commentsParameter);
        }
    
        public virtual int usp_user(Nullable<int> userId, Nullable<int> categoryId, Nullable<int> roleId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("categoryId", categoryId) :
                new ObjectParameter("categoryId", typeof(int));
    
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("roleId", roleId) :
                new ObjectParameter("roleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_user", userIdParameter, categoryIdParameter, roleIdParameter);
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
