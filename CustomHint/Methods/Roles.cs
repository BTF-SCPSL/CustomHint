using System.Linq;
using Exiled.API.Features;
using PlayerRoles;

namespace CustomHint.Methods
{
    public class Roles
    {
        public static string GameRole(Player player)
        {
            var role = Plugin.Instance.Translation.GameRoles
                .FirstOrDefault(r => r.Role == player.Role.Type);

            return role?.Name ?? player.Role.Type.ToString();
        }

        public static RoleCounts CountRoles()
        {
            var counts = new RoleCounts();

            foreach (Player player in Player.List)
            {
                switch (player.Role.Type)
                {
                    case RoleTypeId.ClassD:
                        counts.ClassD++;
                        break;
                    case RoleTypeId.Scientist:
                        counts.Scientist++;
                        break;
                    case RoleTypeId.FacilityGuard:
                        counts.FacilityGuard++;
                        break;
                    case RoleTypeId.Spectator:
                        counts.Spectators++;
                        break;
                    case RoleTypeId.Overwatch:
                        if (Plugin.Instance.Config.EnableOverwatchCounting)
                            counts.Spectators++;
                        break;
                    default:
                        break;
                }

                switch (player.Role.Team)
                {
                    case Team.FoundationForces:
                        if (player.Role.Type != RoleTypeId.FacilityGuard && player.Role.Type != RoleTypeId.Scientist)
                            counts.MTF++;
                        break;
                    case Team.ChaosInsurgency:
                        if (player.Role.Type != RoleTypeId.ClassD)
                            counts.ChaosInsurgency++;
                        break;
                    case Team.SCPs:
                        counts.SCPs++;
                        break;
                    default:
                        break;
                }
            }

            return counts;
        }

        public struct RoleCounts
        {
            public int ClassD;
            public int Scientist;
            public int FacilityGuard;
            public int MTF;
            public int ChaosInsurgency;
            public int SCPs;
            public int Spectators;
        }
    }
}
