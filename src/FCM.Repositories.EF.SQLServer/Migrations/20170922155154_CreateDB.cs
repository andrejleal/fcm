using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FCM.Repositories.EF.SQLServer.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppId = table.Column<Guid>(nullable: false),
                    ConcurrencyToken = table.Column<Guid>(nullable: false),
                    CreationTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdateTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Owner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.UniqueConstraint("AK_Components_AppId", x => x.AppId);
                    table.UniqueConstraint("AK_Components_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ExternalSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlternateToken = table.Column<string>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    ConcurrencyToken = table.Column<Guid>(nullable: false),
                    CreationTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    IsSysAdmin = table.Column<bool>(nullable: false),
                    LastUpdateTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NotificationToken = table.Column<string>(nullable: true),
                    NotificationURL = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalSystem", x => x.Id);
                    table.UniqueConstraint("AK_ExternalSystem_AlternateToken", x => x.AlternateToken);
                    table.UniqueConstraint("AK_ExternalSystem_AppId", x => x.AppId);
                    table.UniqueConstraint("AK_ExternalSystem_Name", x => x.Name);
                    table.UniqueConstraint("AK_ExternalSystem_Token", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "ComponentProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppId = table.Column<Guid>(nullable: false),
                    ConcurrencyToken = table.Column<Guid>(nullable: false),
                    CreationTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdateTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentProperties", x => x.Id);
                    table.UniqueConstraint("AK_ComponentProperties_AppId", x => x.AppId);
                    table.ForeignKey(
                        name: "FK_ComponentProperties_Components_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProperties_ParentId",
                table: "ComponentProperties",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProperties_Name_Owner_ParentId",
                table: "ComponentProperties",
                columns: new[] { "Name", "Owner", "ParentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentProperties");

            migrationBuilder.DropTable(
                name: "ExternalSystem");

            migrationBuilder.DropTable(
                name: "Components");
        }
    }
}
