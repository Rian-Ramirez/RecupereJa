using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecupereJa.Repository;
using RecupereJa.Services;
﻿using Microsoft.AspNetCore.Mvc;
using RecupereJa.Data;
using RecupereJa.ViewModel;
using System.Linq;

namespace RecupereJa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itens;
        private readonly IItemRepositorio _itemRepositorio;

        public HomeController(ILogger<HomeController> logger, IItemService itens, IItemRepositorio itemRepositorio)
        {
            _logger = logger;
            _itens = itens;
            _itemRepositorio = itemRepositorio;

        private readonly RecupereJaContext _context;

        public HomeController(RecupereJaContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {

            // Busca apenas os aprovados
            var recentes = await _itemRepositorio.BuscarItemParaHomeAsync();

            // Monta ViewModel
            var viewModel = recentes.Select(i => new ItemViewModel
            {
                Id = i.Id,
                Titulo = i.Titulo,
                DataEncontrado = i.DataEncontrado?.ToString("dd/MM/yyyy HH:mm") ?? string.Empty,
            }).ToList();

            return View(viewModel);
        }

        //[Authorize(Roles = "Gerente")]
        //public ActionResult Relatorio()
        //{
        //    return View();
        //}

            var itens = _context.Items
                .Select(i => new ItemViewModel
                {
                    Id = i.Id,
                    Titulo = i.Titulo,
                    Descricao = i.Descricao,
                    Status = i.Status,
                    DataEncontrado = i.DataEncontrado   // ✅ DateTime? sem ToString()
                })
                .ToList();

            return View(itens);
        }
    }
}