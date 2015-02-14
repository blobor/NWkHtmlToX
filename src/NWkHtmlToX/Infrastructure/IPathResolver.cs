namespace NWkHtmlToX.Infrastructure {
     
    /// <summary>
    /// Implementations of this interface are used for resolving path.
    /// </summary>
    public interface IPathResolver {

        /// <summary>
        /// Resolves a path.
        /// </summary>
        /// <returns>Resolved path.</returns>
        string ResolvePath();
    }
}