using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO; // Agregado using System.IO para File operations
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.variedades.domain.models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace proyecto_cs.src.shared.utils.pdf;

public class VariedadResumidaPdfGenerator
{
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public Task Compose(AppDbContext context)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        
        var variedades = context.Variedades
            .Include(v => v.Rendimiento)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Porte)
            .ToList();

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.PageColor("#F1f1f1");

                // ENCABEZADO
                page.Header().Height(80).Background(Colors.Green.Medium).Padding(15).Column(col =>
                {
                    col.Item().Text("Resumen - Variedades de Café")
                        .FontSize(22).Bold().FontColor(Colors.White);
                    col.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(12).FontColor(Colors.White);
                });

                // CONTENIDO
                page.Content().Column(col =>
                {
                    col.Item().PaddingBottom(10)
                        .Text("Resumen de Variedades (Nombre, Imagen y Características Principales)")
                        .FontSize(18).Bold().FontColor(Colors.Green.Darken2);

                    // Grid de variedades (2 columnas)
                    for (int i = 0; i < variedades.Count; i += 2)
                    {
                        col.Item().PaddingBottom(15).Row(row =>
                        {
                            // Primera variedad
                            row.RelativeItem().PaddingRight(10).Border(1).BorderColor(Colors.Grey.Lighten1)
                                .Background(Colors.White).Padding(10).Column(varCol =>
                            {
                                var variedad1 = variedades[i];
                                
                                // Nombre
                                varCol.Item().Background(Colors.Green.Lighten2).Padding(8).AlignCenter()
                                    .Text(variedad1.NombreComun)
                                    .FontSize(14).Bold().FontColor(Colors.Green.Darken2);

                                // Imagen placeholder
                                varCol.Item().PaddingVertical(10).AlignCenter().Column(imgCol =>
                                {
                                    try
                                    {
                                        if (!string.IsNullOrWhiteSpace(variedad1.ImagenUrl) && File.Exists(variedad1.ImagenUrl))
                                        {
                                            var bytes = File.ReadAllBytes(variedad1.ImagenUrl);
                                            // Validar que los bytes no estén vacíos y sean una imagen válida
                                            if (bytes != null && bytes.Length > 0)
                                            {
                                                imgCol.Item().Height(80).Width(80).Image(bytes).FitArea();
                                            }
                                            else
                                            {
                                                // Mostrar placeholder si los bytes están vacíos
                                                imgCol.Item().Height(80).Width(80).Background(Colors.Grey.Lighten2)
                                                    .AlignCenter().AlignMiddle()
                                                    .Text("[Imagen no válida]")
                                                    .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                                            }
                                        }
                                        else
                                        {
                                            imgCol.Item().Height(80).Width(80).Background(Colors.Grey.Lighten2)
                                                .AlignCenter().AlignMiddle()
                                                .Text("[Sin imagen]")
                                                .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        // En caso de error al cargar la imagen, mostrar placeholder
                                        imgCol.Item().Height(80).Width(80).Background(Colors.Grey.Lighten2)
                                            .AlignCenter().AlignMiddle()
                                            .Text("[Error imagen]")
                                            .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                                    }
                                });

                                // Características principales
                                varCol.Item().Column(charCol =>
                                {
                                    charCol.Item().Text($"• Porte: {variedad1.Porte?.Nombre ?? "N/A"}").FontSize(10);
                                    charCol.Item().Text($"• Tamaño: {variedad1.TamanioGrano?.Nombre ?? "N/A"}").FontSize(10);
                                    charCol.Item().Text($"• Rendimiento: {variedad1.Rendimiento?.Nivel ?? "N/A"}").FontSize(10);
                                });
                            });

                            // Segunda variedad (si existe)
                            if (i + 1 < variedades.Count)
                            {
                                row.RelativeItem().PaddingLeft(10).Border(1).BorderColor(Colors.Grey.Lighten1)
                                    .Background(Colors.White).Padding(10).Column(varCol =>
                                {
                                    var variedad2 = variedades[i + 1];
                                    
                                    // Nombre
                                    varCol.Item().Background(Colors.Green.Lighten2).Padding(8).AlignCenter()
                                        .Text(variedad2.NombreComun)
                                        .FontSize(14).Bold().FontColor(Colors.Green.Darken2);

                                    // Imagen placeholder
                                    varCol.Item().PaddingVertical(10).AlignCenter().Column(imgCol =>
                                    {
                                        try
                                        {
                                            if (!string.IsNullOrWhiteSpace(variedad2.ImagenUrl) && File.Exists(variedad2.ImagenUrl))
                                            {
                                                var bytes = File.ReadAllBytes(variedad2.ImagenUrl);
                                                // Validar que los bytes no estén vacíos y sean una imagen válida
                                                if (bytes != null && bytes.Length > 0)
                                                {
                                                    imgCol.Item().Height(80).Width(80).Image(bytes).FitArea();
                                                }
                                                else
                                                {
                                                    // Mostrar placeholder si los bytes están vacíos
                                                    imgCol.Item().Height(80).Width(80).Background(Colors.Grey.Lighten2)
                                                        .AlignCenter().AlignMiddle()
                                                        .Text("[Imagen no válida]")
                                                        .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                                                }
                                            }
                                            else
                                            {
                                                imgCol.Item().Height(80).Width(80).Background(Colors.Grey.Lighten2)
                                                    .AlignCenter().AlignMiddle()
                                                    .Text("[Sin imagen]")
                                                    .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            // En caso de error al cargar la imagen, mostrar placeholder
                                            imgCol.Item().Height(80).Width(80).Background(Colors.Grey.Lighten2)
                                                .AlignCenter().AlignMiddle()
                                                .Text("[Error imagen]")
                                                .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                                        }
                                    });

                                    // Características principales
                                    varCol.Item().Column(charCol =>
                                    {
                                        charCol.Item().Text($"• Porte: {variedad2.Porte?.Nombre ?? "N/A"}").FontSize(10);
                                        charCol.Item().Text($"• Tamaño: {variedad2.TamanioGrano?.Nombre ?? "N/A"}").FontSize(10);
                                        charCol.Item().Text($"• Rendimiento: {variedad2.Rendimiento?.Nivel ?? "N/A"}").FontSize(10);
                                    });
                                });
                            }
                        });
                    }
                });

                // PIE DE PÁGINA
                page.Footer().AlignCenter().Text($"Generado el {DateTime.Now:dd/MM/yyyy}")
                    .FontSize(10).FontColor(Colors.Grey.Medium);
            });
        })
        .GeneratePdf("Resumen_Variedades.pdf");

        return Task.CompletedTask;
    }
}