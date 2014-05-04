﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PX.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PXHotelEntities : DbContext
    {
        public PXHotelEntities()
            : base("name=PXHotelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ClientMenu> ClientMenus { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<FileTemplate> FileTemplates { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<HotelBookingRoom> HotelBookingRooms { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<HotelCustomer> HotelCustomers { get; set; }
        public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomService> HotelRoomServices { get; set; }
        public DbSet<HotelRoomType> HotelRoomTypes { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LocalizedResource> LocalizedResources { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<NewsNewsCategory> NewsNewsCategories { get; set; }
        public DbSet<PageLog> PageLogs { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageTag> PageTags { get; set; }
        public DbSet<PageTemplateLog> PageTemplateLogs { get; set; }
        public DbSet<PageTemplate> PageTemplates { get; set; }
        public DbSet<RotatingImageGroup> RotatingImageGroups { get; set; }
        public DbSet<RotatingImage> RotatingImages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SettingType> SettingTypes { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<SQLCommandHistory> SQLCommandHistories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TemplateLog> TemplateLogs { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserInGroup> UserInGroups { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
