using System;
using System.Collections.Generic;
using System.Text;

namespace XenonCompiler
{
    static class StringExtensions
    {
        public static List<string> SplitAndKeep(this string s, List<string> delimiters)
        {

            char[] schars = s.ToCharArray();

            // try matching all delimiters against the first part of s
            int index = 0;
            int lastmatch = 0;

            List<string> tokens = new List<string>();

            while (index < schars.Length)
            {
                foreach (string delim in delimiters)
                {
                    char[] dchars = delim.ToCharArray();

                    // try matching schars to dchars
                    if (schars.Length - index >= dchars.Length)
                    {
                        bool fullmatch = true;
                        for (int i = 0; i < dchars.Length; i++)
                        {
                            if (schars[i + index] != dchars[i])
                            {
                                fullmatch = false;
                            }
                        }
                        if (fullmatch)
                        {
                            // match
                            if (lastmatch + index <= schars.Length)
                            {
                                string precede = new string(schars, lastmatch, index);
                                if (precede != string.Empty) tokens.Add(precede);
                            }
                            string match = new string(schars, index, dchars.Length);
                            tokens.Add(match);
                            // update indicies
                            lastmatch = index + dchars.Length;
                            index += dchars.Length -1;
                            break;
                        }
                    }
                }
                index = index + 1;
            }
            if (lastmatch < schars.Length)
            {
                string postmatch = new string(schars, lastmatch, schars.Length - lastmatch);
                tokens.Add(postmatch);
            }
            return tokens;
        }
    }
}
