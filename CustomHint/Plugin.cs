using CustomHint.API;
using CustomHint.Configs;
using CustomHint.Handlers;
using CustomHint.Menus;
using CustomHint.Methods;
using Exiled.API.Features;
using MEC;
using System;

namespace CustomHint
{
    public class Plugin : Plugin<Config, Translation>
    {
        public static Plugin Instance { get; private set; }
        public PlayerEvents PlayerHandlers { get; private set; }
        public RoundEvents RoundEvents { get; private set; }
        public HintsSystem Hints { get; private set; }
        public AutoUpdater autoUpdater { get; private set; }
        public FilesGenerator filesGenerator { get; private set; }

        public override string Name => "CustomHint";
        public override string Author => "Narin & BTF Team";
        public override Version Version => new Version(1, 6, 5);
        public override Version RequiredExiledVersion => new Version(9, 8, 0);

        public override void OnEnabled()
        {
            Instance = this;

            DependenciesCheck.CheckAndDownloadDependencies().GetAwaiter().GetResult();

            autoUpdater = new AutoUpdater();

            filesGenerator ??= new FilesGenerator();
            filesGenerator.GenerateHintsFile();

            PlayerHandlers = new PlayerEvents();
            RoundEvents = new RoundEvents();
            Hints = new HintsSystem();


            Exiled.Events.Handlers.Server.WaitingForPlayers += RoundEvents.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted += RoundEvents.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += RoundEvents.OnRoundEnded;
            Exiled.Events.Handlers.Player.Verified += PlayerHandlers.OnPlayerVerified;

            //new Statistic().ConnectToServer();

            if (Config.HudSettings)
            {
                ServerHUDSettings.Register();
                Log.Debug("Menu system registered because HudSettings is enabled.");
            }
            else
            {
                Log.Debug("Menu system skipped — HudSettings disabled.");
            }

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= RoundEvents.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted -= RoundEvents.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= RoundEvents.OnRoundEnded;
            Exiled.Events.Handlers.Player.Verified -= PlayerHandlers.OnPlayerVerified;

            Timing.KillCoroutines(Hints.CurrentHint);

            Instance = null;
            PlayerHandlers = null;
            Hints = null;

            base.OnDisabled();
        }
    }
}