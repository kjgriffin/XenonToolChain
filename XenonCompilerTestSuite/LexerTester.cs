using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenonCompiler;
using System.IO;
using System;
using System.Collections.Generic;

namespace XenonCompilerTestSuite
{
    [TestClass]
    public class LexerTester
    {
        [TestMethod]
        public void TestSingleWordSplit()
        {
            Lexer lexer = new Lexer(new List<string>{ "test" });
            lexer.LexFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Source\Lexer\", "s01.txt"));
            string golden = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Golden\Lexer\", "s01.txt"));
            Console.WriteLine("Dump:");
            Console.Write(lexer.Dump());
            Assert.AreEqual(golden, lexer.Dump());
        }

        [TestMethod]
        public void TestMultiWordSplit()
        {
            Lexer lexer = new Lexer(new List<string> { "module", "{", "}" });
            lexer.LexFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Source\Lexer\", "s02.txt"));
            string golden = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Golden\Lexer\", "s02.txt"));
            Console.WriteLine("Dump:");
            Console.Write(lexer.Dump());
            Assert.AreEqual(golden, lexer.Dump());
        }

        [TestMethod]
        public void TestMultiWordSplitWithSingleLetters()
        {
            Lexer lexer = new Lexer(new List<string> { "module", "{", "}", "a", "." });
            lexer.LexFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Source\Lexer\", "s03.txt"));
            string golden = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Golden\Lexer\", "s03.txt"));
            Console.WriteLine("Dump:");
            Console.Write(lexer.Dump());
            Assert.AreEqual(golden, lexer.Dump());
        }

        [TestMethod]
        public void TestOrderPrecedence()
        {
            Lexer lexer = new Lexer(new List<string> { "=>", ">"});
            lexer.LexFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Source\Lexer\", "s04.txt"));
            string golden = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Golden\Lexer\", "s04.txt"));
            Console.WriteLine("Dump:");
            Console.Write(lexer.Dump());
            Assert.AreEqual(golden, lexer.Dump());
        }

        [TestMethod]
        public void TestRemoveInlineComments()
        {
            Lexer lexer = new Lexer();
            lexer.LexFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Source\Lexer\", "s05.txt"));
            string golden = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Golden\Lexer\", "s05.txt"));
            Console.WriteLine("Dump:");
            Console.Write(lexer.Dump());
            Assert.AreEqual(golden, lexer.Dump());
        }

        [TestMethod]
        public void TestRemoveBlockComments()
        {
            Lexer lexer = new Lexer();
            lexer.LexFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Source\Lexer\", "s06.txt"));
            string golden = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Golden\Lexer\", "s06.txt"));
            Console.WriteLine("Dump:");
            Console.Write(lexer.Dump());
            Assert.AreEqual(golden, lexer.Dump());
        }

    }
}
