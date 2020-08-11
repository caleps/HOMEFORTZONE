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

namespace control_db
{
    public partial class AddRoom : Form
    {
        public AddRoom()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)

        {


            string str = Properties.Settings.Default.StrCon;
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string command = "select max (roomId) from RoomInfo";
            SqlCommand cmd = new SqlCommand(command, con);

            //if (newid == null) newid = "0";
            try
            {
                string newid = cmd.ExecuteScalar().ToString();
                txt_id.Text = (int.Parse(newid) + 1) + "";

            }
            catch { txt_id.Text = "1"; }
            con.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = Properties.Settings.Default.StrCon;
                SqlConnection con = new SqlConnection(str);
                string command = "INSERT INTO RoomInfo " +
                    "(roomId, roomTitle, hoomId) " +
                    "VALUES (@roomId,  @roomTitle, @hoomId) ";
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.Parameters.Add("@roomId", SqlDbType.Int).Value = int.Parse(txt_id.Text);
                cmd.Parameters.Add("@roomTitle", SqlDbType.NVarChar,30).Value = text_title.Text;
                cmd.Parameters.Add("@hoomId", SqlDbType.Int).Value = home_id.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Done !");
                this.Visible = false;

                Form1 frm = new Form1();
                frm.ShowDialog();
                //this.Close();
            }



            //string command = "insert into Patient(Patient_id,patient_name,Age,patient_phone,SSN,Blood,Dir_path)values(" + int.Parse(txt_id.Text) + "," + "'" + txt_name.Text + "'" + "," + int.Parse(txt_age.Text) + "," + int.Parse(txt_mbl.Text) + "," + int.Parse(txt_ssn.Text) + ",'" + txt_bld.Text + "','" + txt_dir.Text + "')";
            //try
            //{
            //    string str = Properties.Settings.Default.StrCon;
            //    SqlConnection con = new SqlConnection(str);
            //    con.Close();
            //    con.Open();
            //    string command = "insert into RoomInfo(roomId,roomTitle,hoomId)values(" + int.Parse(txt_id.Text) + ",'" + text_title.Text + "'," +int.Parse( home_id.Text )+ ")";
            //    SqlCommand cmd = new SqlCommand(command, con);
            //    con.Close();
            //    MessageBox.Show("Done !");


            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void text_title_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
            this.Close();
        }

        private void AddRoom_Load(object sender, EventArgs e)
        {

        }
    }
}
