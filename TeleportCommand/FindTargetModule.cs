﻿using System;
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

        public List<CCSPlayerController> FindTarget(string targetname, bool destination = false)
        {
            List<CCSPlayerController> targetlist = new List<CCSPlayerController>();

            for(int i = 1; i < Server.MaxPlayers; i++) 
            {
                CCSPlayerController player = Utilities.GetPlayerFromIndex(i);
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