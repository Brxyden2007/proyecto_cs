using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace proyecto_cs.src.shared.utils.pdf;
public class FichaTecnicaPdfGenerator
{
    private readonly Variedad variedad;

    public FichaTecnicaPdfGenerator(Variedad variedad)
    {
        this.variedad = variedad;
    }

    public byte[] Generate()
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Header().Element(c => PdfBaseStyles.Encabezado(c, "Ficha Técnica"));
                page.Content().Column(col =>
                {
                    col.Item().Text($"{variedad.NombreComun} ({variedad.NombreCientifico})")
                        .Style(PdfBaseStyles.SubTitulo);

                    if (!string.IsNullOrEmpty(variedad.ImagenUrl))
                        col.Item().Image(variedad.ImagenUrl, ImageScaling.FitArea);

                    col.Item().Text(variedad.Descripcion).Style(PdfBaseStyles.Texto);
                    col.Item().Text($"Porte: {variedad.Porte}").Style(PdfBaseStyles.Texto);
                    col.Item().Text($"Tamaño grano: {variedad.TamanioGrano}").Style(PdfBaseStyles.Texto);
                    col.Item().Text($"Altitud óptima: {variedad.Altitud} msnm").Style(PdfBaseStyles.Texto);

                    // Resistencias
                    col.Item().Text($"Resistencia Roya: {variedad.VariedadResistencias}").Style(PdfBaseStyles.Texto);

                    // Info complementaria
                    // col.Item().Text($"Tiempo cosecha: {variedad.TiempoCosecha}").Style(PdfBaseStyles.Texto);
                    // col.Item().Text($"Historia y linaje: {variedad.Historia}").Style(PdfBaseStyles.Texto);
                });
            });
        }).GeneratePdf();
    }
}
