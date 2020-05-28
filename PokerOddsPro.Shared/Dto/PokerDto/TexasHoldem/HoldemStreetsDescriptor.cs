using PokerOddsPro.Shared.Dto.PokerDto.General;
using PokerOddsPro.OddsEngine.Dto;
using System.Collections.Generic;

namespace PokerOddsPro.Shared.Dto.PokerDto.TexasHoldem
{
    public class HoldemBoardDetails : BaseBoardDetails
    {
        public HoldemBoardDetails() : base(HoldemStreets()) { }

        public static List<StreetDetails> HoldemStreets()
        {
            return new List<StreetDetails>()
            {
                new StreetDetails()
                {
                    StreetName = "Flop",
                    StreetStartCardIndex = 1,
                    StreetEndCardIndex = 3
                },
                new StreetDetails()
                {
                    StreetName = "Turn",
                    StreetStartCardIndex = 4,
                    StreetEndCardIndex = 4
                },
                new StreetDetails()
                {
                    StreetName = "River",
                    StreetStartCardIndex = 5,
                    StreetEndCardIndex = 5
                },
            };
        }
    }
}
