using System;

namespace Demo.Business.InfraStructure {
    public class Disposable : IDisposable {
        protected bool _disposed;
        public void Dispose () {
            Dispose (true);
            GC.SuppressFinalize (this);
        }
        ~Disposable () {
            Dispose (false);
        }
        void Dispose (bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    DisposeCore ();
                }
            }
            _disposed = true;
        }
        protected virtual void DisposeCore () {

        }
    }
}