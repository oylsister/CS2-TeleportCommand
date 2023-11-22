using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace TeleportCommand
{
    public class FindTargetModule : IFindTargetModule
    {
        private TeleportCommand _core;
        public FindTargetModule(TeleportCommand plugin)
        {
            _core = plugin;
        }

        public List<CCSPlayerController> FindTarget(CCSPlayerController client, string targetname, bool destination = false)
        {
            List<CCSPlayerController> targetlist = Utilities.GetPlayers();

            foreach(var player in targetlist)
            {
                CCSPlayerPawn playerPawn = player.PlayerPawn.Value;
                bool IsAlive = player.PawnIsAlive;

                // for all counter-terrorist
                if (string.Equals(targetname, "@ct") && !destination)
                {
                    if (playerPawn.TeamNum == 3 && IsAlive)
                    {
                        targetlist.Add(player);
                    }
                }
                // for all terrorist
                else if (string.Equals(targetname, "@t") && !destination)
                {
                    if (playerPawn.TeamNum == 2 && IsAlive)
                    {
                        targetlist.Add(player);
                    }
                }
                // for all player
                else if (string.Equals(targetname, "@all") && !destination)
                {
                    if (IsAlive)
                    {
                        targetlist.Add(player);
                    }
                }
                // for yourself
                else if (string.Equals(targetname, "@me"))
                {
                    if (client.PawnIsAlive)
                    {
                        targetlist.Add(client);
                        break;
                    }
                }
                else
                {
                    StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
                    if (player.PlayerName.Contains(targetname, stringComparison) && IsAlive)
                    {
                        targetlist.Add(player);

                        if (destination) break;
                    }
                }
            }
            return targetlist;
        }
    }
}
