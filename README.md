# new_Karlshop2

4.13 update:

Here are four figures which are from this site:
![showcart](https://github.com/wokaerhenshen/new_Karlshop2/blob/master/wwwroot/images/showcart.png )
----------------
![welcome](https://github.com/wokaerhenshen/new_Karlshop2/blob/master/wwwroot/images/welcome.png)
----------------
![gallery](https://github.com/wokaerhenshen/new_Karlshop2/blob/master/wwwroot/images/gallery.png)
----------------
![gooddetail](https://github.com/wokaerhenshen/new_Karlshop2/blob/master/wwwroot/images/goodDetail.png)
----------------

4.13 update : Add profile Img feature, Redo the showCart Page
------------------------------------------------------
add change quantity inncart feature, which making chaning products very convernient, add profile img feature, change some db info, fix initialize, add two users when seeding, other small bug fixes

3.15 update : Add IOS project API feature
----------------

Add IOS API ,which can show cart , add/delete/minus products in Cart, and Add products to Cart, so all the opeartions are done in 
the real .NET project sqlite database, which can be done in the IOS application later.

3.14 update: Add VS team service feature
---------------
Now sellers can go to an alternative Angular site that have the same functional feature with the Angular built in this .NET project,
the site for this Angular site is https://shopangular.azurewebsites.net  
this site use VSTS to build, the advantage of this feature is that it's an DevOps feature, and people can build their projects 
on cloud and release it easily, people can see the actual changes of their apps as long as they sync their changes to the VSTS,
very awesome feature for teammembers to work together

3.13 update: Add scrapt feature to myshop
-----------
Sellers or Admins can input ASIN (which is the amazon product price identity number) when they add new products.
Karlshop can get the price of this product in Amazon and scrapt the price to my local site, and show both price 
to the customers, customers can press "Sync" which is a refreash icon to get the real-time price in Amazon as well.
（This is only for demo use, and I'm not sure whether it's illeagal to get the data, but I think it's public data and 
for test use so it may be ok）


3.12 update: Add goods API for the IOS project 
------------------------
3.12 update: Add Average Star(score) for each product based on user comments.
-----------



This site is built with ASP.NET Core 2.0 and it's a very hands on shopping cart project.
You need to have a paypal developer account to do the purchase, for your test purpose, I will provide a account information here:

Account:carolynho0422@gmail.com
----------------------------------------
password: Aa19940422~
-------------------------------------

You can always contact me at karlxu0410@gmail.com

This site have a Angular front end for the sellers(Admin role can also add products for sale),the .NET and Angular are connected with Bear
Token.

There are many APIs and javascripts included, and the database/ backend is well designed.


Features and How To use:

Karl Shop is designed for customers to purchase product easily, we provide the best products with the lowest price

Functionalities
There is a simple chat room using websocket in welcome page

Most saled products are calculated and displayed in welcome page

DropList is availabe in Index/Gallery page

Search/Sort is available in index page

Users:

Users can add products to cart

Users need to confirm personal contact information before checkout

Users need to pay with paypal

Users can view their cart/purchased items

Users can write review/rate to each purchased item

Users can send feedback to karl's email by attach information in Contact Page

users can crud there wishlist

Admins:

Admins have all the features that users have.

Admins have a drop down menu

Admins can create/edit/view category/goods/UserRoles

Admins can view customers/payments

Sellers:

The pages for sellers can built with Angular.

Sellers can CRUD their own products.

sellers can view the sold quantity of their products.

Sellers can 
