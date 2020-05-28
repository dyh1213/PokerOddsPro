using System;
using System.Collections.Generic;
using System.Linq;
using static PokerOddsPro.OddsEngine.Helpers.Constants;
using PokerOddsPro.OddsEngine.Dto;

namespace PokerOddsPro.OddsEngine.Helpers
{
    public static class Utilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="cards"></param>
        /// <returns></returns>
        /// SAFEEEEEE
        public static ulong ConvertCardsToMask(List<CardInfo> cards)
        {
            ulong handmask = 0UL;


            if (cards == null) throw new ArgumentNullException("hand");
            // A null hand is okay
            if (!cards.Any()) return 0UL;
            // Hand contains either invalid strings or duplicate entries
            //if (!Hand.ValidateHand(hand)) throw new ArgumentException("Bad hand definition");

            // Parse the hand
            foreach (var card in cards)
            {
                var cardValue = NextCard(card);
                handmask |= 1UL << cardValue;
            }

            return handmask;
        }

        /// <summary>
        /// Parses Card strings (internal)
        /// </summary>
        /// <param name="cards">string containing hand definition</param>
        /// <param name="index">iterator into card string</param>
        /// <returns></returns>
        /// 		/// SAFEEEEEE
        public static int NextCard(CardInfo card)
        {
            int rank = (int)card.CardNumber;
            var suit = (int)card.CardSuit;

            return rank + suit * 13;
        }

        /// <exclude/>
        /// NEEEEED
        public static HandTypeEnum ParseHandToHandType(uint handValue)
        {
            return (HandTypeEnum)(handValue >> HANDTYPE_SHIFT);
        }
    }
}
