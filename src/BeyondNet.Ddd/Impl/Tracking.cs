using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Impl
{
    /// <summary>
    /// Represents a tracking mechanism for detecting changes in objects.
    /// </summary>
    public class Tracking
    {
        private const string TrackingKeyName = "Tracking";

        /// <summary>
        /// Gets a value indicating whether the object is new.
        /// </summary>
        public bool IsNew { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the object is dirty (has changes).
        /// </summary>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Marks the object as clean (no changes).
        /// </summary>
        /// <returns>The tracking object.</returns>
        public static Tracking MarkClean()
        {
            return new Tracking()
            {
                IsNew = false,
                IsDirty = false
            };
        }

        /// <summary>
        /// Marks the object as dirty (has changes) based on the specified properties.
        /// </summary>
        /// <typeparam name="TProp">The type of the properties.</typeparam>
        /// <param name="props">The properties to check for changes.</param>
        /// <returns>The tracking object.</returns>
        public static Tracking MarkDirty<TProp>(TProp props) where TProp : IProps
        {
            var isDirty = FindChanges(props);

            if (isDirty)
            {
                return new Tracking()
                {
                    IsDirty = true,
                    IsNew = false
                };
            }

            return MarkNew();
        }

        /// <summary>
        /// Marks the object as dirty (has changes).
        /// </summary>
        /// <returns>The tracking object.</returns>
        public static Tracking MarkDirty()
        {
            return new Tracking()
            {
                IsDirty = true,
                IsNew = false
            };
        }

        /// <summary>
        /// Marks the object as new.
        /// </summary>
        /// <returns>The tracking object.</returns>
        public static Tracking MarkNew()
        {
            return new Tracking
            {
                IsDirty = false,
                IsNew = true
            };
        }

        /// <summary>
        /// Finds changes in the specified properties.
        /// </summary>
        /// <typeparam name="TProp">The type of the properties.</typeparam>
        /// <param name="props">The properties to check for changes.</param>
        /// <returns><c>true</c> if changes are found; otherwise, <c>false</c>.</returns>
        protected static bool FindChanges<TProp>(TProp props) where TProp : IProps
        {
            if (props == null)
            {
                throw new ArgumentNullException(nameof(props));
            }

            foreach (var prop in props.GetType().GetProperties())
            {
                var value = prop.GetValue(props);

                if (value == null) continue;

                var trackingProperty = value.GetType().GetProperty(TrackingKeyName);

                if (trackingProperty != null)
                {
                    var trackingValue = (Tracking)trackingProperty.GetValue(value)!;

                    if (trackingValue.IsDirty) return true;
                }
            }

            return false;
        }
    }
}
