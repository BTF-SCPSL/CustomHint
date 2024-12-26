using Exiled.API.Features;
using PlayerRoles;

namespace CustomHint
{
    public static class Methods
    {
        public static string GameRole(Player player)
        {
            //TODO: HORRIBLE SYSTEM, If it could look thru every RoleTypeId enum and check every translation it would be cool.
            //Flamingos arent added, didnt find it in enums.
            //Made by saskyc (blame me) :D

            switch(player.Role.Type)
            {
                case RoleTypeId.ClassD:
                    return Plugin.Instance.Translation.GameRoleClassD;

                case RoleTypeId.FacilityGuard:
                    return Plugin.Instance.Translation.GameRoleFacilityGuard;

                case RoleTypeId.Scientist:
                    return Plugin.Instance.Translation.GameRoleScientist;
                
                case RoleTypeId.Tutorial:
                    return Plugin.Instance.Translation.GameRoleTutorial;
                
                case RoleTypeId.Filmmaker:
                    return Plugin.Instance.Translation.GameRoleFilmmaker;
                
                case RoleTypeId.Overwatch:
                    return Plugin.Instance.Translation.GameRoleOverwatch;
                
                case RoleTypeId.NtfPrivate:
                    return Plugin.Instance.Translation.GameRoleNtfPrivate;
                
                case RoleTypeId.NtfSergeant:
                    return Plugin.Instance.Translation.GameRoleNtfSergeant;
                
                case RoleTypeId.NtfSpecialist:
                    return Plugin.Instance.Translation.GameRoleNtfSpecialist;
                
                case RoleTypeId.NtfCaptain:
                    return Plugin.Instance.Translation.GameRoleNtfCaptain;
                
                case RoleTypeId.ChaosConscript:
                    return Plugin.Instance.Translation.GameRoleChaosConscript;
                
                case RoleTypeId.ChaosRifleman:
                    return Plugin.Instance.Translation.GameRoleChaosRifleman;
                
                case RoleTypeId.ChaosRepressor:
                    return Plugin.Instance.Translation.GameRoleChaosRepressor;
                
                case RoleTypeId.ChaosMarauder:
                    return Plugin.Instance.Translation.GameRoleChaosMarauder;

                case RoleTypeId.Scp049:
                    return Plugin.Instance.Translation.GameRoleScp049;
                
                case RoleTypeId.Scp0492:
                    return Plugin.Instance.Translation.GameRoleScp0492;
                
                case RoleTypeId.Scp079:
                    return Plugin.Instance.Translation.GameRoleScp079;
                
                case RoleTypeId.Scp096:
                    return Plugin.Instance.Translation.GameRoleScp096;
                
                case RoleTypeId.Scp106:
                    return Plugin.Instance.Translation.GameRoleScp106;
                
                case RoleTypeId.Scp173:
                    return Plugin.Instance.Translation.GameRoleScp173;
                
                case RoleTypeId.Scp939:
                    return Plugin.Instance.Translation.GameRoleScp939;
            }

            return $"{player.Role}";
            
        }
    }
}
