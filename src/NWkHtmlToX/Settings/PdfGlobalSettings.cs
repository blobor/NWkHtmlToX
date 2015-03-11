using System.Diagnostics.CodeAnalysis;

namespace NWkHtmlToX.Settings {

    [ExcludeFromCodeCoverage]
    public class PdfGlobalSettings {

        /// <summary>
        /// Should the output be printed in color or gray scale
        /// </summary>
        public ColorMode ColorMode { get; set; }

        /// <summary>
        /// The orientation of the output document
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// The paper size of the output document
        /// </summary>
        public PaperSize PaperSize { get; set; }

        /// <summary>
        /// How many copies should we print?
        /// </summary>
        public int Copies { get; set; }

        /// <summary>
        /// The title of the PDF document
        /// </summary>
        public string DocumentTitle { get; set; }

        /// <summary>
        /// Should we use loss less compression when creating the pdf file
        /// </summary>
        public bool UseCompression { get; set; }
    }
}