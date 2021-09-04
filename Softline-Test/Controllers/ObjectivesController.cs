using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Softline_Test.Models;

namespace Softline_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectivesController : ControllerBase
    {
        ObjectivesContext db;

        public ObjectivesController(ObjectivesContext context)
        {
            db = context;

            if (!db.Statuses.Any())
            {
                db.Statuses.Add(new Status { Name = "Создана" });
                db.Statuses.Add(new Status { Name = "В работе" });
                db.Statuses.Add(new Status { Name = "Завершена" });
                db.SaveChanges();
            }

            if (!db.Objectives.Any())
            {
                var Status_Created = db.Statuses.FirstOrDefault(s => s.Name == "Создана");
                var Status_InProcess = db.Statuses.FirstOrDefault(s => s.Name == "В работе");
                var Status_Done = db.Statuses.FirstOrDefault(s => s.Name == "Завершена");

                db.Objectives.Add(new Objective
                {
                    Name = "Изучить ASP.NET Core",
                    Description = "Прочитать материалы с сайта https://metanit.com/sharp/aspnet5/",
                    Status = Status_Done
                });
                db.Objectives.Add(new Objective
                {
                    Name = "Покормить кота",
                    Description = "Положить корм в миску и налить молочка",
                    Status = Status_Created
                });
                db.Objectives.Add(new Objective
                {
                    Name = "Выпонить тестовое задание",
                    Description = "Разработать базу данных и интерфейс по техническому заданию",
                    Status = Status_Done
                });
                db.Objectives.Add(new Objective
                {
                    Name = "Отправить тестовое задание",
                    Description = "Отправить ссылку на репозиторий и ожидать результатов проверки",
                    Status = Status_InProcess
                });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Objective>>> Get()
        {
            return await db.Objectives.Include(o => o.Status).ToListAsync();
        }

        [HttpGet]
        [Route("statuses")]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            return await db.Statuses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Objective>> Get(int id)
        {
            var objective = await db.Objectives.Include(o => o.Status).FirstOrDefaultAsync(o => o.Id == id);

            if (objective == null)
                return NotFound();

            return new ObjectResult(objective);
        }

        [HttpPost]
        public async Task<ActionResult<Objective>> Post(Objective objective)
        {
            if (objective == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //foreach(var obj in db.Objectives)
                //if (objective.Status == null)
                //    objective.Status = await db.Statuses.FirstOrDefaultAsync(s => s.Id == objective.StatusId);

            db.Objectives.Add(objective);
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
            }
            return Ok(objective);
        }

        [HttpPut]
        public async Task<ActionResult<Objective>> Put(Objective objective)
        {
            if (objective == null)
                return BadRequest();

            if (!db.Objectives.Any(o => o.Id == objective.Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if (objective.Status == null)
            //    objective.Status = await db.Statuses.FirstOrDefaultAsync(s => s.Id == objective.StatusId);

            db.Update(objective);
            await db.SaveChangesAsync();
            return Ok(objective);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Objective>> Delete(int id)
        {
            var objective = await db.Objectives.FirstOrDefaultAsync(o => o.Id == id);

            if (objective == null)
                return NotFound();

            db.Objectives.Remove(objective);
            await db.SaveChangesAsync();
            return Ok(objective);
        }
    }
}
