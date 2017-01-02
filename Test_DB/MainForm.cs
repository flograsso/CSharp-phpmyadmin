/*
 * Created by SharpDevelop.
 * User: SoporteSEM
 * Date: 29/12/2016
 * Time: 15:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Test_DB
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			
			/*
			 * DataSet resembles database. DataTable resembles database table, and DataRow resembles a record in a
			 * table.
			 * If you want to add filtering or sorting options, you then do so with a DataView object, and convert
			 * it back to
			 * a separate DataTable object.
			 * If you're using database to store your data, then you first load a database table to a DataSet object
			 * in memory. You can load multiple database tables to one DataSet, and select specific table to read from the
			 * DataSet through DataTable object. Subsequently, you read a specific row of data from your DataTable through
			 *  DataRow
			 */
			
			/*Datos de conexion (conviene ponerlos en un archivo aparte)*/
			string connection = "server=localhost; database=testcsharp;user=root; password=;";
			
			
			MySqlConnection conexion = new MySqlConnection(connection);
			
			/*Intento conectar*/
			try
			{

				conexion.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			/*Si pudo conectar*/
			if (conexion.State==ConnectionState.Open){
				
				/*Creo un DataSet (puede contener varias tablas)*/
				DataSet dst;
				dst = new DataSet();
				
				/*Creo un DataTable (puede contener una tabla)*/
				DataTable dt;
				dt = new DataTable();
				
				/*Ejecuto la consulta y me traigo los datos al programa*/
				MySqlDataAdapter adaptador;
				adaptador = new MySqlDataAdapter("SELECT * FROM `sms`WHERE state=false",conexion);
				
				/*Cargo los datos de la consulta en el DataSet con el nombre interno de tabla elegido*/
				adaptador.Fill(dst,"Phone_Table");
				
				/*Cargo la tabla al DataTable*/
				dt=dst.Tables["Phone_Table"];
				
				
				foreach (DataRow dr in dt.Rows){
					if (dr["state"].ToString() == "FALSE"){
						label1.Text=label1.Text + " // " + dr["number"];
					}
				}
				/*
				if (dst.Tables[0].Rows.Count > 0){
					Array array =dst.Tables[0].Rows[0].ItemArray;
					label1.Text= array.GetValue(Convert.ToInt32(textBox1.Text)).ToString();
				}
				*/
			}
		}

	}
}
