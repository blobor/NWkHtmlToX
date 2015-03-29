using System;
using System.Threading;
using System.Threading.Tasks;

namespace NWkHtmlToX {
    public interface IHtmlConverter : IDisposable {
        Task<byte[]> ConvertAsync();
        Task<byte[]> ConvertAsync(CancellationToken cancellationToken);
    }
}