﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class Added_Person : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignedPersonId",
                table: "AppTasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPersons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTasks_AssignedPersonId",
                table: "AppTasks",
                column: "AssignedPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTasks_AppPersons_AssignedPersonId",
                table: "AppTasks",
                column: "AssignedPersonId",
                principalTable: "AppPersons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTasks_AppPersons_AssignedPersonId",
                table: "AppTasks");

            migrationBuilder.DropTable(
                name: "AppPersons");

            migrationBuilder.DropIndex(
                name: "IX_AppTasks_AssignedPersonId",
                table: "AppTasks");

            migrationBuilder.DropColumn(
                name: "AssignedPersonId",
                table: "AppTasks");
        }
    }
}
