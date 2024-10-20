using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Services
{
    /// <summary>
    /// Represents a tracking mechanism for detecting changes in objects.
    /// </summary>
    public class TrackingManager
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
        public static TrackingManager GetTracking<TProp>(TProp props) where TProp : IProps
        {
            return FindChanges(props);
        }

        /// <summary>
        /// Marks the object as dirty (has changes).
        /// </summary>
        /// <returns>The tracking object.</returns>
        public static TrackingManager MarkDirty()
        {
            return new TrackingManager()
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
        public static TrackingManager MarkNew()
        {
            return new TrackingManager
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
        public static TrackingManager MarkSelfDeleted()
        {
            return new TrackingManager
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
        public static TrackingManager MarkDeleted() {
            return new TrackingManager
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
        public static TrackingManager MarkClean()
        {
            return new TrackingManager()
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
        protected static TrackingManager FindChanges<TProp>(TProp props) where TProp : IProps
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
                    var trackingValue = (TrackingManager)trackingProperty.GetValue(value)!;

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
