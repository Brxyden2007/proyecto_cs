using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using proyecto_cs.src.shared.utils.pdf;

namespace proyecto_cs.src.shared.utils.pdf;
// public class VariedadesTodasPdfGenerator 
// {
//   private readonly Variedad _variedad;
//   public VariedadesTodasPdfGenerator(Variedad variedad) =>_variedad = variedad;
//   public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
//   public Task GenerateSamplePdf(AppDbContext context)
//   {
//     QuestPDF.Settings.License = LicenseType.Community;
//     // Create a simple PDF document using QuestPDF
//     // This is just a sample; you can modify it to suit your needs
//     Document.Create(container =>
//       {
//         container.Page(page =>
//         {
//           page.Size(PageSizes.A4);
//           page.Margin(2, Unit.Centimetre);
//           page.PageColor(Colors.White);
//           page.DefaultTextStyle(x => x.FontSize(20));
//           page.Header().Column(x =>
//           {
//             x.Item().Image("/images/temp.jpeg")
//             x.Item().Text("Variedades de café")
//           });
//                       .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium)          page.Content()
//             .PaddingVertical(1, Unit.Centimetre)
//             .Column(x =>
//             {
//               x.Spacing(20);
//               x.Item().Text("x text");
//               x.Item().Image(Placeholders.Image(200, 100));
//             });
//           page.Footer()
//             .AlignCenter()
//             .Text(x =>
//             {
//               x.Span("Page ");
//               x.CurrentPageNumber();
//             });
//         });
//       })
//       .GeneratePdf("hello.pdf");
//     return Task.CompletedTask;
//   }
// }

public class VariedadesTodasPdfGenerator
{
  private readonly AppDbContext _context;

  public VariedadesTodasPdfGenerator(AppDbContext context)
  {
    _context = context;
  }

  public void GenerateAll()
  {
    var variedades = _context.Variedades.ToList();

    foreach (var variedad in variedades)
    {
      var doc = new VariedadPdfGenerator(variedad).Compose(_context);      
    }
  }
}
