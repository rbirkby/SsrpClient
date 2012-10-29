using System.Runtime.InteropServices;

namespace SsrpClient
{
    internal class NativeMethods
    {
        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetOEMCP();
    }
}
