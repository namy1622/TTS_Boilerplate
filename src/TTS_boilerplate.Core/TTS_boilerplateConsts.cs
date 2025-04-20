using TTS_boilerplate.Debugging;

namespace TTS_boilerplate
{
    public class TTS_boilerplateConsts
    {
        public const string LocalizationSourceName = "TTS_boilerplate";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "0e6db467324840b5a5c0b769f494c27a";
    }
}
