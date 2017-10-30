using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Social2.Data.Migrations
{
    public partial class usersAlbumsManyToManySharedToAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedAlbum",
                table: "SharedAlbum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedAlbum",
                table: "SharedAlbum",
                columns: new[] { "AlbumId", "UserId", "SharedToId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedAlbum",
                table: "SharedAlbum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedAlbum",
                table: "SharedAlbum",
                columns: new[] { "AlbumId", "UserId" });
        }
    }
}
