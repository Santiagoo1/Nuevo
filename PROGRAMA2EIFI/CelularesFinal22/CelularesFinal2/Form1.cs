﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using CapaNegocios;
using System.Collections;

namespace CelularesFinal2
{
    public partial class Form1 : Form
    {
        Celulares NuevoCelular = new Celulares();
        Celulares CelularExistente;
        NegCelulares objNegCelulares = new NegCelulares();

        Tecnicos NuevoTecnico;
        Tecnicos TecnicosExistentes; 
        NegTecnicos objNegTecnicos = new NegTecnicos();


            



        public Form1()
        {
            InitializeComponent();
            CrearDGV();
            LlenarDGV();

            CrearDGVT();
            LlenarDGVT();

            cbEstado.DropDownStyle= ComboBoxStyle.DropDownList;
            //cbTecnico.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEstado.Items.Add("Nuevo");
            cbEstado.Items.Add("Usado");
            cbEstado.Items.Add("Viejo");
        }

        private void LlenarDGV()
        {
            DgvCelulares.Rows.Clear();

            DataSet ds = new DataSet();
            ds = objNegCelulares.listadoCelulares("Todos");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DgvCelulares.Rows.Add(dr[0].ToString(), dr[1], dr[2], dr[3],
                        dr[4].ToString(), dr[5].ToString(),
                        dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                }
            }
            
        }

        private void LlenarDGVT()
        {
            dgvTecnicos.Rows.Clear();

            DataSet ds = new DataSet();
            ds = objNegTecnicos.listadoTecnicos("Todos");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvTecnicos.Rows.Add(dr[0].ToString(), dr[1], dr[2]);
                }
            }
            else
            {
                MessageBox.Show("No hay datos cargados en el sistema");
            }
        }

        private void CrearDGV()
        {
            DgvCelulares.Columns.Add("0", "Codigo");
            DgvCelulares.Columns.Add("1", "Marca");
            DgvCelulares.Columns.Add("2", "Modelo");
            DgvCelulares.Columns.Add("3", "Reparacion");
            DgvCelulares.Columns.Add("4", "Estado");
            DgvCelulares.Columns.Add("5", "DNI_Tecnico");
            DgvCelulares.Columns.Add("6", "Costo_}total");
            DgvCelulares.Columns.Add("7", "Fecha_Ingreso");
            DgvCelulares.Columns.Add("8", "Fecha_Egreso");


            DgvCelulares.Columns[0].Width = 100;
            DgvCelulares.Columns[1].Width = 100;
            DgvCelulares.Columns[2].Width = 100;
            DgvCelulares.Columns[3].Width = 100;
            DgvCelulares.Columns[4].Width = 100;
            DgvCelulares.Columns[5].Width = 100;
            DgvCelulares.Columns[6].Width = 100;
            DgvCelulares.Columns[7].Width = 100;
            DgvCelulares.Columns[8].Width = 100;




        }

        private void CrearDGVT()
        {
            dgvTecnicos.Columns.Add("0", "Dni_Tecnico");
            dgvTecnicos.Columns.Add("1", "Nombre");
            dgvTecnicos.Columns.Add("2", "Apellido");
            


            dgvTecnicos.Columns[0].Width = 100;
            dgvTecnicos.Columns[1].Width = 100;
            dgvTecnicos.Columns[2].Width = 100;
            

        }


        private void LimpiarCelulares()
        {
            txtCodigo.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtReparacion.Text = string.Empty;
            cbEstado.Text = string.Empty;
            cbTecnico.Text = string.Empty;
            txtCosto_Total.Text = string.Empty;
            txtFecha_Ingreso.Text = string.Empty;
            txtFecha_Egreso.Text = string.Empty;
        }

        private void LimpiarTecnicos()
        {
            txtDNI.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
           
        }
        

       
        private void btCargarCelular_Click(object sender, EventArgs e)
        {
            int nGrabados = -1;

            NuevoCelular = new Celulares(int.Parse(txtCodigo.Text), txtMarca.Text, txtModelo.Text, txtReparacion.Text, cbEstado.Text,int.Parse(cbTecnico.Text), int.Parse(txtCosto_Total.Text), txtFecha_Ingreso.Text, txtFecha_Egreso.Text);

            nGrabados = objNegCelulares.abmCelulares("Alta", NuevoCelular);

            if (nGrabados == -1)
                Mensaje.Text = " No se pudieron cargar los celulares en el sistema";
            else
            {
                Mensaje.Text = " Se cargaron los celulares con éxito";
                LlenarDGV();
                LimpiarCelulares();



            }

        }





      
        

        private void btCargarTecnico_Click(object sender, EventArgs e)
        {
            int nGrabados = -1;

            NuevoTecnico = new Tecnicos(int.Parse(txtDNI.Text), txtNombre.Text, txtApellido.Text);

            nGrabados = objNegTecnicos.abmTecnicos("Alta", NuevoTecnico);

            if (nGrabados == -1)
                Mensaje.Text = " No se pudieron cargar los tecnicos en el sistema";
            else
            {
                Mensaje.Text = " Se cargaron los tecnicos con éxito";
                LlenarDGVT();
                LimpiarTecnicos();



            }
        }

   
        private void dgvTecnicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDNI.Text = dgvTecnicos.SelectedRows[0].Cells[0].Value.ToString();
            txtNombre.Text = dgvTecnicos.SelectedRows[0].Cells[1].Value.ToString();
            txtApellido.Text = dgvTecnicos.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btActualizarT_Click(object sender, EventArgs e)
        {

            Aborrar aborrar = new Aborrar();
            int nGrabados = -1;


            aborrar.P_cod = Convert.ToInt32(dgvTecnicos.SelectedRows[0].Cells[0].Value);

            TecnicosExistentes = new Tecnicos(aborrar.P_cod,
                txtNombre.Text,
                txtApellido.Text);


           

            nGrabados = objNegTecnicos.abmTecnicos("Modificar", TecnicosExistentes);

            if (nGrabados == -1)
            {
                MessageBox.Show("No se pudo grabar la informacion en el sistema");
            }
            LlenarDGVT();
            LimpiarTecnicos();

        }

        private void btBorrar_Click(object sender, EventArgs e)
        {
            Aborrar aborrar = new Aborrar();
            int nGrabados = -1;

            TecnicosExistentes = new Tecnicos(Convert.ToInt32(dgvTecnicos.SelectedRows[0].Cells[0].Value),
                dgvTecnicos.SelectedRows[0].Cells[1].Value.ToString(),
                dgvTecnicos.SelectedRows[0].Cells[2].Value.ToString());

            aborrar.P_cod = Convert.ToInt32(dgvTecnicos.SelectedRows[0].Cells[0].Value);

            nGrabados = objNegTecnicos.abmTecnicos("Baja", TecnicosExistentes);

            if (nGrabados == -1)
            {
                MessageBox.Show("No se pudo grabar la informacion en el sistema");
            }

             LlenarDGVT();
            LimpiarTecnicos();
        }

        private void btCargarActualizacion_Click(object sender, EventArgs e)
        {
            Aborrar aborrar = new Aborrar();
            int nResultado = -1;
            TxtBox_a_ObjCelulares();

            aborrar.P_cod = Convert.ToInt32(DgvCelulares.SelectedRows[0].Cells[0].Value);

            NuevoCelular = new Celulares(aborrar.P_cod,
                txtMarca.Text,
                txtModelo.Text, txtReparacion.Text, cbEstado.Text, Convert.ToInt32(cbTecnico.Text), Convert.ToInt32(txtCosto_Total.Text), txtFecha_Ingreso.Text, txtFecha_Egreso.Text);




            nResultado = objNegCelulares.abmCelulares("Modificar", NuevoCelular);

            if (nResultado == -1)
            {
                MessageBox.Show("No se pudo grabar la informacion en el sistema");
            }
            LlenarDGV();
            LimpiarCelulares();






        }

        private void DgvCelulares_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = DgvCelulares.SelectedRows[0].Cells[0].Value.ToString();
            txtMarca.Text = DgvCelulares.SelectedRows[0].Cells[1].Value.ToString();
            txtModelo.Text = DgvCelulares.SelectedRows[0].Cells[2].Value.ToString();
            txtReparacion.Text = DgvCelulares.SelectedRows[0].Cells[3].Value.ToString();
            cbEstado.Text = DgvCelulares.SelectedRows[0].Cells[4].Value.ToString();
            cbTecnico.Text = DgvCelulares.SelectedRows[0].Cells[5].Value.ToString();
            txtCosto_Total.Text = DgvCelulares.SelectedRows[0].Cells[6].Value.ToString();
            txtFecha_Ingreso.Text = DgvCelulares.SelectedRows[0].Cells[7].Value.ToString();
            txtFecha_Egreso.Text = DgvCelulares.SelectedRows[0].Cells[8].Value.ToString();
        }

        private void EliminarCelular_Click(object sender, EventArgs e)
        {
            Aborrar aborrar = new Aborrar();
            int nGrabados = -1;

            aborrar.P_cod = Convert.ToInt32(DgvCelulares.SelectedRows[0].Cells[0].Value);

            CelularExistente = new Celulares(aborrar.P_cod,
                DgvCelulares.SelectedRows[0].Cells[1].Value.ToString(),
                DgvCelulares.SelectedRows[0].Cells[2].Value.ToString(),
                DgvCelulares.SelectedRows[0].Cells[3].Value.ToString(),
                DgvCelulares.SelectedRows[0].Cells[4].Value.ToString(),
                Convert.ToInt32(DgvCelulares.SelectedRows[0].Cells[5].Value),
                Convert.ToInt32(DgvCelulares.SelectedRows[0].Cells[6].Value),
                DgvCelulares.SelectedRows[0].Cells[7].Value.ToString(),
                DgvCelulares.SelectedRows[0].Cells[8].Value.ToString());

         

            nGrabados = objNegCelulares.abmCelulares("Borrar", CelularExistente);

            if (nGrabados == -1)
            {
                MessageBox.Show("No se pudo grabar la informacion en el sistema");
            }
            LlenarDGV();
            LimpiarCelulares();
        }

        private void LlenarDGVCelularesBuscado()
        {
            if (RBcodigo.Checked == true)
            {
                string cual = txtBuscar.Text;
                DgvCelulares.Rows.Clear();
                DataSet ds = new DataSet();
                ds = objNegCelulares.listadoCelularesCodigo(cual);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DgvCelulares.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2], dr[3], dr[4].ToString());
                    }
                }
                else
                if (RBmarca.Checked == true)
                {
                    string cuall = txtBuscar.Text;
                    DgvCelulares.Rows.Clear();
                    DataSet dss = new DataSet();
                    ds = objNegCelulares.listadoCelularesMarca(cuall);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DgvCelulares.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2], dr[3], dr[4].ToString());
                        }
                    }
                    else
                    if(RBmodelo.Checked == true)
                    {
                        string cualll = txtBuscar.Text;
                        DgvCelulares.Rows.Clear();
                        DataSet dsss = new DataSet();
                        ds = objNegCelulares.listadoCelularesMarca(cuall);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DgvCelulares.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2], dr[3], dr[4].ToString());
                            }
                        }
                    }
                }
            }
        }

        //void LlenarCombos()
        //{ 
        //    cbTecnico.DataSource = objNegTecnicos.ObtenerTecnicos();
        //    cbTecnico.DisplayMember = "Tecnicos";
        //    cbTecnico.ValueMember = "IdRubro";
        //}

        #region Validaciones
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >=  32 && e.KeyChar <=47)||(e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se permiten Letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se permiten Letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtReparacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se permiten Letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCosto_Total_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtFecha_Ingreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 46) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingrese una flecha valida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtFecha_Egreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 46) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingrese una flecha valida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se permiten Letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se permiten Letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        #endregion

        private void cbTecnico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            LlenarDGVCelularesBuscado();


        }

        private void dgvTecnicos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void Refrescar_Click(object sender, EventArgs e)
        {
            CrearDGV();
            LlenarDGV();
        }

        private void TxtBox_a_ObjCelulares()
        {
            NuevoCelular.p_id = int.Parse(txtCodigo.Text);
            NuevoCelular.P_Marca = txtMarca.Text;
            NuevoCelular.p_Modelo = txtModelo.Text;
            NuevoCelular.P_Repacion = txtReparacion.Text;
            NuevoCelular.P_Estado = cbEstado.Text;
            NuevoCelular.P_Costo_total = int.Parse(txtCodigo.Text);
            NuevoCelular.P_FechaIngreso = txtFecha_Ingreso.Text;
            NuevoCelular.P_FechaEgreso = txtFecha_Egreso.Text;

        }


    }
}

