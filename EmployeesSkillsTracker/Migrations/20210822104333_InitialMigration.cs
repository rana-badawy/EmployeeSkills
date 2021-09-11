using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesSkillsTracker.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperience = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vertical = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesSkills",
                columns: table => new
                {
                    EmployeeSkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesSkills", x => x.EmployeeSkillID);
                    table.ForeignKey(
                        name: "FK_EmployeesSkills_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesSkills_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Email", "Name", "PhoneNumber", "Role", "Salary", "YearsOfExperience" },
                values: new object[,]
                {
                    { 1, "a@e.com", "a", "123", "Developer", 1000, 1.0 },
                    { 2, "b@e.com", "b", "456", "Developer", 1000, 2.0 },
                    { 3, "c@e.com", "c", "789", "Project Manager", 2000, 2.0 },
                    { 4, "d@e.com", "d", "012", "Manager", 3000, 3.0 },
                    { 5, "e@e.com", "e", "234", "Admin", 4000, 2.0 }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillID", "Name", "Vertical" },
                values: new object[,]
                {
                    { 1, "Blue Prism", 0 },
                    { 2, "UiPath", 0 },
                    { 3, "Knime", 0 },
                    { 4, "Java", 0 },
                    { 5, "Python", 0 },
                    { 6, "PowerBI", 0 },
                    { 7, "PowerApps", 0 },
                    { 8, "VBA", 0 }
                });

            migrationBuilder.InsertData(
                table: "EmployeesSkills",
                columns: new[] { "EmployeeSkillID", "EmployeeID", "Level", "SkillID" },
                values: new object[,]
                {
                    { 1, 1, "Beginner", 1 },
                    { 8, 4, "Beginner", 8 },
                    { 14, 2, "Intermediate", 7 },
                    { 12, 5, "Intermediate", 7 },
                    { 7, 3, "Beginner", 7 },
                    { 17, 1, "Beginner", 6 },
                    { 6, 3, "Advanced", 6 },
                    { 13, 5, "Intermediate", 8 },
                    { 5, 2, "Beginner", 5 },
                    { 4, 2, "Beginner", 4 },
                    { 10, 4, "Intermediate", 3 },
                    { 3, 1, "Advanced", 3 },
                    { 2, 1, "Intermediate", 2 },
                    { 16, 2, "Advanced", 1 },
                    { 9, 4, "Intermediate", 1 },
                    { 11, 5, "Intermediate", 4 },
                    { 15, 2, "Beginner", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesSkills_EmployeeID",
                table: "EmployeesSkills",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesSkills_SkillID",
                table: "EmployeesSkills",
                column: "SkillID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesSkills");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
