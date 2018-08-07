using System;
using System.Collections.Generic;
using System.IO;
using Coc.Data.ControlAndConversionStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTestUtil;

namespace Coc.Data.OATS.UTest
{
    [TestClass]
    public class ParserToExcelOATSTest
    {
        private ParserToExcelOATS parser;
        [TestInitialize()]
        public void Initializer()
        {
            parser = new ParserToExcelOATS();
        }


        [TestMethod()]
        public void GenerateScriptTest()
        {
            DirectoryInfo testDataDirectory, testScriptDirectory, testOutputPath;
            List<GeneralUseStructure> generalUseStructure;

            testOutputPath = new DirectoryInfo("Result Files\\");
            generalUseStructure = StructureCollectionUtil.GetStructureCollectionActOats();

            FileUtil.CleanFolder(testOutputPath);

            parser.GenerateScript(generalUseStructure, testOutputPath.FullName);


            testDataDirectory = new DirectoryInfo(testOutputPath.FullName + "\\TestData");
            testScriptDirectory = new DirectoryInfo(testOutputPath.FullName + "\\TestScript");

            #region Check parsed files
            Assert.IsTrue(testDataDirectory.GetFiles().Length > 0, "No TestData was created");
            Assert.IsTrue(testScriptDirectory.GetFiles().Length > 0, "No TestScript was created");

            Assert.IsTrue(testDataDirectory.GetFiles()[0].Length > 0, "TestData size is zero");
            Assert.IsTrue(testScriptDirectory.GetFiles()[0].Length > 0, "TestScript size is zero");
            #endregion
        }

    }
}
