using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Contexto;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    public class ManutencaosController : Controller
    {
        private readonly DBContext _context;

        public ManutencaosController(DBContext context)
        {
            _context = context;
        }

        // GET: Manutencaos
        public ActionResult Index(string sortOrder)
        {
            ViewBag.AgendaParm = sortOrder == "Data" ? "Data_desc" : "Data";
            var manutencao = from a in _context.Manutencaos select a;

            switch (sortOrder)
            {
                case "Data":
                    manutencao = manutencao.OrderBy(s => s.Agenda);
                    break;
                case "Data_desc":
                    manutencao = manutencao.OrderByDescending(s => s.Agenda);
                    break;
            }
            return View(manutencao.ToList());
        }

        // GET: Manutencaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manutencao = await _context.Manutencaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manutencao == null)
            {
                return NotFound();
            }

            return View(manutencao);
        }

        // GET: Manutencaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manutencaos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuantidadeKm,Registro,Agenda,PlacaVeiculo")] Manutencao manutencao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(manutencao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(manutencao);
            }
            catch
            {
                return NotFound("ERROR 001: A Placa informada já está cadastrada em outro veículo.    CASO O ERROR PERSISTA ENTRE EM CONTATO CONOSCO");
            }
        }

        // GET: Manutencaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manutencao = await _context.Manutencaos.FindAsync(id);
            if (manutencao == null)
            {
                return NotFound();
            }
            return View(manutencao);
        }

        // POST: Manutencaos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuantidadeKm,Registro,Agenda,PlacaVeiculo")] Manutencao manutencao)
        {
            if (id != manutencao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manutencao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManutencaoExists(manutencao.Id))
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
            return View(manutencao);
        }

        // GET: Manutencaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manutencao = await _context.Manutencaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manutencao == null)
            {
                return NotFound();
            }

            return View(manutencao);
        }

        // POST: Manutencaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manutencao = await _context.Manutencaos.FindAsync(id);
            _context.Manutencaos.Remove(manutencao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManutencaoExists(int id)
        {
            return _context.Manutencaos.Any(e => e.Id == id);
        }
        

    }
}
