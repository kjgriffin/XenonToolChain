using System;
using System.IO;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Text;

namespace XenonCompiler
{
    public class Lexer
    {

        private ImmutableList<string> tokens;
        private int index;

        public Lexer()
        {
            tokens = ImmutableList<string>.Empty;
            index = 0;
        }

        public void LexFile(string filename)
        {
            string filetext = string.Empty;
            try
            {
                filetext = File.ReadAllText(filename);
            }
            catch
            {
                Console.WriteLine("Failed to open file {0}", filename);
            }
            // tokenize the string
            Tokenize(filetext);
        }


        public static List<string> keywords = new List<string>
        {
            "module",
            "port",
            "link",
            "map",
            "input",
            "output",
            "#import",
            "#make",
        };

       


        private void Tokenize(string text)
        {
            // split the text based on list of keywords into tokens

        }


        public bool Peek(params string[] tokens)
        {
            throw new NotImplementedException();
        }

        public bool PeekID()
        {
            throw new NotImplementedException();
        }

        public bool PeekEOF()
        {
            throw new NotImplementedException();
        }

        public void Consume(params string[] tokens)
        {

        }

        public void ConsumeID()
        {

        }

        public void ConsumeEOF()
        {

        }

        public string Dump()
        {
            StringBuilder s = new StringBuilder();
            foreach (var t in tokens)
            { 
                s.Append(t);
                s.Append(Environment.NewLine);
            }
            return s.ToString();
        }

    }
}
