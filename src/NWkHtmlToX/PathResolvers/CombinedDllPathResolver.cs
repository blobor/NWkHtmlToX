using System.IO;
using System.Linq;
using NWkHtmlToX.Common.Utilities;

namespace NWkHtmlToX.PathResolvers {
    public class CombinedDllPathResolver : IPathResolver {

        protected IPathResolver[] PathResolvers;

        public CombinedDllPathResolver(params IPathResolver[] pathResolvers) {
            ThrowIf.Argument.IsNull(pathResolvers, nameof(pathResolvers));

            PathResolvers = pathResolvers;
        }

        public string ResolvePath() {
            string result = null;
            foreach (var pathResolver in PathResolvers.Where(resolver => resolver != null)) {
                result = pathResolver.ResolvePath();
                if (File.Exists(result)) {
                    break;
                }
            }
            return result;
        }
    }
}