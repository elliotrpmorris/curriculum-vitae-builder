namespace Chest.Core.Command
{
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Interface for the command bus.
    /// </summary>
    public interface ICommandBus
    {
        Task Send(JObject command, string commandName, CommandMetadata metadata, bool validateOnly = false);
    }
}
