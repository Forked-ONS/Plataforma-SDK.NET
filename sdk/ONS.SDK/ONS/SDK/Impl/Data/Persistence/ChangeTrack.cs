using ONS.SDK.Domain.Base;

namespace ONS.SDK.Impl.Data.Persistence
{
    public class ChangeTrack
    {
        public static string CREATE = "create";
        public static string UPDATE = "update";
        public static string DELETE = "destroy";

        public static bool IsCreate(string changeTrack) {
            return string.Equals(CREATE, changeTrack);
        }

        public static bool IsUpdate(string changeTrack) {
            return string.Equals(UPDATE, changeTrack);
        }

        public static bool IsDelete(string changeTrack) {
            return string.Equals(DELETE, changeTrack);
        }

        public static bool IsTracking(string changeTrack) {
            return IsCreate(changeTrack) || IsUpdate(changeTrack) || IsDelete(changeTrack);
        }

        public static bool IsTracking(Model model) {
            return IsTracking(model._Metadata.ChangeTrack);
        }
    }
}