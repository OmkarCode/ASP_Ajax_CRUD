using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jQuery_Ajax_CRUD_ASP.NET_Core_MVC.Data;
using jQuery_Ajax_CRUD_ASP.NET_Core_MVC.Models;
using static jQuery_Ajax_CRUD_ASP.NET_Core_MVC.Helper;

namespace jQuery_Ajax_CRUD_ASP.NET_Core_MVC.Controllers
{
    public class TransactionModelsController : Controller
    {
        private readonly TransactionDbContext _context;

        public TransactionModelsController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: TransactionModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        // GET: TransactionModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        //// GET: Transaction/Edit(Insert) or GET: Transaction/Edit/5 (Update)
        //public async Task<IActionResult> Edit(int id = 0)
        //{
        //    if (id == 0)
        //    {
        //        // If id is 0, create a new model (for adding)
        //        return View(new TransactionModel()); // Initialize with an empty model for creating
        //    }
        //    else
        //    {
        //        // Retrieve existing model for editing
        //        var transactionModel = await _context.Transactions.FindAsync(id);
        //        if (transactionModel == null)
        //        {
        //            return NotFound(); // If the model is not found, return NotFound
        //        }
        //        return View(transactionModel); // Return the found model for editing
        //    }
        //}


        //// GET: TransactionModels/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TransactionModels/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TransactionModel transactionModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(transactionModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(transactionModel);
        //}

        //// GET: TransactionModels/Edit/5
        ////public async Task<IActionResult> Edit(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    var transactionModel = await _context.Transactions.FindAsync(id);
        ////    if (transactionModel == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return View(transactionModel);
        ////}

        //// POST: TransactionModels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TransactionModel transactionModel)
        //{
        //    if (id != transactionModel.TransactionId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(transactionModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TransactionModelExists(transactionModel.TransactionId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(transactionModel);
        //}

        // GET: TransactionModels/AddOrEdit(Insert)
        // GET: TransactionModels/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new TransactionModel());
            else
            {
                var transactionModel = await _context.Transactions.FindAsync(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    transactionModel.Date = DateTime.Now;
                    _context.Add(transactionModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(transactionModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionModelExists(transactionModel.TransactionId))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
        }

        // GET: TransactionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: TransactionModels/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _context.Transactions.FindAsync(id);
            if (transactionModel != null)
            {
                _context.Transactions.Remove(transactionModel);
                await _context.SaveChangesAsync();
            }

            // Return the updated transaction list as HTML
            var transactions = await _context.Transactions.ToListAsync();
            var html = Helper.RenderRazorViewToString(this, "_ViewAll", transactions);
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });

        }

        private bool TransactionModelExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
