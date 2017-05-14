using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace Arge.Misc
{
    public abstract class AbstractBindable : PropertyChangedBase
    {
        #region Method

        /// <summary>
        /// Checks if the property already matches the desirec value or needs to be updated.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to the backing-filed.</param>
        /// <param name="value">Value to apply.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual bool RequiresUpdate<T>(ref T storage, T value)
        {
            return !Equals(storage, value);
        }

        /// <summary>
        /// Checks if the property already matches the desired value and updates it if not.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to the backing-filed.</param>
        /// <param name="value">Value to apply.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This value is optional 
        /// and can be provided automatically when invoked from compilers that support <see cref="CallerMemberNameAttribute"/>.</param>
        /// <returns><c>true</c> if the value was changed, <c>false</c> if the existing value matched the desired value.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (!RequiresUpdate(ref storage, value)) return false;

            storage = value;
            // ReSharper disable once ExplicitCallerInfoArgument
            NotifyOfPropertyChange(propertyName);
            return true;
        }

        #endregion
    }
}
