using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ModelsDB
{
    public partial class BandItContext : DbContext
    {
        public BandItContext()
        {
        }

        public BandItContext(DbContextOptions<BandItContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<BandGenres> BandGenres { get; set; }
        public virtual DbSet<BandMessages> BandMessages { get; set; }
        public virtual DbSet<BandUserSkills> BandUserSkills { get; set; }
        public virtual DbSet<BandUsers> BandUsers { get; set; }
        public virtual DbSet<Bands> Bands { get; set; }
        public virtual DbSet<Blocks> Blocks { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Invites> Invites { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<NotificationTypes> NotificationTypes { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<UserFiles> UserFiles { get; set; }
        public virtual DbSet<UserMessages> UserMessages { get; set; }
        public virtual DbSet<UserSkills> UserSkills { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=BandIt;User Id=sa;Password=pooky1509;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applications>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BandId).HasColumnName("BandID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Sent).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Band)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.BandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Applicati__BandI__5CD6CB2B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Applicati__UserI__5DCAEF64");
            });

            modelBuilder.Entity<BandGenres>(entity =>
            {
                entity.HasKey(e => new { e.BandId, e.GenreId })
                    .HasName("PK__BandGenr__500EC3DD8E2917EF");

                entity.Property(e => e.BandId).HasColumnName("BandID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.HasOne(d => d.Band)
                    .WithMany(p => p.BandGenres)
                    .HasForeignKey(d => d.BandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BandGenre__BandI__59063A47");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.BandGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BandGenre__Genre__59FA5E80");
            });

            modelBuilder.Entity<BandMessages>(entity =>
            {
                entity.HasKey(e => new { e.BandId, e.MessageId })
                    .HasName("PK__BandMess__7CB153BF95706A64");

                entity.Property(e => e.BandId).HasColumnName("BandID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.HasOne(d => d.Band)
                    .WithMany(p => p.BandMessages)
                    .HasForeignKey(d => d.BandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BandMessa__BandI__4F7CD00D");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.BandMessages)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BandMessa__Messa__5070F446");
            });

            modelBuilder.Entity<BandUserSkills>(entity =>
            {
                entity.HasKey(e => new { e.BandUserId, e.SkillId })
                    .HasName("PK__BandUser__530AD2645E689984");

                entity.Property(e => e.BandUserId).HasColumnName("BandUserID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.HasOne(d => d.BandUser)
                    .WithMany(p => p.BandUserSkills)
                    .HasForeignKey(d => d.BandUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BandUserS__BandU__70DDC3D8");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.BandUserSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BandUserS__Skill__71D1E811");
            });

            modelBuilder.Entity<BandUsers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BandId).HasColumnName("BandID");

                entity.Property(e => e.PermisionId).HasColumnName("PermisionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Band)
                    .WithMany(p => p.BandUsers)
                    .HasForeignKey(d => d.BandId)
                    .HasConstraintName("FK__BandUsers__BandI__403A8C7D");

                entity.HasOne(d => d.Permision)
                    .WithMany(p => p.BandUsers)
                    .HasForeignKey(d => d.PermisionId)
                    .HasConstraintName("FK__BandUsers__Permi__3E52440B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BandUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__BandUsers__UserI__3F466844");
            });

            modelBuilder.Entity<Bands>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Bands__737584F66315DB39")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.InviteMessage).HasColumnType("text");

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Blocks>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Blocked).HasColumnType("datetime");

                entity.Property(e => e.BlockerId).HasColumnName("BlockerID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");

                entity.HasOne(d => d.Blocker)
                    .WithMany(p => p.BlocksBlocker)
                    .HasForeignKey(d => d.BlockerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Blocks__BlockerI__44FF419A");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.BlocksReceiver)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Blocks__Receiver__45F365D3");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invites>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BandId).HasColumnName("BandID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Sent).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Band)
                    .WithMany(p => p.Invites)
                    .HasForeignKey(d => d.BandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invites__BandID__60A75C0F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invites__UserID__619B8048");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.Sent).HasColumnType("datetime");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Sender__48CFD27E");
            });

            modelBuilder.Entity<NotificationTypes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BandId).HasColumnName("BandID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Sent).HasColumnType("datetime");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Band)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.BandId)
                    .HasConstraintName("FK__Notificat__BandI__66603565");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__TypeI__68487DD7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Notificat__UserI__6754599E");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Permissio__UserI__3B75D760");
            });

            modelBuilder.Entity<Skills>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserFiles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FileId })
                    .HasName("PK__UserFile__F1783525618E056E");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.UserFiles)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK__UserFiles__FileI__5441852A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFiles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserFiles__UserI__534D60F1");
            });

            modelBuilder.Entity<UserMessages>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.MessageId })
                    .HasName("PK__UserMess__CB0F0C9BE9B0C261");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.UserMessages)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserMessa__Messa__4CA06362");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMessages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserMessa__UserI__4BAC3F29");
            });

            modelBuilder.Entity<UserSkills>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SkillId })
                    .HasName("PK__UserSkil__7A72C5B211C73871");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.UserSkills)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK__UserSkill__Skill__6E01572D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSkills)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserSkill__UserI__6D0D32F4");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Deleted).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
