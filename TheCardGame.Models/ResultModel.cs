using System;
using System.Collections.Generic;
using System.Text;

namespace TheCardGame.Models
{
    public class ResultModel<T> where T: class
    {
        public T Data { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}
