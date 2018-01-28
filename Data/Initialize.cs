using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Data
{
    public class Initialize
    {
        private ApplicationDbContext _context;
        public Initialize(ApplicationDbContext context)
        {
            _context = context;
            InitializeData();
        }

        public void InitializeData()
        {
            if (_context.Goodses.Count() != 0)
            {
                return;
            }

            //_context.Accounts.Add(
            //    new Account
            //    {
            //        Id = 1,
            //        username = "Karl",
            //        password = "123",
            //        firstName = "Karl",
            //        lastName = "Xu",
            //        email = "karlxu0410@gmail.com",
            //        phone = "7788619422",
            //        address = "6520 Clematis Drive",
            //        type = "admin"
                    
            //    }
            //    );

            _context.SaveChanges();

            _context.Categories.Add(new Category()
            {
                cat_id = 1,
                parent_id = 0,
                cat_name = "Smart Phone",
                intro = "Smart Phone"
            });

            _context.Categories.Add(new Category()
            {
                cat_id = 2,
                parent_id = 0,
                cat_name = "Laptop",
                intro = "Laptop"
            });

            _context.Categories.Add(new Category()
            {
                cat_id = 3,
                parent_id = 0,
                cat_name = "Television",
                intro = "Television"
            });
            _context.Categories.Add(new Category()
            {
                cat_id = 11,
                parent_id = 1,
                cat_name = "iPhone",
                intro = "iPhone"
            });
            _context.Categories.Add(new Category()
            {
                cat_id = 12,
                parent_id = 1,
                cat_name = "Samsung",
                intro = "Samsung"
            });
            _context.Categories.Add(new Category()
            {
                cat_id = 13,
                parent_id = 1,
                cat_name = "HUAWEI",
                intro = "HUAWEI"

            });
            _context.Categories.Add(new Category()
            {
                cat_id = 21,
                parent_id = 2,
                cat_name = "Lenovo",
                intro = "lenovo"
            });
            _context.Categories.Add(new Category()
            {
                cat_id = 22,
                parent_id = 2,
                cat_name = "Dell",
                intro = "Dell"
            });
            _context.Categories.Add(new Category()
            {
                cat_id = 23,
                parent_id = 2,
                cat_name = "HP",
                intro = "HP"
            });

            _context.Categories.Add(new Category()
            {
                cat_id = 31,
                parent_id = 3,
                cat_name = "Hisense",
                intro = "Hisense"
            });
            _context.Categories.Add(new Category()
            {
                cat_id = 32,
                parent_id = 3,
                cat_name = "Samsung",
                intro = "Samsung"
            });
            _context.Categories.Add(new Category()
            {
                cat_id = 33,
                parent_id = 3,
                cat_name = "LG",
                intro = "LG"
            });

            _context.SaveChanges();

         
                 _context.Goodses.Add(new Goods()
                 {
                     cat_id = 11,
                     goods_id = 1,
                     goods_sn = "ECS000001",
                     goods_name = "iPhone 6s",
                     shop_price = 769.99m,
                     market_price = 869.60m,
                     goods_quantity = 127,
                     sold_quantity = 6,
                     goods_weight = 1.2m,
                     goods_desc = "iPhone 6s 4.7-inch display",
                     goods_brief = "iPhone 6s 4.7-inch display",
                     ori_img = "iphone6s-silver-select.png",
                     ori_img1 = "iphone6sp-silver-select-1.png",
                     ori_img2 = "iphone6sp-silver-select-2.png",
                     is_delete = true,
                     is_free_post = true,
                     last_update = new DateTime(2013, 11, 28, 13, 09, 45)
                 });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 11,
                goods_id = 2,
                goods_sn = "ECS000002",
                goods_name = "iPhone 6s Plus",
                shop_price = 899.00m,
                market_price = 969.60m,
                goods_quantity = 17,
                sold_quantity = 6,
                goods_weight = 1.2m,
                goods_desc = "iPhone 6s Plus 5.5-inch display",
                goods_brief = "iPhone 6s Plus 5.5-inch display",
                ori_img = "iphone6sp-silver-select.png",
                ori_img1 = "iphone6sp-silver-select.png",
                ori_img2 = "iphone6sp-silver-select.png",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2010, 11, 28, 13, 09, 45)

            });


            _context.Goodses.Add(new Goods()
            {
                cat_id = 11,
                goods_id = 3,
                goods_sn = "ECS000003",

                goods_name = "iPhone 7",
                shop_price = 899.08m,
                market_price = 1069.60m,
                goods_quantity = 17,
                sold_quantity = 10,
                goods_weight = 1.2m,
                goods_desc = "iPhone 7 4.7-inch display",
                goods_brief = "iPhone 7 4.7-inch display",
                ori_img = "iphone7-select-2016.png",
                ori_img1 = "iphone7-select-2016.png",
                ori_img2 = "iphone7-select-2016.png",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 28, 13, 09, 45)

            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 11,
                goods_id = 4,
                goods_sn = "ECS000004",
                goods_name = "iPhone 7 Plus",
                shop_price = 1049.00m,
                market_price = 1169.60m,
                goods_quantity = 17,
                sold_quantity = 3,
                goods_weight = 1.2m,
                goods_desc = "iPhone 7 Plus 5.5-inch display",
                goods_brief = "iPhone 7 Plus 5.5-inch display",
                ori_img = "iphone7-plus-select-2016.png",
                ori_img1 = "iphone7-plus-select-2016.png",
                ori_img2 = "iphone7-plus-select-2016.png",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2000, 11, 28, 13, 09, 45)

            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 11,
                goods_id = 5,
                goods_sn = "ECS000005",
                goods_name = "iPhone SE",
                shop_price = 579.00m,
                market_price = 669.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "iPhone SE 4-inch display",
                goods_brief = "iPhone SE 4-inch display",
                ori_img = "iphonese-hero-modelselect.png",
                ori_img1 = "iphonese-hero-modelselect.png",
                ori_img2 = "iphonese-hero-modelselect.png",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2015, 11, 28, 13, 09, 45)

            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 12,
                goods_id = 6,
                goods_sn = "ECS000006",
                goods_name = "Galaxy S7 edge",
                shop_price = 900.00m,
                market_price = 1069.60m,
                goods_quantity = 17,
                sold_quantity = 6,
                goods_weight = 1.2m,
                goods_desc = "ca-smartphones-galaxy-s7-galaxys7edge_gold",
                goods_brief = "galaxy-s7 with large screen",
                ori_img = "ca-smartphones-galaxy-s7-galaxys7edge_gold.jpeg",
                ori_img1 = "ca-smartphones-galaxy-s7-galaxys7edge_gold.jpeg",
                ori_img2 = "ca-smartphones-galaxy-s7-galaxys7edge_gold.jpeg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 01, 28, 13, 09, 45)

            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 12,
                goods_id = 7,
                goods_sn = "ECS000007",
                goods_name = "Galaxy S7",
                shop_price = 770.00m,
                market_price = 869.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "ca-smartphones-galaxy-s7-galaxys7edge_gold",
                goods_brief = "galaxy-s7-galaxys7 with gold edge",
                ori_img = "ca-smartphones-galaxy-s7-galaxys7_gold.jpeg",
                ori_img1 = "ca-smartphones-galaxy-s7-galaxys7_gold.jpeg",
                ori_img2 = "ca-smartphones-galaxy-s7-galaxys7_gold.jpeg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 02, 28, 13, 09, 45)

            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 13,
                goods_id = 8,
                goods_sn = "ECS000008",
                goods_name = "HUAWEI Mate 9",
                shop_price = 869.00m,
                market_price = 969.60m,
                goods_quantity = 17,
                sold_quantity = 9,
                goods_weight = 1.2m,
                goods_desc = "HUAWEI Mate 9 Leica dual camera, 5.9” FHD Display",
                goods_brief = "HUAWEI Mate 9 Leica dual camera, 5.9” FHD Display",
                ori_img = "161103205719198317229Mate9_listimage1.jpg",
                ori_img1 = "161103205719198317229Mate9_listimage1.jpg",
                ori_img2 = "161103205719198317229Mate9_listimage1.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 12, 28, 13, 09, 45)

            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 21,
                goods_id = 9,
                goods_sn = "ECS000009",
                goods_name = "ThinkPad X260",
                shop_price = 1070.10m,
                market_price = 1269.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "HThinkPad X260 New = Portable, durable, and fully loaded inside a thin, light design, this 12.5 enterprise-ready powerhouse comes with an extended battery life.",
                goods_brief = "HThinkPad X260 with new features",
                ori_img = "lenovo-laptop-thinkpad-x260-front-side-2.jpg",
                ori_img1 = "lenovo-laptop-thinkpad-x260-front-side-2.jpg",
                ori_img2 = "lenovo-laptop-thinkpad-x260-front-side-2.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 28, 13, 09, 45)

            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 21,
                goods_id = 10,
                goods_sn = "ECS000010",
                goods_name = "ThinkPad X1 Yoga",
                shop_price = 1808.10m,
                market_price = 2069.60m,
                goods_quantity = 17,
                sold_quantity = 1,
                goods_weight = 1.2m,
                goods_desc = "ThinkPad X1 Yoga Ultralight 14 Business 2-in-1 The definition of versatility, this ultralight 2-in-1 adapts to your business with 4 flexible modes to work, present, create, and connect. Features a stunning display with intense color and deep contrast. Even a dockable, rechargeable stylus pen. Plus, the fastest, advanced mobile broadband technology available.",
                goods_brief = "ThinkPad X1 Yoga Ultralight 14 Business 2-in-1 ",
                ori_img = "X260-hero.png",
                ori_img1 = "X260-hero.png",
                ori_img2 = "X260-hero.png",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2002, 11, 28, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 21,
                goods_id = 11,
                goods_sn = "ECS000011",
                goods_name = "ThinkPad X1 Carbon",
                shop_price = 1727.10m,
                market_price = 1869.60m,
                goods_quantity = 17,
                sold_quantity = 2,
                goods_weight = 1.2m,
                goods_desc = "Ultrathin. Ultralight. Ultratough. For the average Ultrabook™ these attributes may sound like a contradiction. But the new X1 Carbon is far above average. It features a carbon-fiber reinforced chassis & passes durability tests in extreme environments. Plus, it delivers more than all-day battery life, includes faster, more powerful storage performance, & has innovative docking options available, including wireless.",
                goods_brief = "Ultrathin. Ultralight. Ultratough",
                ori_img = "X1-Carbon-List-Image.png",
                ori_img1 = "X1-Carbon-List-Image.png",
                ori_img2 = "X1-Carbon-List-Image.png",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2014, 11, 28, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 21,
                goods_id = 12,
                goods_sn = "ECS000012",
                goods_name = "ThinkPad X260",
                shop_price = 1070.10m,
                market_price = 1169.60m,
                goods_quantity = 17,
                sold_quantity = 3,
                goods_weight = 1.2m,
                goods_desc = "HThinkPad X260 New = Portable, durable, and fully loaded inside a thin, light design, this 12.5 enterprise - ready powerhouse comes with an extended battery life.",
                goods_brief = "HThinkPad X260 New = Portable, durable",
                ori_img = "lenovo-laptop-thinkpad-x260-front-side-2.jpg",
                ori_img1 = "lenovo-laptop-thinkpad-x260-front-side-2.jpg",
                ori_img2 = "lenovo-laptop-thinkpad-x260-front-side-2.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2015, 11, 28, 13, 09, 45)


            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 22,
                goods_id = 13,
                goods_sn = "ECS000013",
                goods_name = "ALIENWARE 13",
                shop_price = 1549.99m,
                market_price = 1669.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "Alienware’s most powerful 13 gaming laptop. Featuring quad-core, H-class processors and NVIDIA 10-series graphics, the Alienware 13 has evolved to offer unprecedented gameplay.",
                goods_brief = "Alienware’s most powerful 13 gaming laptop.",
                ori_img = "OriginalPng.png",
                ori_img1 = "OriginalPng.png",
                ori_img2 = "OriginalPng.png",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 21, 13, 09, 45)


            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 22,
                goods_id = 14,
                goods_sn = "ECS000014",
                goods_name = "AHP EliteBook Folio G1",
                shop_price = 1549.99m,
                market_price = 1769.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "HP EliteBook Folio G1 - 12.5 - Core m5 6Y54 - 8 GB RAM - 256 GB SSD",
                goods_brief = "HP EliteBook Folio G1 - 12.5 - Core m5",
                ori_img = "4196553.jpeg",
                ori_img1 = "4196553.jpeg",
                ori_img2 = "4196553.jpeg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 22, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 23,
                goods_id = 15,
                goods_sn = "ECS000015",
                goods_name = "HP EliteBook 2570p",
                shop_price = 1549.99m,
                market_price = 1769.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "HP EliteBook 2570p - 12.5 - Core i7 3520M - 4 GB RAM - 128 GB SSD",
                goods_brief = "HP EliteBook 2570p - 12.5 - Core i7 3520M - 4 GB RAM - 128 GB SSD",
                ori_img = "2832948.jpeg",
                ori_img1 = "2832948.jpeg",
                ori_img2 = "2832948.jpeg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 23, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 31,
                goods_id = 16,
                goods_sn = "ECS000016",
                goods_name = "Hisense H7GB 50",
                shop_price = 699.99m,
                market_price = 869.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "With a stunning Ultra High-Definition 4K resolution, the Hisense 50 LED Smart TV will make your favourite movies and TV shows look like real life. This smart TV is a certified Netflix Recommended TV and offers a modernized smart tv experience.",
                goods_brief = "With a stunning Ultra High-Definition 4K resolution",
                ori_img = "10448897.jpg",
                ori_img1 = "10448897.jpg",
                ori_img2 = "10448897.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2013, 11, 28, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 32,
                goods_id = 17,
                goods_sn = "ECS000017",
                goods_name = "Samsung 40",
                shop_price = 799.99m,
                market_price = 969.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "Samsung 40 KU6270 4K UHD Television",
                goods_brief = "Samsung 40 KU6270 4K UHD Television",
                ori_img = "485572.jpg",
                ori_img1 = "485572.jpg",
                ori_img2 = "485572.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2014, 11, 28, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 33,
                goods_id = 18,
                goods_sn = "ECS000018",
                goods_name = "LG 55 UH6150 4K UHD Smart",
                shop_price = 1549.99m,
                market_price = 1669.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "LG 55 UH6150 4K UHD Smart LED Television",
                goods_brief = "LG 55 UH6150 4K UHD Smart LED Television",
                ori_img = "10414409.jpg",
                ori_img1 = "10414409.jpg",
                ori_img2 = "10414409.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2015, 11, 28, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 32,
                goods_id = 19,
                goods_sn = "ECS000019",
                goods_name = "Samsung 58 Smart Full HD LED ",
                shop_price = 1549.99m,
                market_price = 1869.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "Samsung 58 Smart Full HD LED ",
                goods_brief = "Samsung 58 Smart Full HD LED ",
                ori_img = "485597.jpg",
                ori_img1 = "485597.jpg",
                ori_img2 = "485597.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2016, 11, 28, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 31,
                goods_id = 20,
                goods_sn = "ECS000020",
                goods_name = "Sylvania 39 HD LED ",
                shop_price = 299.99m,
                market_price = 369.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "HP EliteBook 2570p - 12.5 - Core i7 3520M - 4 GB RAM - 128 GB SSD",
                goods_brief = "HP EliteBook 2570p - 12.5 - Core i7 3520M - 4 GB RAM - 128 GB SSD",
                ori_img = "483663.jpg",
                ori_img1 = "483663.jpg",
                ori_img2 = "483663.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 25, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 33,
                goods_id = 21,
                goods_sn = "ECS000021",
                goods_name = "LG 49 UH6100 4K UHD Smart",
                shop_price = 1099.99m,
                market_price = 1169.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "LG 49 UH6100 4K UHD Smart LED TV with webOS™ 3.0",
                goods_brief = "LG 49 UH6100 4K UHD Smart LED TV with webOS™ 3.0",
                ori_img = "436631.jpg",
                ori_img1 = "436631.jpg",
                ori_img2 = "436631.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 26, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 33,
                goods_id = 22,
                goods_sn = "ECS000022",
                goods_name = "LG 55 UH7650 4K Super ",
                shop_price = 1549.99m,
                market_price = 1669.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "LG 55 UH7650 4K Super UHD Television with webOS™ 3.0",
                goods_brief = "LG 55 UH7650 4K Super UHD Television with webOS™ 3.0",
                ori_img = "436115.jpg",
                ori_img1 = "436115.jpg",
                ori_img2 = "436115.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 27, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 32,
                goods_id = 23,
                goods_sn = "ECS000023",
                goods_name = "Samsung 40 1080p LED",
                shop_price = 1549.99m,
                market_price = 1769.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "Samsung 40 1080p LED Smart Hub Smart TV (UN40J5200AFXZC) Model # = UN40J5200AFXZC Web Code = 10382339",
                goods_brief = "Samsung 40 1080p LED Smart Hub Smart TV (UN40J5200AFXZC)",
                ori_img = "10382339.jpg",
                ori_img1 = "10382339.jpg",
                ori_img2 = "10382339.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 29, 13, 09, 45)
            });

            _context.Goodses.Add(new Goods()
            {
                cat_id = 23,
                goods_id = 24,
                goods_sn = "ECS000024",
                goods_name = "HP 15.6 Touchscreen",
                shop_price = 699.99m,
                market_price = 869.60m,
                goods_quantity = 17,
                sold_quantity = 0,
                goods_weight = 1.2m,
                goods_desc = "HP 15.6 Touchscreen Laptop - Black (Intel Core i3-6100U/1024 GB HDD/8 GB RAM/Windows 10 Home)",
                goods_brief = "HP 15.6 Touchscreen Laptop",
                ori_img = "10483535.jpg",
                ori_img1 = "10483535.jpg",
                ori_img2 = "10483535.jpg",
                is_delete = true,
                is_free_post = true,
                last_update = new DateTime(2012, 11, 30, 13, 09, 45)
            });
            _context.SaveChanges();
        }

    

    }
}
