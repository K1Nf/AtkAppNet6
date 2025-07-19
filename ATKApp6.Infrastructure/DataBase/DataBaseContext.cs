using ATKApplication.Configurations;
using ATKApp6.Domain.Models;
using ATKApp6.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Hosting;


namespace ATKApp6.Infrastructure.DataBase
{
    public class DataBaseContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;


        public DataBaseContext(
            DbContextOptions<DataBaseContext> options,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IWebHostEnvironment env)
            : base(options)
        {
            _loggerFactory = loggerFactory;
            _configuration = configuration;
            _env = env;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetValue<string>("DefaultConnectionString");

                optionsBuilder
                    .UseNpgsql(connectionString);
                    

                if (_env.IsDevelopment())
                {
                    optionsBuilder
                        .EnableSensitiveDataLogging()
                        .UseLoggerFactory(_loggerFactory)
                        .EnableDetailedErrors();
                }
            }
        }


        public DbSet<EventBase> EventsBase { get; set; }
        public DbSet<EventForm1> EventForm1s { get; set; }
        public DbSet<EventForm2> EventForm2s { get; set; }
        public DbSet<EventForm3> EventForm3s { get; set; }
        public DbSet<EventForm4> EventForm4s { get; set; }


        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MediaLink> MediaLinks { get; set; }


        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<Concourse> Concourses { get; set; }


        public DbSet<InterAgencyCooperation> InterAgencyCooperations { get; set; }
        public DbSet<Agreement> Agreements { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventBaseConfiguration());
            modelBuilder.ApplyConfiguration(new EventForm1Configuration());
            modelBuilder.ApplyConfiguration(new EventForm4Configuration());
            modelBuilder.ApplyConfiguration(new InterAgencyCooperationConfiguration());
            modelBuilder.ApplyConfiguration(new ThemeConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());

            modelBuilder.Entity<EventBase>().ToTable("EventsBase");
            modelBuilder.Entity<EventForm1>().ToTable("EventsForm1");
            modelBuilder.Entity<EventForm2>().ToTable("EventsForm2");
            modelBuilder.Entity<EventForm3>().ToTable("EventsForm3");
            modelBuilder.Entity<EventForm4>().ToTable("EventsForm4");
            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}