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

            // Hide in menu, if necessary
            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
            {
                _cardListWidget.Hide();
            }

        }

        // Reset on when a new game starts
        internal void GameStart()
        {
            _schoolsPlayed = new List<SpellSchool>();
            _playedList = new ObservableCollection<Card>();
            _cardListWidget.Show();
            _cardListWidget.Update(_playedList);
        }

        internal void OnPlayerPlay(Card card)
        {
            if (card.SpellSchool != SpellSchool.NONE && !_schoolsPlayed.Contains(card.SpellSchool))
            {
                _schoolsPlayed.Add(card.SpellSchool);
                _playedList.Add(card);
                _cardListWidget.Update(_playedList);
            }
        }

        // Need to handle hiding the element when in the game menu
        internal void InMenu()
        {
            if (Config.Instance.HideInMenu)
            {
                _cardListWidget.Hide();

            }
        }
    }
}