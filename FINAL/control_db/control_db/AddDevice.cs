using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_db
{
    public partial class AddDevice : Form
    {
        private SerialPort serialPort = new SerialPort();
        private string ser;
        private string com;
        private List<Form1.message> dig_notcon;
        private List<Form1.message> dig_var_notcon;
        private List<Form1.message> alg_notcon;
        

        public AddDevice(string com, List<Form1.message> dig_notcon, List<Form1.message> dig_var_notcon, List<Form1.message> alg_notcon)
        {
            // TODO: Complete member initialization
            ser = com;
            this.dig_notcon = dig_notcon;
            this.dig_var_notcon = dig_var_notcon;
            this.alg_notcon = alg_notcon;
            InitializeComponent();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void AddDevice_Load(object sender, EventArgs e)
        {
            try
            {
            start_serialport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dev.Items.Add("digital");
            dev.Items.Add("digital_var");
            dev.Items.Add("analog");
            // dev.SelectedIndex = 0;
            soze();
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

      

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string str = Properties.Settings.Default.StrCon;
                SqlConnection con = new SqlConnection(str);
                string command = "INSERT INTO DeviceInfo " +
                    "(pinId, controlledId, deviceName,deviceModel,deviceSerial,deviceState,pinType,deviceMovability,deviceLastState,deviceDegree,roomId,deviceUnit,DeviceOff,DeviceOn,dep,R,pw) " +
             "VALUES (@pinId,  @controlledId, @deviceName,@deviceModel,@deviceSerial,@deviceState,@pinType,@deviceMovability,@deviceLastState,@deviceDegree,@roomId,@deviceUnit,@DeviceOff,@DeviceOn,@dep,@R,@pw) ";
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.Parameters.Add("@pinId", SqlDbType.Int).Value = int.Parse(pin_id.GetItemText(pin_id.SelectedItem));
                cmd.Parameters.Add("@controlledId", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@deviceName", SqlDbType.NVarChar, 20).Value = dev_name.Text;
                cmd.Parameters.Add("@deviceModel", SqlDbType.NVarChar, 20).Value = devmod.Text;
                cmd.Parameters.Add("@deviceSerial", SqlDbType.NVarChar, 20).Value = devser.Text;
                cmd.Parameters.Add("@deviceState", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@pinType", SqlDbType.NVarChar, 20).Value = dev.GetItemText(dev.SelectedItem);
                cmd.Parameters.Add("@deviceMovability", SqlDbType.Int).Value = int.Parse(devmov.Text);
                cmd.Parameters.Add("@deviceLastState", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@deviceDegree", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@deviceUnit", SqlDbType.NChar, 10).Value = devun.Text;
                cmd.Parameters.Add("@roomId", SqlDbType.Int).Value = room.SelectedValue;
                cmd.Parameters.Add("@DeviceOn", SqlDbType.NVarChar).Value = bb.Text;
                cmd.Parameters.Add("@DeviceOff", SqlDbType.NVarChar).Value = textBox2.Text;
                cmd.Parameters.Add("@dep", SqlDbType.NVarChar).Value = dep.Text;
                cmd.Parameters.Add("@R", SqlDbType.Int).Value = int.Parse(res.Text);
                cmd.Parameters.Add("@pw", SqlDbType.Int).Value = int.Parse(pw.Text);

                if (serialPort.IsOpen == true)
                {
                    if (dev.SelectedItem == "digital")
                    {
                        string pinid = pin_id.GetItemText(pin_id.SelectedItem);
                        string x = "2." + pinid + ".0";
                        serialPort.WriteLine(x);
                    }
                    else if (dev.SelectedItem == "digital_var")
                    {
                        string pinid = pin_id.GetItemText(pin_id.SelectedItem);
                        string x = "2." + pinid + ".1";
                        serialPort.WriteLine(x);
                    }
                    else if (dev.SelectedItem == "analog")
                    {
                        string pinid = pin_id.GetItemText(pin_id.SelectedItem);
                        string x = "2." + pinid + ".2";
                        serialPort.WriteLine(x);
                    }

                }
                else MessageBox.Show("start serial connection first", " error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (serialPort.IsOpen == true)
                    serialPort.Close();
                Directory.CreateDirectory(@"D:\Rooms\" + room.GetItemText(room.SelectedItem) + "\\" + dev_name.Text + " ");
                //  txt_dir.Text = @"D:\Hospital\" + txt_id.Text + "";
                File.Copy(bb.Text.ToString(), @"D:\Rooms\" + room.GetItemText(room.SelectedItem) + "\\" + dev_name.Text + " \\" + "on" + ".jpg");
                File.Copy(textBox2.Text.ToString(), @"D:\Rooms\" + room.GetItemText(room.SelectedItem) + "\\" + dev_name.Text + "\\" + "off" + ".jpg");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Done !");
                this.Visible = false;

                // Form1 frm = new Form1();
                //frm.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Stupid error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
       
        }

       

        private void button5_Click_1(object sender, EventArgs e)
        {
            string imagloc;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Images(.jpg,.png,.gif)|*.png;*.jpg;*.gif";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                imagloc = openFileDialog.FileName.ToString();
                pictureBox1.ImageLocation = imagloc;
                bb.Text = openFileDialog.FileName.ToString();
            }
        }

        private void dev_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            pin_id.Items.Clear();
            if (dev.SelectedItem == "digital")
            {
                foreach (Form1.message i in dig_notcon)
                {
                    if (!pin_id.Items.Contains(i.pin))
                        pin_id.Items.Add(i.pin);
                    // pin_id.Items.Add(i.pin).ToString();
                }
            }
            else if (dev.SelectedItem == "digital_var")
            {
                foreach (Form1.message i in dig_var_notcon)
                {
                    if (!pin_id.Items.Contains(i.pin))
                        pin_id.Items.Add(i.pin);
                    //  pin_id.Items.Add(i.pin).ToString();
                }
            }
            else if (dev.SelectedItem == "analog")
            {
                foreach (Form1.message i in alg_notcon)
                {
                    if (!pin_id.Items.Contains(i.pin))
                        pin_id.Items.Add(i.pin);
                    //pin_id.Items.Add(i.pin).ToString();
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string imagloc;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            // openFileDialog.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            openFileDialog.Filter = "Images(.jpg,.png,.gif)|*.png;*.jpg;*.gif";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                imagloc = openFileDialog.FileName.ToString();
                pictureBox2.ImageLocation = imagloc;
                textBox2.Text = openFileDialog.FileName.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
                serialPort.Close();
            this.Close();
        }

        private void pin_id_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}