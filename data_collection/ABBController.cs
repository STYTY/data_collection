using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.IOSystemDomain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Json;

namespace data_collection
{
    internal class ABBController
    {
        public ControllerInfoCollection controllers = null;

        public List<string> errLogger(List<string> err, string str)
        {
            err.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "    " + str);
            Console.Write(err.ToArray());
            return err;
        }

        public void Scan()
        {
            NetworkScanner netscan = new NetworkScanner();
            netscan.Scan();
            controllers = netscan.Controllers;

        }

        public Controller GetController(int Select)
        {
            return new Controller(controllers[Select]);
        }

        public List<String> GetTorqueValue(int Select)
        {
            Controller controller = GetController(Select);
            List<string> list = new List<string>();
            try
            {
                var torque1 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_torque1").Value.ToString();
                var torque2 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_torque2").Value.ToString();
                var torque3 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_torque3").Value.ToString();
                var torque4 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_torque4").Value.ToString();
                var torque5 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_torque5").Value.ToString();
                var torque6 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_torque6").Value.ToString();
                    
                var speed1 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_speed1").Value.ToString();
                var speed2 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_speed2").Value.ToString();
                var speed3 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_speed3").Value.ToString();
                var speed4 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_speed4").Value.ToString();
                var speed5 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_speed5").Value.ToString();
                var speed6 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_speed6").Value.ToString();

                var pos1 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_pos1").Value.ToString();
                var pos2 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_pos2").Value.ToString();
                var pos3 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_pos3").Value.ToString();
                var pos4 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_pos4").Value.ToString();
                var pos5 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_pos5").Value.ToString();
                var pos6 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "axis_pos6").Value.ToString();

                var motor_torque1 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "GO_Torque1").Value.ToString();
                var motor_torque2 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "GO_Torque2").Value.ToString();
                var motor_torque3 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "GO_Torque3").Value.ToString();
                var motor_torque4 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "GO_Torque4").Value.ToString();
                var motor_torque5 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "GO_Torque5").Value.ToString();
                var motor_torque6 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "GO_Torque6").Value.ToString();

                var ext_torque1 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "ext_torque1").Value.ToString();
                var ext_torque2 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "ext_torque2").Value.ToString();
                var ext_torque3 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "ext_torque3").Value.ToString();
                var ext_torque4 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "ext_torque4").Value.ToString();
                var ext_torque5 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "ext_torque5").Value.ToString();
                var ext_torque6 = controller.Rapid.GetRapidData("Torque", "TorqueCheck", "ext_torque6").Value.ToString();

                list.Add(torque1);
                list.Add(torque2);
                list.Add(torque3);
                list.Add(torque4);
                list.Add(torque5);
                list.Add(torque6);
                list.Add(speed1);
                list.Add(speed2);
                list.Add(speed3);
                list.Add(speed4);
                list.Add(speed5);
                list.Add(speed6);
                list.Add(pos1);
                list.Add(pos2);
                list.Add(pos3);
                list.Add(pos4);
                list.Add(pos5);
                list.Add(pos6);
                list.Add(motor_torque1);
                list.Add(motor_torque2);
                list.Add(motor_torque3);
                list.Add(motor_torque4);
                list.Add(motor_torque5);
                list.Add(motor_torque6);
                list.Add(ext_torque1);
                list.Add(ext_torque2);
                list.Add(ext_torque3);
                list.Add(ext_torque4);
                list.Add(ext_torque5);
                list.Add(ext_torque6);
            }
            catch (Exception)
            {
                return list;
            }


            return list;
        }
        public JsonObject GetTorqueList(int Select)
        {
            JsonObject json = new JsonObject();
            try
            {
                List<string> list = GetTorqueValue(Select);

                json["torque1"] = list[0];
                json["torque2"] = list[1];
                json["torque3"] = list[2];
                json["torque4"] = list[3];
                json["torque5"] = list[4];
                json["torque6"] = list[5];
                json["speed1"] = list[6];
                json["speed2"] = list[7];
                json["speed3"] = list[8];
                json["speed4"] = list[9];
                json["speed5"] = list[10];
                json["speed6"] = list[11];
                json["pos1"] = list[12];
                json["pos2"] = list[13];
                json["pos3"] = list[14];
                json["pos4"] = list[15];
                json["pos5"] = list[16];
                json["pos6"] = list[17];
                json["motor_torque1"] = list[18];
                json["motor_torque2"] = list[19];
                json["motor_torque3"] = list[20];
                json["motor_torque4"] = list[21];
                json["motor_torque5"] = list[22];
                json["motor_torque6"] = list[23];
                json["ext_torque1"] = list[24];
                json["ext_torque2"] = list[25];
                json["ext_torque3"] = list[26];
                json["ext_torque4"] = list[27];
                json["ext_torque5"] = list[28];
                json["ext_torque6"] = list[29];
            }
            catch (Exception)
            {
                return null;
            }

            return json;
        }

        public float GetIOValue(int Select, string name)
        {
            Controller controller = GetController(Select);
            Signal s = controller.IOSystem.GetSignal(name);
            return s.Value;
        }

        public List<string> GetIOList(int Select)
        {
            string jsonfile = @".\config.json"; ;//JSON文件路径
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    var value = o["io"].ToString();
                    var v = JsonConvert.DeserializeObject<List<string>>(value);
                    return v;
                }
            }

            //List<String> IOlistValue = new List<String>();
            //for (int j = 0; j < IOlist.Count; j++)
            //{
            //    var signal = GetController(Select).IOSystem.GetSignal(IOlist[j]);
            //    float value;
            //    if (signal == null)
            //    {
            //        value = 0;
            //    }
            //    else
            //    {
            //        value = signal.Value;
            //    }
            //    IOlistValue.Add(value.ToString());
            //}
            //Thread.Sleep(3000);
            //return IOlistValue;
        }

        public JsonObject GetIOValueList(int Select)
        {

            JsonObject json = new JsonObject();
            List<string> list = GetIOList(Select);
            for (int j = 0; j < list.Count; j++)
            {
                var signal = GetController(Select).IOSystem.GetSignal(list[j]);
                float value;
                if (signal == null)
                {
                    value = 0;
                }
                else
                {
                    value = signal.Value;
                }
                json[list[j]] = value;
            }
            return json;
        }
    }
}
