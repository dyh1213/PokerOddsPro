using PokerOddsPro.OddsEngine.Dto;
using PokerOddsPro.OddsEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PokerOddsPro.OddsEngine
{
    public static class OddsCalculator
    {
        /// <summary>
        /// Used to calculate the wining information about each players hand. This function enumerates all 
        /// possible remaining hands and tallies win, tie and losses for each player. This function typically takes
        /// well less than a second regardless of the number of players.
        /// </summary>
        /// <param name="pockets">Array of pocket hand string, one for each player</param>
        /// <param name="board">the board cards</param>
        /// <param name="dead">the dead cards</param>
        /// <param name="wins">An array of win tallies, one for each player</param>
        /// <param name="ties">An array of tie tallies, one for each player</param>
        /// <param name="losses">An array of losses tallies, one for each player</param>
        /// <param name="totalHands">The total number of hands enumarated.</param>
        public static List<PlayerOdds> HandOdds(IBoardDetails streetsDescriptor, List<List<CardInfo>> playerCards, List<CardInfo> boardCards, List<CardInfo> deadCardsInput = null)
        {
            //todo validate each player only has 2 cards !!!!

            var playerTracking = playerCards.Select(x => new PlayerOdds()
            {
                Pockets = x,
                PocketsMask = CreatePocketPairMask(x)
            }).ToList();

            var deadcards_mask = deadCardsInput != null && deadCardsInput.Any() ? Utilities.ConvertCardsToMask(deadCardsInput) : 0UL;
            var boardmask = deadCardsInput != null && deadCardsInput.Any() ? Utilities.ConvertCardsToMask(boardCards) : 0UL;

            Validate(boardCards, playerTracking, deadcards_mask, boardmask);

            // Read pocket cards
            foreach (var player in playerTracking)
            {
                deadcards_mask |= player.PocketsMask;
            }

            var playerMasks = playerTracking.Select(x => x.PocketsMask).ToList();
            var streetsToCompute = streetsDescriptor.streetDetails.Where(x => x.StreetEndCardIndex >= boardCards.Count).OrderByDescending(x => x.StreetEndCardIndex);

            foreach (var streetDescriptor in streetsToCompute)
            {
                var streets = ComputeResultsForStreet(streetDescriptor, playerMasks, boardmask, deadcards_mask);

                for (int i = 0; i < playerTracking.Count; i++)
                {
                    var player = playerTracking[i];
                    player.streets.Add(streets[i]);
                }
            }

            return playerTracking;
        }

        public static void Validate(List<CardInfo> boardCards, List<PlayerOdds> playerTracking, ulong deadcards, ulong boardmask)
        {
            var playerCount = playerTracking.Count();

            Debug.Assert(boardCards.Count >= 0 && boardCards.Count <= 5); // The board must have zero or more cards but no more than a total of 5

            // Check pocket cards, board, and dead cards for duplicates
            if ((boardmask & deadcards) != 0)
                throw new ArgumentException("Duplicate between cards dead cards and board");

            // Validate the input
            for (int i = 0; i < playerCount; i++)
            {
                for (int j = i + 1; j < playerCount; j++)
                {
                    if ((playerTracking[i].PocketsMask & playerTracking[j].PocketsMask) != 0)
                        throw new ArgumentException("Duplicate pocket cards");
                }

                if ((playerTracking[i].PocketsMask & boardmask) != 0)
                    throw new ArgumentException("Duplicate between cards pocket and board");

                if ((playerTracking[i].PocketsMask & deadcards) != 0)
                    throw new ArgumentException("Duplicate between cards pocket and dead cards");
            }
        }

        public static ulong CreatePocketPairMask(List<CardInfo> cards)
        {
            if (cards.Count != 2)
                throw new ArgumentException("There must be two pocket cards."); // Must have 2 cards in each pocket card set.

            return Utilities.ConvertCardsToMask(cards);
        }

        public static List<PlayerTrackingStreet> ComputeResultsForStreet(StreetDetails streetDetails, List<ulong> playerMasks, ulong boardmask, ulong deadcards_mask)
        {
            var pockethands = new uint[playerMasks.Count];
            var targetCardIndex = streetDetails.StreetEndCardIndex;
            var totalHands = 0;
            var street = playerMasks.Select(x => new PlayerTrackingStreet() { BoardCardCount = targetCardIndex, streetDescriptor = streetDetails }).ToList();

            // Iterate through all board possiblities that doesn't include any pocket cards.
            foreach (ulong boardhand in Core.Hands(boardmask, deadcards_mask, targetCardIndex))
            {
                // Evaluate all hands and determine the best hand
                ulong? bestpocket = null;
                bool isTied = false;
                for (int i = 0; i < playerMasks.Count; i++)
                {
                    pockethands[i] = Core.Evaluate(playerMasks[i] | boardhand, 7 - (5 - targetCardIndex));
                    if (bestpocket == null || pockethands[i] > bestpocket)
                    {
                        bestpocket = pockethands[i];
                    }
                    else if (pockethands[i] == bestpocket)
                    {
                        isTied = true;
                    }
                }

                // Calculate wins/ties/loses for each pocket + board combination.
                for (int i = 0; i < playerMasks.Count; i++)
                {
                    var player = street[i];
                    var handValue = pockethands[i];
                    var handType = Utilities.ParseHandToHandType(handValue);

                    player.TotalMesurements++;
                    player.resultsByHand[handType]++;

                    if (pockethands[i] == bestpocket)
                    {
                        if (isTied)
                        {
                            player.TotalTies++;
                        }
                        else
                        {
                            player.TotalWins++;
                        }
                    }
                    else if (pockethands[i] < bestpocket)
                    {
                        player.TotalLosses++;
                    }
                }

                totalHands++;
            }

            return street;
        }
    }
}
