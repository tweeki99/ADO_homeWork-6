using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HwAdo_6
{
    public partial class Form1 : Form
    {
        public DataSet DataSet { get; set; }
        public DbDataAdapter DataAdapter { get; set; }
        public DbCommandBuilder CommandBuilder { get; set; }

        public Form1()
        {
            InitializeComponent();

            var providerName = ConfigurationManager.ConnectionStrings["HomeConnectionString"].ProviderName;
            var connectionString = ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString;

            var factory = DbProviderFactories.GetFactory(providerName);

            var connection = factory.CreateConnection();

            var command = connection.CreateCommand();
            connection.ConnectionString = connectionString;

            command.CommandText = "select * from Users";

           DataSet = new DataSet("userApp");

            DataAdapter = factory.CreateDataAdapter();

            DataAdapter.SelectCommand = command;

            CommandBuilder = factory.CreateCommandBuilder();
            CommandBuilder.DataAdapter = DataAdapter;
            
            connection.Open();
            DataAdapter.Fill(DataSet, "Users");
            connection.Close();

            ApdateListBoxUsers();
        }

        private void listBoxUsers_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItems.Count > 0)
            {

                int rowIndex = 0;

                string login = listBoxUsers.SelectedItem.ToString();
                login = login.Replace("Админ: ", "");

                foreach (var row in DataSet.Tables["Users"].Rows)
                {
                    if (Convert.ToString((row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("Login")]) == login)
                    {
                        break;
                    }
                    rowIndex++;
                }
                Hide();
                buttonDeleteUser.Visible = false;
                FormUpdateUser formUpdateUser = new FormUpdateUser(this, DataAdapter, DataSet, rowIndex);
                formUpdateUser.Show();
            }
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            Hide();
            buttonDeleteUser.Visible = false;
            FormAddUser formAddUser = new FormAddUser(this, DataAdapter, DataSet);
            formAddUser.Show();
        }

        public void ApdateListBoxUsers()
        {
            listBoxUsers.Items.Clear();

            foreach (var row in DataSet.Tables["Users"].Rows)
            {
                if (Convert.ToBoolean((row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("IsAdmin")]) == true)
                {
                    listBoxUsers.Items.Add("Админ: " + (row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("Login")]);
                }
            }
            foreach (var row in DataSet.Tables["Users"].Rows)
            {
                if (Convert.ToBoolean((row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("IsAdmin")]) == false)
                {
                    listBoxUsers.Items.Add((row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("Login")]);
                }
            }
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItems.Count > 0)
            {
                buttonDeleteUser.Visible = true;
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;

            string login = listBoxUsers.SelectedItem.ToString();
            login = login.Replace("Админ: ", "");

            foreach (var row in DataSet.Tables["Users"].Rows)
            {
                if (Convert.ToString((row as DataRow).ItemArray[(row as DataRow).Table.Columns.IndexOf("Login")]) == login)
                {
                    break;
                }
                rowIndex++;
            }
            
            DataSet.Tables["Users"].Rows[rowIndex].Delete();

            DataAdapter.Update(DataSet, "Users");
            ApdateListBoxUsers();
            buttonDeleteUser.Visible = false;
        }
    }
}
