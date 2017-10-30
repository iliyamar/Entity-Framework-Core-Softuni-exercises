using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Social2.Data.Migrations
{
    public partial class temp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedAlbum_Albums_AlbumId",
                table: "SharedAlbum");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedAlbum_Users_UserId",
                table: "SharedAlbum");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedAlbum",
                table: "SharedAlbum");

            migrationBuilder.RenameTable(
                name: "SharedAlbum",
                newName: "SharedAlbums");

            migrationBuilder.RenameIndex(
                name: "IX_SharedAlbum_UserId",
                table: "SharedAlbums",
                newName: "IX_SharedAlbums_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedAlbums",
                table: "SharedAlbums",
                columns: new[] { "AlbumId", "UserId", "SharedToId" });

            migrationBuilder.CreateIndex(
                name: "IX_SharedAlbums_SharedToId",
                table: "SharedAlbums",
                column: "SharedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedAlbums_Albums_AlbumId",
                table: "SharedAlbums",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedAlbums_Users_SharedToId",
                table: "SharedAlbums",
                column: "SharedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedAlbums_Users_UserId",
                table: "SharedAlbums",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedAlbums_Albums_AlbumId",
                table: "SharedAlbums");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedAlbums_Users_SharedToId",
                table: "SharedAlbums");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedAlbums_Users_UserId",
                table: "SharedAlbums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedAlbums",
                table: "SharedAlbums");

            migrationBuilder.DropIndex(
                name: "IX_SharedAlbums_SharedToId",
                table: "SharedAlbums");

            migrationBuilder.RenameTable(
                name: "SharedAlbums",
                newName: "SharedAlbum");

            migrationBuilder.RenameIndex(
                name: "IX_SharedAlbums_UserId",
                table: "SharedAlbum",
                newName: "IX_SharedAlbum_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedAlbum",
                table: "SharedAlbum",
                columns: new[] { "AlbumId", "UserId", "SharedToId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SharedAlbum_Albums_AlbumId",
                table: "SharedAlbum",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedAlbum_Users_UserId",
                table: "SharedAlbum",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
