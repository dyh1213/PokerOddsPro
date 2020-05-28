using PokerOddsPro.Shared.Dto;
using PokerOddsPro.Shared.ViewModels;
using PokerOddsPro.OddsEngine.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PokerOddsPro.Shared.Helpers
{
    public static class Helper
    {
        public static double CardHeightToWidthProportion = 0.6538;
        public static double CardWidthToHeightProportion = 1.5294;

        public static int HorizontalToVerticalCardThreshold = 1250;

        public static int TinyMediaThreshold = 800;
        public static int SuperTinyMediaThreshold = 600;

        public static string GetEnumDescription(Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string IntToDisplayString(int value, DisplayMeasurementTypeEnum displayType = DisplayMeasurementTypeEnum.px, double? multiplier = null)
        {
            if (multiplier.HasValue)
            {
                value = (int)(value * multiplier.Value);
            }

            if (displayType != DisplayMeasurementTypeEnum.percentage)
            {
                return value.ToString() + displayType;
            }
            else
            {
                return value.ToString() + "%";
            }
        }

        public static string GetLinkToSuit(CardSuitEnum? suit, string baseUri = null)
        {
            var baseUriPath = (baseUri != null) ? $"/{baseUri}/Images/" : "Images/";
            if (suit == CardSuitEnum.Heart)
            {
                return baseUriPath + "heart.png";
            }
            else if (suit == CardSuitEnum.Diamond)
            {
                return baseUriPath + "diamond.png";
            }
            else if (suit == CardSuitEnum.Club)
            {
                return baseUriPath + "club.png";
            }
            else if (suit == CardSuitEnum.Spade)
            {
                return baseUriPath + "spade.png";
            }
            return "";
        }

        public static string GetLinkToCard(CardInfo cardInfo, string baseUri = null)
        {
            if (cardInfo == null) return "";
            var baseUriPath = (baseUri != null) ? $"/{baseUri}/Images/card_img/" : "Images/card_img/";
            var cardstring = cardInfo.computeString();
            var root = baseUriPath + $"{cardstring}.png";
            return root;
        }

        public static string GetColor(CardSuitEnum? suit)
        {
            if (suit == CardSuitEnum.Heart || suit == CardSuitEnum.Diamond)
            {
                return "red";
            }
            return "black";
        }

        public static List<Card> GetAllCards()
        {
            var id = 1;
            var allCards = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(CardSuitEnum)).Cast<CardSuitEnum>())
            {
                foreach (var cardNumber in Enum.GetValues(typeof(CardNumberEnum)).Cast<CardNumberEnum>())
                {
                    var card = new CardInfo()
                    {
                        CardId = id,
                        CardSuit = suit,
                        CardNumber = cardNumber
                    };
                    var simpleCard = new Card()
                    {
                        CardInfo = card,
                        IsAvailable = false
                    };
                    allCards.Add(simpleCard);
                    id++;
                }
            }

            return allCards;
        }
    }
}

