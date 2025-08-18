using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.usuarios.domain.models;
using proyecto_cs.src.modules.variedades.domain.models;
using proyecto_cs.src.shared.utils.pdf;

namespace proyecto_cs;
public class PdfService
{
    public byte[] GenerarFichaTecnica(Variedad v) =>
        new FichaTecnicaPdfGenerator(v).Generate();

    public byte[] GenerarCatalogo(IEnumerable<Variedad> variedades) =>
        new CatalogoPdfGenerator(variedades).Generate();

    public byte[] GenerarReporteAdmin(IEnumerable<Usuario> usuarios) =>
        new ReporteAdminPdfGenerator(usuarios).Generate();
}