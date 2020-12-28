using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCardGame.Models;
using TheCardGame.Models.Constants;
using TheCardGame.ServiceInterface;

namespace TheCardGame.Service
{
    /// <summary>
    /// This class deals with the card related activities like play card, shuffled card, removed card etc..
    /// </summary>
    public class CardService:ICardService
    {
        #region Private Variables

        /// <summary>
        /// Card types like CLUBS, SPADES, HEARTS, DIAMONDS
        /// </summary>
        private string[] _cardTypes;
        /// <summary>
        /// Card Values - 2,3,4,5,6,7,8,9,10,A,K,Q,J
        /// </summary>
        private string[] _values;
        /// <summary>
        /// Response service
        /// </summary>
        private readonly IResponseService _responseService;

        #endregion

        #region Properties

        /// <summary>
        /// This property represents Complete deck of cards
        /// </summary>
        public List<CardModel> Deck { get; set; }

        #endregion

        #region Constructor

        public CardService(string[] cardTypes, string[] values)
        {
            _cardTypes = cardTypes;
            _values = values;
            Deck = InitializeDeck(cardTypes, values);
            _responseService = new ResponseService();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method initializes the deck
        /// </summary>
        /// <param name="cardTypes">Card suites</param>
        /// <param name="values">Card values</param>
        /// <returns>Intialized deck</returns>
        private List<CardModel> InitializeDeck(string[] cardTypes, string[] values)
        {
            List<CardModel> cards = new List<CardModel>();
            foreach (string cardType in cardTypes)
            {
                foreach (string value in values)
                {
                    cards.Add(new CardModel { Suit = cardType.Trim(), Value = value.Trim() });
                }
            }
            return cards;
        }

        /// <summary>
        /// This method removes the card from the deck
        /// </summary>
        /// <param name="toBeRemovedCard">Card model to be removed</param>
        private void RemoveCard(CardModel toBeRemovedCard)
        {
            if (toBeRemovedCard != null)
            {
                Deck.Remove(toBeRemovedCard);
            }
        }

        /// <summary>
        /// This method shuffles the deck
        /// </summary>
        private void ShuffleDeck()
        {
            if (Deck.Count > 0)
            {
                Deck.Shuffle();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method starts the game
        /// </summary>
        /// <returns>Response which contains success/error message</returns>
        public ResultModel<CardModel> StartGame()
        {
            ResultModel<CardModel> response;
            try
            {
                //1. InitializeDeck
                Deck = InitializeDeck(_cardTypes, _values);
                //2. Shuffle The Cards
                ShuffleDeck();
                response = _responseService.SetResponse<CardModel>(null, false, DisplayMessageConstatns.START_GAME_SUCCESS);
            }
            catch (Exception ex)
            {
                // Log the error
                response = _responseService.SetResponse<CardModel>(null, true, DisplayMessageConstatns.START_GAME_FAILED);
            }
            return response;
        }

        /// <summary>
        /// This method used to play the card functionality
        /// </summary>
        /// <returns>Response which contains success/error message including card played</returns>
        public ResultModel<CardModel> PlayCard()
        {
            ResultModel<CardModel> response;
            try
            {
                if (Deck.Count > 0)
                {
                    //Deck is not empty
                    CardModel playedCard = Deck.FirstOrDefault();
                    response = _responseService.SetResponse(playedCard, false, DisplayMessageConstatns.PLAY_CARD_SUCCESS);
                    RemoveCard(playedCard);
                }
                else
                {
                    //Deck is empty
                    response = _responseService.SetResponse<CardModel>(null, false, DisplayMessageConstatns.PLAY_CARD_DECK_EMPTY);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                response = _responseService.SetResponse<CardModel>(null, true, DisplayMessageConstatns.PLAY_CARD_FAIL);
            }
            return response;
        }

        /// <summary>
        /// This method is used to shuffle the deck
        /// </summary>
        /// <returns>Response which contains success/error message including shuffled deck</returns>
        public ResultModel<CardModel> ShuffleTheDeck()
        {
            ResultModel<CardModel> response;
            try
            {
                ShuffleDeck();
                response= _responseService.SetResponse<CardModel>(null,false,DisplayMessageConstatns.SHUFFLE_CARD_SUCCESS);
            }
            catch (Exception)
            {
                // Log the error
                response = _responseService.SetResponse<CardModel>(null, true, DisplayMessageConstatns.SHUFFLE_CARD_FAILED);
            }
            return response;
        }

        #endregion
    }
}
