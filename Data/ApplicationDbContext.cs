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
        public virtual Account Account { get; set; }
        public virtual Goods Goods { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }

    }

    public class Comments
    {
        [Key]
        public int ID { get; set; }
        public virtual AccountGood AccountGood { get; set; }
        public string content { get; set; }
        public DateTime create_time { get; set; }
        public double rate_star { get; set; }
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



            //define composite primary key
            builder.Entity<AccountGood>()
                .HasKey(ag => new { ag.Account_ID, ag.Goods_ID, ag.Order_ID });

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
            builder.Entity<Goods>()
                .HasOne(c => c.Category)
                .WithMany(g => g.Goods)
                .HasForeignKey(fk => new { fk.cat_id })
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
        public DbSet<AccountGood> AccountGoods { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
