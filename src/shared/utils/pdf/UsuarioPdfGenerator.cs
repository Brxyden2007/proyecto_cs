using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using proyecto_cs.src.modules.usuarios.domain.models;

namespace proyecto_cs;

public class UsuarioPdfGenerator
{
    private readonly Usuario usuario;

    public UsuarioPdfGenerator(Usuario usuario)
    {
        this.usuario = usuario;
    }

    public byte[] Generate()
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Header().Row(row =>
                {
                    row.RelativeItem().Text("Ficha de Usuario").Style(TextStyle.Default.FontSize(20).Bold().FontColor(Colors.Blue.Medium));
                    row.RelativeItem(60).Height(40).Background(Colors.Blue.Lighten1);
                });

                page.Content().Column(col =>
                {
                    col.Item().Text($"ID: {usuario.Id}").FontSize(12);
                    col.Item().Text($"Nombre: {usuario.Nombre} {usuario.Apellido}").FontSize(12);
                    col.Item().Text($"Email: {usuario.Email}").FontSize(12);
                    col.Item().Text($"Fecha de creación: {usuario.CreatedAt:dd/MM/yyyy}").FontSize(12);
                });
            });
        }).GeneratePdf();
    }
}