using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PlugSpl.Modules {
    /// <summary>
    /// Defines a module to be loaded for Uml component diagram edition. That module must to 
    /// provide a graphical component to support the modeling activity (GetControl method) and
    /// import/export the struct from this graphical control.
    /// </summary>
    public interface IUmlComponentDiagramEditor {
        Control GetControl();
        void ExportStructure();
        void ImportStructure();

        void SetApplicationWindow(MainWindow mainWindow);
    }
}
