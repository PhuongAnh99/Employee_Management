﻿// <auto-generated />
using System;
using EmployeeManage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManager.Migrations
{
    [DbContext(typeof(CompanyContext))]
    partial class CompanyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EmployeeManage.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("departmentid")
                        .HasColumnType("int unsigned")
                        .HasComment("khóa chính");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasComment("Tên bộ phận")
                        .HasMaxLength(255);

                    b.HasKey("DepartmentId");

                    b.HasIndex("DepartmentId")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.ToTable("department");

                    b.HasComment("Bảng chứa thông tin các bộ phận");
                });

            modelBuilder.Entity("EmployeeManage.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("employeeid")
                        .HasColumnType("int unsigned")
                        .HasComment("khóa chính");

                    b.Property<DateTime>("DateOfJoining")
                        .HasColumnType("datetime")
                        .HasComment("Ngày ra nhập");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasComment("Tên bộ phận")
                        .HasMaxLength(255);

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasComment("Tên nhân viên")
                        .HasMaxLength(255);

                    b.HasKey("EmployeeId");

                    b.ToTable("employee");

                    b.HasComment("Bảng chứa thông tin các nhân viên");
                });

            modelBuilder.Entity("EmployeeManage.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int unsigned")
                        .HasComment("Khóa chính");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("user");

                    b.HasComment("Bảng lưu trữ thông tin người dùng");
                });

            modelBuilder.Entity("EmployeeManage.Models.UserSession", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("varbinary(16)")
                        .HasComment("Khóa chính");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("AccessToken");

                    b.Property<DateTime>("LoginDate")
                        .HasColumnType("datetime")
                        .HasComment("Thời gian đăng nhập");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Token refresh");

                    b.Property<DateTime>("RefreshTokenExpireTime")
                        .HasColumnType("datetime")
                        .HasComment("Hạn Token refresh");

                    b.Property<int>("UserId")
                        .HasColumnName("UserID")
                        .HasColumnType("int(11) unsigned")
                        .HasComment("ID người dùng");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("id_UNIQUE");

                    b.ToTable("usersession");

                    b.HasComment("Bảng lưu trữ thông tin phiên đăng nhập user");
                });

            modelBuilder.Entity("EmployeeManage.Models.UserSession", b =>
                {
                    b.HasOne("EmployeeManage.Models.User", "User")
                        .WithMany("UserSessions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Users_UserSession")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
