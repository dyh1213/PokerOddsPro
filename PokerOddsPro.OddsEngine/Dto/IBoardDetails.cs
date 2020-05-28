using System.Collections.Generic;

namespace PokerOddsPro.OddsEngine.Dto
{
    public interface IBoardDetails
    {
        List<StreetDetails> streetDetails { get; }
    }
}
