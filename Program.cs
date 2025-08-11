using Microsoft.EntityFrameworkCore.Internal;
using proyecto_cs;
internal class Program
{
  private static void Main(string[] args)
  {
    var context = DbContextFactory.Create();  
  }
}