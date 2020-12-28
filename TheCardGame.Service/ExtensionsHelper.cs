using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security.Cryptography;

namespace TheCardGame.Service
{
    /// <summary>
    /// This class is used for extensions
    /// </summary>
    public static class ExtensionsHelper
    {
        /// <summary>
        /// This method is used to shuffle the items of a list of type T
        /// </summary>
        /// <typeparam name="T">List of type</typeparam>
        /// <param name="list">List object</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
