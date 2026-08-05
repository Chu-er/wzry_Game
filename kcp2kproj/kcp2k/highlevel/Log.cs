// A simple logger class that uses Console.WriteLine by default.
// Can also do Logger.InfoHandler = Debug.Log for Unity etc.
// (this way we don't have to depend on UnityEngine)
using System;
using System.Runtime.CompilerServices;

namespace kcp2k
{
    public static class Log
    {
        public static Action<string> InfoHandler    = Console.WriteLine;
        public static Action<string> WarningHandler = Console.WriteLine;
        public static Action<string> ErrorHandler   = Console.Error.WriteLine;

        // appends "(at <file>:<line>)" of the call site to every message.
        // consoles which linkify absolute paths (VS Code / Cursor / Rider)
        // turn this into a clickable jump to the logging line.
        public static bool IncludeCallSite = true;

        // 包裹调用点后缀，使其与日志正文区分开。
        // 默认使用 ANSI 黄色转义码。不支持 ANSI 的控制台把两者都设为 ""，
        // Unity 富文本控制台可设为 "<color=yellow>" / "</color>"。
        public static string CallSiteColor      = "\u001b[33m";
        public static string CallSiteColorReset = "\u001b[0m";

        public static void Info(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) =>
            InfoHandler(WithCallSite(message, file, line));

        public static void Warning(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) =>
            WarningHandler(WithCallSite(message, file, line));

        public static void Error(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) =>
            ErrorHandler(WithCallSite(message, file, line));

        static string WithCallSite(string message, string file, int line) =>
            IncludeCallSite
                ? $"{message} {CallSiteColor}(at {file}:{line}){CallSiteColorReset}"
                : message;
    }
}
