using Microsoft.AspNetCore.Mvc;
using TarefasApplication.Models;
using System.Reflection.Metadata.Ecma335;


namespace TarefasApplication.Controllers
{
    public class TarefaController : Controller
    {
        private List<Tarefa> _tarefa = new List<Tarefa>()
        {

        };

        private List<Tarefa> tarefaConcluida = new List<Tarefa>();

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

        public IActionResult Delete(int id)
        {
            var tarefa = _tarefa.FirstOrDefault(t => t.TarefaId == id);
            if (tarefa != null) {
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
                if (existingTarefa == null) 
                {
                    existingTarefa.TarefaNome = tarefa.TarefaNome;
                    existingTarefa.StatusTarefa = tarefa.StatusTarefa;
                    existingTarefa.InicioTarefa = tarefa.InicioTarefa;
                    existingTarefa.ConclusaoTarefa = tarefa.ConclusaoTarefa;
                }
                return RedirectToAction("Index");
            }
            return View(tarefa);
        }

        public IActionResult Concluidas()
        {
            foreach (var tarefa in _tarefa)
            {
                if (tarefa.StatusTarefa == true)
                {
                    tarefaConcluida.Add(tarefa);
                    return RedirectToAction("Concluidas");
                }
            }
            return View(tarefaConcluida);
        }
    }
}
