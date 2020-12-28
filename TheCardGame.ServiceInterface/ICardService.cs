using System;
using System.Collections.Generic;
using System.Text;
using TheCardGame.Models;

namespace TheCardGame.ServiceInterface
{
    public interface ICardService
    {
        public List<CardModel> Deck { get; set; }
        ResultModel<CardModel> StartGame();
         ResultModel<CardModel> PlayCard();
         ResultModel<CardModel> ShuffleTheDeck();
    }
}
