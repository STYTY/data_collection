using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Robotics.Controllers.IOSystemDomain;

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
            var torque1 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_torque1").Value.ToString();
            var torque2 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_torque2").Value.ToString();
            var torque3 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_torque3").Value.ToString();
            var torque4 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_torque4").Value.ToString();
            var torque5 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_torque5").Value.ToString();
            var torque6 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_torque6").Value.ToString();

            var speed1 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_speed1").Value.ToString();
            var speed2 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_speed2").Value.ToString();
            var speed3 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_speed3").Value.ToString();
            var speed4 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_speed4").Value.ToString();
            var speed5 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_speed5").Value.ToString();
            var speed6 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_speed6").Value.ToString();

            var pos1 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_pos1").Value.ToString();
            var pos2 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_pos2").Value.ToString();
            var pos3 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_pos3").Value.ToString();
            var pos4 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_pos4").Value.ToString();
            var pos5 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_pos5").Value.ToString();
            var pos6 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "axis_pos6").Value.ToString();

            var motor_torque1 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "motor_torque1").Value.ToString();
            var motor_torque2 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "motor_torque2").Value.ToString();
            var motor_torque3 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "motor_torque3").Value.ToString();
            var motor_torque4 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "motor_torque4").Value.ToString();
            var motor_torque5 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "motor_torque5").Value.ToString();
            var motor_torque6 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "motor_torque6").Value.ToString();

            var ext_torque1 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "ext_torque1").Value.ToString();
            var ext_torque2 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "ext_torque2").Value.ToString();
            var ext_torque3 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "ext_torque3").Value.ToString();
            var ext_torque4 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "ext_torque4").Value.ToString();
            var ext_torque5 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "ext_torque5").Value.ToString();
            var ext_torque6 = controller.Rapid.GetRapidData("T_ROB1", "GetTorqueValue", "ext_torque6").Value.ToString();

            List<string> list = new List<string>();
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

            return list;
        }

        public float GetIOValue(int Select, string name)
        {
            Controller controller = GetController(Select);
            Signal s =  controller.IOSystem.GetSignal(name);
            return s.Value;
        }
    }
}
