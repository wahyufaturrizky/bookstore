﻿using AutoMapper;
using BobsBookstore.DataAccess.Dtos;
using BobsBookstore.DataAccess.Repository.Interface;
using BobsBookstore.DataAccess.Repository.Interface.InventoryInterface;
using BobsBookstore.Models.Books;
using BookstoreBackend.ViewModel.ResaleBooks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Type = BobsBookstore.Models.Books.Type;

namespace BookstoreBackend.Controllers
{
    public class ResaleController : Controller
    {
        private readonly IGenericRepository<Resale> _resaleRepository;
        private readonly IGenericRepository<ResaleStatus> _resaleStatusRepository;
        private readonly IGenericRepository<Publisher> _publisherRepository;
        private readonly IGenericRepository<Genre> _genreRepository;
        private readonly IGenericRepository<Type> _typeRepository;
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Condition> _conditionRepository;
        private readonly IGenericRepository<Price> _priceRepository;
        private readonly IInventory _inventory;
        private IMapper _mapper;

        public ResaleController(IInventory inventory, IMapper mapper,IGenericRepository<Price> priceRepository, IGenericRepository<Condition> conditionRepository, IGenericRepository<Book> bookRepository, IGenericRepository<Type> typeRepository, IGenericRepository<Genre> genreRepository, IGenericRepository<Publisher> publisherRepository, IGenericRepository<ResaleStatus> resaleStatusRepository, IGenericRepository<Resale> resaleRepository)
        {

            _resaleRepository = resaleRepository;
            _resaleStatusRepository = resaleStatusRepository;
            _publisherRepository = publisherRepository;
            _genreRepository = genreRepository;
            _typeRepository = typeRepository;
            _bookRepository = bookRepository;
            _conditionRepository = conditionRepository;
            _priceRepository = priceRepository;
            _mapper = mapper;
            _inventory = inventory;
        }
        public IActionResult Index()
        {
            var resaleBooks = _resaleRepository.GetAll(includeProperties: "ResaleStatus");

            return View(resaleBooks);
        }

        public IActionResult ApproveResale(long id)
        {
           var resaleBook =  _resaleRepository.Get(c => c.Resale_Id == id, includeProperties: "ResaleStatus").FirstOrDefault();
            resaleBook.ResaleStatus = _resaleStatusRepository.Get(c => c.Status == Constants.ResaleStatusApproved).FirstOrDefault();
            _resaleRepository.Save();
            return RedirectToAction("Index");
        }

        public IActionResult RejectResale(long id)
        {
            var resaleBook = _resaleRepository.Get(c => c.Resale_Id == id, includeProperties: "ResaleStatus").FirstOrDefault();
            resaleBook.ResaleStatus = _resaleStatusRepository.Get(c => c.Status == Constants.ResaleStatusRejected).FirstOrDefault();
            _resaleRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddResaleBookDetails(long id)
        {
            var resaleBooks = _resaleRepository.Get(c => c.Resale_Id == id, includeProperties: "ResaleStatus,Customer").FirstOrDefault();
            ResaleViewModel resaleViewModel = _mapper.Map<ResaleViewModel>(resaleBooks);
            return View(resaleViewModel);
        }

        [HttpPost]
        public IActionResult AddResaleBookDetails(ResaleViewModel resaleViewModel)
        {
            resaleViewModel.ConditionName = resaleViewModel.ConditionName;
            BooksDto booksDto = _mapper.Map<BooksDto>(resaleViewModel);
            _inventory.AddToTables(booksDto);
            var resale = _resaleRepository.Get(c => c.Resale_Id == resaleViewModel.Resale_Id, includeProperties: "ResaleStatus").FirstOrDefault();
            var resaleStatus = _resaleStatusRepository.Get(c => c.Status == Constants.ResaleStatusReceived).FirstOrDefault();
            resale.ResaleStatus = resaleStatus;
            _resaleRepository.Update(resale);
            _resaleRepository.Save();
            return RedirectToAction("Index", new { resale = resale});
        }
        public IActionResult Details(long id)
        {
            var resaleBooks = _resaleRepository.Get(c => c.Resale_Id == id, includeProperties: "ResaleStatus,Customer").FirstOrDefault();
            ResaleViewModel resaleViewModel = _mapper.Map<ResaleViewModel>(resaleBooks);
            return View(resaleViewModel);
        }
        public IActionResult PaymentDone(long id)
        {
            var resaleBook = _resaleRepository.Get(c => c.Resale_Id == id, includeProperties: "ResaleStatus").FirstOrDefault();
            resaleBook.ResaleStatus = _resaleStatusRepository.Get(c => c.Status == Constants.ResaleStatusPaymentCompleted).FirstOrDefault();
            _resaleRepository.Update(resaleBook);
            _resaleRepository.Save();
            return RedirectToAction("Index");
        }

        }
}
