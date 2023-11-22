using CounterStrikeSharp.API.Core;

namespace TeleportCommand
{
    public interface IFindTargetModule
    {
        public List<CCSPlayerController> FindTarget(CCSPlayerController client, string targetname, bool destination = false);
    }
}