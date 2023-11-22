using System.ComponentModel.DataAnnotations;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Admin;

namespace TeleportCommand;
public class TeleportCommand : BasePlugin
{
    public override string ModuleName => "CS2 Teleport Command";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "Oylsister";
    public override string ModuleDescription => "Advanced Teleport Command for Counter-Strike 2";

    private FindTargetModule _findTarget;

    public TeleportCommand()
    {
        _findTarget = new FindTargetModule(this);
    }

    public override void Load(bool hotReload)
    {
        AddCommand("teleport", "Teleport Player to Player", Command_Teleport);
        AddCommand("tele", "Teleport Player to Player", Command_Teleport);
        AddCommand("bring", "Bring Player To User", Command_Bring);
    }

    [RequiresPermissions("@css/slay")]
    private void Command_Teleport(CCSPlayerController? client, CommandInfo info)
    {
        if (info.ArgCount <= 0) 
        {
            info.ReplyToCommand("[Teleport] Usage: css_teleport <client> <dest-client>");
            return;
        }

        // Get the client name that you want to be teleported.
        List<CCSPlayerController> targetname = _findTarget.FindTarget(client!, info.GetArg(0));

        // Get the client name that you want to be destination.
        List<CCSPlayerController> destTemp = _findTarget.FindTarget(client!, info.GetArg(1), true);

        if (targetname.Count <= 0)
        {
            info.ReplyToCommand("[Teleport] Invalid target name.");
            return;
        }

        if (destTemp.Count <= 0)
        {
            info.ReplyToCommand("[Teleport] Invalid target destination name.");
            return;
        }

        // target destination can only be one.
        CCSPlayerController targetdestination = destTemp[0];

        // Find the angle and position.
        CCSPlayerPawn playerPawn = targetdestination.PlayerPawn.Value;
        Vector Position = playerPawn.AbsOrigin!;
        QAngle Angle = playerPawn.AbsRotation!;

        foreach(var player in targetname)
        {
            // Teleport all of them
            CCSPlayerPawn targetPawn = player.PlayerPawn.Value;
            targetPawn.Teleport(Position, Angle, new Vector(0f, 0f, 0f));
        }

        info.ReplyToCommand("[Teleport] Successfully Teleport Client.");
        return;
    }

    [RequiresPermissions("@css/slay")]
    private void Command_Bring(CCSPlayerController? client, CommandInfo info)
    {
        if (info.ArgCount <= 0)
        {
            info.ReplyToCommand("[Teleport] Usage: css_bring <client>");
            return;
        }

        // Get the client name that you want to be teleported.
        List<CCSPlayerController> targetname = _findTarget.FindTarget(client!, info.GetArg(0));

        if (targetname.Count <= 0)
        {
            info.ReplyToCommand("[Teleport] Invalid target name.");
            return;
        }

        // Find the angle and position.
        CCSPlayerPawn playerPawn = client!.PlayerPawn.Value;
        Vector Position = playerPawn.AbsOrigin!;
        QAngle Angle = playerPawn.AbsRotation!;

        foreach (var player in targetname)
        {
            // Teleport all of them
            CCSPlayerPawn targetPawn = player.PlayerPawn.Value;
            targetPawn.Teleport(Position, Angle, new Vector(0f, 0f, 0f));
        }

        info.ReplyToCommand("[Teleport] Successfully Teleport bring client to you.");
        return;
    }
}
