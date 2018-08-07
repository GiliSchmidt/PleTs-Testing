using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTestUtil;

namespace Coc.Data.OATS.UTest
{
    [TestClass]
    public class ParserToOATSTest
    {
        private ParserToOATS parser;

        [TestInitialize()]
        public void Initializer()
        {
            parser = new ParserToOATS();
        }


        [TestMethod()]
        public void GenerateScriptTest()
        {
            DirectoryInfo testOutputFolder;
            String xmiPath;

            testOutputFolder = new DirectoryInfo("TestOutput");

            if (!testOutputFolder.Exists)
            {
                testOutputFolder.Create();
            }

            xmiPath = "..\\..\\..\\TestFiles\\AOTS_certo.xml";

            FileUtil.CleanFolder(testOutputFolder);

            parser.ParseXmiToOATS(xmiPath, testOutputFolder.FullName);

            #region Check parsed files
            Assert.IsTrue((testOutputFolder.GetFiles().Length > 0), "Nothing was parsed");
            Assert.IsTrue(testOutputFolder.GetFiles()[0].Length > 0, "Nothing was parsed. File size is zero");

            Assert.Fail("NOT WORKING - Generated script is empy!");
            #endregion
        }
    }
}
