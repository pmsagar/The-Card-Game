using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TheCardGame.Models;
using TheCardGame.ServiceInterface;

namespace TheCardGame.Service
{
    /// <summary>
    /// This class is used for response related activities like setting the response object
    /// </summary>
    public class ResponseService:IResponseService
    {

        #region Constructor

        public ResponseService()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method is used to set the response object of type T in the generic response 
        /// </summary>
        /// <typeparam name="T">Type T of the object</typeparam>
        /// <param name="data">data object</param>
        /// <param name="isError">Boolean flag for error</param>
        /// <param name="message">Information message text</param>
        /// <returns>Response object with error, message, and data</returns>
        public ResultModel<T> SetResponse<T>(T data, bool isError, string message) where T : class
        {
            ResultModel<T> resultModel = new ResultModel<T>();
            resultModel.Data = data;
            resultModel.IsError = isError;
            resultModel.Message = message;
            return resultModel;
        }

        #endregion

    }
}
