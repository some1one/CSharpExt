﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noggog.Utility.ArgsQuerying
{
    public class ArgsQueryConsoleManager : ArgsQueryManager
    {
        public ArgsQueryConsoleManager(string[] args) 
            : base(Prompt, Read, args)
        {
        }

        public ArgsQueryConsoleManager(IEnumerable<string> args) 
            : base(Prompt, Read, args)
        {
        }

        private static void Prompt(string prompt)
        {
            System.Console.WriteLine(prompt);
        }

        private static string Read()
        {
            return System.Console.ReadLine();
        }
    }
}
