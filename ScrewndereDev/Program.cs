using System;
using DiscordRPC;

namespace ScrewndereDev
{
    class Program
    {
        private const string DISCORD_CLIENT_ID = "560185502691491841"; // the magic value that was totally randomly generated...
        private const string PRESENCE_STATUS = "Awaiting Verification"; // oh and I totally got this by bruteforcing too...

        // A little wrapper around Console.WriteLine so I wouldn't need to type Console..... each time.
        private static void Writeline(string line)
        {
            Console.WriteLine(line);
        }

        private static void Main()
        {
            // If you see this text, that means your .NET Framework installation is not screwed up...
            Writeline("ScrewndereDev V1.0 - Now with 0% Spaghetti code!");
            Writeline("Initializing...");

            // Initialize a new RPC Client.
            DiscordRpcClient discord = new DiscordRpcClient(DISCORD_CLIENT_ID);

            // Define Callbacks.
            discord.OnReady += (sender, e) => Writeline(string.Format("Received Ready from user: {0}#{1}", e.User.Username, e.User.Discriminator));
            discord.OnPresenceUpdate += (sender, e) => Writeline(string.Format("Received new presence: {0}" + Environment.NewLine + "Now go type -verifyme", e.Presence.State));
            discord.OnRpcMessage += (sender, e) => Writeline(string.Format("Recieved RPC call: {0}", e.Type.ToString()));
            discord.OnJoinRequested += (sender, e) => Writeline(string.Format("Join request: {0}#{1}", e.User.Username, e.User.Discriminator));
            discord.OnError += (sender, e) => Writeline(string.Format("Error: {0}", e.Message));
            discord.OnSubscribe += (sender, e) => Writeline(string.Format("OnSubscribe: {0} : {1}", e.Event.ToString(), e.Type.ToString()));
            discord.OnConnectionEstablished += (sender, e) => Writeline(string.Format("Connection established (Pipe Ind): {0}", e.ConnectedPipe.ToString()));

            // Actually ask the library to do something (and print initialization's result).
            Writeline("Initialize() result: " + (discord.Initialize() ? "OK! :)" : "Error! :("));

            // Bypass the check
            discord.SetPresence(new RichPresence() { State = PRESENCE_STATUS });

            // And then we loop forever processing Discord's requests.
            while (true) discord.Invoke();
        }
    }
}