using PlayerHome.Util;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Reflection;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace PlayerHome
{
    public class PlayerHome : RocketPlugin<Config>
    {
        public static PlayerHome Instance;
        public Teleporting teleporting;
        protected override void Load()
        {
            Instance = this;
            teleporting = new Teleporting();
            if (Instance.Configuration.Instance.cancelOnMove) UnturnedPlayerEvents.OnPlayerUpdatePosition += OnMoved;
            Logger.Log($"{Assembly.GetName().Version} loaded!");
        }

        protected override void Unload()
        {
            if (Instance.Configuration.Instance.cancelOnMove) UnturnedPlayerEvents.OnPlayerUpdatePosition -= OnMoved;
            Instance = null;
            teleporting = null;
            Logger.Log($"unloaded!");
        }
        private void OnMoved(UnturnedPlayer player, Vector3 position)
        {
            bool beingTeleported = Instance.teleporting.Has(player.CSteamID);
            if (beingTeleported)
            {
                Instance.teleporting.Remove(player.CSteamID);
                var msg = GetTranslation("onMoved");
                SendMessage(player.CSteamID, msg);
            }
        }
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "onDriving", "{color=green}[Home]{/color} You cannot be teleported when being in a vehicle" },
            { "noBed", "{color=green}[Home]{/color} You have no bed" },
            { "alreadyTeleporting", "{color=green}[Home]{/color} You are already being teleported to your bed" },
            { "teleportCancel", "{color=green}[Home]{/color} Unable to teleport you to bed, you died" },
            { "teleporting", "{color=green}[Home]{/color} You will be teleported to your bed in {seconds} seconds" },
            { "teleportedToBed", "{color=green}[Home]{/color} You've been teleported to your bed" },
            { "onMoved", "{color=green}[Home]{/color} Teleport canceled, you moved" }
        };
        // this exists because we want to use custom placeholders and rich text
        public string GetTranslation(string translationKey)
        {
            string text = Translations.Instance[translationKey];
            if (string.IsNullOrEmpty(text))
            {
                Logger.Log($"unable to get translationKey {translationKey}");
                return DefaultTranslations[translationKey];
            }
            return text;
        }
        public static void SendMessage(CSteamID steamId, string message)
        {
            ChatManager.say(steamId, message.Replace("{", "<").Replace("}", ">"), Color.white, EChatMode.WELCOME, true);
        }
        public static void LogError(string message)
        {
            Logger.LogError($"{Instance.Assembly.GetName().Name} >> " + message);
        }
    }
}
