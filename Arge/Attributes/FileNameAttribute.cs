using System;

namespace Arge.Attributes
{
    public class FileNameAttribute : Attribute
    {
        #region Properties & Fields

        public string FileName { get; }

        #endregion

        #region Constructors

        public FileNameAttribute(string fileName)
        {
            this.FileName = fileName;
        }

        #endregion
    }
}
