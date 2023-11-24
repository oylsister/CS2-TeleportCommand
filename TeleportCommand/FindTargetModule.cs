using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace TeleportCommand
{
    public class FindTargetModule : IFindTargetModule
    {
        private TeleportCommand _core;
        public FindTargetModule(TeleportCommand plugin)
        {
            _core = plugin;
        }

        public List<CCSPlayerController> FindTarget(CCSPlayerController? client, string targetname, bool destination = false)
        {
            var players = Utilities.GetPlayers();
            var target = new List<CCSPlayerController>();
            
            foreach(var player in players)
            {
                var playerPawn = player.PlayerPawn.Value;
                var isAlive = player.PawnIsAlive;

                // for all counter-terrorist
                if (string.Equals(targetname, "@ct") && !destination)
                {
                    if (playerPawn.TeamNum == 3 && isAlive)
                    {
                        target.Add(player);
                    }
                }
                // for all terrorist
                else if (string.Equals(targetname, "@t") && !destination)
                {
                    if (playerPawn.TeamNum == 2 && isAlive)
                    {
                        target.Add(player);
                    }
                }
                // for all player
                else if (string.Equals(targetname, "@all") && !destination)
                {
                    if (isAlive)
                    {
                        target = players;
                    }
                }
                // for yourself
                else if (string.Equals(targetname, "@me"))
                {
                    if (client != null && !client.PawnIsAlive) continue;
                    if (client != null) target.Add(client);
                    break;
                }
                else
                {
                    if (!player.PlayerName.Equals(targetname, StringComparison.OrdinalIgnoreCase) || !isAlive) continue;
                    target.Add(player);

                    if (destination) break;
                }
            }
            return target;
        }
    }
}
