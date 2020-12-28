using System;
using System.Collections.Generic;

namespace TheCardGame.Design
{
    public class CardDesign
    {
        public string AsciiVersionOfCard(string suitType, string cardValue)
        {
            List<string> cardAscii = new List<string>();
            //Instead of a boring text version of the card we render an ASCII image of the card.
            //:param cards: One or more card objects
            //We will use this to prints the appropriate icons for each card
            List<string> suits_name = new List<string> { "SPADES", "DIAMONDS", "HEARTS", "CLUBS" };
            List<string> suits_symbols = new List<string> { "♠", "♦", "♥", "♣" };
            string rank = string.Empty;
            string space = string.Empty;
            //create an empty list of list, each sublist is a line
            List<string> lines = new List<string>();

            //"King" should be "K" and "10" should still be "10"
            if (cardValue == "10") //:  # ten is the only one who's rank is 2 char long
            {
                rank = cardValue;
                space = " ";
            }
            //#if we write "10" on the card that line will be 1 char to long
            else
            {
                rank = cardValue;  // some have a rank of 'King' this changes that to a simple 'K' ("King" doesn't fit)
                space = " "; // no "10", we use a blank space to will the void
            }
            //# get the cards suit in two steps
            int suit = suits_name.FindIndex(x => x == suitType);
            string suitSymbol = suits_symbols[suit];

            //# add the individual card on a line by line basis
            cardAscii.Add("┌─────────┐");
            cardAscii.Add(String.Format("│{0}{1}       │", rank, space)); // # use two {} one for char, one for space or char
            cardAscii.Add("│         │");
            cardAscii.Add("│         │");
            cardAscii.Add(String.Format("│    {0}    │", suit));
            cardAscii.Add("│         │");
            cardAscii.Add("│         │");
            cardAscii.Add(String.Format("│       {0}{1}│", space, rank));
            cardAscii.Add("└─────────┘");
            return string.Join("\n", cardAscii);
        }

    }
}
