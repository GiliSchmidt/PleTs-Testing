using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using PlugSpl.Atlas;

namespace PlugSpl.Modules {
    /// <summary>
    /// Defines a module to be loaded for Feature Model edition. That module must to 
    /// provide a graphical component to support the modeling activity (GetControl method) and
    /// import/export the struct from this graphical control.
    /// </summary>
    public interface IFeatureModelEditor {

        /// <summary>
        /// Return the control itself.
        /// </summary>
        /// <returns></returns>
        Control GetControl();

        /// <summary>
        /// Returns data in atlas format.
        /// </summary>
        AtlasFeatureModel ExportStructure();

        /// <summary>
        /// Import and atlas into diagram.
        /// </summary>
        void ImportStructure(AtlasFeatureModel atlas);

        /// <summary>
        /// Set window where the component is attached.
        /// </summary>
        void SetApplicationWindow(MainWindow window);
    }
}
