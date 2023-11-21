using CounterStrikeSharp.API.Core;

namespace TeleportCommand
{
    public interface IFindTargetModule
    {
        public List<CCSPlayerController> FindTarget(string targetname, bool destionation = false);
    }
}