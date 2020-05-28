namespace PokerOddsPro.OddsEngine.Dto
{
    public class CardInfo
    {
        public int CardId { get; set; }
        public CardSuitEnum CardSuit { get; set; }
        public CardNumberEnum CardNumber { get; set; }

        public string GetNumberDisplay()
        {
            if (CardNumber == CardNumberEnum.Ace) return "A";
            if (CardNumber == CardNumberEnum.Jack) return "J";
            if (CardNumber == CardNumberEnum.Queen) return "Q";
            if (CardNumber == CardNumberEnum.King) return "K";
            return ((int)CardNumber + 2).ToString();
        }

        public string GetSuitDisplay()
        {
            return CardSuit.ToString()[0].ToString();
        }

        public string computeString()
        {
            return GetNumberDisplay() + GetSuitDisplay();
        }
    }
}
