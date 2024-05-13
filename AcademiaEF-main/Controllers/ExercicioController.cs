using Academia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Academia.Controllers
{
    public class ExercicioController : Controller
    {
        public Context context;
        public ExercicioController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Exercicios);
        }

        public IActionResult Details(int id)
        {
            var exercicio = context.Exercicios
                .FirstOrDefault(p => p.ExercicioId == id);
            return View(exercicio);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Exercicio exercicio)
        {
            context.Add(exercicio);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {
            var exercicio = context.Exercicios.Find(id);
            return View(exercicio);
        }

        [HttpPost]
        public IActionResult Edit(Exercicio exercicio)
        {
            //avisa a EF que o registro será modificado
            context.Entry(exercicio).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var aluno = context.Exercicios
                .FirstOrDefault(p => p.ExercicioId == id);
            return View(aluno);
        }

        [HttpPost]
        public IActionResult Delete(Exercicio exercicio)
        {
            context.Exercicios.Remove(exercicio);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
