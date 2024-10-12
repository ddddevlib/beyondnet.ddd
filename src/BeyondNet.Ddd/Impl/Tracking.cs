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
        /// Gets a value indicating whether the object is self-deleted.
        /// </summary>
        public bool IsSelftDeleted { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the object is deleted. 
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Gets the tracking object for the specified properties.
        /// </summary>
        /// <typeparam name="TProp">The type of the properties.</typeparam>
        /// <param name="props">The properties to check for changes.</param>
        /// <returns>The tracking object.</returns>
        public static Tracking GetTracking<TProp>(TProp props) where TProp : IProps
        {
            return FindChanges(props);
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
                IsNew = false,
                IsSelftDeleted = false,
                IsDeleted = false
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
                IsNew = true,
                IsSelftDeleted = false,
                IsDeleted = false
            };
        }

        /// <summary>
        /// Marks the object as deleted.
        /// </summary>
        /// <returns>The tracking object</returns>
        public static Tracking MarkSelfDeleted()
        {
            return new Tracking
            {
                IsDirty = false,
                IsNew = false,
                IsSelftDeleted = true,
                IsDeleted = false
            };
        }

        /// <summary>
        /// Marks the object as deleted.
        /// </summary>
        /// <returns>The tracking object</returns>
        public static Tracking MarkDeleted() {
            return new Tracking
            {
                IsDirty = false,
                IsNew = false,
                IsSelftDeleted = false,
                IsDeleted = true
            };
        }

        /// <summary>
        /// Marks the object as clean (no changes).
        /// </summary>
        /// <returns>The tracking object.</returns>
        public static Tracking MarkClean()
        {
            return new Tracking()
            {
                IsNew = false,
                IsDirty = false,
                IsSelftDeleted = false,
                IsDeleted = false
            };
        }

        /// <summary>
        /// Finds changes in the specified properties.
        /// </summary>
        /// <typeparam name="TProp">The type of the properties.</typeparam>
        /// <param name="props">The properties to check for changes.</param>
        /// <returns><c>Tracking</c> if changes are found; otherwise, <c>false</c>.</returns>
        protected static Tracking FindChanges<TProp>(TProp props) where TProp : IProps
        {
            var tracking = MarkClean();

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

                    if (trackingValue.IsDirty) tracking = MarkDirty();
                    if (trackingValue.IsNew) tracking = MarkNew();
                    if (trackingValue.IsSelftDeleted) tracking = MarkSelfDeleted();
                    if (trackingValue.IsDeleted) tracking = MarkDeleted();
                }
            }

            return tracking;
        }
    }
}
