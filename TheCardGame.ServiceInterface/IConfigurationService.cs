using System;
using System.Collections.Generic;
using System.Text;

namespace TheCardGame.ServiceInterface
{
    public interface IConfigurationService
    {
         string[] ReadDelimitedValue(string key, string delimiter);
    }
}
