using System;
using System.IO;
using CommandLine;

namespace PokemonCLI
{

    class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "verbose output")]
        public bool Verbose { get; set; }

        [Option('f', "filename", Required = true, HelpText = "save data filename")]
        public string Filename { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var parseResult = Parser.Default.ParseArguments<Options>(args);
            switch (parseResult.Tag)
            {
                case ParserResultType.Parsed:
                    var parsed = parseResult as Parsed<Options>;
                    Options opt = parsed.Value;

                    var filename = opt.Filename;
                    var sfm = new SaveFileManager();
                    var isSAV = sfm.LoadFile(filename);
                    if (!isSAV)
                    {
                        Console.WriteLine("not a SAVE file. Aborting.");
                        return;
                    }
                    Console.WriteLine("SAVE file loaded.");
                    sfm.ListPokemon();
                    break;
            }
        }
    }
}