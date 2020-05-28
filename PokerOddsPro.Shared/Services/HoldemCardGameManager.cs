using System.Linq;
using System.Threading.Tasks;
using PokerOddsPro.Shared.ViewModels;
using PokerOddsPro.Shared.Dto.PokerDto.TexasHoldem;
using PokerOddsPro.OddsEngine;
using PokerOddsPro.Shared.Dto.PokerDto.General;

namespace PokerOddsPro.Shared.Services
{
    public class HoldemCardGameManager : PokerCardGameManager
    {
        public IncrementalOddsCalculator currentCalculator { get; set; }

        public HoldemCardGameManager(int numberOfPlayers, int minimumBoardCards = 0) : base(
            new HoldemBoardDetails(),
            numberOfBoardCards: 5,
            numberOfPlayers: numberOfPlayers)
        {
            MoveSelectedCardToNextBestOption();
            currentCalculator = null;
            _minimumBoardCards = minimumBoardCards;
        }

        private int _minimumBoardCards;

        internal override PlayerController CreateNewPlayer() => new PlayerController(2); 

        internal override CardSlot ComputeNextSelectedCard()
        {
            if (SelectedCardSlot != null)
            {
                var allPlayersHaveCards = Players.All(x => x.HasAllCards);
                if (allPlayersHaveCards || SelectedCardSlot.CardSlotType == CardSlotTypeEnum.BoardCardSlot)
                {
                    //If this card is a boardCard => return first available boardCard
                    var potentialCardSlot = BoardCardSlots.FirstOrDefault(x => x.Card == null);
                    if (potentialCardSlot != null || allPlayersHaveCards) return potentialCardSlot;
                }
                else
                {
                    //return first available specific player slot
                    var relevatntPlayer = Players.First(x => x.Cards.Contains(SelectedCardSlot));
                    var potentialCardSlot = relevatntPlayer.Cards.FirstOrDefault(x => x.Card == null);
                    if (potentialCardSlot != null) return potentialCardSlot;
                }

                //return first available player slot
                foreach (var Player in Players.Where(x => !x.HasAllCards))
                {
                    var potentialCardSlot = Player.Cards.FirstOrDefault(x => x.Card == null);
                    if (potentialCardSlot != null) return potentialCardSlot;
                }
            }

            return null;
        }

        private bool BoardIsValid()
        {
            var foundIncompleteStreet = false;
            foreach (var street in BoardDetails.streetDetails)
            {
                var indexesInStreet = Enumerable.Range(street.StreetStartCardIndex - 1, street.StreetEndCardIndex - street.StreetStartCardIndex + 1).ToList();

                var streetComplete = true;
                foreach (var index in indexesInStreet)
                {
                    if (streetComplete && BoardCardSlots[index].Card == null)
                    {
                        streetComplete = false;
                    }
                }

                if (!streetComplete)
                {
                    if (!foundIncompleteStreet)
                    {
                        foundIncompleteStreet = true;
                    }
                }
                else
                {
                    if (foundIncompleteStreet)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        internal override async Task RecalculateOdds()
        {
            // If possible, recompute the odds
            var isBoardValid = BoardIsValid();
            var atLeastTwoPlayersWithCards = Players.Count(x => x.HasAllCards) > 1;
            var enoughBoardCards = BoardCardSlots.Count(x => x.hasCard) >= _minimumBoardCards;

            if (isBoardValid && atLeastTwoPlayersWithCards && enoughBoardCards)
            {
                var playersWithEnoughCards = Players.Where(x => x.HasAllCards).ToList();
                var playerCards = playersWithEnoughCards.Select(p => p.Cards.Select(x => x.Card.CardInfo).ToList()).ToList();
                var boardCards = BoardCardSlots.Where(x => x.Card != null).Select(x => x.Card.CardInfo).ToList();

                var oddsComputer = new IncrementalOddsCalculator(BoardDetails, playerCards, boardCards);

                if (currentCalculator != null)
                {
                    if (oddsComputer.Key.Equals(currentCalculator?.Key ?? "")) return;
                    currentCalculator.IsCancellationRequested = true;
                }

                var firstStreet = true;
                currentCalculator = oddsComputer;

                var playersThatShouldBeCleared = Players.Where(x => !x.HasAllCards && x.HasResults).ToList();
                foreach (var player in playersThatShouldBeCleared)
                {
                    player.LatestResults = null;
                }

                foreach (var streetToCompute in oddsComputer.StreetsToCompute)
                {
                    var results = oddsComputer.HandOdds(streetToCompute);

                    if (firstStreet)
                    {
                        for (int i = 0; i < results.Count; i++)
                        {
                            var resultsForPlayer = results[i];
                            playersWithEnoughCards[i].LatestResults = resultsForPlayer;
                        }
                    }

                    await Task.Delay(1);
                    if (currentCalculator.IsCancellationRequested)
                    {
                        return;
                    }
                }
            }
            else
            {
                // Reset the player results
                foreach (var player in Players)
                {
                    player.LatestResults = null;
                }
            }
        }
    }
}
