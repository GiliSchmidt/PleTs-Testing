using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace PlugSpl {
    /// <summary>
    /// Opens XML file and load configuration. Note that
    /// this class is sealed, so it cannot be inherited.
    /// </summary>
    public sealed class ConfigurationSet {

        /// <summary>
        /// Represents the singleton instance.
        /// </summary>
        private static ConfigurationSet instance;

        /// <summary>
        /// File from where the configuration will be loaded.
        /// Change it to reflect configuration file location.
        /// </summary>
        private const string filename = "./Configuration/PlugSPL.xml";

        /// <summary>
        /// Gets or Sets a list of available modules.
        /// </summary>
        [XmlArray()]
        public List<Module> Modules {get;set;}
        
        /// <summary>
        /// Singleton implementation. (Google it faggot.)
        /// </summary>
        public static ConfigurationSet GetInstance(){
            if(ConfigurationSet.instance == null){
                ConfigurationSet.instance = new ConfigurationSet();
            }
            return ConfigurationSet.instance;
        }

        /// <summary>
        /// Private constructor. Singleton implementation present on this class.
        /// To get an instance of it, call GetInstance method;
        /// </summary>
        private ConfigurationSet(){}


        /// <summary>
        /// Retrieve configuration object from a config file.
        /// </summary>
        public void LoadConfiguration(){

            this.Modules = new List<Module>();
            this.Modules.Add(new Module(){ 
                Assembly = "asseeeemmmbllyy", 
                Interface = "Interfaceeee",
                Name = "nameee",
                Type = "typeee"});

            TextReader reader = new StreamReader(ConfigurationSet.filename);
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationSet));
            ConfigurationSet x = (ConfigurationSet)serializer.Deserialize(reader);
            this.Modules = x.Modules;
            reader.Close();
        }

        /// <summary>
        /// Save configuration to file.
        /// </summary>
        public void SaveConfiguration(){

            this.Modules = new List<Module>();
            this.Modules.Add(new Module(){ 
                Assembly = "asseeeemmmbllyy", 
                Interface = "Interfaceeee",
                Name = "nameee",
                Type = "typeee"});

            using(TextWriter writer = new StreamWriter("./Configuration/PlugSPL.xml")){
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationSet));
                serializer.Serialize(writer, this);
            }            
        }
    }
}
