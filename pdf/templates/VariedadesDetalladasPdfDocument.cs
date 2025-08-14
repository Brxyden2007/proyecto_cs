using System.Collections.Generic;

namespace proyecto_cs;
public class VariedadesDetalladasPdfDocument 
    : PdfBaseDocument<IEnumerable<VariedadDetalladaPdfDocument>>
{
    public override byte[] Generate(IEnumerable<VariedadDetalladaPdfDocument> variedades)
    {
        using var stream = CreatePdfStream();

        AddHeader("Catálogo de Variedades (Detalle)");

        foreach (var variedad in variedades)
        {
            // Aquí agregarías:
            // - Nombre completo
            // - Imagen principal
            // - Ficha técnica completa (porte, altitud, rendimiento, etc.)
            // - Descripciones extensas
            // - Múltiples imágenes si existen
        }

        AddFooter("Generado automáticamente por el sistema.");

        return stream.ToArray();
    }
}
