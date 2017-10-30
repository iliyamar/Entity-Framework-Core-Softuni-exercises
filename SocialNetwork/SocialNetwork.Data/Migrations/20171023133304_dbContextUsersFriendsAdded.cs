using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Social2.Data.Migrations
{
    public partial class dbContextUsersFriendsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_Users_FriendFriendId",
                table: "UserFriend");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_Users_FriendId",
                table: "UserFriend");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriend",
                table: "UserFriend");

            migrationBuilder.RenameTable(
                name: "UserFriend",
                newName: "UsersFriends");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriend_FriendFriendId",
                table: "UsersFriends",
                newName: "IX_UsersFriends_FriendFriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFriends",
                table: "UsersFriends",
                columns: new[] { "FriendId", "FriendFriendId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFriends_Users_FriendFriendId",
                table: "UsersFriends",
                column: "FriendFriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFriends_Users_FriendId",
                table: "UsersFriends",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersFriends_Users_FriendFriendId",
                table: "UsersFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFriends_Users_FriendId",
                table: "UsersFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFriends",
                table: "UsersFriends");

            migrationBuilder.RenameTable(
                name: "UsersFriends",
                newName: "UserFriend");

            migrationBuilder.RenameIndex(
                name: "IX_UsersFriends_FriendFriendId",
                table: "UserFriend",
                newName: "IX_UserFriend_FriendFriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriend",
                table: "UserFriend",
                columns: new[] { "FriendId", "FriendFriendId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_Users_FriendFriendId",
                table: "UserFriend",
                column: "FriendFriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_Users_FriendId",
                table: "UserFriend",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
