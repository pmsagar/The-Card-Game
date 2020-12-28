using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TheCardGame.ServiceInterface;

namespace TheCardGame.Service
{
    /// <summary>
    /// This class used for configuration activities
    /// </summary>
    public class ConfigurationService:IConfigurationService
    {
        #region Private Variables

        /// <summary>
        /// Configuration 
        /// </summary>
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor
        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is used to read delimited value of the given configuration key
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <param name="delimiter">Delimiter</param>
        /// <returns>List of values. If the given key is not present it returns null</returns>
        public string[] ReadDelimitedValue(string key, string delimiter)
        {
            IConfigurationSection section = _configuration.GetSection(key);
            string value = section.Value;
            if (!string.IsNullOrWhiteSpace(value))
            {
                string[] values = value.Split(delimiter);
                return values;
            }
            else
            {
                return null;
            }
        }

        #endregion

    }
}
