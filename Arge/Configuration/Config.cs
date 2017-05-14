using System;
using System.Collections.Generic;
using System.ComponentModel;
using Arge.Extensions;

namespace Arge.Configuration
{
    public class Config : Dictionary<ConfigEntryType, string>
    {
        #region Constants

        private const string DEFAULT_FILE = "arge.config";

        #endregion

        #region Properties & Fields

        public static Config _instance;
        public static Config Instance => _instance ?? (_instance = new Config());

        private string _currentFile;

        #endregion

        #region Constructors

        private Config()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            foreach (ConfigEntryType configEntryType in Enum.GetValues(typeof(ConfigEntryType)))
                this[configEntryType] = configEntryType.GetAttributeOfType<DefaultValueAttribute>()?.Value as string;
        }

        public void Load(string file = null)
        {
            file = file ?? _currentFile ?? DEFAULT_FILE;
            if (string.IsNullOrWhiteSpace(file)) return;

            Initialize();

            //TODO DarthAffe 14.05.2017: Load Config

            _currentFile = file;
        }

        public void Save(string file = null)
        {
            file = file ?? _currentFile ?? DEFAULT_FILE;
            if (string.IsNullOrWhiteSpace(file)) return;

            //TODO DarthAffe 14.05.2017: Save Config

            _currentFile = file;
        }

        public string GetEntry(ConfigEntryType entry)
        {
            return this[entry];
        }

        public T GetEntry<T>(ConfigEntryType entry)
        {
            return (T)Convert.ChangeType(GetEntry(entry), typeof(T));
        }

        #endregion
    }
}
