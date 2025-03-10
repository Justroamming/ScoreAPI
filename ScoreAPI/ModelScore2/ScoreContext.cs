using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScoreAPI.ModelScore2;

public partial class ScoreContext : DbContext
{
    public ScoreContext()
    {
    }

    public ScoreContext(DbContextOptions<ScoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAdmin> TblAdmins { get; set; }

    public virtual DbSet<TblClassLesson> TblClassLessons { get; set; }

    public virtual DbSet<TblCohort> TblCohorts { get; set; }

    public virtual DbSet<TblGrade> TblGrades { get; set; }

    public virtual DbSet<TblLessonClass> TblLessonClasses { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblSubject> TblSubjects { get; set; }

    public virtual DbSet<TblTeacher> TblTeachers { get; set; }

    public virtual DbSet<TblTeacherSubject> TblTeacherSubjects { get; set; }

    public virtual DbSet<TblTest> TblTests { get; set; }

    public virtual DbSet<TblTestWeight> TblTestWeights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=cmcsv.ric.vn,10000;Initial Catalog=N9_NHOM3;Persist Security Info=True;User ID=cmcsv;Password=cM!@#2025;TrustServerCertificate=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAdmin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__tblAdmin__719FE4E810A17A01");

            entity.ToTable("tblAdmins");

            entity.HasIndex(e => e.Email, "UQ__tblAdmin__A9D105340926D8D3").IsUnique();

            entity.Property(e => e.AdminId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AdminID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<TblClassLesson>(entity =>
        {
            entity.HasKey(e => e.ClassLessonId).HasName("PK__tblClass__C3ADC325CF0871BA");

            entity.ToTable("tblClassLessons");

            entity.Property(e => e.ClassLessonId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ClassLessonID");
            entity.Property(e => e.CohortId).HasColumnName("CohortID");
            entity.Property(e => e.LessonClassId).HasColumnName("LessonClassID");

            entity.HasOne(d => d.Cohort).WithMany(p => p.TblClassLessons)
                .HasForeignKey(d => d.CohortId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassLessons_Cohorts");

            entity.HasOne(d => d.LessonClass).WithMany(p => p.TblClassLessons)
                .HasForeignKey(d => d.LessonClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassLessons_LessonClasses");
        });

        modelBuilder.Entity<TblCohort>(entity =>
        {
            entity.HasKey(e => e.CohortId).HasName("PK__tblCohor__4A2288FF4F8D0ACF");

            entity.ToTable("tblCohorts");

            entity.HasIndex(e => e.CohortName, "UQ__tblCohor__7FF5ECD1476215D6").IsUnique();

            entity.Property(e => e.CohortId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CohortID");
            entity.Property(e => e.CohortName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<TblGrade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__tblGrade__54F87A376EDC3D4D");

            entity.ToTable("tblGrades");

            entity.HasIndex(e => new { e.StudentId, e.TestId }, "UQ_StudentTest").IsUnique();

            entity.Property(e => e.GradeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GradeID");
            entity.Property(e => e.GradeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
            entity.Property(e => e.TestId).HasColumnName("TestID");

            entity.HasOne(d => d.Student).WithMany(p => p.TblGrades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Students");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TblGrades)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Teachers");

            entity.HasOne(d => d.Test).WithMany(p => p.TblGrades)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Tests");
        });

        modelBuilder.Entity<TblLessonClass>(entity =>
        {
            entity.HasKey(e => e.LessonClassId).HasName("PK__tblLesso__8CD42968AB21C6D9");

            entity.ToTable("tblLessonClasses");

            entity.Property(e => e.LessonClassId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("LessonClassID");
            entity.Property(e => e.LessonDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Subject).WithMany(p => p.TblLessonClasses)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonClasses_Subjects");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TblLessonClasses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonClasses_Teachers");
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__tblStude__32C52A7964D8467B");

            entity.ToTable("tblStudents");

            entity.HasIndex(e => e.Email, "UQ__tblStude__A9D10534E3476F42").IsUnique();

            entity.Property(e => e.StudentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("StudentID");
            entity.Property(e => e.CohortId).HasColumnName("CohortID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.Cohort).WithMany(p => p.TblStudents)
                .HasForeignKey(d => d.CohortId)
                .HasConstraintName("FK_Students_Cohorts");
        });

        modelBuilder.Entity<TblSubject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__tblSubje__AC1BA3888CC20934");

            entity.ToTable("tblSubjects");

            entity.HasIndex(e => e.SubjectName, "UQ__tblSubje__4C5A7D5543CA786D").IsUnique();

            entity.Property(e => e.SubjectId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SubjectID");
            entity.Property(e => e.SubjectName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblTeacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__tblTeach__EDF259441CD6E531");

            entity.ToTable("tblTeachers");

            entity.HasIndex(e => e.Email, "UQ__tblTeach__A9D1053495E95DC6").IsUnique();

            entity.Property(e => e.TeacherId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TeacherID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<TblTeacherSubject>(entity =>
        {
            entity.HasKey(e => e.TeacherSubjectId).HasName("PK__tblTeach__FB4DA4267AFD03B1");

            entity.ToTable("tblTeacherSubjects");

            entity.Property(e => e.TeacherSubjectId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TeacherSubjectID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Subject).WithMany(p => p.TblTeacherSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherSubjects_Subjects");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TblTeacherSubjects)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherSubjects_Teachers");
        });

        modelBuilder.Entity<TblTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__tblTests__8CC331004F34F658");

            entity.ToTable("tblTests");

            entity.Property(e => e.TestId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TestID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TestType).HasMaxLength(50);

            entity.HasOne(d => d.Subject).WithMany(p => p.TblTests)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tests_Subjects");
        });

        modelBuilder.Entity<TblTestWeight>(entity =>
        {
            entity.HasKey(e => e.TestWeightId).HasName("PK__tblTestW__6B4629C8B07AB464");

            entity.ToTable("tblTestWeights");

            entity.Property(e => e.TestWeightId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TestWeightID");
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Test).WithMany(p => p.TblTestWeights)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestWeights_Tests");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
