using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Services.Impl;

namespace BeyondNet.Ddd.Services.Interfaces
{
    public interface ITrackingStateManager
    {
        bool IsDeleted { get; }
        bool IsDirty { get; }
        bool IsNew { get; }
        bool IsSelftDeleted { get; }

        TrackingStateManager GetTracking<TProp>(TProp props) where TProp : IProps;
        void MarkAsClean();
        void MarkAsDeleted();
        void MarkAsDirty();
        void MarkAsNew();
        void MarkAsSelfDeleted();
    }
}