using System;
using System.Collections.Generic;
using System.Text;
using TheCardGame.ServiceInterface;
using System.IO;
using Microsoft.Extensions.Configuration;
using TheCardGame.Models;

namespace TheCardGame.Service
{
    /// <summary>
    /// This class is used for card game service which contains main functionalities like running the application, retrieving the configuraiton
    /// </summary>
    public class CardGameService
    {
        #region Private Variables

        /// <summary>
        /// Configuration Service
        /// </summary>
        private readonly IConfigurationService _configurationService;
        /// <summary>
        /// Card Service
        /// </summary>
        private readonly ICardService _cardService;
        /// <summary>
        /// Card Design Service
        /// </summary>
        private readonly ICardDesignService _cardDesignService;
        /// <summary>
        /// Card type like - SUITS, CLUBS, DIAMONDS, SPADES
        /// </summary>
        private string[] _cardTypes;
        /// <summary>
        /// Card Values - A, 2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K 
        /// </summary>
        private string[] _cardValues;

        #endregion

        #region Constructor

        public CardGameService(IConfiguration configuration)
        {
            _configurationService = new ConfigurationService(configuration);
            GetConfiguration();
            _cardService = new CardService(_cardTypes, _cardValues);
            _cardDesignService = new CardDesignService();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method displays open card on the screen
        /// </summary>
        /// <param name="card">Card model</param>
        private void DisplayCard(CardModel card)
        {
            //Retrieves the design of the card
            ResultModel<CardDesignPropertiesModel> cardAscii = _cardDesignService.GetAsciiOfCard(card.Suit,card.Value);
            if(!cardAscii.IsError)
                _cardDesignService.DisplayCardFace(cardAscii.Data);
        }




        /// <summary>
        /// This method handles display of the card response
        /// </summary>
        /// <param name="response">Card response</param>
        private void HandlePlayCardDisplay(ResultModel<CardModel> response)
        {
            if (!response.IsError && response.Data != null)
            {
                DisplayCard(response.Data);
            }
        }

        private void HandleShuffleCardsDisplay(ResultModel<CardModel> response)
        {
            if (!response.IsError)
            {
                _cardDesignService.DisplayBlankCard();
            }
        }

        private void HandleRestartDisplay(ResultModel<CardModel> response)
        {
            if (!response.IsError)
            {
                _cardDesignService.DisplayBlankCard();
            }
        }

        private void HandleEmptyDeckDisplay(ResultModel<CardModel> response)
        {
            if (!response.IsError)
            {
                _cardDesignService.DisplayBlankCard();
            }
        }

        /// <summary>
        /// This method is used to execute different steps in running application
        /// </summary>
        private void RunApplication()
        {
            //Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.P)
                    {
                        //*** Play Card
                        ResultModel<CardModel> response = _cardService.PlayCard();
                        if (_cardService.Deck?.Count <= 0)
                        {
                            HandleEmptyDeckDisplay(response);
                            _cardDesignService.HandleResponse(response, false, true, 1000);
                        }
                        else
                        {
                            HandlePlayCardDisplay(response);
                            _cardDesignService.HandleResponse(response, true, true, 1000);
                        }
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.S)
                    {
                        //*** Shuffle
                        ResultModel<CardModel> response = _cardService.ShuffleTheDeck();
                        if (_cardService.Deck?.Count == 52)
                        {
                            HandleShuffleCardsDisplay(response);
                            _cardDesignService.HandleResponse(response, true, true, 1000);
                        }
                        else
                        {
                            _cardDesignService.HandleResponse(response, false, true, 1000);
                        }
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.R)
                    {
                        //*** Re-start
                        ResultModel<CardModel> response = _cardService.StartGame();
                        if (_cardService.Deck?.Count == 52)
                        {
                            HandleRestartDisplay(response);
                            _cardDesignService.HandleResponse(response, true, true, 1000);
                        }
                        else
                        {
                            _cardDesignService.HandleResponse(response, false, true, 1000);
                        }
                        _cardDesignService.HandleResponse(response, false, true, 1000);
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// This method retrieves configuration of the deck
        /// </summary>
        private void GetConfiguration()
        {
            _cardTypes = _configurationService.ReadDelimitedValue("SuitTypes", ",");
            _cardValues = _configurationService.ReadDelimitedValue("CardValues", ",");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This methods runs the game
        /// </summary>
        public void Run()
        {
            _cardDesignService.DisplayGame();
            // 1. Configuration
            GetConfiguration();
            // 2. Initialize Card Service
            _cardService.StartGame();
            RunApplication();
        }

        #endregion
    }
}