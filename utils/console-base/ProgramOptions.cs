using System;
using System.Collections.Generic;

namespace Reboard.Utils.ConsoleBase
{
    public class ProgramOptions
    {
        internal string DisplayTitle { get; private set; }

        public ProgramOptions SetTitle(string title)
        {
            DisplayTitle = title;
            return this;
        }

    }
}
