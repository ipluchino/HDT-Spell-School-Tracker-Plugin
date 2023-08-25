using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace SpellSchoolCounter
{
    internal class SpellSchoolCounter
    {
        private SchoolCountWidget _cardListWidget = null;
        private List<SpellSchool> _schoolsPlayed = new List<SpellSchool>();
        private ObservableCollection<Card> _playedList = new ObservableCollection<Card>();

        public SpellSchoolCounter(SchoolCountWidget playerList)
        {
            _cardListWidget = playerList;

            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
            {
                _cardListWidget.Hide();
            }

        }

        // Triggers when a hearthstone game begins and resets the tracked spell school list
        internal void GameStart()
        {
            _schoolsPlayed = new List<SpellSchool>();
            _playedList = new ObservableCollection<Card>();
            _cardListWidget.Show();
            _cardListWidget.Update(_playedList);
        }

        // Triggers when a card is played by the player
        internal void OnPlayerPlay(Card card)
        {
            // Only display the card on the tracker if it has a spell school and it isn't already being tracked
            if (card.SpellSchool != SpellSchool.NONE && !_schoolsPlayed.Contains(card.SpellSchool))
            {
                _schoolsPlayed.Add(card.SpellSchool);
                _playedList.Add(card);
                _cardListWidget.Update(_playedList);
            }
        }

        // Triggers when the player is in a hearthstone menu
        internal void InMenu()
        {
            // Hides the tracker when in a hearthstone menu
            if (Config.Instance.HideInMenu)
            {
                _cardListWidget.Hide();

            }
        }
    }
}