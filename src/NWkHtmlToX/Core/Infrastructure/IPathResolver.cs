namespace NWkHtmlToX.Core.Infrastructure {
     
    /// <summary>
    /// Implementations of this interface are used for resolving path.
    /// </summary>
    internal interface IPathResolver {

        /// <summary>
        /// Resolves a path.
        /// </summary>
        /// <returns>Resolved path.</returns>
        string ResolvePath();
    }
}