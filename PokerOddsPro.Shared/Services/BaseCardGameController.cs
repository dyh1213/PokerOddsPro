using PokerOddsPro.Shared.Dto.PokerDto.General;
using PokerOddsPro.Shared.Helpers;
using PokerOddsPro.Shared.ViewModels;
using PokerOddsPro.OddsEngine.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerOddsPro.Shared.Services
{
    public abstract class PokerCardGameManager
    {
        public PokerCardGameManager(
            BaseBoardDetails boardDetails,
            int numberOfBoardCards,
            int numberOfPlayers)
        {
            BoardDetails = boardDetails;

            AllCards = Helper.GetAllCards();
            BoardCardSlots = Enumerable.Range(0, numberOfBoardCards).Select(x => new CardSlot() { CardSlotType = CardSlotTypeEnum.BoardCardSlot }).ToList();
            Players = new List<PlayerController>();
            foreach (var _ in Enumerable.Range(0, numberOfPlayers))
            {
                AddPlayer();
            }

            foreach (var card in BoardCardSlots)
            {
                card.IsSelectedCardSet += async (s, e) => await UpdateSelectedCard((CardSlot)s);
            }
        }

        internal void MoveSelectedCardToNextBestOption()
        {
            SelectedCardSlot = ComputeNextSelectedCard();
            if (SelectedCardSlot != null) SelectedCardSlot.IsSelected = true;
        }

        private List<Card> AllCards { get; set; }
        public List<CardSlot> BoardCardSlots { get; }
        public List<PlayerController> Players { get; }

        public BaseBoardDetails BoardDetails { get; }

        public bool AllCardSlotsFilled => BoardCardSlots.All(x => x.Card != null) && Players.SelectMany(x=>x.Cards).All(x => x.Card != null);

        // Abstract functions
        internal abstract CardSlot ComputeNextSelectedCard();

        // Manages the currently selected card slot
        private CardSlot _SelectedCardSlot { get; set; }
        internal CardSlot SelectedCardSlot
        {
            get { return _SelectedCardSlot; }
            set
            {
                if (_SelectedCardSlot != value)
                {
                    var updateFromNull = SelectedCardSlotUpdatedFromNull();

                    _SelectedCardSlot = value;

                    if (_SelectedCardSlot == null)
                    {
                        foreach (var card in AllCards)
                        {
                            card.IsAvailable = false;
                        }
                    }

                    selectedCardSlotUpdated?.Invoke(_SelectedCardSlot, EventArgs.Empty);

                    if (updateFromNull) selectedCardSlotUpdatedFromNull.Invoke(_SelectedCardSlot, EventArgs.Empty);
                }
            }
        }

        private bool SelectedCardSlotUpdatedFromNull()
        {
            if (_SelectedCardSlot == null)
            {
                var unavailableCards = GetUnavailableCards();
                foreach (var card in AllCards.Where(x => !unavailableCards.Contains(x)))
                {
                    card.IsAvailable = true;
                }
                return true;
            }
            return false;
        }

        public event EventHandler selectedCardSlotUpdatedFromNull;
        public event EventHandler selectedCardSlotUpdated;

        //Utility functions
        public List<Card> GetUnavailableCards()
        {
            var playerCards = Players.SelectMany(x => x.Cards).Where(x => x.Card != null).Select(x => x.Card).ToList();
            var boardCards = BoardCardSlots.Where(x => x.Card != null).Select(x => x.Card).ToList();
            playerCards.AddRange(boardCards);
            return playerCards;
        }

        internal virtual void ResetBoard()
        {
            foreach (var card in AllCards)
            {
                card.IsAvailable = true;
            }

            foreach (var player in Players)
            {
                player.LatestResults = null;

                foreach (var playerCard in player.Cards)
                {
                    playerCard.Card = null;
                    playerCard.IsSelected = false;
                }
            }

            foreach (var boardCard in BoardCardSlots)
            {
                boardCard.Card = null;
                boardCard.IsSelected = false;
            }

            SelectedCardSlot = null;
        }

        internal abstract PlayerController CreateNewPlayer();
        public async Task AddPlayer()
        {
            var newPlayer = CreateNewPlayer();
            var shouldBeSelected = SelectedCardSlot == null && AllCardSlotsFilled;

            Players.Add(newPlayer);

            foreach (var card in newPlayer.Cards)
            {
                card.IsSelectedCardSet += async (s, e) =>
                {
                    var item = (CardSlot)s;
                    await UpdateSelectedCard(item);
                };
            }

            await Task.Delay(1);

            if (shouldBeSelected)
            {
                newPlayer.Cards[0].IsSelected = true;
                //SelectedCardSlot = newPlayer.Cards[0];
            }
        }

        public virtual async Task RemovePlayer(PlayerController player)
        {
            Players.Remove(player);

            foreach(var card in player.Cards)
            {
                if (card.IsSelected)
                {
                    card.IsSelected = false;
                    SelectedCardSlot = null;
                }
            }

            var cardIds = player.Cards.Where(x => x.Card != null).Select(x => x.Card.CardInfo.CardId).ToList();

            if (SelectedCardSlot != null)
            {
                foreach (var deckCard in AllCards.Where(x => cardIds.Contains(x.CardInfo.CardId)))
                {
                    deckCard.IsAvailable = true;
                }
            }

            await RecalculateOdds();
        }

        internal async Task SetSelectedCard(Card card)
        {
            if (SelectedCardSlot != null)
            {
                SelectedCardSlot.IsSelected = false;
                SelectedCardSlot.Card = card;
                card.IsAvailable = false;
            }

            var cardSlot = ComputeNextSelectedCard();
            if (cardSlot != null)
            {
                cardSlot.IsSelected = true;
            }
            else
            {
                SelectedCardSlot = null;
            }


            await Task.Delay(1);

            await RecalculateOdds();
        }

        internal abstract Task RecalculateOdds();

        public IEnumerable<Card> GetCardsOfSuit(CardSuitEnum cardSuit)
        {
            return AllCards.Where(x => x.CardInfo.CardSuit == cardSuit);
        }

        public async Task UpdateSelectedCard(CardSlot cardSlot)
        {
            if (!cardSlot.IsSelected) throw new Exception();

            if (SelectedCardSlot != null)
            {
                SelectedCardSlot.IsSelected = false;
            }

            SelectedCardSlot = cardSlot;

            // In case the card was removed, decide if to re-compute odds
            if (cardSlot.Card != null)
            {
                cardSlot.Card.IsAvailable = true;
                cardSlot.Card = null;

                if (cardSlot.CardSlotType == CardSlotTypeEnum.PlayerCardSlot)
                {
                    var cardSlotOwner = Players.First(x => x.Cards.Contains(cardSlot));
                    var oneCardMissing = cardSlotOwner.Cards.Count - 1 == cardSlotOwner.NumberOfCards;

                    // If the card was the seconds (or last) card, then clear all odds and recompute odds.
                    if (!oneCardMissing)
                    {
                        return;
                    }
                }

                await RecalculateOdds();
            }

            return;
        }
    }
}
