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

        public Version Version => new Version(0,0,2);

        public MenuItem MenuItem => null;

        private PatchesReminderLogic _logic;
        private PatchesReminderDisplay _prd;

        public void OnButtonPress()
        {
            //throw new NotImplementedException();
        }

        private PatchesReminderLogic InitLogic()
        {
            var logic = new PatchesReminderLogic();
            logic.PatchesAttacked += (sender, args) => _prd.Hide();
            logic.PatchesEnter += (sender, cardName) =>
            {
                Log.Info($"Attack with {cardName}");
                //prd.DisplayText = $"ATTACK WITH {cardName.ToUpper()}";
                _prd.Show();
            };

            return logic;
        }

        private PatchesReminderDisplay InitDisplay()
        {
            var display = new PatchesReminderDisplay();
            GameEvents.OnGameEnd.Add(display.Hide);
            GameEvents.OnTurnStart.Add(a => display.Hide());

            return display;
        }

        public void OnLoad()
        {
            _prd = _prd ?? InitDisplay();
            Core.OverlayCanvas.Children.Add(_prd);

            _logic = _logic ?? InitLogic();
        }

        public void OnUnload()
        {
            Core.OverlayCanvas.Children.Remove(_prd);
            _prd.Hide();
            //throw new NotImplementedException();
        }

        public void OnUpdate()
        {
            //throw new NotImplementedException();
        }
    }
}