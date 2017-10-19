using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace PatchesReminder
{
    public class PatchesReminderPlugin : IPlugin
    {
        public string Name => "Patches Reminder";

        public string Description => "Reminds you to attack with Patches";

        public string ButtonText => "Settings";

        public string Author => "Martin Johansson";

        public Version Version => new Version(0,0,1);

        public MenuItem MenuItem => null;

        private PatchesReminderLogic _logic;
        private PatchesReminderDisplay _prd;

        public void OnButtonPress()
        {
            //throw new NotImplementedException();
        }

        public void OnLoad()
        {
            _prd = new PatchesReminderDisplay();
            Core.OverlayCanvas.Children.Add(_prd);
            _logic = new PatchesReminderLogic();
            _logic.PatchesAttacked += (sender, args) => _prd.Hide();
            _logic.PatchesEnter += (sender, cardName) =>
            {
                Log.Info($"Attack with {cardName}");
                //prd.DisplayText = $"ATTACK WITH {cardName.ToUpper()}";
                _prd.Show();
            };
            GameEvents.OnPlayerPlay.Add(_logic.PlayerPlay);

            GameEvents.OnPlayerDeckToPlay.Add(_logic.PlayerDeckToPlay);
            GameEvents.OnPlayerMinionAttack.Add(_logic.PlayerMinionAttack);
            GameEvents.OnGameEnd.Add(_prd.Hide);
            GameEvents.OnTurnStart.Add( a => _prd.Hide());
        }

        public void OnUnload()
        {
            _prd.Hide();
            //throw new NotImplementedException();
        }

        public void OnUpdate()
        {
            //throw new NotImplementedException();
        }
    }
}