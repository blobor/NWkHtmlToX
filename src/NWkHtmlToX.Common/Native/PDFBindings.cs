using System.Runtime.InteropServices;

namespace NWkHtmlToX.Common.Native {

    /// <summary>
    /// Setup wkhtmltopdf.
    /// Must be called before any other functions.
    /// </summary>
    /// <param name="use_graphics">Should we use a graphics system.</param>
    /// <returns>True on success and False otherwise.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool wkhtmltopdf_init(bool use_graphics);

    /// <summary>
    /// Free up resources used by wkhtmltopdf, when this has been called no other wkhtmltopdf function can be called.
    /// </summary>
    /// <returns>True on success and False otherwise.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool wkhtmltopdf_deinit();

    /// <summary>
    /// Check if the library is build against the wkhtmltopdf version of QT.
    /// </summary>
    /// <returns>True if the library was build against the wkhtmltopdf version of QT and False otherwise.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool wkhtmltopdf_extended_qt();

    /// <summary>
    /// Return the version of wkhtmltopdf.
    /// </summary>
    /// <returns>Qt version.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    internal delegate string wkhtmltopdf_version();
}