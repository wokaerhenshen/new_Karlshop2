using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using new_Karlshop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace new_Karlshop.Data
{
    public class ViewedGoods
    {
        [Key, Column(Order = 0)]
        public string Account_ID { get; set; }
        [Key, Column(Order = 1)]
        public int Goods_ID { get; set; }
        public int ViewedSequence { get; set; }
        public virtual Account Account { get; set; }
        public virtual Goods Goods { get; set; }
    }


    public class AccountGood
    {
        [Key, Column(Order = 0)]
        public string Account_ID { get; set; }
        [Key, Column(Order = 1)]
        public int Goods_ID { get; set; }
        [Key]
        public int Order_ID { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public bool Viewed { get; set; }
        public virtual Account Account { get; set; }
        public virtual Goods Goods { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }

    }

    public class Order
    {
        //[Key, Column(Order=0)]
        //public string Account_ID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Order_id { get; set; }
        public string Account_ID { get; set; }
        public DateTime order_time { get; set; }
        public decimal total_price { get; set; }
        public int total_number { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<OrderGoods> OrderGoods { get; set; }
    }

    public class OrderGoods
    {
        [Key,Column(Order=0)]
        public int Order_id { get; set; }
        [Key,Column(Order=1)]
        public int goods_id { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Goods Goods { get; set; }


    }


    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public virtual AccountGood AccountGood { get; set; }
        public string content { get; set; }
        public DateTime create_time { get; set; }
        public double rate_star { get; set; }
        public string ProfileImg { get; set; }
        public string AuthorName { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ApplicationUser>()
            .HasOne(ac => ac.Account)
            .WithOne(au => au.ApplicationUser)
            .HasForeignKey<Account>(ac => ac.Id);

            //builder.Entity<Order>()
            //    .HasKey(or => new { or.Account_ID, or.Order_id });
            //builder.Entity<Order>()
            //    .HasOne(c => c.Account)
            builder.Entity<ViewedGoods>()
                .HasKey(ag => new { ag.Account_ID, ag.Goods_ID });

            //define foreign key, this is a many to many relationship so this cartgood is a
            // connection table.
            builder.Entity<ViewedGoods>()
                .HasOne(c => c.Account)
                .WithMany(ag => ag.ViewedGoods)
                .HasForeignKey(fk => new { fk.Account_ID })
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ViewedGoods>()
                .HasOne(g => g.Goods)
                .WithMany(cg => cg.ViewedGoods)
                .HasForeignKey(fk => new { fk.Goods_ID })
                .OnDelete(DeleteBehavior.Restrict);



            //define composite primary key
            builder.Entity<AccountGood>()
                .HasKey(ag => new { ag.Account_ID, ag.Goods_ID , ag.Order_ID });

            //define foreign key, this is a many to many relationship so this cartgood is a
            // connection table.
            builder.Entity<AccountGood>()
                .HasOne(c => c.Account)
                .WithMany(ag => ag.AccountGood)
                .HasForeignKey(fk => new { fk.Account_ID })
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AccountGood>()
                .HasOne(g => g.Goods)
                .WithMany(cg => cg.AccountGood)
                .HasForeignKey(fk => new { fk.Goods_ID })
                .OnDelete(DeleteBehavior.Restrict);


            // goods and category is one to many relationship
            // I think it's unnecessary?
            builder.Entity<Goods>()
                .HasOne(c => c.Category)
                .WithMany(g => g.Goods)
                .HasForeignKey(fk => new { fk.cat_id })
                .OnDelete(DeleteBehavior.Restrict);

            //order and account is one to many relationship
            builder.Entity<Order>()
                .HasOne(a=>a.Account)
                .WithMany(o=>o.Order)
                .HasForeignKey(fk=>new { fk.Account_ID })
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderGoods>()
               .HasKey(og => new { og.Order_id ,og.goods_id });

            //configure the many to many relationship of goods and orders
            //by using this kind of stuff
            builder.Entity<OrderGoods>()
               .HasOne(o => o.Order)
               .WithMany(og => og.OrderGoods)
               .HasForeignKey(fk => fk.Order_id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderGoods>()
                .HasOne(g=> g.Goods)
                .WithMany(og=>og.OrderGoods)
                .HasForeignKey(fk=> fk.goods_id)
                .OnDelete(DeleteBehavior.Restrict);




            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Goods> Goodses { get; set; }
        public DbSet<ViewedGoods> ViewedGoods { get; set; }
        public DbSet<AccountGood> AccountGoods { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderGoods> OrderGoods { get; set; }
        public DbSet<IPN> IPNs { get; set; }
    }
}
