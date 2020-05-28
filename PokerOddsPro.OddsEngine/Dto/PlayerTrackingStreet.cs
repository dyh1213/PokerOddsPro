using System.Collections.Generic;

namespace PokerOddsPro.OddsEngine.Dto
{
    public class PlayerTrackingStreet
    {
        public StreetDetails streetDescriptor { get; set; }
        public int BoardCardCount { get; set; }
        public long TotalMesurements { get; set; }
        public long TotalWins { get; set; }
        public long TotalTies { get; set; }
        public long TotalLosses { get; set; }

        public Dictionary<HandTypeEnum, long> resultsByHand = new Dictionary<HandTypeEnum, long>()
        {
            {HandTypeEnum.HighCard,0},
            {HandTypeEnum.Pair,0},
            {HandTypeEnum.TwoPair,0},
            {HandTypeEnum.Trips,0},
            {HandTypeEnum.Straight,0},
            {HandTypeEnum.Flush,0},
            {HandTypeEnum.FullHouse,0},
            {HandTypeEnum.FourOfAKind,0},
            {HandTypeEnum.StraightFlush,0},
        };
    }
}
