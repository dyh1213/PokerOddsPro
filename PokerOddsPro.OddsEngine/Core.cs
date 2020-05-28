using System;
using System.Collections.Generic;
using static PokerOddsPro.OddsEngine.Helpers.Constants;
using static PokerOddsPro.OddsEngine.PreComputedTables.CardMaskTable;
using PokerOddsPro.OddsEngine.PreComputedTables;

namespace PokerOddsPro.OddsEngine
{
    public static class Core
    {
        //safeeeeeee
        public static uint Evaluate(ulong cards, int numberOfCards)
        {
            uint retval = 0, four_mask, three_mask, two_mask;

            // This functions supports 1-7 cards
            if (numberOfCards < 1 || numberOfCards > 7)
                throw new ArgumentOutOfRangeException("numberOfCards");

            // Seperate out by suit
            uint sc = (uint)(cards >> CLUB_OFFSET & 0x1fffUL);
            uint sd = (uint)(cards >> DIAMOND_OFFSET & 0x1fffUL);
            uint sh = (uint)(cards >> HEART_OFFSET & 0x1fffUL);
            uint ss = (uint)(cards >> SPADE_OFFSET & 0x1fffUL);

            uint ranks = sc | sd | sh | ss;
            uint n_ranks = NBitsTable.nBitsTable[ranks];
            uint n_dups = (uint)(numberOfCards - n_ranks);

            /* Check for straight, flush, or straight flush, and return if we can
               determine immediately that this is the best possible hand 
            */
            if (n_ranks >= 5)
            {
                if (NBitsTable.nBitsTable[ss] >= 5)
                {
                    if (StraitTable.straightTable[ss] != 0)
                        return HANDTYPE_VALUE_STRAIGHTFLUSH + (uint)(StraitTable.straightTable[ss] << TOP_CARD_SHIFT);
                    else
                        retval = HANDTYPE_VALUE_FLUSH + TopFiveCardTable.topFiveCardsTable[ss];
                }
                else if (NBitsTable.nBitsTable[sc] >= 5)
                {
                    if (StraitTable.straightTable[sc] != 0)
                        return HANDTYPE_VALUE_STRAIGHTFLUSH + (uint)(StraitTable.straightTable[sc] << TOP_CARD_SHIFT);
                    else
                        retval = HANDTYPE_VALUE_FLUSH + TopFiveCardTable.topFiveCardsTable[sc];
                }
                else if (NBitsTable.nBitsTable[sd] >= 5)
                {
                    if (StraitTable.straightTable[sd] != 0)
                        return HANDTYPE_VALUE_STRAIGHTFLUSH + (uint)(StraitTable.straightTable[sd] << TOP_CARD_SHIFT);
                    else
                        retval = HANDTYPE_VALUE_FLUSH + TopFiveCardTable.topFiveCardsTable[sd];
                }
                else if (NBitsTable.nBitsTable[sh] >= 5)
                {
                    if (StraitTable.straightTable[sh] != 0)
                        return HANDTYPE_VALUE_STRAIGHTFLUSH + (uint)(StraitTable.straightTable[sh] << TOP_CARD_SHIFT);
                    else
                        retval = HANDTYPE_VALUE_FLUSH + TopFiveCardTable.topFiveCardsTable[sh];
                }
                else
                {
                    uint st = StraitTable.straightTable[ranks];
                    if (st != 0)
                        retval = HANDTYPE_VALUE_STRAIGHT + (st << TOP_CARD_SHIFT);
                };

                /* 
                   Another win -- if there can't be a FH/Quads (n_dups < 3), 
                   which is true most of the time when there is a made hand, then if we've
                   found a five card hand, just return.  This skips the whole process of
                   computing two_mask/three_mask/etc.
                */
                if (retval != 0 && n_dups < 3)
                    return retval;
            }

            /*
             * By the time we're here, either: 
               1) there's no five-card hand possible (flush or straight), or
               2) there's a flush or straight, but we know that there are enough
                  duplicates to make a full house / quads possible.  
             */
            switch (n_dups)
            {
                case 0:
                    /* It's a no-pair hand */
                    return HANDTYPE_VALUE_HIGHCARD + TopFiveCardTable.topFiveCardsTable[ranks];
                case 1:
                    {
                        /* It's a one-pair hand */
                        uint t, kickers;

                        two_mask = ranks ^ sc ^ sd ^ sh ^ ss;

                        retval = (uint)(HANDTYPE_VALUE_PAIR + (TopCardTable.topCardTable[two_mask] << TOP_CARD_SHIFT));
                        t = ranks ^ two_mask;      /* Only one bit set in two_mask */
                        /* Get the top five cards in what is left, drop all but the top three 
                         * cards, and shift them by one to get the three desired kickers */
                        kickers = TopFiveCardTable.topFiveCardsTable[t] >> CARD_WIDTH & ~FIFTH_CARD_MASK;
                        retval += kickers;
                        return retval;
                    }

                case 2:
                    /* Either two pair or trips */
                    two_mask = ranks ^ sc ^ sd ^ sh ^ ss;
                    if (two_mask != 0)
                    {
                        uint t = ranks ^ two_mask; /* Exactly two bits set in two_mask */
                        retval = (uint)(HANDTYPE_VALUE_TWOPAIR
                            + (TopFiveCardTable.topFiveCardsTable[two_mask]
                            & (TOP_CARD_MASK | SECOND_CARD_MASK))
                            + (TopCardTable.topCardTable[t] << THIRD_CARD_SHIFT));

                        return retval;
                    }
                    else
                    {
                        uint t, second;
                        three_mask = (sc & sd | sh & ss) & (sc & sh | sd & ss);
                        retval = (uint)(HANDTYPE_VALUE_TRIPS + (TopCardTable.topCardTable[three_mask] << TOP_CARD_SHIFT));
                        t = ranks ^ three_mask; /* Only one bit set in three_mask */
                        second = TopCardTable.topCardTable[t];
                        retval += second << SECOND_CARD_SHIFT;
                        t ^= 1U << (int)second;
                        retval += (uint)(TopCardTable.topCardTable[t] << THIRD_CARD_SHIFT);
                        return retval;
                    }

                default:
                    /* Possible quads, fullhouse, straight or flush, or two pair */
                    four_mask = sh & sd & sc & ss;
                    if (four_mask != 0)
                    {
                        uint tc = TopCardTable.topCardTable[four_mask];
                        retval = (uint)(HANDTYPE_VALUE_FOUR_OF_A_KIND
                            + (tc << TOP_CARD_SHIFT)
                            + (TopCardTable.topCardTable[ranks ^ 1U << (int)tc] << SECOND_CARD_SHIFT));
                        return retval;
                    };

                    /* Technically, three_mask as defined below is really the set of
                       bits which are set in three or four of the suits, but since
                       we've already eliminated quads, this is OK */
                    /* Similarly, two_mask is really two_or_four_mask, but since we've
                       already eliminated quads, we can use this shortcut */

                    two_mask = ranks ^ sc ^ sd ^ sh ^ ss;
                    if (NBitsTable.nBitsTable[two_mask] != n_dups)
                    {
                        /* Must be some trips then, which really means there is a 
                           full house since n_dups >= 3 */
                        uint tc, t;
                        three_mask = (sc & sd | sh & ss) & (sc & sh | sd & ss);
                        retval = HANDTYPE_VALUE_FULLHOUSE;
                        tc = TopCardTable.topCardTable[three_mask];
                        retval += tc << TOP_CARD_SHIFT;
                        t = (two_mask | three_mask) ^ 1U << (int)tc;
                        retval += (uint)(TopCardTable.topCardTable[t] << SECOND_CARD_SHIFT);
                        return retval;
                    };

                    if (retval != 0) /* flush and straight */
                        return retval;
                    else
                    {
                        /* Must be two pair */
                        uint top, second;

                        retval = HANDTYPE_VALUE_TWOPAIR;
                        top = TopCardTable.topCardTable[two_mask];
                        retval += top << TOP_CARD_SHIFT;
                        second = TopCardTable.topCardTable[two_mask ^ 1 << (int)top];
                        retval += second << SECOND_CARD_SHIFT;
                        retval += (uint)(TopCardTable.topCardTable[ranks ^ 1U << (int)top ^ 1 << (int)second] << THIRD_CARD_SHIFT);
                        return retval;
                    }
            }
        }

        /// <summary>
        /// Enables a foreach command to enumerate all possible ncard hands.
        /// </summary>
        /// <param name="shared">A bitfield containing the cards that must be in the enumerated hands</param>
        /// <param name="dead">A bitfield containing the cards that must not be in the enumerated hands</param>
        /// <param name="numberOfCards">the number of cards in the hand (must be between 1 and 7)</param>
        public static IEnumerable<ulong> Hands(ulong shared, ulong dead, int numberOfCards)
        {
            int _i1, _i2, _i3, _i4, _i5, _i6, _i7, length;
            ulong _card1, _card2, _card3, _card4, _card5, _card6, _card7;
            ulong _n2, _n3, _n4, _n5, _n6;

            dead |= shared;

            switch (numberOfCards - BitCountTable.BitCount(shared))
            {
                case 7:
                    for (_i1 = NumberOfCards - 1; _i1 >= 0; _i1--)
                    {
                        _card1 = CardMasksTable[_i1];
                        if ((dead & _card1) != 0) continue;
                        for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                        {
                            _card2 = CardMasksTable[_i2];
                            if ((dead & _card2) != 0) continue;
                            _n2 = _card1 | _card2;
                            for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                            {
                                _card3 = CardMasksTable[_i3];
                                if ((dead & _card3) != 0) continue;
                                _n3 = _n2 | _card3;
                                for (_i4 = _i3 - 1; _i4 >= 0; _i4--)
                                {
                                    _card4 = CardMasksTable[_i4];
                                    if ((dead & _card4) != 0) continue;
                                    _n4 = _n3 | _card4;
                                    for (_i5 = _i4 - 1; _i5 >= 0; _i5--)
                                    {
                                        _card5 = CardMasksTable[_i5];
                                        if ((dead & _card5) != 0) continue;
                                        _n5 = _n4 | _card5;
                                        for (_i6 = _i5 - 1; _i6 >= 0; _i6--)
                                        {
                                            _card6 = CardMasksTable[_i6];
                                            if ((dead & _card6) != 0) continue;
                                            _n6 = _n5 | _card6;
                                            for (_i7 = _i6 - 1; _i7 >= 0; _i7--)
                                            {
                                                _card7 = CardMasksTable[_i7];
                                                if ((dead & _card7) != 0) continue;
                                                yield return _n6 | _card7 | shared;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 6:
                    for (_i1 = NumberOfCards - 1; _i1 >= 0; _i1--)
                    {
                        _card1 = CardMasksTable[_i1];
                        if ((dead & _card1) != 0) continue;
                        for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                        {
                            _card2 = CardMasksTable[_i2];
                            if ((dead & _card2) != 0) continue;
                            _n2 = _card1 | _card2;
                            for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                            {
                                _card3 = CardMasksTable[_i3];
                                if ((dead & _card3) != 0) continue;
                                _n3 = _n2 | _card3;
                                for (_i4 = _i3 - 1; _i4 >= 0; _i4--)
                                {
                                    _card4 = CardMasksTable[_i4];
                                    if ((dead & _card4) != 0) continue;
                                    _n4 = _n3 | _card4;
                                    for (_i5 = _i4 - 1; _i5 >= 0; _i5--)
                                    {
                                        _card5 = CardMasksTable[_i5];
                                        if ((dead & _card5) != 0) continue;
                                        _n5 = _n4 | _card5;
                                        for (_i6 = _i5 - 1; _i6 >= 0; _i6--)
                                        {
                                            _card6 = CardMasksTable[_i6];
                                            if ((dead & _card6) != 0)
                                                continue;
                                            yield return _n5 | _card6 | shared;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 5:
                    for (_i1 = NumberOfCards - 1; _i1 >= 0; _i1--)
                    {
                        _card1 = CardMasksTable[_i1];
                        if ((dead & _card1) != 0) continue;
                        for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                        {
                            _card2 = CardMasksTable[_i2];
                            if ((dead & _card2) != 0) continue;
                            _n2 = _card1 | _card2;
                            for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                            {
                                _card3 = CardMasksTable[_i3];
                                if ((dead & _card3) != 0) continue;
                                _n3 = _n2 | _card3;
                                for (_i4 = _i3 - 1; _i4 >= 0; _i4--)
                                {
                                    _card4 = CardMasksTable[_i4];
                                    if ((dead & _card4) != 0) continue;
                                    _n4 = _n3 | _card4;
                                    for (_i5 = _i4 - 1; _i5 >= 0; _i5--)
                                    {
                                        _card5 = CardMasksTable[_i5];
                                        if ((dead & _card5) != 0) continue;
                                        yield return _n4 | _card5 | shared;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    for (_i1 = NumberOfCards - 1; _i1 >= 0; _i1--)
                    {
                        _card1 = CardMasksTable[_i1];
                        if ((dead & _card1) != 0) continue;
                        for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                        {
                            _card2 = CardMasksTable[_i2];
                            if ((dead & _card2) != 0) continue;
                            _n2 = _card1 | _card2;
                            for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                            {
                                _card3 = CardMasksTable[_i3];
                                if ((dead & _card3) != 0) continue;
                                _n3 = _n2 | _card3;
                                for (_i4 = _i3 - 1; _i4 >= 0; _i4--)
                                {
                                    _card4 = CardMasksTable[_i4];
                                    if ((dead & _card4) != 0) continue;
                                    yield return _n3 | _card4 | shared;
                                }
                            }
                        }
                    }

                    break;
                case 3:
                    for (_i1 = NumberOfCards - 1; _i1 >= 0; _i1--)
                    {
                        _card1 = CardMasksTable[_i1];
                        if ((dead & _card1) != 0) continue;
                        for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                        {
                            _card2 = CardMasksTable[_i2];
                            if ((dead & _card2) != 0) continue;
                            _n2 = _card1 | _card2;
                            for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                            {
                                _card3 = CardMasksTable[_i3];
                                if ((dead & _card3) != 0) continue;
                                yield return _n2 | _card3 | shared;
                            }
                        }
                    }
                    break;
                case 2:
                    length = AllPocketCardMasks.TwoCardTable.Length;
                    for (_i1 = 0; _i1 < length; _i1++)
                    {
                        _card1 = AllPocketCardMasks.TwoCardTable[_i1];
                        if ((dead & _card1) != 0) continue;
                        yield return _card1 | shared;
                    }
                    break;
                case 1:
                    length = CardMasksTable.Length;
                    for (_i1 = 0; _i1 < length; _i1++)
                    {
                        _card1 = CardMasksTable[_i1];
                        if ((dead & _card1) != 0) continue;
                        yield return _card1 | shared;
                    }
                    break;
                case 0:
                    yield return shared;
                    break;
                default:
                    yield return 0UL;
                    break;
            }
        }
    }
}
