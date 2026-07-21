// kcp specific error codes to allow for error switching, localization,
// translation to Mirror errors, etc.
namespace kcp2k
{
    public enum ErrorCode : byte
    {
        /// <summary>
        /// failed to resolve a host name
        /// </summary>
        DnsResolve,
        /// <summary>
        /// ping timeout or dead link
        /// </summary>
        Timeout,
        /// <summary>
        /// more messages than transport / network can process
        /// </summary>
        Congestion,
        /// <summary>
        /// recv invalid packet (possibly intentional attack)
        /// </summary>
        InvalidReceive,
        /// <summary>
        /// user tried to send invalid data
        /// </summary>
        InvalidSend,
        /// <summary>
        /// connection closed voluntarily or lost involuntarily
        /// </summary>
        ConnectionClosed,
        /// <summary>
        /// unexpected error / exception, requires fix.
        /// </summary>
        Unexpected,
    }
}