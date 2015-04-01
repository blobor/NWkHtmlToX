using System;
using System.Threading;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Native;
using NWkHtmlToX.Common.Native.Win32;
using NWkHtmlToX.Common.SafeHandles;
using NWkHtmlToX.Common.Threading;
using NWkHtmlToX.PathResolvers;
using NWkHtmlToX.Settings;

namespace NWkHtmlToX.Converters {
    public class HtmlToPDFConverter : IHtmlConverter {

        private readonly ILibraryLoader _libraryLoader;
        private readonly IPathResolver _pathResolver;
        private readonly BinderFactory _binderFactory;
        private static readonly Lazy<TaskFactory> _staTaskFactoryLazy = new Lazy<TaskFactory>(() => new TaskFactory(CancellationToken.None,
                                                                                                                   TaskCreationOptions.DenyChildAttach,
                                                                                                                   TaskContinuationOptions.None,
                                                                                                                   SingleSTAThreadTaskScheduler.Instance),
                                                                                             true);

        protected bool Disposed;
        protected virtual TaskFactory STATaskFactory => _staTaskFactoryLazy.Value;

        private SafeLibraryHandle _pdfLibraryHandle;

        public PdfGlobalSettings GlobalSettings { get; protected set; }
        public HtmlToPDFConverter(PdfGlobalSettings globalSettings) {
            GlobalSettings = globalSettings;
            _libraryLoader = new WindowsLibraryLoader();
            _pathResolver = new CombinedDllPathResolver(new DllRegistryPathResolver());
            _binderFactory = new BinderFactory(_libraryLoader);
        }

        public Task<byte[]> ConvertAsync() {
            return ConvertAsync(CancellationToken.None);
        }

        public async Task<byte[]> ConvertAsync(CancellationToken cancellationToken) {
            await STATaskFactory.StartNew(() => {
                _pdfLibraryHandle = _libraryLoader.LoadLibrary(_pathResolver.ResolvePath());
            }, cancellationToken);
            return null;
        }

        #region IDisposable implementation
        protected virtual void Dispose(bool disposing) {

            if (Disposed) return;

            if (disposing) {
                // Free any other managed objects here.
                _libraryLoader.FreeLibrary(_pdfLibraryHandle);
            }

            // Free any unmanaged objects here.

            Disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~HtmlToPDFConverter() {
            Dispose(false);
        }
        #endregion
    }
}