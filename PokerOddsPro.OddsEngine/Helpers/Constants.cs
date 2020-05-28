using PokerOddsPro.OddsEngine.Dto;

namespace PokerOddsPro.OddsEngine.Helpers
{
    public static class Constants
    {
        ///<exclude/>
        public static readonly int CardJoker = 52;
        /// <summary>
        /// The total number of cards in a deck
        /// </summary>
        public static readonly int NumberOfCards = 52;
        /// <exclude/>
        public static readonly int NCardsWJoker = 53;
        /// <exclude/>
        public static readonly int HANDTYPE_SHIFT = 24;
        /// <exclude/>
        public static readonly int TOP_CARD_SHIFT = 16;
        /// <exclude/>
        public static readonly uint TOP_CARD_MASK = 0x000F0000;
        /// <exclude/>
        public static readonly int SECOND_CARD_SHIFT = 12;
        /// <exclude/>
        public static readonly uint SECOND_CARD_MASK = 0x0000F000;
        /// <exclude/>
        public static readonly int THIRD_CARD_SHIFT = 8;
        /// <exclude/>
        public static readonly int FOURTH_CARD_SHIFT = 4;
        /// <exclude/>
        public static readonly int FIFTH_CARD_SHIFT = 0;
        /// <exclude/>
        public static readonly uint FIFTH_CARD_MASK = 0x0000000F;
        /// <exclude/>
        public static readonly int CARD_WIDTH = 4;
        /// <exclude/>
        public static readonly uint CARD_MASK = 0x0F;


        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_STRAIGHTFLUSH = (uint)HandTypeEnum.StraightFlush << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_STRAIGHT = (uint)HandTypeEnum.Straight << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_FLUSH = (uint)HandTypeEnum.Flush << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_FULLHOUSE = (uint)HandTypeEnum.FullHouse << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_FOUR_OF_A_KIND = (uint)HandTypeEnum.FourOfAKind << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_TRIPS = (uint)HandTypeEnum.Trips << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_TWOPAIR = (uint)HandTypeEnum.TwoPair << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_PAIR = (uint)HandTypeEnum.Pair << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly uint HANDTYPE_VALUE_HIGHCARD = (uint)HandTypeEnum.HighCard << HANDTYPE_SHIFT;
        /// <exclude/>
        public static readonly int SPADE_OFFSET = 13 * (int)CardSuitEnum.Spade;
        /// <exclude/>
        public static readonly int CLUB_OFFSET = 13 * (int)CardSuitEnum.Club;
        /// <exclude/>
        public static readonly int DIAMOND_OFFSET = 13 * (int)CardSuitEnum.Diamond;
        /// <exclude/>
        public static readonly int HEART_OFFSET = 13 * (int)CardSuitEnum.Heart;
    }
}
