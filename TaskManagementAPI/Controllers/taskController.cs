using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    public class TaskController : ApiController
    {
        private TestEntities db = new TestEntities();

        // GET: api/Task
        public IQueryable<tb_task> Gettb_task()
        {
            return db.tb_task;
        }

        // GET: api/Task/5
        [ResponseType(typeof(tb_task))]
        public IHttpActionResult Gettb_task(int id)
        {
            tb_task tb_task = db.tb_task.Find(id);
            if (tb_task == null)
            {
                return NotFound();
            }

            return Ok(tb_task);
        }

        // PUT: api/Task/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttb_task(int id, tb_task tb_task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tb_task.task_id)
            {
                return BadRequest();
            }

            db.Entry(tb_task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tb_taskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Task
        [ResponseType(typeof(tb_task))]
        public IHttpActionResult Posttb_task(tb_task tb_task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tb_task.Add(tb_task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tb_task.task_id }, tb_task);
        }

        // DELETE: api/Task/5
        [Route("api/Task/deleteid")]
        [ResponseType(typeof(tb_task))]
        public IHttpActionResult Deletetb_task(int id)
        {
            tb_task tb_task = db.tb_task.Find(id);
            if (tb_task == null)
            {
                return NotFound();
            }

            db.tb_task.Remove(tb_task);
            db.SaveChanges();

            return Ok(tb_task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("api/Task/searchid")]
        public async Task<IHttpActionResult> GetIDValidation(int id)
        {
            IQueryable<tb_task> tb_task = db.tb_task.Where(b => b.task_id == (id));
            if (tb_task == null)
            {
                return NotFound();
            }
            return Ok(await tb_task.FirstAsync());
        }
        private bool tb_taskExists(int id)
        {
            return db.tb_task.Count(e => e.task_id == id) > 0;
        }
    }
}