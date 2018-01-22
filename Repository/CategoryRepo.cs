using new_Karlshop.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Repository
{
    public class CategoryRepo
    {
        ApplicationDbContext _context;
        const int MAX_CATEGORY = 10;

        public CategoryRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetOneCategoryByID(int id)
        {
            return _context.Categories.Where(ca => ca.cat_id == id).FirstOrDefault();
        }


            public  int GetCategoryMaxID(int parent_id)
            {
                var id = 0;

                foreach (var item in _context.Categories)
                {
                    // same parent_id
                    if ((item.parent_id == parent_id) && (item.cat_id > id))
                        id = item.cat_id;
                }

                if (id == 0)
                {
                    // First One this Category
                    id = parent_id * MAX_CATEGORY;
                }
                return id;
            }
        

        //we can get the menu list for the main page by using this method.
        public  List<BtnMenuItem> GetMenuList()
        {
            List<BtnMenuItem> menus = new List<BtnMenuItem>();

            foreach (var catItem in _context.Categories.ToList())
            {
                BtnMenuItem menuItem = new BtnMenuItem();

                if (catItem.parent_id == 0)   // Every parent_id == 0 item, generate a dropdown menu.
                {
                    // First Root menu
                    MenuItem pointMenu_Root = new MenuItem();

                    pointMenu_Root.menuPath = catItem.cat_id;
                    pointMenu_Root.menuName = catItem.cat_name;

                    menuItem.mainMenu = pointMenu_Root;

                    List<MenuItem> subMenu = new List<MenuItem>();

                    foreach (var subCatItem in _context.Categories.ToList()) //this is where the dropdown list generate and how it could.
                    {
                        // Drop Menu
                        MenuItem pointMenu = new MenuItem();

                        if (subCatItem.parent_id == catItem.cat_id)
                        {
                            pointMenu.menuName = subCatItem.cat_name;
                            pointMenu.menuPath = subCatItem.cat_id;

                            subMenu.Add(pointMenu);
                        }
                    }

                    menuItem.MenuItem = subMenu;

                    menus.Add(menuItem);
                }
            }

            return menus;
        }


        // For [Create Category] drop down select list option
        // we can get the select menu list for category by using this method.
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetPCatSelectList()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Root Category", Value = "0" });

            foreach (var catItem in _context.Categories.ToList())
            {
                if (catItem.parent_id == 0)   // Every parent_id == 0 的项，产生一个 Drop Menu 项-- 根节点 + 几个子节点 
                {
                    items.Add(new SelectListItem { Text = catItem.cat_name, Value = catItem.cat_id.ToString() });
                }
            }
            return items;
        }

        // For [Create Goods] Category drop down select list option
        // we can get the select menu list for goods by using this method.
        public  List<SelectListItem> GetSubCatSelectList()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var catItem in _context.Categories.ToList())
            {
                if (catItem.parent_id != 0)   // Every parent_id == 0 的项，产生一个 Drop Menu 项-- 根节点 + 几个子节点 
                {
                    items.Add(new SelectListItem { Text = GetCatNameByCatID(catItem.parent_id) + " " + catItem.cat_name, Value = catItem.cat_id.ToString() });
                }

            }
            return items;
        }

        public void AddOneCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        // For Create Goods Category drop down select list option
        // we can get the category name by using category by this method.
        public string GetCatNameByCatID(int id)
        {
            foreach (var catItem in _context.Categories.ToList())
            {
                if (catItem.cat_id == id) 
                {
                    return catItem.cat_name;
                }
            }
            return null;
        }

        public void EditOneCategory(Category category)
        {
            Category result = _context.Categories.Where(ca => ca.cat_id == category.cat_id).FirstOrDefault();
            result = category;

            _context.SaveChanges();
        }

        // delete one category
        public void DelOneCategoryByID(int id)
        {
            Category category = _context.Categories.Where(c => c.cat_id == id).FirstOrDefault();
            _context.Categories.Remove(category);
            _context.SaveChanges();

        }


    }
}
