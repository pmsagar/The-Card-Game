using System;
using System.Collections.Generic;
using System.Text;
using TheCardGame.Models;

namespace TheCardGame.ServiceInterface
{
    public interface IResponseService
    {
         ResultModel<T> SetResponse<T>(T data, bool isError, string message) where T : class;
    }
}
