using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Performance.Testing.Utilities.ReportConsole.Framework
{
    public static class XDocumentExtensions
    {
         public static Dictionary<string, XNamespace> GetNamespaces(this XDocument document)
         {
             return document.Root.Attributes().
                        Where(a => a.IsNamespaceDeclaration).
                        GroupBy(a => a.Name.Namespace == XNamespace.None ? String.Empty : a.Name.LocalName,
                                a => XNamespace.Get(a.Value)).
                        ToDictionary(g => g.Key,
                                     g => g.First());
         }
    }
}