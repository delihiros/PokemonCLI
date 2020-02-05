# PokemonCLI

Command Line Interface for PKHeX.

Only focus on Gen8(SWSH).

PKHeX is an excellent Pokemon save editor written in C#.
PKHeX is a Windows dependent application so in order to run on other OS we could use Wine.
Bad news is that Wine uses 32-bit code internally which is making it difficult to keep up with OS version upgrades these days.
So I decided to use only the core part of code in PKHeX which is independent from Windows environment.

The main motivation of this project is to use PKHeX's feature on other OS environment, but I also would like to provide a JSON/Web interface to make it easier to manipulate Pokemon SWSH data from programming languages other than C#.