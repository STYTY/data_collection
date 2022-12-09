using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MqttConnector
{
    class Utils
    {
        /// <summary>
        /// 全局配置
        /// </summary>
        public static Settings Setting = new Settings();
        /// <summary>
        /// 软件根目录完整路径
        /// </summary>
        public static string Path;
        /// <summary>
        /// 初始化函数
        /// </summary>
        public static void Initial()
        {
            using (var processModule = Process.GetCurrentProcess().MainModule)
                Path = System.IO.Path.GetDirectoryName(processModule?.FileName) + "/";
            if (File.Exists(Path + "settings.json"))
            {
                Setting = JsonConvert.DeserializeObject<Settings>(
                    File.ReadAllText(Path + "settings.json"));
            }
            else
            {
                Setting = new Settings();
            }
            connector.Mqtt.Initial();
            Setting.MqttEnable = Setting.MqttEnable;
        }

        public static void Work()
        {
            while (true)
            {
                Thread.Sleep(2000);
                var topic = "v1/devices/me/telemetry";
                bool v = connector.Mqtt.Publish(topic, "{'aaaaa': '54651'}", 0);
                Console.WriteLine(v);
                //Console.Write("Press any key to continue . . . ");
                //Console.ReadKey(true);
            }

        }




        ///  <summary>
        /// 获取指定驱动器的剩余空间总大小(单位为MB)
        ///  </summary>
        ///  <param name="str_HardDiskName">只需输入代表驱动器的字母即可 </param>
        ///  <returns> </returns>
        public static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024);
                }
            }
            return freeSpace;
        }

    }
}
