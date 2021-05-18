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
    public class VeiculoController : Controller
    {
        private readonly DBContext _context;

        public VeiculoController(DBContext context)
        {
            _context = context;
        }

        // GET: Veiculo
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "Nome_desc" : "";
            ViewBag.PlacaParm = sortOrder == "Placa" ? "Placa_desc" : "Placa";

            var veiculos = from m in _context.Veiculos
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                veiculos = veiculos.Where(m => m.Nome.ToUpper().Contains(searchString.ToUpper()) || m.Placa.ToUpper().Contains(searchString.ToUpper()));
            }


            switch (sortOrder)
            {
                case "Nome_desc":
                    veiculos = veiculos.OrderByDescending(s => s.Nome);
                    break;
                case "Placa":
                    veiculos = veiculos.OrderBy(s => s.Placa);
                    break;
                case "Placa_desc":
                    veiculos = veiculos.OrderByDescending(s => s.Placa);
                    break;
                case "Nome":
                    veiculos = veiculos.OrderBy(s => s.Nome);
                    break;
            }

            return View(veiculos.ToList());
        }

        // GET: Veiculo/Create
        public IActionResult Create()
        {
            return View();
        }

    // POST: Veiculo/Create
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Marca,Modelo,Ano,Cor,Placa")] Veiculo veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(veiculo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(veiculo);
            }
            catch
            {
                return NotFound("ERROR: A Placa informada já está cadastrada em outro veículo.    CASO O ERROR PERSISTA ENTRE EM CONTATO CONOSCO");
            }
        }

        // GET: Veiculo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Marca,Modelo,Ano,Cor,Placa")] Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.Id))
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
            return View(veiculo);
        }

        // GET: Veiculo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(int id)
        {
            return _context.Veiculos.Any(e => e.Id == id);
        }
    }
}
