﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BobBookstore.Data;
using BobBookstore.Models.Carts;
using BobBookstore.Models.Book;
using BobBookstore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using BobBookstore.Models.Order;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Amazon.AspNetCore.Identity.Cognito;

namespace BobBookstore.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly UsedBooksContext _context;
        private readonly SignInManager<CognitoUser> _SignInManager;
        private readonly UserManager<CognitoUser> _userManager;
        public CartItemsController(UsedBooksContext context, SignInManager<CognitoUser> SignInManager, UserManager<CognitoUser> userManager)
        {
            _context = context;
            _SignInManager = SignInManager;
            _userManager = userManager;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            
            var id = Convert.ToInt32(HttpContext.Request.Cookies["CartId"]);
            var cart = _context.Cart.Find(id);
            var cartItem = from c in _context.CartItem
                           where c.Cart==cart&&c.WantToBuy==true
                       select new CartViewModel()
                       {
                           BookId=c.Book.Book_Id,
                           Url=c.Book.Back_Url,
                           Prices=c.Price.ItemPrice,
                           BookName=c.Book.Name,
                           CartItem_Id=c.CartItem_Id,
                           quantity=c.Price.Quantity,
                           PriceId=c.Price.Price_Id,

                       };
            
            return View(await cartItem.ToListAsync());
            //return View(Tuple.Create(item1,book));
            
        }
        public async Task<IActionResult> WishListIndex()
        {
            var id = Convert.ToInt32(HttpContext.Request.Cookies["CartId"]);
            var cart = _context.Cart.Find(id);
            var cartItem = from c in _context.CartItem
                           where c.Cart == cart && c.WantToBuy==false
                           select new CartViewModel()
                           {
                               BookId = c.Book.Book_Id,
                               Url = c.Book.Back_Url,
                               Prices = c.Price.ItemPrice,
                               BookName = c.Book.Name,
                               CartItem_Id = c.CartItem_Id,
                               quantity = c.Price.Quantity,
                               PriceId = c.Price.Price_Id

                           };

            return View(await cartItem.ToListAsync());
            //return View(Tuple.Create(item1,book));

        }
        public async Task<IActionResult> MoveToCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            cartItem.WantToBuy = true;
            _context.Update(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("WishListIndex");
        }
        [HttpPost]
        public async Task<IActionResult> AllMoveToCart(string[] fruits)
        {
            string a = "1,";
            foreach (var ids in fruits)
            {
                var id = Convert.ToInt32(ids);
                if (id == null)
                {
                    return NotFound();
                }

                var cartItem = await _context.CartItem.FindAsync(id);
                if (cartItem == null)
                {
                    return NotFound();
                }
                cartItem.WantToBuy = true;
                _context.Update(cartItem);
            }



            await _context.SaveChangesAsync();

            return RedirectToAction("WishListIndex");
        }
        

        public async Task<IActionResult> AddtoCartitem(long bookid,long priceid)
        {
            var book = _context.Book.Find(bookid);
            var price = _context.Price.Find(priceid);
            var cartId = HttpContext.Request.Cookies["CartId"];
            var cart = _context.Cart.Find(Convert.ToInt32(cartId));

            var cartItem = new CartItem() { Book = book, Price = price, Cart = cart,WantToBuy=true };

            _context.Add(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddtoWishlist(long bookid, long priceid)
        {
            var book = _context.Book.Find(bookid);
            var price = _context.Price.Find(priceid);
            var cartId = HttpContext.Request.Cookies["CartId"];
            var cart = _context.Cart.Find(Convert.ToInt32(cartId));

            var cartItem = new CartItem() { Book = book, Price = price, Cart = cart, WantToBuy = true };

            _context.Add(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CheckOut(string[] fruits,string[] IDs,string[] quantity,string[] bookF,string[]priceF)
        {

            //if (!_SignInManager.IsSignedIn(User))
            //{
            //    //Response.Write(" <script>function window.onload() {alert( ' 弹出的消息' ); } </script> ");
            //    await Response.WriteAsync(" <script>function window.onload() {alert( ' 弹出的消息' ); } </script> ");
            //}
            long statueId = 1;
            var orderStatue = _context.OrderStatus.Find(statueId);
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Attributes[CognitoAttribute.Sub.AttributeName];
            var customer = _context.Customer.Find(userId);
            double subTotal = 0.0;
            var itemIdList = new List<CartItem>();
            for (int i = 0; i < IDs.Length; i++)
            {
                var cartItem = from c in _context.CartItem
                               where c.CartItem_Id == Convert.ToInt32(IDs[i])
                               select c;
                              
                var item = new CartItem();
                foreach (var ii in cartItem)
                {
                    item = ii;
                }
                //var item = _context.CartItem.Find(Convert.ToInt32(IDs[i]));
                itemIdList.Add(item);
                subTotal += Convert.ToDouble( fruits[i]) * Convert.ToInt32(quantity[i]);
            }

            //var s1 = "";
            //var s2 = "";
            //for (int i = 0; i < IDs.Length; i++)
            //{
            //    var item = _context.CartItem.Find(Convert.ToInt32(IDs[i]));

            //    s1 += itemIdList[i].BookN;
            //    s1 += "A";
            //    s2 += quantity[i];
            //    s2 += "A";
            //}
            //if (!HttpContext.Request.Cookies.ContainsKey("SS1"))
            //{
            //    CookieOptions options = new CookieOptions();

            //    HttpContext.Response.Cookies.Append("SS1", s1);


            //}
            //else
            //{
            //    HttpContext.Response.Cookies.Delete("SS1");
            //    HttpContext.Response.Cookies.Append("SS1", s1);
            //}
            //if (!HttpContext.Request.Cookies.ContainsKey("SS2"))
            //{
            //    CookieOptions options = new CookieOptions();

            //    HttpContext.Response.Cookies.Append("SS2", s2);


            //}
            //else
            //{
            //    HttpContext.Response.Cookies.Delete("SS2");
            //    HttpContext.Response.Cookies.Append("SS2", s2);
            //}


            //return RedirectToAction(nameof(Index));
            var recentOrder = new Order() { OrderStatus = orderStatue, Subtotal = subTotal, Tax = subTotal * 0.1, Customer = customer };
            _context.Add(recentOrder);
            _context.SaveChanges();
            var orderId = recentOrder.Order_Id;
            for (int i = 0; i < itemIdList.Count; i++)
            {
                var orderDetailBook = itemIdList[i].Book;
                var orderDetailPrice = itemIdList[i].Price;

                var orderDetail = new OrderDetail() { Book = orderDetailBook, Price = orderDetailPrice, price = Convert.ToDouble(fruits[i]), quantity = Convert.ToInt32(quantity[i]), Order = recentOrder, IsRemoved = false };
                _context.Add(orderDetail);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(ConfirmCheckout));
            //return View();
        }
        public async Task<IActionResult> ConfirmCheckout()
        {
            return View();
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem
                .FirstOrDefaultAsync(m => m.CartItem_Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartItem_Id")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartItem_Id")] CartItem cartItem)
        {
            if (id != cartItem.CartItem_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.CartItem_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem
                .FirstOrDefaultAsync(m => m.CartItem_Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItem.FindAsync(id);
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItem.Any(e => e.CartItem_Id == id);
        }
    }
}
