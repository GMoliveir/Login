using Academia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Academia.Controllers
{
    public class AlunoController : Controller
    {
        public Context context;
        public AlunoController(Context ctx) 
        {
            context = ctx;   
        }
        public IActionResult Index()
        {
            ViewBag.AlunoId = new SelectList(context.Alunos
                .OrderBy(f => f.Nome), "AlunoId", "Nome");
            return View(context.Alunos.OrderBy(a => a.Nome).Include(p=>p.Personal));
        }

        public IActionResult Details(int id)
        {
            var aluno = context.Alunos
                .Include(f => f.Personal)
                .FirstOrDefault(p => p.AlunoId == id);
            return View(aluno);
        }


        public IActionResult Create()
        {
            //utiliza uma Viewbag para gerar uma lista com os nomes dos fabricantes
            ViewBag.PersonalId = new SelectList(context.Personais
                .OrderBy(f => f.Nome), "PersonalId", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            context.Add(aluno);
            context.SaveChanges();
            TempData["SuccessMessage"] = $"Aluno {aluno.Nome} adicionado com sucesso!";
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {
            var aluno = context.Alunos.Find(id);
            ViewBag.PersonalId = new SelectList(context.Personais.OrderBy(f => f.Nome), "PersonalId", "Nome");
            return View(aluno);
        }

        [HttpPost]
        public IActionResult Edit(Aluno aluno)
        {
            //avisa a EF que o registro será modificado
            TempData["SuccessMessage"] = $"Aluno {aluno.Nome} editado com sucesso!";
            context.Entry(aluno).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var aluno = context.Alunos
                .Include(f => f.Personal)
                .FirstOrDefault(p => p.AlunoId == id);
            return View(aluno);
        }

        [HttpPost]
        public IActionResult Delete(Aluno aluno)
        {
            TempData["SuccessMessage"] = $"Aluno {aluno.Nome} deletado com sucesso!";
            context.Alunos.Remove(aluno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
