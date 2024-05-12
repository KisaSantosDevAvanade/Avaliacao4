using Microsoft.AspNetCore.Mvc;
using TarefasApplication.Models;
using System.Reflection.Metadata.Ecma335;


namespace TarefasApplication.Controllers
{
    public class TarefaController : Controller
    {


        private static List<Tarefa> _tarefa = new List<Tarefa>
        {
            new Tarefa { TarefaId = 1, TarefaNome = "Tarefa 1", StatusTarefa = "Pendente", InicioTarefa = DateTime.Now },
            new Tarefa { TarefaId = 2, TarefaNome = "Tarefa 2", StatusTarefa = "Pendente", InicioTarefa = DateTime.Now },
            new Tarefa { TarefaId = 3, TarefaNome = "Tarefa 3", StatusTarefa = "Concluida", InicioTarefa = DateTime.Now, ConclusaoTarefa = DateTime.Now },
            new Tarefa { TarefaId = 3, TarefaNome = "Tarefa 4", StatusTarefa = "Concluida", InicioTarefa = DateTime.Now, ConclusaoTarefa = DateTime.Now }

        };


        public IActionResult Index()
        {
            return View(_tarefa);
        }

        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.TarefaId = _tarefa.Count > 0 ? _tarefa.Max(t => t.TarefaId) + 1 : 1;

                _tarefa.Add(tarefa);
            }
            return RedirectToAction("Index");
            
        }

        public IActionResult Details(int id)
        {
            var tarefas = _tarefa.FirstOrDefault(t => t.TarefaId == id);
            if (tarefas == null)
            {
                return NotFound();
            }
            return View(tarefas);
        }

        public IActionResult Delete(int id)
        {
            var tarefa = _tarefa.FirstOrDefault(t => t.TarefaId == id);
            if (tarefa == null) {
                return NotFound();
            }

            _tarefa.Remove(tarefa);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var tarefa = _tarefa.FirstOrDefault(t => t.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }


        [HttpPost]
        public IActionResult Edit(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                var existingTarefa = _tarefa.FirstOrDefault(t => t.TarefaId == tarefa.TarefaId);
                if (existingTarefa != null) 
                {
                    existingTarefa.TarefaNome = tarefa.TarefaNome;
                    existingTarefa.StatusTarefa = tarefa.StatusTarefa;
                    existingTarefa.InicioTarefa = tarefa.InicioTarefa;
                    existingTarefa.ConclusaoTarefa = tarefa.ConclusaoTarefa;
                    if (tarefa.StatusTarefa.Equals("Concluida", StringComparison.CurrentCultureIgnoreCase))
                    {
                        existingTarefa.ConclusaoTarefa = DateTime.Now;
                    }
                    else
                    {
                        existingTarefa.ConclusaoTarefa = null;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(tarefa);
        }

        public IActionResult Concluidas()
        {
            var tarefaConcluida = _tarefa.Where(t => t.StatusTarefa == "Concluida").ToList();
            return View(tarefaConcluida);
        }

        public IActionResult Pendente()
        {
            var tarefaPendente = _tarefa.Where(t => t.StatusTarefa == "Pendente").ToList();
            return View(tarefaPendente);
        }
    }
}
