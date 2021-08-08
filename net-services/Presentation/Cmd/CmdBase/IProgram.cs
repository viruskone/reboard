using System.Threading.Tasks;

namespace Reboard.Presentation.Cmd.CmdBase
{
    public interface IProgram
    {
        public Task Run(CommandLineMethods methods);
    }
}