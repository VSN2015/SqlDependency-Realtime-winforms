using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace testWS
{
    public partial class Form1 : Form
    {
        //public ServiceReference1.WebService1SoapClient webservice = new ServiceReference1.WebService1SoapClient();
        public ServiceReference2.WebService1SoapClient webservice = new ServiceReference2.WebService1SoapClient();
        public string str_connect = @"Data Source=.\SQLEXPRESS;Initial Catalog=QL_NHAHANG;User ID=sa;Password=sa2012";
        //SqlConnection conn = null;

        //public delegate void newHome();

        //public event newHome onNewHome;

        public Form1()
        {
            InitializeComponent();

            //try
            //{
            //    SqlClientPermission ss = new SqlClientPermission(System.Security.Permissions.PermissionState.Unrestricted);
            //    ss.Demand();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            //SqlDependency.Stop(str_connect);
            //SqlDependency.Start(str_connect);
            //conn = new SqlConnection(str_connect);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //onNewHome += Form1_onNewHome;
            //load();
            loadData();
        }

        //==============================================================================================

        //private void Form1_onNewHome()
        //{
        //    ISynchronizeInvoke i = (ISynchronizeInvoke)this;
        //    if(i.InvokeRequired){
        //        newHome n = new newHome(Form1_onNewHome);
        //        i.BeginInvoke(n,null);
        //        return;
        //    }
        //    load();
        //    //throw new NotImplementedException();
        //}
        //private void load()
        //{
        //    DataTable dt = new DataTable();

        //    conn.Open();

        //    SqlCommand command = new SqlCommand("Select TOP 3 ID_BAN,TENBAN,STATUS from BAN ",conn);

        //    SqlDependency d = new SqlDependency(command);

        //    d.OnChange += d_OnChange;

        //    dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
        //    //dt.Load(command.ExecuteReader());
        //    conn.Close();

        //    dtgv.DataSource = dt;

        //}

        //void d_OnChange(object sender, SqlNotificationEventArgs e)
        //{

        //    SqlDependency de = sender as SqlDependency;

        //    de.OnChange -=d_OnChange;

        //    if(onNewHome !=null){
        //        onNewHome();
        //    }
        //    //throw new NotImplementedException();
        //}



        //--------------------------------------------------------------------------------------



        //public DataTable ConvertToDataTable<T>(IList<T> data)
        //{
        //    PropertyDescriptorCollection properties =
        //       TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    foreach (PropertyDescriptor prop in properties)
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    foreach (T item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //        table.Rows.Add(row);
        //    }
        //    return table;

        //}


        public void loadData()
        {
            ConvertDataTable convert = new ConvertDataTable();

            dtgv.AutoGenerateColumns = false; // ko ra cột extension data

            dtgv.DataSource =convert.ToDataTable(webservice.getData());

        }
        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtgv.CurrentCell = dtgv.Rows[0].Cells[0];
            int id = 0;
            string tenban = null;
            string status = null;
            for (int i = 0; i < dtgv.RowCount - 1; i++ )
            {
                id = Int32.Parse(dtgv.Rows[i].Cells[0].Value.ToString());
                tenban = dtgv.Rows[i].Cells[1].Value.ToString();
                status = dtgv.Rows[i].Cells[2].Value.ToString();
                webservice.updateData(id,tenban,status); 
            }

            MessageBox.Show("Update success");

            loadData();
        }


    }
}
