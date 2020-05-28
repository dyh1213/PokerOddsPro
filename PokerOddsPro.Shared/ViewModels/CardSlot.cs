using PokerOddsPro.Shared.Dto.PokerDto.General;
using System;

namespace PokerOddsPro.Shared.ViewModels
{
    public class CardSlot
    {
        public CardSlotTypeEnum CardSlotType { get; set; }

        private Card Card_;
        public Card Card
        {
            get { return Card_; }
            set
            {
                Card_ = value;
                if (Card_ != null)
                {
                    CardSlotCardUpdated.Invoke(null, EventArgs.Empty);
                }
            }
        }
        public event EventHandler CardSlotCardUpdated;

        public bool IsSelectedFromRemoval = false;

        private bool IsSelected_;  
        public bool IsSelected
        {
            get { return IsSelected_; }
            set
            {
                IsSelected_ = value;
                if (IsSelected_)
                {
                    IsSelectedCardSet.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    IsSelectedCardRemoved.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public event EventHandler IsSelectedCardSet;
        public event EventHandler IsSelectedCardRemoved;

        public Card removeCardFromCardSlot()
        {
            var card = Card_;
            Card_ = null;
            return card;
        }

        public void SelectCardSlot() => IsSelected = true;
        public bool hasCard => Card_ != null;
    }
}
