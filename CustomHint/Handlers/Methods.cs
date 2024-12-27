using System.Linq;
using Exiled.API.Features;

namespace CustomHint.Handlers
{
    public static class Methods
    {
        public static string GameRole(Player player)
        {
            var role = Plugin.Instance.Translation.GameRoles
                .FirstOrDefault(r => r.Role == player.Role.Type);

            return role?.Name ?? player.Role.Type.ToString();
        }
    }
}