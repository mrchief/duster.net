// Guids.cs
// MUST match guids.h

using System;

namespace Duster.Net
{
    static class GuidList
    {
        public const string GuidPkgString = "2b38f76b-0df7-4a21-b3f2-1d55547265b9";
        public const string GuidCmdSetString = "6cd513bb-35a2-4911-b302-4aef9d83ff74";

		public static readonly Guid GuidCmdSet = new Guid(GuidCmdSetString);
    };
}