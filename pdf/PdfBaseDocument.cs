using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public abstract class PdfBaseDocument<T> : IPdfDocumentGenerator<T>
{
    public abstract byte[] Generate(T data);

    protected MemoryStream CreatePdfStream()
    {
        return new MemoryStream();
    }

    protected void AddHeader(string title)
    {
        // Lógica para agregar encabezado (puede usar librería como iText7, QuestPDF, etc.)
    }

    protected void AddFooter(string text)
    {
        // Lógica para agregar pie de página
    }
}
