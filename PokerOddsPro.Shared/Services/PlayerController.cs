using PokerOddsPro.Shared.Dto.PokerDto.General;
using PokerOddsPro.Shared.ViewModels;
using PokerOddsPro.OddsEngine.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerOddsPro.Shared.Services
{
    public class PlayerController : IDisposable
    {
        public List<CardSlot> Cards { get; set; }
        private PlayerOdds _LatestResults { get; set; }
        public PlayerOdds LatestResults
        {
            private get { return _LatestResults; }
            set
            {
                _LatestResults = value;
                if (_LatestResults != null)
                {
                    _LatestResults.streets = _LatestResults.streets.OrderBy(x => x.streetDescriptor.StreetStartCardIndex).ToList();
                }
                LatestResultsUpdated.Invoke(null, EventArgs.Empty);
            }
        }

        public List<PlayerTrackingStreet> streets => LatestResults?.streets;

        public event EventHandler LatestResultsUpdated;

        public double WinPercentage => LatestResults != null ? LatestResults.streets.Last().TotalWins / (double)LatestResults.streets.Last().TotalMesurements : 0;
        public double TiePercentage => LatestResults != null ? LatestResults.streets.Last().TotalTies / (double)LatestResults.streets.Last().TotalMesurements : 0;
        public double LoosePercentage => LatestResults != null ? LatestResults.streets.Last().TotalLosses / (double)LatestResults.streets.Last().TotalMesurements : 0;

        public bool IsActive => Cards != null;
        public bool HasAllCards => Cards.All(x => x.Card?.CardInfo != null);
        public int NumberOfCards => Cards.Count(x => x.Card?.CardInfo != null);
        public bool HasResults => LatestResults != null;

        public PlayerController(int numberOfCards)
        {
            Cards = Enumerable.Range(0, numberOfCards).Select(x => new CardSlot() { CardSlotType = CardSlotTypeEnum.PlayerCardSlot }).ToList();
        }

        public void Dispose()
        {
            Cards = null;
            LatestResults = null;
        }
    }
}
