using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
//using DataSet_Generator;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace control_db
{
    public partial class control : Form
    {
        private SerialPort serialPort = new SerialPort();
        private string ser;
        public control(string com)
        {
            InitializeComponent();
            ser = com;
        }
        private void soze()
        {
            string str = Properties.Settings.Default.StrCon;
            SqlConnection con = new SqlConnection(str);
            con.Close();
            con.Open();
            string command = "select roomTitle,roomId from RoomInfo";
            SqlCommand cmd = new SqlCommand(command, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            room.DataSource = dt;
            room.DisplayMember = "roomTitle";
            room.ValueMember = "roomId";
          //  room.SelectedIndex = 0;

            room_add.DataSource = dt;
            room_add.DisplayMember = "roomTitle";
            room_add.ValueMember = "roomId";
           // room_add.SelectedIndex = 0;

            con.Close();
        }
     
        private void control_Load(object sender, EventArgs e)
        {
            serialPort.PortName = ser;
            serialPort.BaudRate = 9600;
            serialPort.NewLine = "\n";
            try
            {
                start_serialport();
                groupBox2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            soze();
           
        }
        private void start_serialport()
        {
            serialPort.PortName = ser;
            serialPort.BaudRate = 9600;
            serialPort.NewLine = "\n";
            if (serialPort.IsOpen == true)
                serialPort.Close();
            try
            {
                serialPort.Open();
                groupBox2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dev_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = Properties.Settings.Default.StrCon;
            SqlConnection con = new SqlConnection(str);
            con.Close();
            con.Open();
            string command = "select deviceName,pinId  from DeviceInfo  where deviceName ='" + dev.Text + "'";
            SqlCommand cmd = new SqlCommand(command, con);
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string pinn = (string)dr["pinId"].ToString();
                pintxt.Text = pinn;
                string devv = (string)dr["deviceName"].ToString();
                devtxt.Text = devv;
            }

            con.Close();
          
        }

        private void room_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str = Properties.Settings.Default.StrCon;
                SqlConnection con = new SqlConnection(str);
                con.Close();
                con.Open();
                string command = "select deviceName,pinId  from DeviceInfo  where roomId =" + room.SelectedValue + "";
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string pinn = (string)dr["pinId"].ToString();
                    pintxt.Text = pinn;
                    string devv = (string)dr["deviceName"].ToString();
                    devtxt.Text = devv;
                }

                dev.DataSource = dt;
                dev.DisplayMember = "deviceName";
                dev.ValueMember = "pinId";
                dev.SelectedIndex = 0;
                con.Close();
            }
            catch { }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            string pinid = pintxt.Text;
            string x = "1." + pinid + ".1";
            serialPort.WriteLine(x);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            string pinid = pintxt.Text;
            string x = "1." + pinid + ".0";
            serialPort.WriteLine(x);
        }
       

    }
}
