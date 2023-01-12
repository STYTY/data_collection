using MqttConnector;
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ABB.Robotics.Controllers.EventLogDomain;
using MySqlX.XDevAPI;
using S7;
using S7.Net;
using ABB.Robotics.Controllers.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace data_collection
{
    public partial class Form1 : Form
    {
        ABBController robot;
        List<string> errLog = new List<string>();
        private bool _IsLoading = false;

        MysqlConnector mc = new MysqlConnector();


        public Form1()
        {
            InitializeComponent();//InitUserFace
            CheckForIllegalCrossThreadCalls = false;
            robot = new ABBController();

            // mqtt start
            Utils.Initial();

            //robot.Scan();
            this.listView1.Items.Clear();
            robot.Scan();
            ListViewItem item = null;
            if (robot.controllers == null) return;
            for (int i = 0; i < robot.controllers.Count(); i++)

            {
                item = new ListViewItem(robot.controllers[i].IPAddress.ToString());
                item.SubItems.Add(robot.controllers[i].Id);
                item.SubItems.Add(robot.controllers[i].Availability.ToString());
                item.SubItems.Add(robot.controllers[i].IsVirtual.ToString());
                item.SubItems.Add(robot.controllers[i].SystemName);
                item.SubItems.Add(robot.controllers[i].Version.ToString());
                item.SubItems.Add(robot.controllers[i].ControllerName);
                //item.SubItems.Add(robot.GetController(i).OperatingMode.ToString());
                item.SubItems.Add(robot.controllers[i].SystemId.ToString());
                this.listView1.Items.Add(item);
                item.Tag = robot.controllers[i];
            }
            errLog = robot.errLogger(errLog, "[Scan]             " + robot.controllers.Count().ToString() + "  controllers");
            richTextBox1.Lines = errLog.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //NetworkScanner netscan = new NetworkScanner();
            //netscan.Scan();
            //controllers = netscan.Controllers;
            this.listView1.Items.Clear();
            robot.Scan();
            ListViewItem item = null;
            if (robot.controllers == null) return;
            for (int i = 0; i < robot.controllers.Count(); i++)
            {
                item = new ListViewItem(robot.controllers[i].IPAddress.ToString());

                item.SubItems.Add(robot.controllers[i].Id);
                item.SubItems.Add(robot.controllers[i].Availability.ToString());
                item.SubItems.Add(robot.controllers[i].IsVirtual.ToString());
                item.SubItems.Add(robot.controllers[i].SystemName);
                item.SubItems.Add(robot.controllers[i].Version.ToString());
                item.SubItems.Add(robot.controllers[i].ControllerName);
                //item = new ListViewItem(robot.controllers[i].SystemId.ToString());//Guid SystemId
                item.SubItems.Add(robot.GetController(i).OperatingMode.ToString());
                item.SubItems.Add(robot.controllers[i].SystemId.ToString());
                //item.SubItems.Add(robot.controllers[i].ControllerName);//PP to Main
                this.listView1.Items.Add(item);
                item.Tag = robot.controllers[i];
            }
            errLog = robot.errLogger(errLog, "[Scan]             " + robot.controllers.Count().ToString() + "  controllers");
            richTextBox1.Lines = errLog.ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
            {
                List<string> list = robot.GetTorqueValue(this.listView1.SelectedIndices[0]);
                if (list != null && list.Count > 0)
                {
                    this.textBox1.Text = list[0];
                    this.textBox2.Text = list[1];
                    this.textBox3.Text = list[2];
                    this.textBox4.Text = list[3];
                    this.textBox5.Text = list[4];
                    this.textBox6.Text = list[5];

                    this.textBox7.Text = list[11];
                    this.textBox8.Text = list[10];
                    this.textBox9.Text = list[9];
                    this.textBox10.Text = list[8];
                    this.textBox11.Text = list[7];
                    this.textBox12.Text = list[6];

                    this.textBox13.Text = list[17];
                    this.textBox14.Text = list[16];
                    this.textBox15.Text = list[15];
                    this.textBox16.Text = list[14];
                    this.textBox17.Text = list[13];
                    this.textBox18.Text = list[12];

                    this.textBox19.Text = list[23];
                    this.textBox20.Text = list[22];
                    this.textBox21.Text = list[21];
                    this.textBox22.Text = list[20];
                    this.textBox23.Text = list[19];
                    this.textBox24.Text = list[18];

                    this.textBox25.Text = list[29];
                    this.textBox26.Text = list[28];
                    this.textBox27.Text = list[27];
                    this.textBox28.Text = list[26];
                    this.textBox29.Text = list[25];
                    this.textBox30.Text = list[24];
                }
                else
                {
                    errLog = robot.errLogger(errLog, "未获取到相关torque数据");
                    richTextBox1.Lines = errLog.ToArray();
                }

            }

            //var mqttClient = new MqttFactory().CreateMqttClient();
            //var option = new MqttClientOptionsBuilder().WithTcpServer(MQTTParameter.ip, MQTTParameter.port)
            //                                   .WithClientId(MQTTParameter.clientID)
            //                                   .WithCredentials(MQTTParameter.userName, MQTTParameter.password).Build();
            //mqttClient.ConnectAsync(option);
            //String topic = "v1/devices/me/telemetry";


            //try
            //{
            //    while (true)
            //    {

            //        List<string> result = new List<string>();
            //        if (this.listView1.SelectedIndices.Count < 1)
            //        {
            //            MessageBox.Show("Please select at least 1 controller to send command.");
            //            return;
            //        }
            //        for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
            //        {
            //            List<string> list = robot.GetTorqueValue(i);
            //            List<string> IOlist = new List<string> {
            //                "aiG1MtrlTmp",
            //                "aiG1MtrlPrs",
            //                "doG1Needle1",
            //                "doG1Needle2",
            //                "doG1Needle3",
            //                "doD1CircStart",
            //                "aiD1A_Torque",
            //                "aiD1A_DriveTmp",
            //                "aiD1MtrlSupPrs",
            //                "aiD1A_MtrlPrsOut",
            //                "aiD1B_MtrlPrsOut",
            //                "doD1A_ValveFill",
            //                "doD1B_ValveFill",
            //                "doD1A_ValveOut",
            //                "doD1B_ValveOut",
            //                "aoG1Fluid",
            //                "aoG1PRA_SP",
            //                "aiG1PRA_MotAct",
            //                "aiPelB_TmpSP",
            //                "aiPel1A_Tmp",
            //                "aiPel1B_Tmp",
            //                "aiPel1A_Water",
            //                "aiPel1B_Water",
            //                "aoPel1A_TmpSP",
            //                "aoPel1B_TmpSP",
            //                "aiPel1A_Tmp",
            //                "aiPel1B_Tmp",
            //                "aoG1MtrlTmpSP",
            //                "aiPel1A_FanCurr",
            //                "aiG1MtrlTmpSP",
            //                "aoG1MtrlTmpSP",
            //                "doD1ValveCirc",
            //                "go_cleantimes"
            //            };
            //            List<String> IOlistValue = new List<String>();
            //            for (int j = 0; j < IOlist.Count; j++)
            //            {
            //                var signal = robot.GetController(i).IOSystem.GetSignal(IOlist[j]);
            //                float value;
            //                if (signal == null)
            //                {
            //                    value = 0;
            //                }
            //                else
            //                {
            //                    value = signal.Value;
            //                }
            //                IOlistValue.Add(value.ToString());
            //            }
            //            if (list != null)
            //            {
            //                JsonObject json = new JsonObject();
            //                json["torque1"] = list[0];
            //                json["torque2"] = list[1];
            //                json["torque3"] = list[2];
            //                json["torque4"] = list[3];
            //                json["torque5"] = list[4];
            //                json["torque6"] = list[5];
            //                json["speed1"] = list[6];
            //                json["speed2"] = list[7];
            //                json["speed3"] = list[8];
            //                json["speed4"] = list[9];
            //                json["speed5"] = list[10];
            //                json["speed6"] = list[11];
            //                json["pos1"] = list[12];
            //                json["pos2"] = list[13];
            //                json["pos3"] = list[14];
            //                json["pos4"] = list[15];
            //                json["pos5"] = list[16];
            //                json["pos6"] = list[17];
            //                json["motor_torque1"] = list[18];
            //                json["motor_torque2"] = list[19];
            //                json["motor_torque3"] = list[20];
            //                json["motor_torque4"] = list[21];
            //                json["motor_torque5"] = list[22];
            //                json["motor_torque6"] = list[23];
            //                json["ext_torque1"] = list[24];
            //                json["ext_torque2"] = list[25];
            //                json["ext_torque3"] = list[26];
            //                json["ext_torque4"] = list[27];
            //                json["ext_torque5"] = list[28];
            //                json["ext_torque6"] = list[29];

            //                json["aiG1MtrlTmp"] = IOlistValue[0];
            //                json["aiG1MtrlPrs"] = IOlistValue[1];
            //                json["doG1Needle1"] = IOlistValue[2];
            //                json["doG1Needle2"] = IOlistValue[3];
            //                json["doG1Needle3"] = IOlistValue[4];
            //                json["doD1CircStart"] = IOlistValue[5];
            //                json["aiD1A_Torque"] = IOlistValue[6];
            //                json["aiD1A_DriveTmp"] = IOlistValue[7];
            //                json["aiD1MtrlSupPrs"] = IOlistValue[8];
            //                json["aiD1A_MtrlPrsOut"] = IOlistValue[9];
            //                json["aiD1B_MtrlPrsOut"] = IOlistValue[10];
            //                json["doD1A_ValveFill"] = IOlistValue[11];
            //                json["doD1B_ValveFill"] = IOlistValue[12];
            //                json["doD1A_ValveOut"] = IOlistValue[13];
            //                json["doD1B_ValveOut"] = IOlistValue[14];
            //                json["aoG1Fluid"] = IOlistValue[15];
            //                json["aoG1PRA_SP"] = IOlistValue[16];
            //                json["aiG1PRA_MotAct"] = IOlistValue[17];
            //                json["aiPelB_TmpSP"] = IOlistValue[18];
            //                json["aiPel1A_Tmp"] = IOlistValue[19];
            //                json["aiPel1B_Tmp"] = IOlistValue[20];
            //                json["aiPel1A_Water"] = IOlistValue[21];
            //                json["aiPel1B_Water"] = IOlistValue[22];
            //                json["aoPel1A_TmpSP"] = IOlistValue[23];
            //                json["aoPel1B_TmpSP"] = IOlistValue[24];
            //                json["aiPel1A_Tmp"] = IOlistValue[25];
            //                json["aiPel1B_Tmp"] = IOlistValue[26];
            //                json["aoG1MtrlTmpSP"] = IOlistValue[27];
            //                json["aiPel1A_FanCurr"] = IOlistValue[28];
            //                json["aiG1MtrlTmpSP"] = IOlistValue[29];
            //                json["aoG1MtrlTmpSP"] = IOlistValue[30];
            //                json["doD1ValveCirc"] = IOlistValue[31];
            //                json["go_cleantimes"] = IOlistValue[32];
            //                // 发送消息
            //                var message = new MqttApplicationMessage()
            //                {
            //                    Topic = topic,
            //                    Payload = Encoding.UTF8.GetBytes(json.ToString())
            //                };
            //                mqttClient.PublishAsync(message);
            //            }
            //            this.textBox1.Text = list[0];
            //            this.textBox2.Text = list[1];
            //            this.textBox3.Text = list[2];
            //            this.textBox4.Text = list[3];
            //            this.textBox5.Text = list[4];
            //            this.textBox6.Text = list[5];

            //            this.textBox7.Text = list[11];
            //            this.textBox8.Text = list[10];
            //            this.textBox9.Text = list[9];
            //            this.textBox10.Text = list[8];
            //            this.textBox11.Text = list[7];
            //            this.textBox12.Text = list[6];

            //            this.textBox13.Text = list[17];
            //            this.textBox14.Text = list[16];
            //            this.textBox15.Text = list[15];
            //            this.textBox16.Text = list[14];
            //            this.textBox17.Text = list[13];
            //            this.textBox18.Text = list[12];

            //            this.textBox19.Text = list[23];
            //            this.textBox20.Text = list[22];
            //            this.textBox21.Text = list[21];
            //            this.textBox22.Text = list[20];
            //            this.textBox23.Text = list[19];
            //            this.textBox24.Text = list[18];

            //            this.textBox25.Text = list[29];
            //            this.textBox26.Text = list[28];
            //            this.textBox27.Text = list[27];
            //            this.textBox28.Text = list[26];
            //            this.textBox29.Text = list[25];
            //            this.textBox30.Text = list[24];
            //        }
            //        Thread.Sleep(300);
            //    }
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.ToString());
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.listView2.Items.Clear();
            ListViewItem item = null;
            if (robot.controllers == null) return;
            for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
            {
                List<string> list = robot.GetIOList(this.listView1.SelectedIndices[0]);
                List<String> list1 = new List<String>();
                for (int j = 0; j < list.Count; j++)
                {
                    var signal = robot.GetController(this.listView1.SelectedIndices[0]).IOSystem.GetSignal(list[j]);
                    float value;
                    if (signal == null)
                    {
                        value = 0;
                    }
                    else
                    {
                        value = signal.Value;
                    }

                    item = new ListViewItem(list[j]);
                    list1.Add(value.ToString());
                    item.SubItems.Add(value.ToString());
                    this.listView2.Items.Add(item);
                }
            }
            errLog = robot.errLogger(errLog, "读取io");
            richTextBox1.Lines = errLog.ToArray();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThreadStart thStart1 = new ThreadStart(CollectIO);//threadStart委托 
            Thread thread1 = new Thread(thStart1);

            //ThreadStart thStart2 = new ThreadStart(CollectTorque);//threadStart委托 
            //Thread thread2 = new Thread(thStart2);

            thread1.Start();
        }





        private void CollectIO()
        {
            errLog = robot.errLogger(errLog, "开始记录IO");
            richTextBox1.Lines = errLog.ToArray();
            _IsLoading = true;

            MqttConnector.connector.Mqtt.Connect();
            string topic = "v1/devices/me/telemetry";

            short Rack = 0, Slot = 2; // default for S7300
            // s7 connect
            var plc = new Plc(CpuType.S7300, "172.47.102.10", Rack, Slot);
            try
            {
                plc.Open();
            }
            catch (Exception)
            {
                //Console.WriteLine($"连接到PLC设备失败：IsConnect={plc.IsConnected}");
                errLog = robot.errLogger(errLog, $"连接到PLC设备失败：IsConnect={plc.IsConnected}");
                richTextBox1.Lines = errLog.ToArray();
                plc.Close();
                return;
            }
            
           


            while (true)
            {
                if (_IsLoading)
                {
                    for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
                    {
                        ABB.Robotics.Controllers.ControllerState state = robot.GetController(this.listView1.SelectedIndices[0]).State;
                        ABB.Robotics.Controllers.ControllerOperatingMode operatingMode = robot.GetController(this.listView1.SelectedIndices[0]).OperatingMode;

                        JsonObject jsonObject = new JsonObject();
                        jsonObject["state"] = state.ToString();
                        jsonObject["operatingMode"] = operatingMode.ToString();

                        // plc
                        // DWord
                        var text = plc.ReadBytes(DataType.DataBlock, 230, 1208, 4);
                        string type_code = Encoding.UTF8.GetString(text);
                        var skid_no = (ushort)plc.Read("DB230.DBW1218.0"); // correct
                        jsonObject["type_code"] = type_code.ToString();
                        jsonObject["skid_no"] = skid_no.ToString();

                        MqttConnector.connector.Mqtt.Publish(topic, jsonObject.ToString(), 0);
                        // io
                        JsonObject json = robot.GetIOValueList(this.listView1.SelectedIndices[0]);
                        MqttConnector.connector.Mqtt.Publish(topic, json.ToString(), 0);
                        // torque
                        JsonObject json1 = robot.GetTorqueList(this.listView1.SelectedIndices[0]);
                        if (json1 == null)
                        {
                            break;
                        }
                        MqttConnector.connector.Mqtt.Publish(topic, json1.ToString(), 0);
                    };
                    Thread.Sleep(350);
                }
                else
                {
                    MqttConnector.connector.Mqtt.Disconnect();
                    plc.Close();
                    break;
                }

            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            _IsLoading = false;
            errLog = robot.errLogger(errLog, "停止记录");
            richTextBox1.Lines = errLog.ToArray();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length; //Set the current caret position at the end
            richTextBox1.ScrollToCaret(); //Now scroll it automatically

        }

        private void button6_Click(object sender, EventArgs e)
        {
            errLog = robot.errLogger(errLog, "读取log");
            richTextBox1.Lines = errLog.ToArray();
            if (robot.controllers == null) return;

            Control.CheckForIllegalCrossThreadCalls = false;
            Thread thread = new Thread(getEventLog);
            thread.IsBackground = true;
            thread.Start();
        }

        private void getEventLog()
        {
            _IsLoading = true;
            string topic = "v1/devices/me/telemetry";

            while (true)
            {
                int i = 0;
                int LogTotalSize = 0;
                for (int j = 0; j < this.listView1.SelectedIndices.Count; j++)
                {
                    if (_IsLoading)
                    {
                        EventLogCategory[] _cats = robot.GetController(this.listView1.SelectedIndices[0]).EventLog.GetCategories();
                        foreach (EventLogCategory _cat in _cats)
                        {
                            foreach (EventLogMessage _msg in _cat.Messages)
                            {
                                JsonObject json = new JsonObject();
                                json["type"] = _msg.Type.ToString();
                                json["CategoryId"] = _msg.CategoryId.ToString();
                                json["name"] = _msg.Name.ToString();
                                json["timestamp"] = _msg.Timestamp.ToString();
                                json["sequence_number"] = _msg.SequenceNumber.ToString();
                                json["number"] = _msg.Number.ToString();
                                json["title"] = _msg.Title.ToString();
                                json["log_content"] = _msg.Body;
                                // emsg.Timestamp + " " + emsg.Number+emsg.SequenceNumber+" "+emsg.Title 
                                JsonObject log = new JsonObject();
                                log["log"] = json;
                                MqttConnector.connector.Mqtt.Publish(topic, log.ToString(), 0);
                                ++i;
                            }

                            if (LogTotalSize < i)
                            {
                                errLog = robot.errLogger(errLog, "有新的记录");
                                richTextBox1.Lines = errLog.ToArray();
                            }
                            LogTotalSize = i;
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }

        private void get_car_info()
        {
            _IsLoading = true;
            short Rack = 0, Slot = 2; // default for S7300
            using (var plc = new Plc(CpuType.S7300, "172.47.102.10", Rack, Slot))
            {
                try
                {
                    plc.Open();
                    var type_code = (string)plc.Read("DB230.DBD1208.0");
                    var skid_no = (ushort)plc.Read("DB230.DBW1218.0"); // correct
                    errLog = robot.errLogger(errLog, $"车型：{type_code}，滑橇号：{skid_no}");
                    richTextBox1.Lines = errLog.ToArray();
                }
                catch (Exception)
                {
                    errLog = robot.errLogger(errLog, $"连接到PLC设备失败：IsConnect={plc.IsConnected}");
                    richTextBox1.Lines = errLog.ToArray();
                    plc.Close();
                    return;
                }
                plc.Close();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            short Rack = 0, Slot = 2; // default for S7300
            // s7 connect
            var plc = new Plc(CpuType.S7300, "172.47.102.10", Rack, Slot);
            try
            {
                plc.Open();
                var type_code = plc.ReadBytes(DataType.DataBlock, 230, 1208, 4);
                string text = Encoding.UTF8.GetString(type_code);
                var skid_no = (ushort)plc.Read("DB230.DBW1218.0"); // correct
                errLog = robot.errLogger(errLog, $"车型：{text}，滑橇号：{skid_no}");
                richTextBox1.Lines = errLog.ToArray();
            }
            catch (Exception)
            {
                //Console.WriteLine($"连接到PLC设备失败：IsConnect={plc.IsConnected}");
                errLog = robot.errLogger(errLog, $"连接到PLC设备失败：IsConnect={plc.IsConnected}");
                richTextBox1.Lines = errLog.ToArray();
                plc.Close();
                return;
            }
            plc.Close();

        }
    }
}
