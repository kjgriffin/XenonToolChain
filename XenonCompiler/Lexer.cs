using System;
using System.IO;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XenonCompiler
{
    public class Lexer
    {

        private ImmutableList<string> tokens;
        private int index;
        private string file = string.Empty;
        private string comment;
        private string cblockstart;
        private string cblockend;

        public Lexer(List<string> keywords, string comment = "//", string cblockstart = "/*", string cblockend = "*/")
        {
            tokens = ImmutableList<string>.Empty;
            index = 0;
            // init keywords
            this.keywords = keywords;
            this.comment = comment;
            this.cblockstart = cblockstart;
            this.cblockend = cblockend;
        }

        public Lexer(string comment = "//", string cblockstart = "/*", string cblockend = "*/")
        {
            tokens = ImmutableList<string>.Empty;
            index = 0;
            // init keywords
            keywords = new List<string>();
            this.comment = comment;
            this.cblockstart = cblockstart;
            this.cblockend = cblockend;
        }

        public void ReadFile(string filename)
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
            file = filetext;
        }

        public void LexFile(string filename)
        {
            ReadFile(filename);
            // tokenize the string
            PreProcStripInlineComments(comment);
            PreProcStripMultilineComments(cblockstart, cblockend);
            Tokenize();
        }


        public List<string> keywords;

        public static List<string> trimers = new List<string>
        {
            " ",
            System.Environment.NewLine,
            "\n",
            "\r",
            "\t"
        };

       


        private void Tokenize()
        {
            // get rid of trimmers first
            string[] firstpass = file.Split(trimers.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            // split the text based on list of keywords into tokens
            foreach (string s in firstpass)
            {
                tokens = tokens.AddRange(s.SplitAndKeep(keywords));
            }
        }

        private void PreProcStripInlineComments(string commentkey)
        {
            string pattern = @"(.*?)" + Regex.Escape(commentkey) + @"(.*)";
            file = Regex.Replace(file, pattern, @"$1");
        }

        private void PreProcStripMultilineComments(string blockcommentstart, string blockcommentend)
        {
            // split by line
            // go throuch line by line if see line has it take start, then skip all lines until end
            string[] lines = file.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            List<string> strippedlines = new List<string>();

            bool inside = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (inside)
                {
                    if (lines[i].Contains(blockcommentend))
                    {
                        inside = false;
                        string pattern = @"(.*?)" + Regex.Escape(blockcommentend) + "(.*)";
                        // add everything after the line
                        strippedlines.Add(Regex.Replace(lines[i], pattern, @"$2"));
                    }
                }
                else
                {
                    if (lines[i].Contains(blockcommentstart))
                    {
                        inside = true;
                        string pattern = @"(.*?)" + Regex.Escape(blockcommentstart) + ".*";
                        // add everything before the line
                        strippedlines.Add(Regex.Replace(lines[i], pattern, @"$1"));
                    }
                    else
                    {
                        // add line
                        strippedlines.Add(lines[i]);
                    }
                }
            }
            // rebuild string
            StringBuilder sb = new StringBuilder();
            foreach (string line in strippedlines)
            {
                sb.AppendLine(line);
            }
            file = sb.ToString();
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
            return s.ToString().TrimEnd();
        }

    }
}
