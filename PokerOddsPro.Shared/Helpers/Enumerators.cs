using PokerOddsPro.OddsEngine.Dto;
using System;
using System.Collections.Generic;

namespace PokerOddsPro.Shared.Helpers
{
    public static class Enumerators
    {
        public static IEnumerable<CardSuitEnum> EnumerateCardSuits()
        {
            return GetAllItems<CardSuitEnum>();
        }

        private static IEnumerable<T> GetAllItems<T>() where T : struct
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }
    }
}
