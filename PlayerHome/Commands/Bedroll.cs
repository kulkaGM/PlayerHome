using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerHome.Commands
{
    public class Home : IRocketCommand
    {
        public string Name => "home";
        public string Help => "Teleports you to your bed";
        public string Syntax => "";
        public List<string> Aliases => new List<string>() { "bedroll" };
        public List<string> Permissions => new List<string>() { "home", "bedroll" };
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public async void Execute(IRocketPlayer caller, string[] bed)
        {
            var player = caller as UnturnedPlayer;
            var playerId = player.CSteamID;
            bool alreadyTeleporting = PlayerHome.Instance.teleporting.Has(playerId);
            if (alreadyTeleporting)
            {
                var msg = PlayerHome.Instance.GetTranslation("alreadyTeleporting");
                PlayerHome.SendMessage(playerId, msg);
                return;
            }

            if (player.Stance == EPlayerStance.DRIVING || player.Stance == EPlayerStance.SITTING)
            {
                var msg = PlayerHome.Instance.GetTranslation("onDriving");
                PlayerHome.SendMessage(playerId, msg);
                return;
            }

            if (!BarricadeManager.tryGetBed(playerId, out _, out _))
            {
                var msg = PlayerHome.Instance.GetTranslation("noBed");
                PlayerHome.SendMessage(playerId, msg);
                return;
            }

            var delay = PlayerHome.Instance.Configuration.Instance.delay;

            PlayerHome.Instance.teleporting.Add(playerId);
            PlayerHome.SendMessage(playerId, PlayerHome.Instance.GetTranslation("teleporting").Replace("{seconds}", delay.ToString()));
            await Task.Delay(delay * 1000);
            bool isStillTeleporting = PlayerHome.Instance.teleporting.Has(playerId);
            if (!isStillTeleporting) return;
            PlayerHome.Instance.teleporting.Remove(playerId);

            // Yes we are getting bed again in case it gets destroyed whe waiting or reclaimed to other bed
            if (!BarricadeManager.tryGetBed(playerId, out var bedPosition, out var bedAngle))
            {
                var msg = PlayerHome.Instance.GetTranslation("noBed");
                PlayerHome.SendMessage(playerId, msg);
                return;
            }

            if (player.Dead)
            {
                PlayerHome.SendMessage(playerId, PlayerHome.Instance.GetTranslation("teleportCancel"));
                return;
            }

            bedPosition.y += .5f;
            player.Teleport(bedPosition, MeasurementTool.byteToAngle(bedAngle));
            PlayerHome.SendMessage(playerId, PlayerHome.Instance.GetTranslation("teleportedToBed"));
        }
    }
}
