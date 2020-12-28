using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TheCardGame.Models;
using TheCardGame.Models.Constants;
using TheCardGame.ServiceInterface;

namespace TheCardGame.Service
{
    /// <summary>
    /// This class is used for the design related activities like displaying and getting game designs
    /// </summary>
    public class CardDesignService:ICardDesignService
    {
        #region Private Variables

        List<string> suits_name = new List<string> { "SPADES", "DIAMONDS", "HEARTS", "CLUBS" };
        List<string> suits_symbols = new List<string> { "♠", "♦", "♥", "♣" };
        List<ConsoleColor> suits_colors = new List<ConsoleColor> { ConsoleColor.Black, ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Black };
        private readonly IResponseService _responseService;

        #endregion

        #region Constructor
        public CardDesignService()
        {
            _responseService = new ResponseService();
        }
        #endregion

        #region Functional Design Methods

        /// <summary>
        /// This method retrieves the card design properties for the given value(like 2,3,4,etc) and suit type(CLUBS,SPADES,etc)
        /// </summary>
        /// <param name="suitType">suit type(CLUBS,SPADES,etc)</param>
        /// <param name="cardValue">value(like 2,3,4,etc)</param>
        /// <returns>Card design properties model </returns>
        public ResultModel<CardDesignPropertiesModel> GetAsciiOfCard(string suitType, string cardValue)
        {
            List<string> cardAscii = new List<string>();
            string rank = string.Empty;
            string space = string.Empty;
            List<string> lines = new List<string>(); //create an empty list of list, each sublist is a line
            CardDesignPropertiesModel cardDesignPropertiesModel = new CardDesignPropertiesModel();
            ResultModel<CardDesignPropertiesModel> result = new ResultModel<CardDesignPropertiesModel>();
            try
            {
                if (cardValue == "10") //10 is the only one who's rank is 2 char long
                {
                    rank = cardValue;
                    space = "";
                }
                //if we write "10" on the card that line will be 1 char to long
                else
                {
                    rank = cardValue;
                    space = " "; // no "10", a blank space to fill the void
                }
                //get the cards suit in two steps
                int suit = suits_name.IndexOf(suitType);
                string suitSymbol = suits_symbols[suit];
                ConsoleColor suitColor = suits_colors[suit];
                //Add the individual card on a line by line basis
                cardAscii = GetAsciiForFaceOfCard(rank, space, suitSymbol);
                cardDesignPropertiesModel.CardText = string.Join("\n", cardAscii);
                cardDesignPropertiesModel.Color = suitColor;
                cardDesignPropertiesModel.CardPattern = cardAscii;
                cardDesignPropertiesModel.CardWidth = 26;
                cardDesignPropertiesModel.CardHeight = 22;
                result = _responseService.SetResponse<CardDesignPropertiesModel>(cardDesignPropertiesModel, false, DisplayMessageConstatns.ASCII_CARD_RETRIEVAL_SUCCESS);
            }
            catch (Exception ex)
            {
                // Log The Error 
                result = _responseService.SetResponse<CardDesignPropertiesModel>(null, true, DisplayMessageConstatns.ASCII_CARD_RETRIEVAL_FAILED);
            }
            return result;
        }

        /// <summary>
        /// This method retrieves a list of strings to form the design of the card according to the suit type
        /// </summary>
        /// <param name="rank">Card value like 2,3,4, etc</param>
        /// <param name="space">String which contains space or empty string</param>
        /// <param name="suitSymbol">Suit symbol like CLUBS,SPADES etc</param>
        /// <returns>List of strings which contains design of the face of the card</returns>
        public List<string> GetAsciiForFaceOfCard(string rank, string space, string suitSymbol)
        {
            List<string> cardAscii = new List<string>();
            cardAscii.Add(" ┌─────────────────────────┐ ");
            cardAscii.Add(String.Format(" │{0}{1}{2}                      │ ", rank, space, suitSymbol));
            cardAscii.Add(" │  ┌───────────────────┐  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(String.Format(" │  │          {0}        │  │ ", suitSymbol));
            cardAscii.Add(String.Format(" │  │        {0}   {0}      │  │ ", suitSymbol));
            cardAscii.Add(String.Format(" │  │      {0}   {0}   {0}    │  │ ", suitSymbol));
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  └───────────────────┘  │ ");
            cardAscii.Add(String.Format(" │                      {0}{1}{2}│ ", suitSymbol, space, rank));
            cardAscii.Add(" └─────────────────────────┘ ");
            return cardAscii;
        }

        /// <summary>
        /// This method retrieves the design of the closed deck
        /// </summary>
        /// <returns>List of strings for the design of closed deck</returns>
        public List<string> GetAsciiForDeck()
        {
            List<string> deck = new List<string>();
            deck.Add("┌─────────────────────────┐");
            deck.Add("│/////////////////////////│");
            deck.Add("│///┌─────────────────┐///│");
            deck.Add("│///│       ♣         │///│");
            deck.Add("│///│       T         │///│");
            deck.Add("│///│       H         │///│");
            deck.Add("│///│       E         │///│");
            deck.Add("│///│                 │///│");
            deck.Add("│///│       ♠         │///│");
            deck.Add("│///│       C         │///│");
            deck.Add("│///│       A         │///│");
            deck.Add("│///│       R         │///│");
            deck.Add("│///│       D         │///│");
            deck.Add("│///│       ♦         │///│");
            deck.Add("│///│                 │///│");
            deck.Add("│///│       G         │///│");
            deck.Add("│///│       A         │///│");
            deck.Add("│///│       M         │///│");
            deck.Add("│///│       E         │///│");
            deck.Add("│///│       ♥         │///│");
            deck.Add("│///└─────────────────┘///│");
            deck.Add("│/////////////////////////│");
            deck.Add("└─────────────────────────┘");
            return deck;
        }

        public List<string> GetAsciiForFaceOfCardBlank()
        {
            List<string> cardAscii = new List<string>();
            cardAscii.Add(" ┌─────────────────────────┐ ");
            cardAscii.Add(" │                         │ ");
            cardAscii.Add(" │  ┌───────────────────┐  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  │                   │  │ ");
            cardAscii.Add(" │  └───────────────────┘  │ ");
            cardAscii.Add(" │                         │ ");
            cardAscii.Add(" └─────────────────────────┘ ");
            return cardAscii;
        }

        #endregion

        #region Functional Display Methods

        /// <summary>
        /// This method is used to display title banner
        /// </summary>
        public void DisplayTitleBanner()
        {
            Console.Title = "The Card Game";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(@"                                                                        _____   _              ____               _    _____");
            Console.WriteLine(@"                                                                       |_  _ | | |            / __ \             | |  |  __ \");
            Console.WriteLine(@"                                                                         | |   | |__   ___   | /  \/ __ _ _ __ __| |  | |  \/ __ _ _ __ ___   ___");
            Console.WriteLine(@"                                                                         | |   | '_ \ / _ \  | |    / _` | '__/ _` |  | | __ / _` | '_ ` _ \ / _ \");
            Console.WriteLine(@"                                                                         | |   | | | |  __/  | \__/\ (_| | | | (_| |  | |_\ \ (_| | | | | | |  __/ ");
            Console.WriteLine(@"                                                                         \_/   |_| |_|\___|   \____/\__,_|_|  \__,_|   \____/\__,_|_| |_| |_|\___|");
            Console.WriteLine("\n");
        }

        /// <summary>
        /// This method used to display the screen required at the start of the game
        /// </summary>
        public void DisplayGame()
        {
            //Background color
            Console.BackgroundColor = ConsoleColor.Black;
            // Title Banner
            DisplayTitleBanner();
            // Frame
            DisplayFrame();
            // Closed deck
            DisplayDeck();
            //Manual
            DisplayManual();
        }
        /// <summary>
        /// This method is used to display the closed deck
        /// </summary>
        public void DisplayDeck()
        {
            int left = 55;
            int top = 12;
            Console.ForegroundColor = ConsoleColor.Green;
            List<string> deck = GetAsciiForDeck();
            foreach (string design in deck)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(design);
                top++;
            }
        }

        /// <summary>
        /// This method is used to display blank card/ clear the played card
        /// </summary>
        public void DisplayBlankCard()
        {
            int left = 120;
            int top = 12;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            List<string> deck = GetAsciiForFaceOfCardBlank();
            foreach (string design in deck)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(design);
                top++;
            }
            Console.SetCursorPosition(Console.CursorLeft - 26, top++);
        }



        /// <summary>
        /// This method displays the user manual of the game
        /// </summary>
        public void DisplayManual()
        {
            // Play Card
            DisplayText(DisplayMessageConstatns.GAME_KEY_PLAY_CARD_MANUAL, 85, 15, ConsoleColor.DarkGray);
            // Shuffle
            DisplayText(DisplayMessageConstatns.GAME_KEY_SHUFFLE_MANUAL, 85, 18, ConsoleColor.White);
            // Restart
            DisplayText(DisplayMessageConstatns.GAME_KEY_RESTART_MANUAL, 85, 21, ConsoleColor.DarkMagenta);

        }

        /// <summary>
        /// This method is used to display the frame for the game screen
        /// </summary>
        public void DisplayFrame()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("                                          ¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦                                                                                                                         ¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦");
            Console.WriteLine("                                          ¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦");
        }

        /// <summary>
        /// This method is used to display the face of the open card
        /// </summary>
        /// <param name="cardProp">Card design properties</param>
        public void DisplayCardFace(CardDesignPropertiesModel cardProp)
        {
            int left = 120;
            int top = 12;
            foreach (string design in cardProp.CardPattern)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = cardProp.Color;
                Console.SetCursorPosition(left, top);
                Console.Write(design);
                top++;
            }
            Console.SetCursorPosition(Console.CursorLeft - cardProp.CardWidth, top++);
        }

        #endregion

        #region Display Utility Method

        /// <summary>
        /// This method clears the current line by considering the current text in the respective line
        /// </summary>
        /// <param name="text">Current text int the current line</param>
        public void ClearCurentLine(string text)
        {
            int left = Console.CursorLeft;
            Console.CursorLeft = left - text.Length;
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(" ");
            }
            Console.CursorLeft = Console.CursorLeft - text.Length;
        }
        /// <summary>
        /// This method handles response object i.e displaying the success and error/warning/information messages
        /// </summary>
        /// <typeparam name="T">Type of data type in the response generic type</typeparam>
        /// <param name="result">Response object</param>
        /// <param name="nextLine">True - displays text at the next line with starting relatively tot he current position, False - displays text at the current cursor position</param>
        /// <param name="clearCurrent">True- clears the current line, False - No clear </param>
        /// <param name="timeOut">Between displaying the message and clearing the current text required timeout in milliseconds</param>
        public void HandleResponse<T>(ResultModel<T> result, bool nextLine, bool clearCurrent, int timeOut) where T : class
        {
            if (result.IsError)
            {
                //Error handling logic
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                // Success Handling Logic
                if (nextLine)
                {
                    WriteAtNextLineSamePosition(result.Message);
                }
                else
                {
                    Console.Write(result.Message);
                }
                if (timeOut > 0)
                {
                    Thread.Sleep(timeOut);
                }
                if (clearCurrent)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    ClearCurentLine(result.Message);
                }

            }
        }
        /// <summary>
        /// This method used to write a text at specific position
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="left">Left position of the cursor</param>
        /// <param name="top">Top position of the cursor</param>
        public void WriteAt(string text, int left, int top)
        {
            if (left >= 0 && top >= 0)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(text);
            }
        }
        /// <summary>
        /// This method is used to display the text at the next line but at the same left position of the current cursor
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        public void WriteAtNextLineSamePosition(string text)
        {

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);
            Console.Write(text);
        }
        /// <summary>
        /// This mthod displays text with al formatting and position properties
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="left">Left position of the cursor</param>
        /// <param name="top">Top positon of the cursor</param>
        /// <param name="foreGroundColor">Color of the foregorund</param>
        /// <param name="backgroundColor">Color of the background</param>
        public void DisplayText(string text, int left, int top, ConsoleColor? foreGroundColor = null, ConsoleColor? backgroundColor = null)
        {
            if (backgroundColor.HasValue)
            {
                Console.BackgroundColor = backgroundColor.Value;
            }
            if (foreGroundColor.HasValue)
            {
                Console.ForegroundColor = foreGroundColor.Value;
            }
            WriteAt(text, left, top);
        }

        #endregion

    }
}
