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
    }
}
