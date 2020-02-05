using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PKHeX.Core;

namespace PokemonCLI
{
    class SaveFileManager
    {
        private FilteredGameDataSource datasource;
        private SAV8SWSH savedata;

        public bool HaX { get; private set; }

        private static bool IsSaveFile(string path)
        {
            object obj = FileUtil.GetSupportedFile(path);
            return obj != null && Path.GetExtension(path) == ".sav";
        }

        private bool OpenSAV(SaveFile sav, string path)
        {
            if (sav is SAV8SWSH z)
            {
                var shift = z.Game + (GameVersion.SW - GameVersion.SN);
                if (shift == (int)GameVersion.SW || shift == (int)GameVersion.SH)
                {
                    z.Game = shift;
                }
                sav.SetFileInfo(path);
                PKMConverter.SetPrimaryTrainer(sav);
                datasource = new FilteredGameDataSource(sav, GameInfo.Sources, HaX);
                savedata = (SAV8SWSH)z;

                return true;
            }
            else return false;
        }

        public bool LoadFile(string path)
        {
            if (!IsSaveFile(path)) return false;
            object obj = FileUtil.GetSupportedFile(path);
            if (obj is SaveFile s)
            {
                OpenSAV(s, path);
                return true;
            }
            return false;
        }

        public void ListPokemon()
        {

            var strings = GameInfo.GetStrings("ja");

            foreach (var monster in savedata.PartyData)
            {
                Console.WriteLine(string.Join("\t",
                    ((LanguageID)monster.Language).ToString(),
                    monster.CurrentLevel.ToString(),
                    SpeciesName.GetSpeciesName(monster.Species, 1),
                    monster.Nickname.ToString(),
                    monster.OT_Name));

                var moves = "";
                foreach (var move in monster.Moves)
                {
                    moves += strings.movelist[move] + "\t";
                }
                Console.WriteLine(moves);
            }
        }

    }
}