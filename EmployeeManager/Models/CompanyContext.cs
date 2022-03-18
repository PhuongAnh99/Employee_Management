using Microsoft.EntityFrameworkCore;

namespace EmployeeManage.Models
{
    public partial class CompanyContext : DbContext
    {
        public CompanyContext()
        {
        }

        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Employee> Employee { get; set; } = null!;
        public virtual DbSet<Department> Department { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;
        public virtual DbSet<UserSession> UserSession { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;database=EmployeeManager;user=root;password=Phuonganh@123");
            }
            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasComment("Bảng chứa thông tin các bộ phận");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("departmentid")
                    .HasComment("khóa chính")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(255)
                    .HasComment("Tên bộ phận");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasComment("Bảng chứa thông tin các nhân viên");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("employeeid")
                    .HasComment("khóa chính");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(255)
                    .HasComment("Tên nhân viên");

                entity.Property(e => e.Department)
                    .HasMaxLength(255)
                    .HasComment("Tên bộ phận");

                entity.Property(e => e.DateOfJoining)
                    .HasColumnType("datetime")
                    .HasComment("Ngày ra nhập");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasComment("Bảng lưu trữ thông tin người dùng");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("ID")
                    .HasComment("Khóa chính");

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Password).HasColumnType("text");
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.ToTable("usersession");

                entity.HasComment("Bảng lưu trữ thông tin phiên đăng nhập user");

                entity.HasIndex(e => e.UserId)
                    .HasName("id_UNIQUE");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("Khóa chính");

                entity.Property(e => e.LoginDate)
                    .HasColumnType("datetime")
                    .HasComment("Thời gian đăng nhập");

                entity.Property(e => e.AccessToken)
                    .HasColumnType("text")
                    .HasComment("AccessToken");

                entity.Property(e => e.RefreshToken)
                    .HasColumnType("text")
                    .HasComment("Token refresh");

                entity.Property(e => e.RefreshTokenExpireTime)
                    .HasColumnType("datetime")
                    .HasComment("Hạn Token refresh");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("UserID")
                    .HasComment("ID người dùng");

                entity.HasOne(e => e.User)
                    .WithMany(d => d.UserSessions)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Users_UserSession");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
