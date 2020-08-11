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
using System.Collections;
using Timer = System.Timers.Timer; 

namespace control_db
{
    public partial class Form1 : Form
    {
        private DateTime now = DateTime.Now;
        private SerialPort serialPort = new SerialPort();

        private bool flag = false;

        private string com;
        private int room_id;
        // Threads
        Thread t;
        ManualResetEvent runThread = new ManualResetEvent(false);

        // Delegates
        private delegate void DelegateAddToList(string msg);
        private DelegateAddToList m_DelegateAddToList ;
        private delegate void DelegateStopPerfmormClick();
        private DelegateStopPerfmormClick m_DelegateStop;
        private delegate void DelegateSave(string msg);
        private DelegateSave m_DelgateSave;

        //lists
        private List<message> dig_notcon = new List<message>();
        private List<message> dig_var_notcon = new List<message>();
        private List<message> alg_notcon = new List<message>();


      
       
        
        public class message
        {
            public string pin { get; set; }
            public string state { get; set; }
        }
        public class depen
        {
            public string device { get; set; }
            public int degree { get; set; }
            public string type { get; set; }
        }

        public Form1()
        {
           
            InitializeComponent();
            try
            {
                string[] allSerialPorts = SerialPort.GetPortNames();
                comboBox1.DataSource = allSerialPorts;
               comboBox1.SelectedIndex = 0;
               com = comboBox1.SelectedItem.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("connect your arduino!", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
           time.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p2.Visible = false;
            p3.Visible = false;

            m_DelegateAddToList = new DelegateAddToList(AddToList);
            m_DelegateStop = new DelegateStopPerfmormClick(close_serialport);
            m_DelgateSave = new DelegateSave(save);


            t = new Thread(ReceiveThread);
            t.Start();
            soze();
         

            Timer x = new Timer(60000);
            x.AutoReset = true;
            x.Elapsed += new System.Timers.ElapsedEventHandler(myTimer);
            x.Start();


            Timer z = new Timer(60000);
            z.AutoReset = true;
            z.Elapsed += new System.Timers.ElapsedEventHandler(myTimer2);
            z.Start();
        }
        private void myTimer2(object sender, System.Timers.ElapsedEventArgs e)
        {
              string str = Properties.Settings.Default.StrCon;
                SqlConnection con = new SqlConnection(str);
                con.Close();
                con.Open();
                string command = "  SELECT SUM(Qt) from DeviceLog";
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.ExecuteNonQuery();
                string R = cmd.ExecuteScalar().ToString();
                if (Convert.ToDouble(R) >= 7)
                    MessageBox.Show("warnning","you are about reach the limit of the hour ! ");

        }

        private void myTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string str = Properties.Settings.Default.StrCon;
                SqlConnection con = new SqlConnection(str);
                con.Close();
                con.Open();
                // int.Parse(pin_id.GetItemText(pin_id.SelectedItem));
                //room_id = int.Parse(room.SelectedValue);
                string command = "select roomId from RoomInfo";
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int room = (int)dr["roomId"];
                    depenency(room, "lght");
                    depenency(room, "tmp");
                }

            }
            catch { }
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
          //  comboBox3.SelectedIndex = 0;
            con.Close();
        }
      
        private void ReceiveThread()
        {
            while (true)
            {

                runThread.WaitOne(Timeout.Infinite);
                while (true)
                {
                    try
                    {
                        string msg = serialPort.ReadLine();
                        if (msg.Length > 36)
                        {
                            this.Invoke(this.m_DelegateAddToList, new Object[] { "R: " + msg });
                            this.Invoke(this.m_DelgateSave, new Object[] { msg });
                        }

                    }
                    catch
                    {
                        try
                        {
                            this.Invoke(this.m_DelegateStop, new Object[] { });
                        }
                        catch { }
                        runThread.Reset();
                        break;
                    }
                }
            }
        }

       private void save(string msg)
       {
           try
           {
                   string[] digital_static_pin = { "2", "4", "7", "8", "12", "13" };
                   string[] digital_var_pin = { "3", "5", "9", "10", "11" };
                   string[] analog_pin = { "20", "21", "22", "23", "24", "25" };
                   // receive data 

                   string pp = msg.Substring(1);
                   string[] sep = pp.Split('@');

                   string[] dig_static = sep[0].Split(',');
                   List<message> dig_con = new List<message>();
                   //List<message> dig_notcon = new List<message>();
                   for (int i = 0; i < dig_static.Length; i++)
                   {
                       if (Convert.ToInt32(dig_static[i]) > -1)
                       {
                           message m = new message();
                           m.pin = digital_static_pin[i];
                           m.state = dig_static[i];
                           dig_con.Add(m);
                       }
                       else
                       {
                           message nm = new message();
                           nm.pin = digital_static_pin[i];
                           nm.state = dig_static[i];
                           dig_notcon.Add(nm);
                       }
                   }
                   //////////////////////////////////////////////////////////////////////////////
                   string[] dig_var = sep[1].Split(',');
                   List<message> dig_var_con = new List<message>();
                  // List<message> dig_var_notcon = new List<message>();
                   for (int i = 0; i < dig_var.Length; i++)
                   {
                       if (Convert.ToInt32(dig_var[i]) > -1)
                       {
                           message m = new message();
                           m.pin = digital_var_pin[i];
                           m.state = dig_var[i];
                           dig_var_con.Add(m);
                       }
                       else
                       {
                           message nm = new message();
                           nm.pin = digital_var_pin[i];
                           nm.state = dig_var[i];
                           dig_var_notcon.Add(nm);
                       }
                   }
                   //////////////////////////////////////////////////////////////////////////////                    
                   string[] alg = sep[2].Split(',');
                   List<message> alg_con = new List<message>();
                   //List<message> alg_notcon = new List<message>();
                   for (int i = 0; i < alg.Length; i++)
                   {
                       if (Convert.ToInt32(alg[i]) > -1)
                       {
                           message m = new message();
                           m.pin = analog_pin[i];
                           m.state = alg[i];
                           alg_con.Add(m);
                       }
                       else
                       {
                           message nm = new message();
                           nm.pin = analog_pin[i];
                           nm.state = alg[i];
                           alg_notcon.Add(nm);
                       }
                   }
                   ///////////////////////////////////////////////////////////////////////////////////////              
                   DateTime dateOffsetValue = Convert.ToDateTime(time.Text);
                   // ------------------------------------------------------------------------------------
                   //  connection
                   ////////////////////////////////////////////////////////////////////////////////////////
                   string state = "off";
                   string str = Properties.Settings.Default.StrCon;
                   SqlConnection con = new SqlConnection(str);
                   //SqlConnection con = new SqlConnection(@"Server = DESKTOP-4NPOS86; Database = ay7aga; Trusted_Connection = True");
                   con.Close();
                   con.Open();
                   foreach (message i in dig_con)
                   {
                       if (Convert.ToInt32(i.state) != 0)
                           state = "on";
                       else
                           state = "off";

                       string commandr = "select  R from DeviceInfo where pinId='"+ Convert.ToInt32(i.pin) +"'";
                       SqlCommand cmdr = new SqlCommand(commandr, con);
                        string R = cmdr.ExecuteScalar().ToString();
                        string commandp = "select  pw from DeviceInfo where pinId='" + Convert.ToInt32(i.pin) + "'";
                        SqlCommand cmdp = new SqlCommand(commandp, con);
                        string pw = cmdp.ExecuteScalar().ToString();


                        double q = (Math.Sqrt(Convert.ToInt32(pw) * 25 / Convert.ToInt32(R))) / 100;
                     

                        string command = "INSERT INTO  DeviceLog (pinId,controlledId,deviceCurrentState,deviceDegree,currentDate,Qt) VALUES ('" + Convert.ToInt32(i.pin) + "','" + 1 + "','" + state + "','" + Convert.ToInt32(i.state) + "','" + dateOffsetValue + "','"+q+"')";
                        SqlCommand cmd = new SqlCommand(command, con);
                       cmd.ExecuteNonQuery();
                       command = "update DeviceInfo set deviceLastState=" + Convert.ToInt32(i.state) + ", deviceDegree=" + Convert.ToInt32(i.state) + " where pinId=" + Convert.ToInt32(i.pin) + "";
                       cmd = new SqlCommand(command, con);
                       cmd.ExecuteNonQuery();
                   }
                   con.Close();
                   //--------------------------------------------------------
                   con.Open();
                   foreach (message i in dig_var_con)
                   {
                       if (Convert.ToInt32(i.state) != 0)
                           state = "on";
                       else
                           state = "off"; string commandr = "select  R from DeviceInfo where pinId='" + Convert.ToInt32(i.pin) + "'";
                       SqlCommand cmdr = new SqlCommand(commandr, con);
                       string R = cmdr.ExecuteScalar().ToString();
                       string commandp = "select  pw from DeviceInfo where pinId='" + Convert.ToInt32(i.pin) + "'";
                       SqlCommand cmdp = new SqlCommand(commandp, con);
                       string pw = cmdp.ExecuteScalar().ToString();


                       double q = (Math.Sqrt(Convert.ToInt32(pw) * 25 / Convert.ToInt32(R)))/100;


                       string command = "INSERT INTO  DeviceLog (pinId,controlledId,deviceCurrentState,deviceDegree,currentDate,Qt) VALUES ('" + Convert.ToInt32(i.pin) + "','" + 1 + "','" + state + "','" + Convert.ToInt32(i.state) + "','" + dateOffsetValue + "','" + q + "')";
                       SqlCommand cmd = new SqlCommand(command, con);
                       cmd.ExecuteNonQuery();
                       command = "update DeviceInfo set deviceLastState=" + Convert.ToInt32(i.state) +", deviceDegree=" + Convert.ToInt32(i.state) +  " where pinId=" + Convert.ToInt32(i.pin) + "";
                       cmd = new SqlCommand(command, con);
                       cmd.ExecuteNonQuery();
                   }
                   con.Close();
                   //--------------------------------------------------------
                   con.Open();
                   foreach (message i in alg_con)
                   {
                       if (Convert.ToInt32(i.state) != 0)
                           state = "on";
                       else
                           state = "off";
                       string commandr = "select  R from DeviceInfo where pinId='" + Convert.ToInt32(i.pin) + "'";
                       SqlCommand cmdr = new SqlCommand(commandr, con);
                       string R = cmdr.ExecuteScalar().ToString();
                       string commandp = "select  pw from DeviceInfo where pinId='" + Convert.ToInt32(i.pin) + "'";
                       SqlCommand cmdp = new SqlCommand(commandp, con);
                       string pw = cmdp.ExecuteScalar().ToString();


                       double q = (Math.Sqrt(Convert.ToInt32(pw) * 25 / Convert.ToInt32(R))) / 100;


                       string command = "INSERT INTO  DeviceLog (pinId,controlledId,deviceCurrentState,deviceDegree,currentDate,Qt) VALUES ('" + Convert.ToInt32(i.pin) + "','" + 1 + "','" + state + "','" + Convert.ToInt32(i.state) + "','" + dateOffsetValue + "','" + q + "')";
                       SqlCommand cmd = new SqlCommand(command, con);
                       cmd.ExecuteNonQuery();
                       command = "update DeviceInfo set deviceLastState=" + Convert.ToInt32(i.state) + ", deviceDegree=" + Convert.ToInt32(i.state) + " where pinId=" + Convert.ToInt32(i.pin) + "";
                       cmd = new SqlCommand(command, con);
                       cmd.ExecuteNonQuery();
                       //command = "update DeviceInfo set deviceLastState=" + Convert.ToInt32(i.state) + " where pinId=" + Convert.ToInt32(i.pin) + "";
                       //cmd = new SqlCommand(command, con);
                       //cmd.ExecuteNonQuery();
                   }
                   con.Close();

                   

 //======================================================================================================
           //}
               //catch {
               //    string[] digital_static_pin = { "2", "4", "7", "8", "12", "13" };
               //    string[] digital_var_pin = { "3", "5", "9", "10", "11" };
               //    string[] analog_pin = { "20", "21", "22", "23", "24", "25" };
               //    // receive data 

               //    string pp = msg.Substring(1);
               //    string[] sep = pp.Split('@');

               //    string[] dig_static = sep[0].Split(',');
               //    string[] dig_var = sep[1].Split(',');
               //    string[] alg = sep[2].Split(',');
               //  //  DateTime now = DateTime.Now;
               //   // time.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
               //    DateTime dateOffsetValue = Convert.ToDateTime(time.Text);
               //    // ---------------------------------------------
               //    //  connection
               //    //////////////////////////////////////////////////////////
               //    string state = "off";
               //    string str = Properties.Settings.Default.StrCon;
               //    SqlConnection con = new SqlConnection(str);
               //    //SqlConnection con = new SqlConnection(@"Server = DESKTOP-4NPOS86; Database = ay7aga; Trusted_Connection = True");
               //    con.Close();
               //    con.Open();
               //    for (int i = 0; i < dig_static.Length; i++)
               //    {
               //        if (Convert.ToInt32(dig_static[i]) != 0)
               //            state = "on";
               //        else
               //            state = "off";

               //        string command = "INSERT INTO  DeviceLog (pinId,controlledId,deviceCurrentState,deviceDegree,currentDate) VALUES ('" + digital_static_pin[i] + "','" + 1 + "','" + state + "','" + dig_static[i] + "','" + dateOffsetValue + "')";

               //        //string command = "UPDATE DeviceLog set deviceCurrentState='" + state + "',deviceDegree='" + dig_static[i] + "' ,currentDate = '" + dateOffsetValue + "' where pinId='" + int.Parse(digital_static_pin[i]) + "'";
               //        SqlCommand cmd = new SqlCommand(command, con);
               //        cmd.ExecuteNonQuery();
               //    }
               //    con.Close();
               //    con.Open();
               //    for (int i = 0; i < dig_var.Length; i++)
               //    {
               //        if (Convert.ToInt32(dig_var[i]) != 0)
               //            state = "on";
               //        else
               //            state = "off";

               //        string command = "INSERT INTO  DeviceLog (pinId,controlledId,deviceCurrentState,deviceDegree,currentDate) VALUES ('" + digital_var_pin[i] + "','" + 1 + "','" + state + "','" + dig_var[i] + "','" + dateOffsetValue + "')";

               //        //string command = "UPDATE DeviceLog set deviceCurrentState='" + state + "',deviceDegree='" + dig_static[i] + "' ,currentDate = '" + dateOffsetValue + "' where pinId='" + int.Parse(digital_static_pin[i]) + "'";
               //        SqlCommand cmd = new SqlCommand(command, con);
               //        cmd.ExecuteNonQuery();
               //    }
               //    con.Close();
               //    con.Open();
               //    for (int i = 0; i < alg.Length; i++)
               //    {

               //        if (Convert.ToInt32(alg[i]) != 0)
               //            state = "on";
               //        else
               //            state = "off";

               //        char[] x = analog_pin[i].ToCharArray();
               //        int z = x[0];

               //        z.ToString();
               //        string v = z + i.ToString();
               //        int b = Convert.ToInt32(v);
               //        string command = "INSERT INTO  DeviceLog (pinId,controlledId,deviceCurrentState,deviceDegree,currentDate) VALUES ('" + analog_pin[i] + "','" + 1 + "','" + state + "','" + alg[i] + "','" + dateOffsetValue + "')";
               //        //string command = "UPDATE DeviceLog set deviceCurrentState='" + state + "',deviceDegree='" + dig_static[i] + "' ,currentDate = '" + dateOffsetValue + "' where pinId='" + int.Parse(digital_static_pin[i]) + "'";
               //        SqlCommand cmd = new SqlCommand(command, con);
               //        cmd.ExecuteNonQuery();
               //    }

               //    con.Close();
               //}
           }
           catch (Exception ex)
           {
               close_serialport();
               start_serialport();
              // MessageBox.Show(ex.Message, "Stupid error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
           
       }
        
        private void AddToList(string msg)
        {
            int n = msg_listbox.Items.Add(msg);
            msg_listbox.SelectedIndex = n;
            msg_listbox.ClearSelected();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
                serialPort.Close();
            start_serialport();
         }
         private void start_serialport()
        {
            serialPort.PortName = com;
            serialPort.BaudRate = 9600;
            serialPort.NewLine = "\n";
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
            int n = msg_listbox.Items.Add("Connection established...");
            msg_listbox.SelectedIndex = n;
            msg_listbox.ClearSelected();

            runThread.Set();
        }

        private void close_serialport()
        {
            try
            {
                if (serialPort.IsOpen == true)
                    serialPort.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            runThread.Reset();
            int n = msg_listbox.Items.Add("Connection closed.");
            msg_listbox.SelectedIndex = n;
            msg_listbox.ClearSelected();
        }
        private void stop_button_Click(object sender, EventArgs e)
        {
            close_serialport();
        }

    

        private void button10_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
            {
                string pinid = pintxt.Text;
                string x = "1." + pinid + ".0";
                serialPort.WriteLine(x);
            }
            else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button13_Click(object sender, EventArgs e)
        {

            if (serialPort.IsOpen == true)
            {
                if (flag == true) 
                {
                    string pinid = pintxt.Text;
                    string x = "4." + pinid + ".20";
                    serialPort.WriteLine(x);
                }
                else
                {
                    string pinid = pintxt.Text;
                    string x = "1." + pinid + ".1";
                    serialPort.WriteLine(x);
                }
            }
            else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
     
        }


        private void dev_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = Properties.Settings.Default.StrCon;
            SqlConnection con = new SqlConnection(str);
            con.Close();
            con.Open();

            string command = "select deviceName,pinId  from DeviceInfo  where deviceName ='" + dev.Text + "' and deviceLastState !='-1' and roomId =" + room.SelectedValue + "";
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
                if(devv=="fan")
                {
                    flag = true;
                    button13.Text ="p1";
                    p2.Visible = true;
                    p3.Visible = true;
                }
                else
                {
                    flag = false;
                    button13.Text = "turn on";
                    p2.Visible = false;
                    p3.Visible = false;
                }
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
               // int.Parse(pin_id.GetItemText(pin_id.SelectedItem));
                //room_id = int.Parse(room.SelectedValue);
                string command = "select deviceName,pinId  from DeviceInfo  where roomId =" + room.SelectedValue + " and deviceLastState !='-1' ";
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

        private void button1_Click(object sender, EventArgs e)
        {
            close_serialport();
            this.Visible = false;
            control frm = new control(com);
            frm.ShowDialog();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pin_TextChanged(object sender, EventArgs e)
        {

        }

      
      

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            close_serialport();
            this.Visible = false;
            AddRoom frm = new AddRoom();
            frm.ShowDialog();
            //  this.Visible = true;
            // this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
            {
                close_serialport();
                this.Visible = true;
                AddDevice frm = new AddDevice(com, dig_notcon, dig_var_notcon, alg_notcon);
                frm.ShowDialog();
            }
            else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
                if (serialPort.IsOpen == true)
                    serialPort.Close();
                this.Visible = false;
                device_page frm = new device_page(com, room.GetItemText(room.SelectedItem), dev.GetItemText(dev.SelectedItem));
                frm.ShowDialog();
            }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
                serialPort.Close();
            this.Close();
        }

        private void depenency(int roomId, string dep)
        {  List<depen> comparison = new List<depen>();
            string str = Properties.Settings.Default.StrCon;
            SqlConnection con = new SqlConnection(str);
            con.Close();
            con.Open();
            string command = "select deviceName,dep,deviceDegree,pinType from DeviceInfo where roomId=" + roomId + "and deviceDegree >=" + 1 + "";
            SqlCommand cmd = new SqlCommand(command, con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if ((string)dr["dep"] == dep)
                {
                    depen d = new depen();
                    d.device = (string)dr["deviceName"];
                    d.degree = (int)dr["deviceDegree"];
                    d.device = (string)dr["deviceName"];
                    d.type = (string)dr["pinType"];
                    comparison.Add(d);
                }
            }
            con.Close();
            Int32 length = comparison.Count;
            if (length >= 2)
            {
                for (int i = 1; i <length; i++)
                {
                    depen dd = new depen();
                    dd = comparison[i - 1];
                    depen ddd = new depen();
                    ddd = comparison[i];
                    if (dd.type == "analog")
                    {
                        if ((ddd.device == "led"||ddd.device=="fan") && dd.degree > 50)
                        {
                            DialogResult dialogResult = MessageBox.Show("there is enough light do you want to turn the led off?", "alarm", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                if (serialPort.IsOpen == true)
                                {
                                    string pinid = pintxt.Text;
                                    string x = "1." + pinid + ".0";
                                    serialPort.WriteLine(x);
                                }
                                else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            else if (dialogResult == DialogResult.No)
                            { break; }
                        }
                    }
                    else if (ddd.type == "analog")
                    {
                        if ((dd.device == "led" || dd.device == "fan") && ddd.degree > 50)
                        {
                            DialogResult dialogResult = MessageBox.Show("there is enough light do you want to turn the led off?", "alarm", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                if (serialPort.IsOpen == true)
                                {
                                    string pinid = pintxt.Text;
                                    string x = "1." + pinid + ".0";
                                    serialPort.WriteLine(x);
                                }
                                else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            else if (dialogResult == DialogResult.No)
                            { break;}
                        }
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
            {
                if (flag == true)
                {
                    string pinid = pintxt.Text;
                    string x = "4." + pinid + ".70";
                    serialPort.WriteLine(x);
                }
                else
                {
                    string pinid = pintxt.Text;
                    string x = "1." + pinid + ".1";
                    serialPort.WriteLine(x);
                }
            }
            else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
     
        }

        private void p3_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
            {
                if (flag == true)
                {
                    string pinid = pintxt.Text;
                    string x = "4." + pinid + ".100";
                    serialPort.WriteLine(x);
                }
                else
                {
                    string pinid = pintxt.Text;
                    string x = "1." + pinid + ".1";
                    serialPort.WriteLine(x);
                }
            }
            else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);
     
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            string str = Properties.Settings.Default.StrCon;
            SqlConnection con = new SqlConnection(str);
            con.Close();
            con.Open();
            string command = "  SELECT SUM(Qt) from DeviceLog";
            SqlCommand cmd = new SqlCommand(command, con);
            cmd.ExecuteNonQuery();
            string R = cmd.ExecuteScalar().ToString();
            if (Convert.ToDouble(R) >= 50)
                MessageBox.Show("warnning", "you are about reach the limit of the hour ! ");

        }

      

      
        

   
    }
}
