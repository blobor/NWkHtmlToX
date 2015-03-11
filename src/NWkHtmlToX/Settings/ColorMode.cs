namespace NWkHtmlToX.Settings {

    /// <summary>
    /// This enum type is used to indicate whether NWkHtmlToPdf should print in color or not.
    /// </summary>
    public enum ColorMode {

        /// <summary>
        /// print in color if available, otherwise in grayscale
        /// </summary>
        Color = 1,

        /// <summary>
        /// Print in grayscale, even on color printers
        /// </summary>
        Grayscale = 0
    }
}