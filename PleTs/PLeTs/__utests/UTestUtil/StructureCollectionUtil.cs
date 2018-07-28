
using Coc.Data.ControlAndConversionStructures;
using Coc.Data.Xmi;
using OgmaJOATSParser;
using System;
using System.Collections.Generic;

namespace UTestUtil
{
    public class StructureCollectionUtil
    {
        public static List<GeneralUseStructure> GetStructureCollectionActOats()
        {
            String name, path;
            OgmaJ parser;

            name = "test";
            path = "..\\..\\..\\TestFiles\\java oats script.java";
            parser = new OgmaJ();

            return parser.ParserMethod(path, ref name, null).listGeneralStructure;
        }

        public static List<GeneralUseStructure> GetStructureCollectionSeqJUnit()
        {
            String path, name;
            SequenceDiagramImporter importer;

            name = "test";
            path = "..\\..\\..\\TestFiles\\tcc edemar junit -sequence diag - astah.xml";
            importer = new SequenceDiagramImporter();

            return importer.ParserMethod(path, ref name, null).listGeneralStructure;
        }
    }
}
