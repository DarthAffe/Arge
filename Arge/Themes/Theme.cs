﻿using ReactiveUI;

namespace Arge.Themes
{
    public class Theme : ReactiveObject
    {
        #region Properties & Fields

        public string Name { get; }
        public string Path { get; }

        #endregion

        #region Constructors

        public Theme(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }

        #endregion
    }
}
