using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace EmployeeManager.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    departmentid = table.Column<int>(type: "int unsigned", nullable: false, comment: "khóa chính")
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DepartmentName = table.Column<string>(maxLength: 255, nullable: false, comment: "Tên bộ phận")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.departmentid);
                },
                comment: "Bảng chứa thông tin các bộ phận");

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employeeid = table.Column<int>(type: "int unsigned", nullable: false, comment: "khóa chính")
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EmployeeName = table.Column<string>(maxLength: 255, nullable: false, comment: "Tên nhân viên"),
                    Department = table.Column<string>(maxLength: 255, nullable: false, comment: "Tên bộ phận"),
                    DateOfJoining = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày ra nhập")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employeeid);
                },
                comment: "Bảng chứa thông tin các nhân viên");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int unsigned", nullable: false, comment: "Khóa chính")
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.ID);
                },
                comment: "Bảng lưu trữ thông tin người dùng");

            migrationBuilder.CreateTable(
                name: "usersession",
                columns: table => new
                {
                    ID = table.Column<byte[]>(nullable: false, comment: "Khóa chính"),
                    UserID = table.Column<int>(type: "int(11) unsigned", nullable: false, comment: "ID người dùng"),
                    LoginDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Thời gian đăng nhập"),
                    AccessToken = table.Column<string>(type: "text", nullable: false, comment: "AccessToken"),
                    RefreshToken = table.Column<string>(type: "text", nullable: false, comment: "Token refresh"),
                    RefreshTokenExpireTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Hạn Token refresh")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersession", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_UserSession",
                        column: x => x.UserID,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Bảng lưu trữ thông tin phiên đăng nhập user");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "department",
                column: "departmentid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "usersession",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "usersession");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
