using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace PatchesReminder
{
    public class PatchesReminderLogic
    {
        //public delegate void PEEventHandler(string cardName);
        /// <summary>
        /// Triggers when Patches enters the Battlefield from the Deck
        /// </summary>
        public event EventHandler<string> PatchesEnter;
        /// <summary>
        /// Triggers when Patches no longer can attack
        /// </summary>
        public event EventHandler PatchesAttacked;

        private readonly HashSet<string> _attackers = new HashSet<string>();

        public PatchesReminderLogic()
        {
            GameEvents.OnPlayerPlay.Add(PlayerPlay);

            GameEvents.OnPlayerDeckToPlay.Add(PlayerDeckToPlay);
            GameEvents.OnPlayerMinionAttack.Add(PlayerMinionAttack);
        }

        public void PlayerDeckToPlay(Card card)
        {
            if (card.Mechanics.Contains("charge", StringComparer.InvariantCultureIgnoreCase))
            {
                //Notify player to attack with patches
                if (_attackers.Add(card.Name))
                {
                    OnPatchesEnter(card.Name);
                }
            }
        }

        public void PlayerMinionAttack(AttackInfo attackInfo)
        {
            //Check if Patches still can attack
            //If Patches can not, stop telling player to attack with patches

            //Check if the player attacked with Patches
            if (_attackers.Contains(attackInfo.Attacker.Name))
            {
                _attackers.Remove(attackInfo.Attacker.Name);
                OnPatchesAttacked();
            }
        }

        public void PlayerPlay(Card card)
        {
            //Hearthstone_Deck_Tracker.API.Core.Game.Player.PlayerCardList.Any(c => c.Id == HearthDb.CardIds.Collectible.Neutral.PatchesThePirate)
            //Hearthstone_Deck_Tracker.API.Core.Game.Player.Deck.Any(e => e.CardId == HearthDb.CardIds.Collectible.Neutral.PatchesThePirate)
            if (card.Race == HearthDbConverter.RaceConverter(HearthDb.Enums.Race.PIRATE) &&
                Hearthstone_Deck_Tracker.API.Core.Game.Player.PlayerCardList.Any(c => c.Id == HearthDb.CardIds.Collectible.Neutral.PatchesThePirate && c.Count > 0))
            {
                if (Hearthstone_Deck_Tracker.API.Core.Game.Player.Board.Count(e => e.GetTag(HearthDb.Enums.GameTag.ZONE_POSITION) > 0) < 6)
                {
                    Card patches = Hearthstone_Deck_Tracker.API.Core.Game.Player.PlayerCardList.First(c => c.Id == HearthDb.CardIds.Collectible.Neutral.PatchesThePirate);
                    //Hearthstone_Deck_Tracker.API.Core.Game.Player.
                    _attackers.Add(patches.Name);
                    OnPatchesEnter(patches.Name);
                }
            }
            //if (Hearthstone_Deck_Tracker.API.Core.Game.Player.Deck.Any(e => e.Card.Name == "Patches the Pirate"))

            //throw new NotImplementedException();
        }

        protected virtual void OnPatchesEnter(string cardName)
        {
            PatchesEnter?.Invoke(this, cardName);
        }

        protected virtual void OnPatchesAttacked()
        {
            PatchesAttacked?.Invoke(this, EventArgs.Empty);
        }
    }
}