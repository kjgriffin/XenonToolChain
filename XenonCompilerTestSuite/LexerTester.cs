using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenonCompiler;
using System.IO;
using System;

namespace XenonCompilerTestSuite
{
    [TestClass]
    public class LexerTester
    {
        [TestMethod]
        public void TestSplitting()
        {
            Lexer lexer = new Lexer();
            lexer.LexFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Source\", "s01.txt"));
            string golden = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Golden\", "s01.txt"));
            Console.WriteLine("Dump:");
            Console.Write(lexer.Dump());
            Assert.AreEqual(golden, lexer.Dump());
        }
    }
}
