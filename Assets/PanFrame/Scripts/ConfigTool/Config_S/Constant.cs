namespace SQFramework
{
    public static class Constant
    {

        public const string CONFIG_DIR_NAME = "PanFrame/Config";

        public const string CONFIG_FILE_NAME = "config.ini";

        public static string[] SUPPORTED_PICTURE_FORMAT = { ".jpeg", ".jpg", ".png" };

        public static string[] SUPPORTED_VIDEO_FORMAT = { ".ogg", ".ogv", ".mp4", ".mpeg", ".avi",
        ".flv", ".mkv", ".wmv", ".mov" };

        public static class ConfigField
        {
            public const string RES_PATH = "res_path";


        }

        public static class Mode
        {
            //透视模式
            public const string PERSPECTIVE = "1";
            //触发模式
            public const string TRIGGER = "2";
        }

        public enum ResType
        {
            PICTURE,
            PICTURE_FRAMES,
            VIDEO
        }
    }
}