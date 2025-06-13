using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApp.AppSettings;
using QuizApp.Entities;

namespace QuizApp.DataContext
{
    public class QuizDbContext : DbContext
    {
        private readonly PostgreSetting _postgreSetting;
        public QuizDbContext(PostgreSetting postgreSetting)
        {
            _postgreSetting = postgreSetting;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_postgreSetting.ConnectionString ?? "");
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<QuizInfo> QuizInfos { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("UserInfos").HasKey(x => x.Id);

            modelBuilder.Entity<QuizInfo>().ToTable("QuizInfos").HasKey(x => x.Id);
            modelBuilder.Entity<QuizInfo>()
            .HasOne<UserInfo>(s => s.UserInfos)
            .WithMany(g => g.QuizInfos)
            .HasForeignKey(s => s.UserInfoId);

            modelBuilder.Entity<Question>().ToTable("Questions").HasKey(x => x.Id);

            modelBuilder.Entity<Option>().ToTable("Options").HasKey(x => x.Id);
            modelBuilder.Entity<Option>()
            .HasOne<Question>(s => s.Questions)
            .WithMany(g => g.Options)
            .HasForeignKey(s => s.QuestionId);

            modelBuilder.Entity<Answer>().ToTable("Answers").HasKey(x => x.Id);
            modelBuilder.Entity<Answer>()
            .HasOne<QuizInfo>(s => s.QuizInfos)
            .WithMany(g => g.Answers)
            .HasForeignKey(s => s.QuizInfoId);
            modelBuilder.Entity<Answer>()
            .HasOne(s => s.Questions)
            .WithMany()
            .HasForeignKey(s => s.QuestionId);
            modelBuilder.Entity<Answer>()
            .HasOne(s => s.SelectedOptions)
            .WithMany()
            .HasForeignKey(s => s.SelectedOptionId);
        }


        public override int SaveChanges()
        {
            var dateNow = DateTime.UtcNow;
            var errorList = new List<ValidationResult>();

            var entries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added ||
                            p.State == EntityState.Modified)
                .ToList();

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.CreateDate = itemBase.UpdateDate = dateNow;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.UpdateDate = dateNow;
                    }
                }

                Validator.TryValidateObject(entity, new ValidationContext(entity), errorList);
            }

            if (errorList.Count != 0)
            {
                throw new Exception(string.Join(", ", errorList.Select(p => p.ErrorMessage)).Trim());
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var dateNow = DateTime.UtcNow;
            var errorList = new List<ValidationResult>();

            var entries = ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified).ToList();

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.CreateDate = itemBase.UpdateDate = dateNow;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.UpdateDate = dateNow;
                    }
                }

                Validator.TryValidateObject(entity, new ValidationContext(entity), errorList);
            }

            if (errorList.Count != 0)
            {
                throw new Exception(string.Join(", ", errorList.Select(p => p.ErrorMessage)).Trim());
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
