using PokerOddsPro.OddsEngine.Dto;
using System.Collections.Generic;
using System.Linq;

namespace PokerOddsPro.Shared.Dto.PokerDto.General
{
    public abstract class BaseBoardDetails : IBoardDetails
    {
        public List<StreetDetails> streetDetails { get; set; }

        public BaseBoardDetails(List<StreetDetails> streetDescribers)
        {
            streetDetails = streetDescribers;
        }

        public int NumberOfBoardCards => streetDetails.Max(x => x.StreetEndCardIndex);
        public bool IsLastCardInAnyStreet(int index) => streetDetails.Any(x => x.StreetEndCardIndex == index);
    }
}
