using proyecto_cs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

public class VariedadDetalladaPdfDocument : IDocument
{
    private readonly Variedad _variedad;

    public VariedadDetalladaPdfDocument(Variedad variedad)
    {
        _variedad = variedad;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(20);
            page.Header().Text(_variedad.NombreComun).FontSize(20).Bold();
            page.Content().Text($"Altitud: {_variedad.Altitud}");
            // Aquí agregas más contenido, imágenes, tablas, etc.
        });
    }
}
