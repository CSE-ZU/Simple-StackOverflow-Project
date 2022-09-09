using Microsoft.EntityFrameworkCore;
using OnBoarding.Entities;

namespace OnBoarding.Helper;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        :base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<AnswerVotes> AnswerVotes { get; set; }
    public DbSet<QuestionVotes> QuestionVotes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //User Question one to many relation
        modelBuilder.Entity<Question>()
            .HasOne(u => u.User)
            .WithMany(q => q.Questions);
        
        //User Answer one to many relation
        modelBuilder.Entity<Answer>()
            .HasOne(u => u.User)
            .WithMany(a => a.Answers);
        
        //Question & answer one to many relation 
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(b => b.Answers);
        
        // QuestionVotes relation 
        modelBuilder.Entity<QuestionVotes>().HasKey(v => new {v.UserId, v.QuestionId});
        
        // AnswersVotes relation 
        modelBuilder.Entity<AnswerVotes>().HasKey(v => new {v.UserId, v.AnswerId});

    }
}