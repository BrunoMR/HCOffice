namespace ExtratorDeDados.TxtLines
{
    using System;

    internal sealed class ImportTxtAttribute : Attribute
    {
        public string LineStart { get; }

        public ImportTxtAttribute(string lineStart) { LineStart = lineStart; }
    }
}