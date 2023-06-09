using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElectroCommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addProductToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Discount", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 2, "Discover a collection of high-performance laptops and notebooks designed to meet your computing needs. From lightweight and portable models for students and professionals on the move to powerhouse machines for gamers and content creators, our selection offers a variety of options. Enjoy seamless multitasking, stunning displays, and powerful processors for an optimized computing experience.", 10.0, 999.99000000000001, "Laptop" },
                    { 2, 1, "Experience the latest smartphone technology with our cutting-edge devices. With advanced features, high-resolution displays, and powerful processors, our smartphones offer a seamless and immersive user experience. Stay connected and capture every moment with stunning photos and videos.", 15.0, 799.99000000000001, "Smartphone" },
                    { 3, 4, "Immerse yourself in music with our premium headphones. Designed for exceptional sound quality and comfort, our headphones deliver an immersive audio experience. Whether you're a music enthusiast or a professional audio engineer, our headphones are perfect for enjoying your favorite tunes or producing studio-quality sound.", 20.0, 199.99000000000001, "Headphones" },
                    { 4, 4, "Track your fitness goals and stay motivated with our advanced fitness trackers. Monitor your steps, heart rate, sleep patterns, and more with precision. Set targets, receive personalized insights, and achieve a healthier lifestyle with our intuitive and user-friendly fitness trackers.", 5.0, 149.99000000000001, "Fitness Tracker" },
                    { 5, 4, "Capture life's precious moments with our high-quality cameras. From compact point-and-shoot cameras to professional DSLRs, our range of cameras offers versatility and exceptional image quality. Explore your creativity and take stunning photos and videos with ease.", 10.0, 1299.99, "Camera" },
                    { 6, 4, "Experience the thrill of gaming with our powerful gaming consoles. Enjoy cutting-edge graphics, immersive gameplay, and a vast library of games. Whether you're a casual gamer or a competitive esports player, our gaming consoles provide the ultimate gaming experience.", 10.0, 499.99000000000001, "Gaming Console" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
