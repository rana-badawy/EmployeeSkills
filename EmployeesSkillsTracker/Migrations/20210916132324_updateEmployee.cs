﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesSkillsTracker.Migrations
{
    public partial class updateEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName", "Password" },
                values: new object[] { "a", "A", "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName", "Password" },
                values: new object[] { "b", "A", "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                columns: new[] { "FirstName", "LastName", "Password" },
                values: new object[] { "c", "A", "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 4,
                columns: new[] { "FirstName", "LastName", "Password" },
                values: new object[] { "d", "A", "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 5,
                columns: new[] { "FirstName", "LastName", "Password" },
                values: new object[] { "e", "A", "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Employees",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "Name",
                value: "a");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "Name",
                value: "b");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "Name",
                value: "c");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 4,
                column: "Name",
                value: "d");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 5,
                column: "Name",
                value: "e");
        }
    }
}
