using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace control_db
{
    public partial class device_page : Form
    {
        private string room_name;
        private string dev_name;
        private string ser;
        private  string dev_on;
        private string dev_off;
        private string type;
      
        private bool flag = false;
        private SerialPort serialPort = new SerialPort();


        public device_page(string com,string p1, string p2)
        {
            InitializeComponent();
            ser = com;
             room_name = p1;
             dev_name = p2;
         
            label4.Text = dev_name + " in room " + room_name;
           
        }

        private void device()
        {
            string str = Properties.Settings.Default.StrCon;
            SqlConnection con = new SqlConnection(str);
            con.Close();
            con.Open();
            string command = "select deviceName,pinId,deviceLastState,DeviceOff,DeviceOn,pinType  from DeviceInfo  where deviceName ='" + dev_name + "'";
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
                string lstate = (string)dr["deviceLastState"].ToString();
                state.Text = lstate;
                type = (string)dr["pinType"].ToString();
                dev_on = (string)dr["DeviceOn"].ToString();
                pictureBox1.ImageLocation = dev_on;
                dev_off = (string)dr["DeviceOff"].ToString();
                pictureBox2.ImageLocation = dev_off;
                if (int.Parse(lstate) > 0)
                {
                    flag = true;
                   
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                    onPtn.Text = "Turn off ?";
                    //offPtn.Visible = true;
                }
                else
                {
                    flag = false;
                   
                    pictureBox2.Visible = true;
                    pictureBox1.Visible = false;
                    onPtn.Text = "Turn on ?";
                    //offPtn.Visible = false;
                }

            }

            con.Close();

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void device_page_Load(object sender, EventArgs e)
        {
            device();
            //try
            //{
            //    start_serialport();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
        }

        //private void onPtn_Click(object sender, EventArgs e)
        //{
        //    if (serialPort.IsOpen == true)
        //    {
        //        string pinid = pintxt.Text;
        //        string x = "1." + pinid + ".1";
        //        serialPort.WriteLine(x);
        //        pictureBox1.ImageLocation = dev_on;
        //        //onPtn.Visible = false;
        //        //offPtn.Visible = true;
                
        //    }
        //    else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
     
        //}

        //private void offPtn_Click(object sender, EventArgs e)
        //{
        //    if (serialPort.IsOpen == true)
        //    {
        //        string pinid = pintxt.Text;
        //        string x = "1." + pinid + ".0";
        //        serialPort.WriteLine(x);
        //        pictureBox1.ImageLocation = dev_off;
        //        //onPtn.Visible = true;
        //       // offPtn.Visible = false;
               
        //    }
        //    else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
                serialPort.Close();
            this.Visible=false;
            Form1 frm = new Form1();
            frm.ShowDialog();
           
        }

        private void onPtn_Click(object sender, EventArgs e)
        {

           

            if (serialPort.IsOpen == true)
                serialPort.Close();
            try
            {
                start_serialport();

                if (flag == true)
                {
                    string pinid = pintxt.Text;
                    string x = "1." + pinid + ".0";
                    serialPort.WriteLine(x);
                    pictureBox2.Visible = true;
                    pictureBox1.Visible = false;
                    onPtn.Text = "Turn on ?";
                    flag = false;
                  
                }
                else
                {
                    string pinid = pintxt.Text;
                    string x = "1." + pinid + ".1";
                    serialPort.WriteLine(x);
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                    onPtn.Text = "Turn off ?";
                    flag = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          
        }

  

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
                serialPort.Close();
             string str = Properties.Settings.Default.StrCon;
                   SqlConnection con = new SqlConnection(str);
                   con.Close();
                   con.Open();
            try{
                start_serialport();
                string pinid = pintxt.Text;
                if (type == "digital")
                {
                    string x = "3." + pinid + ".0";
                    serialPort.WriteLine(x);
                    string command = " Delete from DeviceLog Where  pinId=" + Convert.ToInt32(pintxt.Text) + "";
                   string commandd = " Delete from DeviceInfo Where pinId=" + Convert.ToInt32(pintxt.Text) + " and controlledId = "+1+"";
                    SqlCommand cmd = new SqlCommand(command, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    SqlCommand cmdd = new SqlCommand(commandd, con);
                    cmdd.ExecuteNonQuery();
                    con.Close();
                }
                else if (type == "digital_var")
                {
                    string x = "3." + pinid + ".1";
                    serialPort.WriteLine(x);
                    string command = " Delete from DeviceLog Where  pinId=" + Convert.ToInt32(pintxt.Text) + "";
                  string  commandd = " Delete from DeviceInfo Where pinId=" + Convert.ToInt32(pintxt.Text) + " and controlledId = " + 1 + "";
                    
                  //  string command = "update DeviceInfo set deviceLastState=" + -1 + " where pinId=" + Convert.ToInt32(pintxt.Text) + "";
                    SqlCommand cmd = new SqlCommand(command, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    SqlCommand cmdd = new SqlCommand(commandd, con);
                    cmdd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    string x = "3." + pinid + ".2";
                    serialPort.WriteLine(x);
                    string command = " Delete from DeviceLog Where  pinId=" + Convert.ToInt32(pintxt.Text) + "";
                   string commandd =" Delete from DeviceInfo Where pinId=" + Convert.ToInt32(pintxt.Text) + " and controlledId = "+1+"";
                    //
                   // string command = "update DeviceInfo set deviceLastState=" + -1 + " where pinId=" + Convert.ToInt32(pintxt.Text) + "";
                    SqlCommand cmd = new SqlCommand(command, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    SqlCommand cmdd = new SqlCommand(commandd, con);
                    cmdd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Device Removed Successfully");
                if (serialPort.IsOpen == true)
                    serialPort.Close();
                this.Visible = false;
                Form1 frm = new Form1();
                frm.ShowDialog();
               
               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
