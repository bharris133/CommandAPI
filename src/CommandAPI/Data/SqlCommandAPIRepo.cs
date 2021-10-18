using System.Collections.Generic;
using System.Linq;
using CommandAPI.Models;

namespace CommandAPI.Data
{
 public class SqlCommandAPIRepo : ICommandAPIRepo
 {
  private readonly CommandContext _context;
  public SqlCommandAPIRepo(CommandContext context)
  {
   _context = context;

  }
  public void CreateCommand(Command cmd)
  {
   throw new System.NotImplementedException();
  }

  public void DeleteCommand(Command command)
  {
   throw new System.NotImplementedException();
  }

  public IEnumerable<Command> GetAllCommands()
  {
   return _context.CommandItems.ToList();
  }

  public Command GetCommandById(int id)
  {
   return _context.CommandItems.FirstOrDefault(p => p.Id == id);
  }

  public bool saveChanges()
  {
   throw new System.NotImplementedException();
  }

  public void UpdateCommand(Command cmd)
  {
   throw new System.NotImplementedException();
  }
 }
}