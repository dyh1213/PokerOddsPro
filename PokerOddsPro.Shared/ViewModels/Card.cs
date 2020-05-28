using PokerOddsPro.OddsEngine.Dto;
using System;

namespace PokerOddsPro.Shared.ViewModels
{
    public class Card
    {
        public CardInfo CardInfo { get; set; }

        private bool IsAvailable_;
        public bool IsAvailable
        {
            get { return IsAvailable_; }
            set
            {
                if (IsAvailable_ != value)
                {
                    IsAvailable_ = value;
                    IsAvailableUpdated?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public event EventHandler IsAvailableUpdated;
    }
}
