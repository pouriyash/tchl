using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Tochal.Core.DomainModels.Entity.Identity;

using Tochal.Core.DomainModels.ViewModel.Identity.Settings;
using Tochal.Core.DomainModels.Entity.AuditableEntity;
using Tochal.Core.Common.PersianToolkit;
using Tochal.Core.Common.GuardToolkit;
using Tochal.Infrastructure.DataLayer.Mappings;
using Tochal.Core.DomainModels.Entity.Routine2;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.Entity.Blog;
using Tochal.Core.DomainModels.Entity.Menu;
using Tochal.Core.DomainModels.Entity.Content;

namespace Tochal.Infrastructure.DataLayer.Context
{

    public class ApplicationDbContext :
        IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IUnitOfWork
    {
        // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        #region BaseClass

        public virtual DbSet<AppLogItem> AppLogItems { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<AppSqlCache> AppSqlCache { get; set; }
        public virtual DbSet<AppDataProtectionKey> AppDataProtectionKeys { get; set; }
        public virtual DbSet<ContentEntity> ContentEntity { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<MenuEntity> MenuEntity { get; set; }
        public virtual DbSet<ActionViewType> ActionViewType { get; set; }

        public virtual DbSet<FooterInfo> FooterInfo { get; set; }
        public virtual DbSet<FooterColleague> FooterColleague { get; set; }
        public virtual DbSet<Season> Season { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }


        #region Routin2

        public virtual DbSet<Routine2> Routine2 { get; set; }
        public virtual DbSet<Routine2Log> Routine2Log { get; set; }
        public virtual DbSet<Routine2Role> Routine2Role { get; set; }
        public virtual DbSet<Routine2Step> Routine2Step { get; set; }
        public virtual DbSet<Routine2Action> Routine2Action { get; set; }
        public virtual DbSet<Routine2Notice> Routine2Notice { get; set; }
        public virtual DbSet<Routine2Autodash> Routine2Autodash { get; set; }
        public virtual DbSet<Routine2Reminder> Routine2Reminder { get; set; }

        public virtual DbSet<Notice2> Notice2 { get; set; }

        #endregion

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public void ExecuteSqlCommand(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        public void ExecuteSqlCommand(string query, params object[] parameters)
        {
            Database.ExecuteSqlCommand(query, parameters);
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = this.Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return this.Entry(entity).Property(propertyName).CurrentValue;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges(); //NOTE: changeTracker.Entries<T>() will call it automatically.

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void beforeSaveTriggers()
        {
            validateEntities();
            setShadowProperties();
            this.ApplyCorrectYeKe();
        }

        private void setShadowProperties()
        {
            // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();
            httpContextAccessor.CheckArgumentIsNull(nameof(httpContextAccessor));
            ChangeTracker.SetAuditableEntityPropertyValues(httpContextAccessor);
        }

        private void validateEntities()
        {
            var errors = this.GetValidationErrors();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
                var loggerFactory = this.GetService<ILoggerFactory>();
                loggerFactory.CheckArgumentIsNull(nameof(loggerFactory));
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogError(errors);
                throw new InvalidOperationException(errors);
            }
        }

        #endregion

        public virtual DbSet<Language> Language { set; get; }

        //public virtual DbSet<Payment> Payment { get; set; }

        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationUser> NotificationUser { get; set; }
        public virtual DbSet<GalleryEntity> GalleryEntity { get; set; }
        public virtual DbSet<GroupGallery> GroupGallery { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //string lang = CultureInfo.CurrentCulture.Name;
            //builder.Entity<ContentEntity>().HasQueryFilter(p => p.Language.LanguageTitle == lang);

            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            #region FluentApi

            #region Routine2

            builder.Entity<Routine2Role>()
                .HasKey(c => new { c.RoutineId, c.DashboardEnum });

            builder.Entity<Routine2Step>()
                .HasKey(c => new { c.RoutineId, c.Step });

            builder.Entity<Routine2Action>()
                .HasKey(c => new { c.RoutineId, c.Step, c.Action });

            builder.Entity<Routine2Notice>()
                .HasKey(c => new { c.RoutineId, c.FromStep, c.ToStep, c.Key });

            builder.Entity<Routine2Autodash>()
                .HasKey(c => new { c.RoutineId, c.Step, c.ToStep });

            builder.Entity<Routine2Reminder>()
                .HasKey(c => new { c.RoutineId, c.Step, c.Key, c.TimeoutDays });

            builder.Entity<Routine2Role>().HasOne(p => p.Routine).WithMany(b => b.Roles).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Routine2Step>().HasOne(p => p.Routine).WithMany(b => b.Steps).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Routine2Action>().HasOne(p => p.Routine).WithMany(b => b.Actions).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Routine2Notice>().HasOne(p => p.Routine).WithMany(b => b.Notices).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Routine2Reminder>().HasOne(p => p.Routine).WithMany(b => b.Reminders).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Routine2Autodash>().HasOne(p => p.Routine).WithMany(b => b.Autodashes).OnDelete(DeleteBehavior.Cascade);

            #endregion


            #endregion

            // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
            var siteSettings = this.GetService<IOptionsSnapshot<SiteSettings>>();
            siteSettings.CheckArgumentIsNull(nameof(siteSettings));
            siteSettings.Value.CheckArgumentIsNull(nameof(siteSettings.Value));
            // Adds all of the ASP.NET Core Identity related mappings at once.
            builder.AddCustomIdentityMappings(siteSettings.Value);

            // This should be placed here, at the end.
            builder.AddAuditableShadowProperties();
        }
    }
}