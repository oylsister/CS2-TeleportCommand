using CounterStrikeSharp.API.Core;

namespace TeleportCommand
{
    public interface IFindTargetModule
    {
        List<CCSPlayerController> FindTarget(string targetname, bool destionation = false);
    }
}