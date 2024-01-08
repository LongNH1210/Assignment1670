﻿using BookShoppingCart.Models;
using BookShoppingCart.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookShoppingCart.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IHomeResponsitory _homeResponsitory;

        public HomeController(ILogger<HomeController> logger,IHomeResponsitory homeResponsitory)
		{
			_homeResponsitory = homeResponsitory;
			_logger = logger;
		}

		public async Task<IActionResult> Index(string sterm="", int genreId=0)
		{
			
			IEnumerable<Book> books = await _homeResponsitory.GetBooks(sterm,genreId);
			IEnumerable<Genre> genres = await _homeResponsitory.Genres();
			BookDisplayModel bookModel = new BookDisplayModel
			{
				Books = books,
				Genres = genres,
				Sterm = sterm,
				GenreId = genreId

			};
            return View(bookModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}