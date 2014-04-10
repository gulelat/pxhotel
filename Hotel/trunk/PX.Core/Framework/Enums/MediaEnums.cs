namespace PX.Core.Framework.Enums
{
    public class MediaEnums
    {
        public enum MoveNodeStatusEnums
        {
            Success = 1,
            Failure = 2,
            MoveParentNodeToChild = 3,
            MoveNodeToFile = 4,
            MoveSameLocation = 5
        }

        public enum RenameStatusEnums
        {
            Success = 1,
            Failure = 2,
            DuplicateName = 3
        }

        public enum EditImageEnums
        {
            SaveFail = 0,
            SaveSuccess = 1,
            OverWriteConfirm = 2
        }
    }
}
