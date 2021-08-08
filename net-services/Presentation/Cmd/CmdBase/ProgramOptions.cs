namespace Reboard.Presentation.Cmd.CmdBase
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