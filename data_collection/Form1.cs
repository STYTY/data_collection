using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABB.Robotics.Controllers;

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
            try
            {
                    List<string> result = new List<string>();
                    if (this.listView1.SelectedIndices.Count < 1)
                    {
                        MessageBox.Show("Please select at least 1 controller to send command.");
                        return;
                    }
                    for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
                    {
                        List<string> list = robot.GetTorqueValue(i);
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
                    }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.listView2.Items.Clear();
            ListViewItem item = null;
            if (robot.controllers == null) return;
            for (int i = 0; i < robot.controllers.Count(); i++)
            {
                List<string> list = new List<string> { "AUTO1", "MAN1", "MOTLMP" };
                for (int j = 0; j < list.Count; j++)
                {
                    float value = robot.GetController(i).IOSystem.GetSignal(list[j]).Value;
                    item = new ListViewItem(list[j]);
                    item.SubItems.Add(value.ToString());
                    this.listView2.Items.Add(item);

                }
            }
            errLog = robot.errLogger(errLog, "");
            richTextBox1.Lines = errLog.ToArray();

        }
    }
}
