using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.MotionDomain;
using Assets.Scripts.Communication;
using MQTTnet;
using MQTTnet.Client;
using static System.Json.JsonObject;


namespace data_collection
{
    public partial class Form1 : Form
    {
        ABBController robot;
        List<string> errLog = new List<string>();
        public Form1()
        {
            InitializeComponent();//InitUserFace
            robot = new ABBController();
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
                item.SubItems.Add(robot.GetController(i).OperatingMode.ToString());
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
                List<string> list = robot.GetTorqueValue(i);
                if(list!= null && list.Count > 0)
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
            for (int i = 0; i < robot.controllers.Count(); i++)
            {
                List<string> list = new List<string> { "aiG1MtrlTmp",
                            "aiG1MtrlPrs",
                            "doG1Needle1",
                            "doG1Needle2",
                            "doG1Needle3",
                            "doD1CircStart",
                            "aiD1A_Torque",
                            "aiD1A_DriveTmp",
                            "aiD1MtrlSupPrs",
                            "aiD1A_MtrlPrsOut",
                            "aiD1B_MtrlPrsOut",
                            "doD1A_ValveFill",
                            "doD1B_ValveFill",
                            "doD1A_ValveOut",
                            "doD1B_ValveOut",
                            "aoG1Fluid",
                            "aoG1PRA_SP",
                            "aiG1PRA_MotAct",
                            "aiPelB_TmpSP",
                            "aiPel1A_Tmp",
                            "aiPel1B_Tmp",
                            "aiPel1A_Water",
                            "aiPel1B_Water",
                            "aoPel1A_TmpSP",
                            "aoPel1B_TmpSP",
                            "aiPel1A_Tmp",
                            "aiPel1B_Tmp",
                            "aoG1MtrlTmpSP",
                            "aiPel1A_FanCurr",
                            "aiG1MtrlTmpSP",
                            "aoG1MtrlTmpSP",
                            "doD1ValveCirc",
                            "go_cleantimes",
                            "do13_JobInPro"
                };
                List<String> list1 = new List<String>();
                for (int j = 0; j < list.Count; j++)
                {
                    var signal = robot.GetController(i).IOSystem.GetSignal(list[j]);
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
            errLog = robot.errLogger(errLog, "");
            richTextBox1.Lines = errLog.ToArray();

        }
    }
}
