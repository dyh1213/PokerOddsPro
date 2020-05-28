#pragma checksum "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4bbdf6ab43f2c6dfec7d4d71c1882aaf4c4e9e83"
// <auto-generated/>
#pragma warning disable 1591
namespace PokerOddsPro.Shared.Components
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
using PokerOddsPro.Shared.Dto;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
using PokerOddsPro.Shared.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
using PokerOddsPro.Shared.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
using PokerOddsPro.OddsEngine.Dto;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
using PokerOddsPro.Shared.ViewModels;

#line default
#line hidden
#nullable disable
    public partial class CardSelectionPanel : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 9 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
         if (displayDirection == DisplayDirectionEnum.Vertical)
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(0, "        ");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "display-panel vertical");
            __builder.AddMarkupContent(3, "\r\n            ");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "style", "height: calc(100% - 50px); overflow-x: hidden; overflow-y: auto;");
            __builder.AddMarkupContent(6, "\r\n");
#nullable restore
#line 13 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                 foreach (var suit in Enumerators.EnumerateCardSuits())
                {

#line default
#line hidden
#nullable disable
            __builder.AddContent(7, "                    ");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "style", "float:left;height:100%; width:25%");
            __builder.AddMarkupContent(10, "\r\n");
#nullable restore
#line 16 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                         foreach (var card in _cardGameController.GetCardsOfSuit(suit))
                        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(11, "                            ");
            __builder.OpenComponent<PokerOddsPro.Shared.Components.SimpleCard>(12);
            __builder.AddAttribute(13, "card", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<PokerOddsPro.Shared.ViewModels.Card>(
#nullable restore
#line 18 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                               card

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(14, "SetSelectedCard", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<PokerOddsPro.Shared.ViewModels.Card>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<PokerOddsPro.Shared.ViewModels.Card>(this, 
#nullable restore
#line 18 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                                      SetSelectedCard

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(15, "displayDirection", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<PokerOddsPro.Shared.Dto.DisplayDirectionEnum>(
#nullable restore
#line 18 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                                                                          DisplayDirectionEnum.Vertical

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(16, "\r\n");
#nullable restore
#line 19 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(17, "                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n");
#nullable restore
#line 21 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                }

#line default
#line hidden
#nullable disable
            __builder.AddContent(19, "            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n\r\n            ");
            __builder.OpenElement(21, "div");
            __builder.AddAttribute(22, "class", "card-selection active simple-button");
            __builder.AddAttribute(23, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 24 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                                       async () => _cardGameController.ResetBoard()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(24, "\r\n                CLEAR\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\r\n");
#nullable restore
#line 28 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
            }
            else
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(27, "            ");
            __builder.OpenElement(28, "div");
            __builder.AddAttribute(29, "class", "display-panel");
            __builder.AddMarkupContent(30, "\r\n                ");
            __builder.OpenElement(31, "div");
            __builder.AddAttribute(32, "style", "width:50%; float:left");
            __builder.AddMarkupContent(33, "\r\n                    ");
            __builder.OpenElement(34, "div");
            __builder.AddAttribute(35, "class", "card-selection active simple-button");
            __builder.AddAttribute(36, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 33 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                                               async () => _cardGameController.ResetBoard()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(37, "\r\n                        CLEAR\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n                ");
            __builder.OpenElement(40, "div");
            __builder.AddAttribute(41, "style", "width:50%; float:left");
            __builder.AddMarkupContent(42, "\r\n                    ");
            __builder.OpenElement(43, "div");
            __builder.AddAttribute(44, "class", "card-selection active simple-button");
            __builder.AddAttribute(45, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 38 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                                               () => ToggleVerticalPanel()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(46, "\r\n                        ");
            __builder.AddContent(47, 
#nullable restore
#line 39 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                         DisplayString

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(48, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(49, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n                ");
            __builder.OpenElement(51, "div");
            __builder.AddAttribute(52, "class", "accordion-content");
            __builder.AddAttribute(53, "style", "max-height:" + (
#nullable restore
#line 42 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                                  heightOfHorizontalPanel

#line default
#line hidden
#nullable disable
            ) + ";" + " display:flex;" + " flex-direction:column;" + " width:100%");
            __builder.AddMarkupContent(54, "\r\n");
#nullable restore
#line 43 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                     foreach (var suit in Enum.GetValues(typeof(CardSuitEnum)).Cast<CardSuitEnum>())
                    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(55, "                        ");
            __builder.OpenElement(56, "div");
            __builder.AddAttribute(57, "style", "float:left; width:100%; flex: 1 0 auto;");
            __builder.AddMarkupContent(58, "\r\n");
#nullable restore
#line 46 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                             foreach (var card in _cardGameController.GetCardsOfSuit(suit))
                            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(59, "                                ");
            __builder.OpenComponent<PokerOddsPro.Shared.Components.SimpleCard>(60);
            __builder.AddAttribute(61, "card", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<PokerOddsPro.Shared.ViewModels.Card>(
#nullable restore
#line 48 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                   card

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(62, "SetSelectedCard", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<PokerOddsPro.Shared.ViewModels.Card>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<PokerOddsPro.Shared.ViewModels.Card>(this, 
#nullable restore
#line 48 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                                          SetSelectedCard

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(63, "displayDirection", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<PokerOddsPro.Shared.Dto.DisplayDirectionEnum>(
#nullable restore
#line 48 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                                                                                                              DisplayDirectionEnum.Horizonal

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(64, "\r\n");
#nullable restore
#line 49 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(65, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(66, "\r\n");
#nullable restore
#line 51 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                    }

#line default
#line hidden
#nullable disable
            __builder.AddContent(67, "                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(68, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(69, "\r\n");
#nullable restore
#line 54 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
                }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(70, "\r\n\r\n\r\n");
            __builder.AddMarkupContent(71, @"<style>

    .display-panel {
        background-color: #3D3D3D;
    }

        .display-panel.vertical {
            height: calc(100vh - 56px);
            flex: 1 0 440px;
        }

    .accordion-content {
        -webkit-transition: max-height 1s;
        -moz-transition: max-height 1s;
        -ms-transition: max-height 1s;
        -o-transition: max-height 1s;
        transition: max-height 1s;
        overflow: hidden;
    }

</style>");
        }
        #pragma warning restore 1998
#nullable restore
#line 80 "C:\Users\dyh12\source\repos\PokerOddsPro\PokerOddsPro.Shared\Components\CardSelectionPanel.razor"
       

    [Parameter]
    public HoldemCardGameManager _cardGameController { get; set; }

    [Parameter]
    public DisplayDirectionEnum displayDirection { get; set; }

    private async Task SetSelectedCard(Card CardInfo) => await _cardGameController.SetSelectedCard(CardInfo);

    //code used in horizontal panel toggle (move up/down)
    private bool IsToggleOn = false;
    private string DisplayString => (IsToggleOn) ? "Hide" : "Show";

    protected override async Task OnInitializedAsync()
    {
        _cardGameController.selectedCardSlotUpdatedFromNull += (s, e) => ToggleVerticalPanel();
    }

    private void ToggleVerticalPanel()
    {
        IsToggleOn = !IsToggleOn;
        StateHasChanged();
    }

    private string heightOfHorizontalPanel => (IsToggleOn) ? "400px" : "0px";


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591