using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsControl.Migrations
{
	public partial class CreationUsersTable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "DB_Users",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Profile = table.Column<int>(type: "int", nullable: false),
					RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					ProfileUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => table.PrimaryKey
				(
					"PK_Users", x => x.Id
				)
			);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "DB_Users"
				);
		}
	}
}
