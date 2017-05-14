﻿using System.Windows;

namespace Arge.Misc
{
    public class BindingProxy : Freezable
    {
        #region Properties & Fields
        // ReSharper disable InconsistentNaming

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        // ReSharper restore InconsistentNaming
        #endregion

        #region Methods

        protected override Freezable CreateInstanceCore() => new BindingProxy();

        #endregion
    }
}
