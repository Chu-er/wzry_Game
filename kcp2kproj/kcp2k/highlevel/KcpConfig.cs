// common config struct, instead of passing 10 parameters manually every time.
using System;

namespace kcp2k
{
    // [Serializable] to show it in Unity inspector.
    // 'class' so we can set defaults easily.
    [Serializable]
    public class KcpConfig
    {
        // socket configuration ////////////////////////////////////////////////
        // DualMode uses both IPv6 and IPv4. not all platforms support it.
        // (Nintendo Switch, etc.)
        public bool DualMode;

        // UDP服务器只使用一个socket。
        // 最大化缓冲区以处理尽可能多的连接。
        //
        //   M1 mac pro:
        //     默认接收缓冲区: 786896 (771 KB)
        //     默认发送缓冲区:  9216 (9 KB)
        //     最大可配置: ~7 MB
        public int RecvBufferSize;
        public int SendBufferSize;

        /// <summary>
        /// KCP configuration ///////////////////////////////////////////////////
        /// Configurable MTU in case KCP sits on top of other abstractions like
        /// encrypted transports, relays, etc.
        /// MTU (Maximum Transmission Unit) specifies the maximum packet size for a single data unit,
        /// limiting the maximum number of bytes for each KCP fragment.
        /// </summary>
        public int Mtu;

        // NoDelay is recommended to reduce latency. This also scales better
        // without buffers getting full.
        public bool NoDelay;

        // KCP internal update interval. 100ms is KCP default, but a lower
        // interval is recommended to minimize latency and to scale to more
        // networked entities.
        public uint Interval;

        // KCP fastresend parameter. Faster resend for the cost of higher
        // bandwidth.
        public int FastResend;

        // KCP congestion window heavily limits messages flushed per update.
        // congestion window may actually be broken in kcp:
        // - sending max sized message @ M1 mac flushes 2-3 messages per update
        // - even with super large send/recv window, it requires thousands of
        //   update calls
        // best to leave this disabled, as it may significantly increase latency.
        public bool CongestionWindow;

        /// <summary>
        /// SendWindowSize：发送窗口大小。指定 KCP 允许未被确认的最大数据包数量（即允许在网络中“飞行”的数据包数），增大此值可以提升高带宽/高延迟网络的吞吐量。
        /// 例如，在 Mirror 的性能测试中，对不同负载需求可提升窗口大小：
        ///   128, 128 用于 4000 个怪物
        ///   512, 512 用于 10000 个怪物
        ///   8192, 8192 用于 20000 个怪物
        /// </summary>
        public uint SendWindowSize;
        /// <summary>
        /// ReceiveWindowSize：接收窗口大小。指定 KCP 可缓存的还未交付给应用层的最大数据包数量，增大此值可以防止因应用层处理不及时而丢包。
        /// </summary>
        public uint ReceiveWindowSize;

        // timeout in milliseconds
        public int Timeout;

        // maximum retransmission attempts until dead_link
        public uint MaxRetransmits;

        // constructor /////////////////////////////////////////////////////////
        // constructor with defaults for convenience.
        // makes it easy to define "new KcpConfig(DualMode=false)" etc.
        public KcpConfig(
            bool DualMode          = true,
            int RecvBufferSize     = 1024 * 1024 * 7, // 1024 * 1024 * 7 = 7,340,032 bytes (7 MiB)
            int SendBufferSize     = 1024 * 1024 * 7,
            int Mtu                = Kcp.MTU_DEF,
            bool NoDelay           = true,
            uint Interval          = 10,
            int FastResend         = 0,
            bool CongestionWindow  = false,
            uint SendWindowSize    = Kcp.WND_SND,
            uint ReceiveWindowSize = Kcp.WND_RCV,
            int Timeout            = KcpPeer.DEFAULT_TIMEOUT,
            uint MaxRetransmits    = Kcp.DEADLINK)
        {
            this.DualMode = DualMode;
            this.RecvBufferSize = RecvBufferSize;
            this.SendBufferSize = SendBufferSize;
           
            this.Mtu = Mtu;
    
            this.NoDelay = NoDelay;
            this.Interval = Interval;
            this.FastResend = FastResend;
            this.CongestionWindow = CongestionWindow;
            this.SendWindowSize = SendWindowSize;
            this.ReceiveWindowSize = ReceiveWindowSize;
            this.Timeout = Timeout;
            this.MaxRetransmits = MaxRetransmits;
        }
    }
}
