namespace NWkHtmlToX.Common.Native {
    internal class PDFBinding {
        internal wkhtmltopdf_init Initialize { get; set; }
        internal wkhtmltopdf_deinit DeInitialize { get; set; }
        internal wkhtmltopdf_version Version { get; set; }
    }
}