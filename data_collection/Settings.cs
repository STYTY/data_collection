using Newtonsoft.Json;
using System.IO;

namespace MqttConnector
{
    class Settings
    {
        private bool mqttEnable = true;
        private string mqttBroker = "localhost";
        private int mqttPort = 1883;
        private string mqttUser = "robot2";
        private string mqttPassword = "robot2";
        private bool mqttTLS = false;
        private string clientID = "robot2";
        private int keepAlive = 60;


        /// <summary>
        /// 保存配置
        /// </summary>
        private void Save()
        {
            File.WriteAllText(Utils.Path + "settings.json", JsonConvert.SerializeObject(this));
        }



        /// <summary>
        /// 是否开启mqtt连接功能
        /// </summary>
        public bool MqttEnable
        {
            get => mqttEnable;
            set
            {
                mqttEnable = value;
                if (value)
                {
                    connector.Mqtt.Connect();
                }
                else
                {
                    connector.Mqtt.Disconnect();
                }
                Save();
            }
        }

        /// <summary>
        /// 启用tls
        /// </summary>
        public bool MqttTLS
        {
            get => mqttTLS;
            set
            {
                mqttTLS = value;
                Save();
            }
        }

        /// <summary>
        /// mqtt服务端
        /// </summary>
        public string MqttBroker
        {
            get => mqttBroker;
            set
            {
                mqttBroker = value;
                Save();
            }
        }

        /// <summary>
        /// mqtt端口号
        /// </summary>
        public int MqttPort
        {
            get => mqttPort;
            set
            {
                mqttPort = value;
                Save();
            }
        }

        /// <summary>
        /// mqtt服务器用户名
        /// </summary>
        public string MqttUser
        {
            get => mqttUser;
            set
            {
                mqttUser = value;
                Save();
            }
        }

        /// <summary>
        /// mqtt服务器密码
        /// </summary>
        public string MqttPassword
        {
            get => mqttPassword;
            set
            {
                mqttPassword = value;
                Save();
            }
        }

        /// <summary>
        /// GUID唯一识别码
        /// </summary>
        public string ClientID
        {
            get => clientID;
            set
            {
                clientID = value;
                Save();
            }
        }

        /// <summary>
        /// mqtt保活心跳周期
        /// </summary>
        public int KeepAlive
        {
            get => keepAlive;
            set
            {
                keepAlive = value;
                Save();
            }
        }


    }
}
