using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Models
{
    public class CodenationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Acceleration> Accelerations { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=Codenation;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Acceleration>(eb => {
                eb.ToTable("acceleration");

                eb.HasKey(a => a.Id);

                eb.Property(p => p.Id).HasColumnName("id").HasColumnType("int").IsRequired();
                eb.Property(p => p.Name).HasColumnName("name").HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
                eb.Property(p => p.Slug).HasColumnName("slug").HasColumnType("varchar(50)").HasMaxLength(50).IsRequired();
                eb.Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").IsRequired();
                //Estava alterando o nome da propriedade Challenge, não challengeId
                eb.Property(p => p.ChallengeId).HasColumnName("challenge_id").HasColumnType("int").IsRequired();
                

                //Relação um-para-muitos entre acceleration e candidate
                eb.HasMany(a => a.Candidates)
                    .WithOne(c => c.Acceleration)
                        .HasForeignKey(c => c.AccelerationId);
            });

            mb.Entity<Company>(eb => {
                eb.ToTable("company");
                eb.HasKey(c => c.Id);

                eb.Property(p => p.Id).HasColumnName("id").HasColumnType("int").IsRequired();
                eb.Property(p => p.Name).HasColumnName("name").HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
                eb.Property(p => p.Slug).HasColumnName("slug").HasColumnType("varchar(50)").HasMaxLength(50).IsRequired();
                eb.Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").IsRequired();
                //Relação um-para-muitos entre company e candidate
                eb.HasMany<Candidate>(c => c.Candidates)
                    .WithOne(c => c.Company)
                        .HasForeignKey(c => c.CompanyId);
            });
            mb.Entity<User>(eb =>
            {
                eb.ToTable("user");
                eb.HasKey(u => u.Id);
                eb.Property(p => p.Id).HasColumnName("id").HasColumnType("int").IsRequired();
                eb.Property(p => p.FullName).HasColumnName("full_name").HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
                eb.Property(p => p.Email).HasColumnName("email").HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
                eb.Property(p => p.Nickname).HasColumnName("nickname").HasColumnType("varchar(50)").HasMaxLength(50).IsRequired();
                eb.Property(p => p.Password).HasColumnName("password").HasColumnType("varchar(255)").HasMaxLength(255).IsRequired();
                eb.Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").IsRequired();
                //Relação um-para-muitos entre user e candidate
                eb.HasMany<Candidate>(u => u.Candidates)
                    .WithOne(c => c.User)
                        .HasForeignKey(c => c.UserId);
                //Relação um-para-muitos entre user e submission
                eb.HasMany<Submission>(c => c.Submissions)
                    .WithOne(s => s.User)
                        .HasForeignKey(s => s.UserId);
            });
            mb.Entity<Candidate>(eb => {
                eb.ToTable("candidate");
                //Definindo chave composta
                eb.HasKey(k => new { k.UserId, k.AccelerationId, k.CompanyId });
                eb.Property(p => p.Status).HasColumnName("status").HasColumnType("int").IsRequired();
                eb.Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").IsRequired();

                //Estava faltando trocar o nome das outras propriedades
                eb.Property(p => p.AccelerationId).HasColumnName("acceleration_id").HasColumnType("int").IsRequired();
                eb.Property(p => p.CompanyId).HasColumnName("company_id").HasColumnType("int").IsRequired();
                eb.Property(p => p.UserId).HasColumnName("user_id").HasColumnType("int").IsRequired();


            });

            mb.Entity<Challenge>(eb =>
            {
                eb.ToTable("challenge");

                eb.HasKey(c => c.Id);
                eb.Property(p => p.Id).HasColumnName("id").HasColumnType("int").IsRequired();
                eb.Property(p => p.Name).HasColumnName("name").HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
                eb.Property(p => p.Slug).HasColumnName("slug").HasColumnType("varchar(50)").HasMaxLength(50).IsRequired();
                eb.Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").IsRequired();
                
                //Relação um-para-muitos entre challenge e submission
                eb.HasMany<Submission>(c => c.Submissions)
                    .WithOne(s => s.Challenge)
                        .HasForeignKey(s => s.ChallengeId);

                //Relação um-para-muitos entre challenge e acceleration
                eb.HasMany<Acceleration>(c => c.Accelerations)
                    .WithOne(s => s.Challenge)
                        .HasForeignKey(s => s.ChallengeId);

                //estava duplicada
                //eb.HasMany<Acceleration>(c => c.Accelerations)
                //    .WithOne(a => a.Challenge)
                //        .HasForeignKey(a => a.ChallengeId);
            });
            
            mb.Entity<Submission>(eb => {
                eb.ToTable("submission");
                //Definindo chave composta
                eb.HasKey(k => new {k.UserId, k.ChallengeId });
                eb.Property(p => p.Score).HasColumnName("score").HasColumnType("float").IsRequired();
                eb.Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").IsRequired();

                //Estava faltando trocar o nome das outras propriedades
                eb.Property(p => p.UserId).HasColumnName("user_id").HasColumnType("int").IsRequired();
                eb.Property(p => p.ChallengeId).HasColumnName("challenge_id").HasColumnType("int").IsRequired();
            });

        }
        
    }
}