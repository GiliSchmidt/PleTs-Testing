using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.DataStructs.ProductConfigurator
{
    /// <summary>
    /// Represents a single instance of a socket.
    /// </summary>
    public class DanuSocket
    {
        /// <summary>
        /// Interface to which it is connected.
        /// </summary>
        private DanuInterfaceObject interfaceUsed;
        public DanuInterfaceObject InterfaceUsed
        {
            get { return interfaceUsed; }
            set { interfaceUsed = value; }
        }

        /// <summary>
        /// Parent Component.
        /// </summary>
        private DanuComponent parent;
        public DanuComponent Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public DanuSocket(DanuComponent parent, DanuInterfaceObject interfaceUsed)
        {
            this.parent = parent;
            this.interfaceUsed = interfaceUsed;
        }

        private DanuSocket() { }
    }
}