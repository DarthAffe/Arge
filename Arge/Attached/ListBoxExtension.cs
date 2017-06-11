using System;
using System.Windows;
using System.Windows.Controls;

namespace Arge.Attached
{
    public class ListBoxExtension
    {
        #region AttachedProperties

        // ReSharper disable once InconsistentNaming
        public static readonly DependencyProperty AlwaysSelectItemProperty = DependencyProperty.RegisterAttached(
            "AlwaysSelectItem", typeof(bool), typeof(ListBoxExtension), new PropertyMetadata(default(bool), AlwaysSelectItemChanged));

        public static void SetAlwaysSelectItem(DependencyObject element, bool value) => element.SetValue(AlwaysSelectItemProperty, value);
        public static bool GetAlwaysSelectItem(DependencyObject element) => (bool)element.GetValue(AlwaysSelectItemProperty);

        #endregion

        #region Methods

        private static void AlwaysSelectItemChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ListBox listBox = dependencyObject as ListBox;
            if (listBox == null) return;

            if (dependencyPropertyChangedEventArgs.NewValue as bool? == true)
                listBox.SelectionChanged += AlwaysSelectItemSelectionChanged;
            else
                listBox.SelectionChanged -= AlwaysSelectItemSelectionChanged;
        }

        private static void AlwaysSelectItemSelectionChanged(object sender,
                                                             SelectionChangedEventArgs selectionChangedEventArgs)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == null) return;

            if (listBox.SelectedItem != null) return;

            object selectionTarget = null;
            if (selectionChangedEventArgs.RemovedItems.Count > 0)
            {
                object unselectedItem = selectionChangedEventArgs.RemovedItems[0];
                if (listBox.Items.Contains(unselectedItem))
                    selectionTarget = unselectedItem;
            }

            if ((selectionTarget == null) && (listBox.Items.Count > 0))
                selectionTarget = listBox.Items[0];

            if (selectionTarget != null)
                listBox.Dispatcher.BeginInvoke(new Action(() => listBox.SelectedItem = selectionTarget));
        }

        #endregion
    }
}
