using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using NWkHtmlToX.Common.SafeHandles;

namespace NWkHtmlToX.Common.Native {
    internal class BinderFactory {

        private readonly ILibraryLoader _libraryLoader;

        internal BinderFactory(ILibraryLoader libraryLoader) {
            _libraryLoader = libraryLoader;
        }

        internal T MapDelegates<T>(SafeLibraryHandle libraryHandle) where T : class, new() {
            var result = new T();
            var objectTypeInfo = typeof (T).GetTypeInfo();
            var delegateType = typeof (Delegate);

            foreach (var propertyInfo in objectTypeInfo.DeclaredProperties.Where(property => delegateType.IsAssignableFrom(property.PropertyType)
                                                                                          && property.CanWrite)) {
                IntPtr procedure;
                if (_libraryLoader.TryGetProcAddress(libraryHandle, propertyInfo.PropertyType.Name, out procedure)) {
                    propertyInfo.SetValue(result, Marshal.GetDelegateForFunctionPointer(procedure, propertyInfo.PropertyType));
                }
            }

            return result;
        }
    }
}