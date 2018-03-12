using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_Karlshop.Data;

namespace new_Karlshop.Repository
{
    public class GoodsRepo
    {
        ApplicationDbContext _context;
        public GoodsRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Goods> getAll()
        {
            return _context.Goodses;
        }

        // we can use this method to get one good by its id.
        public Goods GetOneGoods(int id)
        {
            return _context.Goodses.Where(g => g.goods_id == id).FirstOrDefault();
        }

        public Goods GetOneGoods(IEnumerable<Goods> goods, int id)
        {
            goods = goods.Where(
                        s => s.goods_id == id);

            return goods.First();
        }

        public int GetGoodsMaxID()
        {
            return _context.Goodses.Select(id => id.goods_id).Max();
        }

        public void GetGoodRating(int id)
        {
            IEnumerable<double> comments = from cm in _context.Comments
                                             where (cm.AccountGood.Goods_ID == id)
                                             select (cm.rate_star);
            if (comments.Count() == 0)
            {
                _context.Goodses.Where(g => g.goods_id == id).FirstOrDefault().star_rate = 0;
            }
            else
            {
                _context.Goodses.Where(g => g.goods_id == id).FirstOrDefault().star_rate = comments.Average();
            }

        }


        //This occurs in the very first test of passing all images to the font end by using viewbag
        //public List<string> GetAllImagePath_Phone()
        //{
        //    List<string> phone_img_paths = _context.Goodses.Where(id => id.Category.parent_id == 1).Select(img => img.ori_img).ToList();
        //    return phone_img_paths;
        //}

        public List<Goods> GetAllData_Phone()
        {
            List<Goods> phone_all_data = _context.Goodses.Where(id => id.Category.parent_id == 1).Select(all=> all).ToList();
            return phone_all_data;
        }

        public List<Goods> GetAllData_Laptop()
        {
            List<Goods> Laptop_all_data = _context.Goodses.Where(id => id.Category.parent_id == 2).Select(all => all).ToList();
            return Laptop_all_data;
        }

        public List<Goods> GetAllData_Tv()
        {
            List<Goods> Tv_all_data = _context.Goodses.Where(id => id.Category.parent_id == 3).Select(all => all).ToList();
            return Tv_all_data;
        }

        public List<Goods> GetPopularItem()
        {
            List<Goods> all_goods = _context.Goodses.Select(all => all).ToList();
            List<Goods> popular_goods = all_goods.OrderByDescending(sq => sq.sold_quantity).Take(5).ToList();
    
            return popular_goods;
        }



        public void AddOneGoods(Goods goods)
        {
            _context.Goodses.Add(goods);
            _context.SaveChanges();
        }

        public void EditOneGoods(Goods goods)
        {
            Goods result = _context.Goodses.Where(g => g.goods_id == goods.goods_id).FirstOrDefault();
           // result.goods_quantity = goods.goods_quantity;

            //can i have a better way?

            result.goods_name = goods.goods_name;
            result.shop_price = goods.shop_price;
            result.market_price = goods.market_price;
            result.goods_quantity = goods.goods_quantity;
            result.goods_desc = goods.goods_desc;
            result.ori_img = goods.ori_img;
            result.last_update = goods.last_update;
            _context.SaveChanges();
        }

        // we can use this method to delete one good by its id.
        public void DeletOneGoods(int id)
        {
            Goods good = _context.Goodses.Where(g => g.goods_id == id).FirstOrDefault();
            _context.Goodses.Remove(good);
            _context.SaveChanges();
        }

        public int GenerateCommentId()
        {
            if (_context.Comments.Count() == 0)
            {
                return 1;
            }
            else
            {
                return _context.Comments.Select(c => c.ID).Max() + 1;
            }

        }

        public void CreateOneComment(string AccountID, int GoodID, int OrderID, string comment, DateTime date,double star)
        {
            Comments comments = new Comments
            {
                ID = GenerateCommentId(),
                AccountGood = _context.AccountGoods.Where(a => a.Account_ID == AccountID && a.Goods_ID == GoodID && a.Order_ID == OrderID).FirstOrDefault(),
                content = comment,
                create_time = date,
                rate_star = star
            };
            _context.Comments.Add(comments);
            _context.SaveChanges();
        }
        
        public IEnumerable<Comments> GetCommentsByGoodID(int id)
        {
            
            return _context.Comments.Where(i => i.AccountGood.Goods_ID == id);
        }

        public int GetMaxID()
        {
            int maxID = _context.Goodses.Select(g => g.goods_id).DefaultIfEmpty(0).Max();
            return maxID;
        }
        // we can get the next goods in the same category.
        public Goods GetNextGoods(int id)
        {
            if (id < GetMaxID())
            {
                return _context.Goodses.Where(g => g.goods_id == (id + 1)).FirstOrDefault();
            }
            else
            {
                return _context.Goodses.Where(g => g.goods_id == 1).FirstOrDefault();
            }
        }

        // we can get the previous goods in the same category.
        public  Goods GetLastGoods(int id)
        {
            if (id > 1)
            {
                return _context.Goodses.Where(g => g.goods_id == (id - 1)).FirstOrDefault();
            }
            else
            {
                return _context.Goodses.Where(g => g.goods_id == GetMaxID()).FirstOrDefault();
            }
        }

        // in this case we want to choose goods that are in  the same category, so we are going to get a list of goods, so we 
        // create a empty list of goods first and then add goods(meet our requirements) to this list and get this list finally.
        public List<Goods> GetGoodsByCatID(int id)
        {
            return _context.Goodses.Where(g => g.cat_id == id).ToList();
        }

        // in this method we have the goods list as parameter so we can use a query to do the selection.
        public static IEnumerable<Goods> GetGoodsByCatID(IEnumerable<Goods> goods, int id)
        {
            goods = goods.Where(
                        s => s.cat_id == id);

            return goods;
        }

        public string GetAsinFromId(int id)
        {
            return _context.Goodses.Where(i => i.goods_id == id).Select(ai => ai.asin).FirstOrDefault();
        }


        // use a contains to do the filter of the goods. very likely to the get goods by id one.

        public List<Goods> SearchGoodsByStr(string searchStr)
        {
            return _context.Goodses.Where(g => g.goods_name.Contains(searchStr)).ToList();   
        }

        public const string NAME = "Name";
        public const string NAME_DESC = "Name_desc";
        public const string ID = "Id";
        public const string ID_DESC = "Id_desc";
        public const string DATE = "Date";
        public const string DATE_DESC = "Date_desc";


        //in this case we sort goods by specific property and it use the feature of OrderByDescending and OrderBy
        public static IEnumerable<Goods> SortGoods(IEnumerable<Goods> goods, string sortOrder)
        {

            switch (sortOrder)
            {
                case ID:
                    goods = goods.OrderByDescending(s => s.goods_id);
                    break;
                case ID_DESC:
                    goods = goods.OrderBy(s => s.goods_id);
                    break;
                case NAME_DESC:
                    goods = goods.OrderByDescending(s => s.goods_name);
                    break;
                case NAME:
                    goods = goods.OrderBy(s => s.goods_name);
                    break;
                case DATE:
                    goods = goods.OrderBy(s => s.last_update);
                    break;
                case DATE_DESC:
                    goods = goods.OrderByDescending(s => s.last_update);
                    break;
                default:
                    goods = goods.OrderBy(s => s.goods_id);
                    break;
            }
            return goods;
        }

        //In this case we get the sorterd goods, this time is the whole goods, but not the selected one because this time we
        //dont have a paramter of goods.
        public  IEnumerable<Goods> GetGoods(string sortOrder)
        {
         
            IEnumerable<Goods> goods = SortGoods(_context.Goodses, sortOrder);
            return goods;
        }

        // this is likely as the search goods one ,but this method has goods as parameter so it can filter the 
        // goods with the filter.
        public static IEnumerable<Goods> FilterGoods(IEnumerable<Goods> goods, string searchString)
        {
            // Filter results based on search.
            if (!String.IsNullOrEmpty(searchString))
                goods = goods.Where(
                            s => s.goods_name.ToUpper().Contains(searchString.ToUpper())
                              || s.goods_desc.ToUpper().Contains(searchString.ToUpper()));
            return goods;
        }

        // this is the override of the GetGoods method and not only have the sortorder, we can also have the searchstring
        // to help us do the filter opeartion.
        public IEnumerable<Goods> GetGoods(string searchString, string sortOrder)
        {

            IEnumerable<Goods> goods = _context.Goodses;

            goods = FilterGoods(goods, searchString);
            goods = SortGoods(goods, sortOrder);

            return goods;
        }

        public IEnumerable<Goods> GetSellingList(string email)
        {
            IEnumerable<Goods> goods = _context.Goodses.Where(se => se.seller == email).Select(All=>All);
            return goods;
        }
    }
}
