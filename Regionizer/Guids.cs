// Guids.cs
// MUST match guids.h
using System;

namespace DataJuggler.Regionizer
{
    static class GuidList
    {
        public const string guidRegionizerPkgString = "d44d0c72-ce3a-49c6-8b35-25b2a7c3907b";
        public const string guidRegionizerCmdSetString = "99470ae8-db16-4ebd-8bc2-19f709cdbc39";
        public const string guidToolWindowPersistanceString = "05b79354-eae9-49a9-8cb2-bcaa9f1ab4de";

        public static readonly Guid guidRegionizerCmdSet = new Guid(guidRegionizerCmdSetString);
    };
}