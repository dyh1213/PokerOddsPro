#pragma checksum "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardDisplay.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f4444e4e8711be1e16d93d9d2779a3f407fec567"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace PokerOddsPro.Shared.Components
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
#nullable restore
#line 1 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardDisplay.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardDisplay.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardDisplay.razor"
using PokerOddsPro.Shared.Dto;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardDisplay.razor"
using PokerOddsPro.Shared.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardDisplay.razor"
using PokerOddsPro.Shared.ViewModels;

#line default
#line hidden
#nullable disable
    public partial class CardDisplay : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 145 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardDisplay.razor"
       
    private CardSlot _card { get; set; }
    [Parameter]
    public CardSlot card { get; set; }

    [Parameter]
    public DisplayModeEnum DisplayMode { get; set; }

    [Parameter]
    public CardDisplayTypeEnum CardDisplayType { get; set; }

    [Parameter]
    public string CustomWrapperCss { get; set; }

    private const string standardWrapperCss = "margin:10px; float:left; min-width:50px;";
    private const string faseInTime = "0.7s";

    private bool ShowCard => (card.Card != null);

    private string invisCardIcon => (DisplayMode == DisplayModeEnum.LightMode) ? $"url('{ConstPaths.whiteEmptyCardCirclePath}')" : $"url('{ConstPaths.blackEmptyCardCirclePath}')";

    private string cardNumberDisplay => (ShowCard) ? card.Card.CardInfo.GetNumberDisplay() : "";
    private string cardSuitDisplay => (ShowCard) ? card.Card.CardInfo.CardSuit.ToString().ToLower() : "";
    private string degreesDisplay => (ShowCard) ? "0deg" : "180deg";

    private string cardImageModernPath => $"url('{Helper.GetLinkToSuit(card.Card?.CardInfo.CardSuit)}')";
    private string cardImageClassicPath => $"url('{Helper.GetLinkToCard(card.Card?.CardInfo)}')";
    private string cardTextColor => Helper.GetColor(card.Card?.CardInfo.CardSuit);

    private bool cardRendered;
    private string shouldFadeInAnimation => ((!cardRendered) ? ConstPaths.fadeInAnimationClass : "");

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_card != card)
        {
            card.IsSelectedCardSet += (s, e) => StateHasChanged();
            card.IsSelectedCardRemoved += (s, e) => StateHasChanged();
            card.CardSlotCardUpdated += (s, e) => StateHasChanged();

            if (!firstRender)
            {
                _card.IsSelectedCardSet -= (s, e) => StateHasChanged();
                _card.IsSelectedCardRemoved -= (s, e) => StateHasChanged();
                _card.CardSlotCardUpdated -= (s, e) => StateHasChanged();
            }

            _card = card;
        }
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
