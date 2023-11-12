using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DomainModels
{
    public partial class QuizAppContext : DbContext
    {
        public QuizAppContext()
        {
        }

        public QuizAppContext(DbContextOptions<QuizAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Mark> Marks { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionLevel> QuestionLevels { get; set; } = null!;
        public virtual DbSet<SetExam> SetExams { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5F7UE03\\SQLEXPRESS;Database=QuizApp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.HasIndex(e => e.Username, "UQ__tbl_admi__84D9FB7ABB5ABB2C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(25)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExamCode).HasColumnName("exam_code");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.Marks).HasColumnName("marks");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Marks_SetExam");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Marks_Student");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.QId)
                    .HasName("PK_tbl_questions");

                entity.Property(e => e.QId).HasColumnName("q_id");

                entity.Property(e => e.Ans1)
                    .HasMaxLength(25)
                    .HasColumnName("ans1");

                entity.Property(e => e.Ans2)
                    .HasMaxLength(25)
                    .HasColumnName("ans2");

                entity.Property(e => e.Ans3)
                    .HasMaxLength(25)
                    .HasColumnName("ans3");

                entity.Property(e => e.Ans4)
                    .HasMaxLength(25)
                    .HasColumnName("ans4");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CorrectAns)
                    .HasMaxLength(25)
                    .HasColumnName("correct_ans");

                entity.Property(e => e.QText).HasColumnName("q_text");

                entity.Property(e => e.QuestionLevelId).HasColumnName("question-level-id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Questions_Category");

                entity.HasOne(d => d.QuestionLevel)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionLevelId)
                    .HasConstraintName("FK_Questions_Question-Level");
            });

            modelBuilder.Entity<QuestionLevel>(entity =>
            {
                entity.ToTable("Question-Level");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.QuestionLevel1)
                    .HasMaxLength(25)
                    .HasColumnName("questionLevel");
            });

            modelBuilder.Entity<SetExam>(entity =>
            {
                entity.HasKey(e => e.Examid)
                    .HasName("PK__tbl_setE__A56C2E67A255860F");

                entity.ToTable("SetExam");

                entity.Property(e => e.Examid).HasColumnName("examid");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.ExamDate)
                    .HasColumnType("datetime")
                    .HasColumnName("exam_date");

                entity.Property(e => e.ExamName).HasColumnName("exam_name");

                entity.Property(e => e.NoOfQuestions).HasColumnName("no. of questions");

                entity.Property(e => e.RoomCode).HasColumnName("room_code");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SetExams)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_SetExam_Category");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.Username, "UC_Username")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__tbl_stud__BA9CAC34DD60573B")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(25)
                    .HasColumnName("password");

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(25)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
