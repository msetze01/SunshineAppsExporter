using System;

namespace SunshineAppsExporter.Exporters {
    internal class GameNotFoundException : ApplicationException {
        public GameNotFoundException() : base() { }
        public GameNotFoundException(string message) : base(message) { }
        public GameNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
