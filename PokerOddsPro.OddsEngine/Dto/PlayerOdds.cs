using System.Collections.Generic;

namespace PokerOddsPro.OddsEngine.Dto
{
    public class PlayerOdds
    {
        public List<CardInfo> Pockets { get; set; }
        public ulong PocketsMask { get; set; }

        public List<PlayerTrackingStreet> streets = new List<PlayerTrackingStreet>();
    }
}
