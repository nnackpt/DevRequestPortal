using System.Runtime.InteropServices;

namespace DevRequestPortal.Helpers
{
    public class WindowsAuthHelper
    {
        private readonly ILogger<WindowsAuthHelper> _logger;

        public WindowsAuthHelper(ILogger<WindowsAuthHelper> logger)
        {
            _logger = logger;
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool LogonUser(
                    string lpszUsername, string lpszDomain, string lpszPassword,
                    int dwLoginType, int dwLogonProvider, out IntPtr phToken
                );

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        private const int LOGON32_LOGON_NETWORK = 3;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        public bool ValidateCredentials(string username, string password)
        {
            string domain = "ap";
            string user = username;

            if (username.Contains('\\'))
            {
                var parts = username.Split('\\', 2);
                domain = parts[0];
                user = parts[1];
            }

            try
            {
                bool result = LogonUser(user, domain, password, LOGON32_LOGON_NETWORK, LOGON32_PROVIDER_DEFAULT, out IntPtr token);

                if (token != IntPtr.Zero)
                    CloseHandle(token);

                if (!result)
                    _logger.LogWarning("LogonUser failed. Error: {Error}", Marshal.GetLastWin32Error());

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Windows credential validation error");
                return false;
            }
        }
    }
}
