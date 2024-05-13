using Academia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Academia.Controllers
{
    public class PersonalController : Controller
    {
        public Context context;
        public PersonalController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            ViewBag.PersonalId = new SelectList(context.Personais
                .OrderBy(f => f.Nome), "PersonalId", "Nome");
            return View(context.Personais
                .OrderBy(p => p.Nome)
                .Include(a=>a.Alunos));
        }

        public IActionResult Details(int id)
        {
            var personal = context.Personais
                .Include(f => f.Alunos)
                .FirstOrDefault(p => p.PersonalId == id);
            return View(personal);
        }


        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Personal personal)
        {
            context.Add(personal);
            context.SaveChanges();
            TempData["SuccessMessage"] = $"Personal {personal.Nome} adicionado com sucesso!";
            return RedirectToAction("Index");
        }




        public IActionResult Edit(int id)
        {
            var personal = context.Personais.Find(id);
            
            return View(personal);
        }

        [HttpPost]
        public IActionResult Edit(Personal personal)
        {
            //avisa a EF que o registro será modificado
            TempData["SuccessMessage"] = $"Personal {personal.Nome} editado com sucesso!";
            context.Entry(personal).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var personal = context.Personais
                .Include(f => f.Alunos)
                .FirstOrDefault(p => p.PersonalId == id);
            return View(personal);
        }

        [HttpPost]
        public IActionResult Delete(Personal personal)
        {
            TempData["SuccessMessage"] = $"Personal {personal.Nome} deletado com sucesso!";
            context.Personais.Remove(personal);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
