using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public interface IVariedadPdfService
{
    byte[] GenerarPdfVariedad(int variedadId);
    byte[] GenerarPdfTodasVariedades();
    byte[] GenerarPdfFiltrado(PdfFiltroVariedad filtro);
    byte[] GenerarPdfOrdenado(PdfOrdenVariedad orden);
    byte[] GenerarPdfSeleccion(IEnumerable<int> idsVariedades);
    byte[] GenerarPdfResumido(IEnumerable<int> idsVariedades);
    byte[] GenerarPdfDetallado(IEnumerable<int> idsVariedades);
}
