using proyecto_cs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

// public class VariedadPdfService : IVariedadPdfService
// {
//     private readonly IVariedadRepository _variedadRepository;

//     public VariedadPdfService(IVariedadRepository variedadRepository)
//     {
//         _variedadRepository = variedadRepository;
//     }

//     public byte[] GenerarPdfVariedad(int variedadId)
//     {
//         var variedad = _variedadRepository.ObtenerPorId(variedadId);
//         return new VariedadDetalladaPdfDocument(variedad).GeneratePdf();
//     }

//     public byte[] GenerarPdfTodasVariedades()
//     {
//         var variedades = _variedadRepository.ObtenerTodas();
//         return new VariedadesListadoPdfDocument(variedades).GeneratePdf();
//     }

//     public byte[] GenerarPdfFiltrado(PdfFiltroVariedad filtro)
//     {
//         var variedades = _variedadRepository.ObtenerFiltradas(filtro);
//         return new VariedadesListadoPdfDocument(variedades).GeneratePdf();
//     }

//     public byte[] GenerarPdfOrdenado(PdfOrdenVariedad orden)
//     {
//         var variedades = _variedadRepository.ObtenerOrdenadas(orden);
//         return new VariedadesListadoPdfDocument(variedades).GeneratePdf();
//     }

//     public byte[] GenerarPdfSeleccion(IEnumerable<int> idsVariedades)
//     {
//         var variedades = _variedadRepository.ObtenerPorIds(idsVariedades);
//         return new VariedadesListadoPdfDocument(variedades).GeneratePdf();
//     }

//     public byte[] GenerarPdfResumido(IEnumerable<int> idsVariedades)
//     {
//         var variedades = _variedadRepository.ObtenerPorIds(idsVariedades);
//         return new VariedadesResumidasPdfDocument(variedades).GeneratePdf();
//     }

//     public byte[] GenerarPdfDetallado(IEnumerable<int> idsVariedades)
//     {
//         var variedades = _variedadRepository.ObtenerPorIds(idsVariedades);
//         return new VariedadesDetalladasPdfDocument(variedades).GeneratePdf();
//     }
// }
