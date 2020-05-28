using PokerOddsPro.OddsEngine.Dto;
using PokerOddsPro.OddsEngine.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerOddsPro.OddsEngine
{
    public class IncrementalOddsCalculator
    {
        public IncrementalOddsCalculator(IBoardDetails streetsDescriptor, List<List<CardInfo>> playerCards, List<CardInfo> boardCards, List<CardInfo> deadCardsInput = null)
        {
            PlayerCards = playerCards;
            BoardCards = boardCards;
            DeadCardsInput = deadCardsInput;

            _streetsToCompute = streetsDescriptor.streetDetails.Where(x => x.StreetEndCardIndex >= boardCards.Count).OrderByDescending(x => x.StreetEndCardIndex).ToList();

            var stringAgg = new StringBuilder();
            var playerCardsKey = string.Join("_", playerCards.Select(x => $"{x[0].computeString()}-{x[1].computeString()}"));
            stringAgg.Append(playerCardsKey);

            if (boardCards.Any())
            {
                stringAgg.Append("_");
                var boardCardsKey = string.Join("_", boardCards.Select(x => x.computeString()));
                stringAgg.Append(boardCardsKey);
            }
            if (deadCardsInput != null && deadCardsInput.Any())
            {
                stringAgg.Append(" ");
                var deadCardsKey = string.Join("_", deadCardsInput.Select(x => x.computeString()));
                stringAgg.Append(deadCardsKey);
            }

            Key = stringAgg.ToString();
        }

        public string Key { get; }

        private List<PlayerOdds> PlayerTracking { get; set; }
        private ulong deadcards_mask { get; set; }
        private ulong boardmask { get; set; }
        private List<ulong> playerMasks { get; set; }

        private bool initialized = false;
        private List<List<CardInfo>> PlayerCards { get; set; }
        private List<CardInfo> BoardCards { get; set; }
        private List<CardInfo> DeadCardsInput { get; set; }
        public bool IsCancellationRequested { get; set; } = false;
        private List<StreetDetails> _streetsToCompute { get; set; }
        public IEnumerable<StreetDetails> StreetsToCompute => _streetsToCompute;

        public List<PlayerOdds> HandOdds(StreetDetails streetToCompute)
        {
            if (!initialized) Initialize();

            var streets = OddsCalculator.ComputeResultsForStreet(streetToCompute, playerMasks, boardmask, deadcards_mask);

            for (int i = 0; i < PlayerTracking.Count; i++)
            {
                var player = PlayerTracking[i];
                player.streets.Add(streets[i]);
            }

            return PlayerTracking;
        }

        private void Initialize()
        {
            PlayerTracking = PlayerCards.Select(x => new PlayerOdds()
            {
                Pockets = x,
                PocketsMask = OddsCalculator.CreatePocketPairMask(x)
            }).ToList();

            deadcards_mask = DeadCardsInput != null && DeadCardsInput.Any() ? Utilities.ConvertCardsToMask(DeadCardsInput) : 0UL;
            boardmask = BoardCards != null && BoardCards.Any() ? Utilities.ConvertCardsToMask(BoardCards) : 0UL;

            OddsCalculator.Validate(BoardCards, PlayerTracking, deadcards_mask, boardmask);

            foreach (var player in PlayerTracking)
            {
                deadcards_mask |= player.PocketsMask;
            }

            playerMasks = PlayerTracking.Select(x => x.PocketsMask).ToList();

            initialized = true;
        }
    }
}
