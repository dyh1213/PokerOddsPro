using System.ComponentModel;

namespace PokerOddsPro.OddsEngine.Dto
{
    public enum HandTypeEnum
    {
        /// <summary>
        /// Only a high card
        /// </summary>
        [Description("High Card")]
        HighCard = 0,
        /// <summary>
        /// One Pair
        /// </summary>
        [Description("Pair")]
        Pair = 1,
        /// <summary>
        /// Two Pair
        /// </summary>
        [Description("Two Pair")]
        TwoPair = 2,
        /// <summary>
        /// Three of a kind (Trips)
        /// </summary>
        [Description("Trips")]
        Trips = 3,
        /// <summary>
        /// Straight
        /// </summary>
        [Description("Straight")]
        Straight = 4,
        /// <summary>
        /// Flush
        /// </summary>
        [Description("Flush")]
        Flush = 5,
        /// <summary>
        /// FullHouse
        /// </summary>
        [Description("Full House")]
        FullHouse = 6,
        /// <summary>
        /// Four of a kind
        /// </summary>
        [Description("Four Of a Kind")]
        FourOfAKind = 7,
        /// <summary>
        /// Straight Flush
        /// </summary>
        [Description("Strait Flush")]
        StraightFlush = 8
    }
}
