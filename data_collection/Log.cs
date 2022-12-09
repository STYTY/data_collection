using System;

namespace MqttConnector
{
    class Log
    {
        private static string GetTime() => DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss:ffff]");

        /// <summary>
        /// debug日志，release模式下不显示
        /// </summary>
        /// <param name="m">模块名</param>
        /// <param name="s">日志信息</param>
        public static void Debug(string m, string s)
        {
#if DEBUG
            Console.WriteLine($"{GetTime()} [调试][{m}] {s}");
#endif
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="m">模块名</param>
        /// <param name="s">日志信息</param>
        public static void Info(string m, string s) =>
            Console.WriteLine($"{GetTime()} [信息][{m}] {s}");

        /// <summary>
        /// 显示警告
        /// </summary>
        /// <param name="m">模块名</param>
        /// <param name="s">日志信息</param>
        public static void Warn(string m, string s) =>
            Console.WriteLine($"{GetTime()} [警告][{m}] {s}");

        /// <summary>
        /// 显示错误，并退出软件
        /// 一般只用在开头启动时，软件运行中别用
        /// </summary>
        /// <param name="m">模块名</param>
        /// <param name="s">日志信息</param>
        public static void Error(string m, string s)
        {
            Console.WriteLine($"{GetTime()} [错误][{m}] {s}");
            Environment.Exit(-1);
        }
    }
}
