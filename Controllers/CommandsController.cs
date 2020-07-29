using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController: ControllerBase
    {
        private readonly CommandContext _context;

        public CommandsController(CommandContext context)
        {
            _context = context;
        }
        
//      GET
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

//      GET Particular Id
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItems(int id)
        {
            var commandItems = _context.CommandItems.Find(id);
            if (commandItems == null){
                return NotFound();
            }
            return commandItems;
        }

//      POST
        [HttpPost]
        public ActionResult<Command> PostCommandItems(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItems", new Command{Id = command.Id}, command);
        }

//      PUT
        [HttpPut("{id}")]
        public ActionResult<Command> PutCommandItem(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

//      DELETE
        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if(commandItem == null)
            {
                return NotFound();
            }

            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            return commandItem;
        }
    }
}