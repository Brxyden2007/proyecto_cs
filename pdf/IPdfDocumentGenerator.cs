using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public interface IPdfDocumentGenerator<T>
{
    byte[] Generate(T data); // Devuelve el PDF en bytes
}

