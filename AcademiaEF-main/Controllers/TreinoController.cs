using Academia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Academia.Controllers
{
    public class TreinoController : Controller
    {
        public Context context;
        public TreinoController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Treinos.Include(e=>e.Exercicios).Include(a=>a.Aluno).Include(a=>a.Aluno.Personal));
        }

        public IActionResult Details(int id)
        {
            var treino = context.Treinos
                .Include(f => f.Aluno)
                .Include(a=>a.Aluno.Personal)
                .FirstOrDefault(p => p.TreinoId == id);
            return View(treino);
        }


        public IActionResult Create()
        {
            //utiliza uma Viewbag para gerar uma lista com os nomes dos fabricantes
            ViewBag.PersonalId = new SelectList(context.Personais
                .OrderBy(f => f.Nome), "PersonalId", "Nome");
            ViewBag.AlunoId = new SelectList(context.Alunos
                .OrderBy(a => a.Nome), "AlunoId", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Treino treino)
        {
            context.Add(treino);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {
            var treino = context.Treinos.Find(id);
            ViewBag.PersonalId = new SelectList(context.Personais.OrderBy(f => f.Nome), "PersonalId", "Nome");
            ViewBag.AlunoId = new SelectList(context.Alunos
                .OrderBy(a => a.Nome), "AlunoId", "Nome");
            return View(treino);
        }

        [HttpPost]
        public IActionResult Edit(Treino treino)
        {
            //avisa a EF que o registro será modificado
            context.Entry(treino).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var aluno = context.Treinos
                .Include(f => f.Aluno)
                .Include(p=>p.Aluno.Personal)
                .FirstOrDefault(p => p.TreinoId == id);
            return View(aluno);
        }

        [HttpPost]
        public IActionResult Delete(Treino treino)
        {
            context.Treinos.Remove(treino);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
