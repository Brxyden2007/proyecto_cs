using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.variedades.domain.models;
using proyecto_cs.src.modules.atributos_agronomicos.domain.models;
using proyecto_cs.src.modules.historias_geneticas.domain.models;

namespace proyecto_cs.src.shared.utils.pdf;

public class VariedadPdfGenerator
{
    public VariedadPdfGenerator() { }
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public async Task<byte[]> Compose(AppDbContext context,int idVariedad)
    {
        var variedad = await context.Variedades
            .Include(v => v.Rendimiento)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Porte)
            .Include(v => v.Altitud)
            .Include(v => v.CalidadAltitud)
            .Include(v => v.AtributosAgronomicos)
            .Include(v => v.HistoriasGeneticas)
            .FirstOrDefaultAsync(v => v.IdVariedad == idVariedad);

        if (variedad == null)
            throw new Exception($"No se encontró la variedad con ID {idVariedad}");

        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Helvetica"));
                page.PageColor(Colors.White);

                page.Header().Element(ComposeHeader);
                page.Content().Element(c => ComposeContent(c, variedad));
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        });

        return document.GeneratePdf();
    }

    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Height(80).Background(Colors.Green.Darken1).AlignCenter().AlignMiddle().Text("Ficha Técnica de Variedad de Café")
                .FontColor(Colors.White)
                .FontSize(20)
                .Bold();
        });
    }

    private void ComposeContent(IContainer container, Variedad variedad)
    {
        container.PaddingVertical(10).Column(column =>
        {
            // Sección de Información Básica
            column.Item().Row(row =>
            {
                // Columna Izquierda - Datos Básicos
                row.RelativeItem(1).PaddingRight(10).Column(col =>
                {
                    // Tarjeta de Información Principal
                    col.Item().Border(1).BorderColor(Colors.Green.Darken1).Background(Colors.Green.Lighten5).Padding(15).Column(card =>
                    {
                        card.Item().Text(variedad.NombreComun)
                            .FontSize(24)
                            .Bold()
                            .FontColor(Colors.Green.Darken2);

                        card.Item().PaddingTop(5).Text(variedad.Descripcion)
                            .FontSize(12)
                            .Italic();

                        card.Item().PaddingTop(10).Row(r =>
                        {
                            r.RelativeItem().Text($"Nombre Científico: ").SemiBold();
                            r.RelativeItem(2).Text(variedad.NombreCientifico ?? "No especificado");
                        });
                    });

                    // Sección de Características Principales
                    col.Item().PaddingTop(15).Column(c =>
                    {
                        c.Item().Text("CARACTERÍSTICAS PRINCIPALES")
                            .FontSize(14)
                            .Bold()
                            .FontColor(Colors.Green.Darken2);

                        AddFeatureCard(c, "POTENCIAL DE RENDIMIENTO", $"{variedad.Rendimiento?.Nivel}", Colors.Green.Lighten3);
                        AddFeatureCard(c, "TAMAÑO DEL GRANO", $"{variedad.TamanioGrano?.Nombre}", Colors.Green.Lighten4);
                        AddFeatureCard(c, "PORTE DE LA PLANTA", $"{variedad.Porte?.Nombre}", Colors.Green.Lighten3);
                        AddFeatureCard(c, "RANGO DE ALTITUD", $"{variedad.Altitud?.Rango}", Colors.Green.Lighten4);
                    });
                });

                // Columna Derecha - Imagen
                row.RelativeItem(1).Height(300).Column(col =>
                {
                    if (!string.IsNullOrWhiteSpace(variedad.ImagenUrl) && File.Exists(variedad.ImagenUrl))
                    {
                        byte[] imageBytes = File.ReadAllBytes(variedad.ImagenUrl);
                        col.Item()
                          .Border(1)
                          .BorderColor(Colors.Grey.Lighten1)
                          .Image(imageBytes)
                          .FitArea();
                    }
                    else
                    {
                        col.Item().Height(250).Border(1).BorderColor(Colors.Grey.Lighten1)
                            .Background(Colors.Grey.Lighten3)
                            .AlignCenter()
                            .AlignMiddle()
                            .Text("[Imagen no disponible]")
                            .Italic()
                            .FontColor(Colors.Grey.Medium);
                    }

                    // Resistencia a Enfermedades
                    col.Item().PaddingTop(15).Column(c =>
                    {
                        c.Item().Text("RESISTENCIA A ENFERMEDADES")
                            .FontSize(14)
                            .Bold()
                            .FontColor(Colors.Green.Darken2);

                        AddResistanceBar(c, "ROYA DEL CAFETO", "Resistente", 90);
                        AddResistanceBar(c, "NEMÁTODOS", "Moderadamente resistente", 70);
                        AddResistanceBar(c, "BROCA DEL CAFÉ", "Susceptible", 30);
                    });
                });
            });

            // Sección de Atributos Agronómicos (Tabla)
            column.Item().PaddingTop(20).Column(c =>
            {
                c.Item().Text("ATRIBUTOS AGRONÓMICOS")
                    .FontSize(16)
                    .Bold()
                    .FontColor(Colors.Green.Darken2);

                if (variedad.AtributosAgronomicos != null)
                {
                    c.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // Propiedad
                            columns.RelativeColumn(3); // Valor
                            columns.RelativeColumn(2); // Notas
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Green.Darken1).Padding(5).Text("Propiedad").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Green.Darken1).Padding(5).Text("Valor").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Green.Darken1).Padding(5).Text("Notas").FontColor(Colors.White).Bold();
                        });

                        // AddAgronomicRow(table, "Tiempo de Cosecha", variedad.AtributosAgronomicos.TiempoCosecha);
                        // AddAgronomicRow(table, "Maduración", variedad.AtributosAgronomicos.Maduracion);
                        // AddAgronomicRow(table, "Requerimientos Nutricionales", variedad.AtributosAgronomicos.Nutricion);
                        // AddAgronomicRow(table, "Densidad de Siembra", variedad.AtributosAgronomicos.);
                    });
                }
                else
                {
                    c.Item().PaddingTop(5).Text("No hay información agronómica disponible").Italic().FontColor(Colors.Grey.Medium);
                }
            });

            // Sección de Historia Genética (Tabla)
            column.Item().PaddingTop(20).Column(c =>
            {
                c.Item().Text("HISTORIA GENÉTICA")
                    .FontSize(16)
                    .Bold()
                    .FontColor(Colors.Green.Darken2);

                if (variedad.HistoriasGeneticas != null)
                {
                    c.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // Propiedad
                            columns.RelativeColumn(5); // Valor
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Green.Darken1).Padding(5).Text("Propiedad").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Green.Darken1).Padding(5).Text("Valor").FontColor(Colors.White).Bold();
                        });

                        // AddGeneticRow(table, "Obtentor", variedad.HistoriasGeneticas.Obtentor);
                        // AddGeneticRow(table, "Familia Genética", variedad.HistoriasGeneticas.Familia);
                        // AddGeneticRow(table, "Grupo Varietal", variedad.HistoriasGeneticas.Grupo);
                        // AddGeneticRow(table, "Descripción Genética", variedad.HistoriasGeneticas.Descripcion);
                    });
                }
                else
                {
                    c.Item().PaddingTop(5).Text("No hay información genética disponible").Italic().FontColor(Colors.Grey.Medium);
                }
            });

            // Notas Adicionales
            column.Item().PaddingTop(15).BorderTop(1).BorderColor(Colors.Grey.Lighten1).PaddingVertical(10).Text("NOTAS:")
                .FontSize(12)
                .Italic()
                .FontColor(Colors.Grey.Darken1);
        });
    }

    private void AddFeatureCard(ColumnDescriptor column, string title, string value, string color)
    {
        column.Item().PaddingVertical(5).Border(1).BorderColor(Colors.Grey.Lighten1).Background(color).Padding(8).Row(row =>
        {
            row.RelativeItem().Text(title).SemiBold().FontSize(12);
            row.RelativeItem().Text(value).FontSize(12).AlignRight();
        });
    }

    private void AddResistanceBar(ColumnDescriptor column, string disease, string resistance, int percentage)
    {
        column.Item().PaddingVertical(3).Column(c =>
        {
            c.Item().Row(r =>
            {
                r.RelativeItem().Text(disease).FontSize(11).SemiBold();
                r.RelativeItem().Text(resistance).FontSize(11).AlignRight();
            });

            var color = percentage > 70 ? Colors.Green.Lighten1 : 
                       percentage > 40 ? Colors.Orange.Lighten1 : 
                       Colors.Red.Lighten1;

            c.Item().Height(8).Background(Colors.Grey.Lighten3).Border(1).BorderColor(Colors.Grey.Lighten1)
                .Layers(layers =>
                {
                    // layers.PrimaryLayer().Width($"{percentage}").Background(color);
                });
        });
    }

    private void AddAgronomicRow(TableDescriptor table, string property, string value)
    {
        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(property).FontSize(11).SemiBold();
        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(value ?? "N/A").FontSize(11);
        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("").FontSize(11); // Espacio para notas adicionales
    }

    private void AddGeneticRow(TableDescriptor table, string property, string value)
    {
        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(property).FontSize(11).SemiBold();
        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(value ?? "N/A").FontSize(11);
    }
}