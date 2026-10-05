using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddStorePublicFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoverImageId",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Stores",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Tagline",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThemePalette",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowOnLanding",
                table: "Products",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StoreSocialLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    DeleteDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    Platform = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreSocialLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreSocialLinks_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_CoverImageId",
                table: "Stores",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreSocialLinks_StoreId",
                table: "StoreSocialLinks",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ImageAssets_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "ImageAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_ImageAssets_CoverImageId",
                table: "Stores",
                column: "CoverImageId",
                principalTable: "ImageAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ImageAssets_ImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_ImageAssets_CoverImageId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "StoreSocialLinks");

            migrationBuilder.DropIndex(
                name: "IX_Stores_CoverImageId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Tagline",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ThemePalette",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShowOnLanding",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Products");
        }
    }
}
