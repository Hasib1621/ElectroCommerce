using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectroCommerce.Models;
using System.Reflection.Emit;

namespace ElectroCommerce.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Euro Bangla iT", StreetAddress = "123 Adidas St", City = "New York", PostalCode = "12121", State = "IL", PhoneNumber = "01737281939" },
                new Company { Id = 2, Name = "Pondit Limited", StreetAddress = "123 Nike St", City = "New York City", PostalCode = "12121", State = "INL", PhoneNumber = "01977281939" },
                new Company { Id = 3, Name = "4M Design", StreetAddress = "123 Puma St", City = "Delhi", PostalCode = "1216", State = "IND", PhoneNumber = "01583945939" }
                );
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mobile Phones",Description= "Explore a wide range of cutting-edge mobile phones that keep you connected on the go. Discover sleek designs, vibrant displays, powerful processors, and advanced camera systems to capture every moment with clarity. Choose from top brands and experience the latest features and innovations in mobile technology.", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Laptops and Notebooks", Description= "Discover a collection of high-performance laptops and notebooks designed to meet your computing needs. From lightweight and portable models for students and professionals on the move to powerhouse machines for gamers and content creators, our selection offers a variety of options. Enjoy seamless multitasking, stunning displays, and powerful processors for an optimized computing experience.", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Tablets and iPads", Description= "Experience the perfect balance of portability and functionality with our range of tablets and iPads. Whether you're seeking a device for entertainment, productivity, or both, our selection offers an array of sizes, resolutions, and features to suit your preferences. Enjoy vivid displays, intuitive interfaces, and extensive app ecosystems, making these devices ideal for work, play, and everything in between.", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Computer Accessories", Description= "Enhance your computing setup with our wide range of computer accessories. From essential peripherals such as keyboards, mice, and monitors to adapters, cables, and storage solutions, we have everything you need to optimize your workspace. Explore ergonomic designs, wireless connectivity, and innovative accessories that complement your devices and elevate your computing experience.", DisplayOrder = 4 }
                );
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Laptop",
                    Description = "Discover a collection of high-performance laptops and notebooks designed to meet your computing needs. From lightweight and portable models for students and professionals on the move to powerhouse machines for gamers and content creators, our selection offers a variety of options. Enjoy seamless multitasking, stunning displays, and powerful processors for an optimized computing experience.",
                    Price = 999.99,
                    Discount = 10,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 2,
                    Title = "Smartphone",
                    Description = "Experience the latest smartphone technology with our cutting-edge devices. With advanced features, high-resolution displays, and powerful processors, our smartphones offer a seamless and immersive user experience. Stay connected and capture every moment with stunning photos and videos.",
                    Price = 799.99,
                    Discount = 15,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Title = "Headphones",
                    Description = "Immerse yourself in music with our premium headphones. Designed for exceptional sound quality and comfort, our headphones deliver an immersive audio experience. Whether you're a music enthusiast or a professional audio engineer, our headphones are perfect for enjoying your favorite tunes or producing studio-quality sound.",
                    Price = 199.99,
                    Discount = 20,
                    CategoryId = 4
                },
                new Product
                {
                    Id = 4,
                    Title = "Fitness Tracker",
                    Description = "Track your fitness goals and stay motivated with our advanced fitness trackers. Monitor your steps, heart rate, sleep patterns, and more with precision. Set targets, receive personalized insights, and achieve a healthier lifestyle with our intuitive and user-friendly fitness trackers.",
                    Price = 149.99,
                    Discount = 5,
                    CategoryId = 4
                },
                new Product
                {
                    Id = 5,
                    Title = "Camera",
                    Description = "Capture life's precious moments with our high-quality cameras. From compact point-and-shoot cameras to professional DSLRs, our range of cameras offers versatility and exceptional image quality. Explore your creativity and take stunning photos and videos with ease.",
                    Price = 1299.99,
                    Discount = 10,
                    CategoryId = 4
                },
                new Product
                {
                    Id = 6,
                    Title = "Gaming Console",
                    Description = "Experience the thrill of gaming with our powerful gaming consoles. Enjoy cutting-edge graphics, immersive gameplay, and a vast library of games. Whether you're a casual gamer or a competitive esports player, our gaming consoles provide the ultimate gaming experience.",
                    Price = 499.99,
                    Discount = 10,
                    CategoryId = 4
                }
                );
        }
    }
}
