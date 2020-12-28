using System;
using System.Collections.Generic;
using System.Text;

namespace TheCardGame.Models
{
    public class CardDesignPropertiesModel
    {
        public ConsoleColor Color { get; set; }
        public string CardText { get; set; }
        public List<string> CardPattern { get; set; }
        public int CardWidth { get; set; }
        public int CardHeight { get; set; }
    }
}
