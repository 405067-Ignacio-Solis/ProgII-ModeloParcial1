
using ParcialApp.Dominio;
using ParcialApp.Servicio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ParcialApp.Presentacion
{
    public partial class Frm_Alta : Form
    {
        IServicio servicio = null;
        List<Material> listMateriales;
        OrdenRetiro orden = null;
        // lista para verificar si un material ya fue agregado a un detalle
        List<string> materialesAgregados = new List<string>();
        List<DetalleOrden> detalles;



        public Frm_Alta(ServicioFactory fabrica)
        {
            servicio = fabrica.crearServicio();
            InitializeComponent();
            listMateriales = servicio.traerMateriales();
            cargarCombo();
            detalles = new List<DetalleOrden>();
            
            

        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //hay materiales?

            if (dgvDetalles.Rows.Count == 0) {
                MessageBox.Show("Debe cargar al menos un detalle","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            // hay stock?
            
            foreach (DataGridViewRow fila in dgvDetalles.Rows) {

                if ((Convert.ToInt32(fila.Cells["stock"].Value) < Convert.ToInt32(fila.Cells["cantidad"].Value))) 
                { 
                    MessageBox.Show("No se puede cargar el material " + listMateriales[fila.Index] +
                        " porque la cantidad supera al stock disponible");
                    return;
                }
            }


            OrdenRetiro nuevaOrden = new OrdenRetiro(txtResp.Text,detalles);
            if (servicio.confirmarOrden(txtResp.Text, detalles))
            {
                MessageBox.Show("Se ha cargado la orden con exito","Confirmado");
            }
            else { MessageBox.Show("Ocurrio un error al cargar la orden", "Error"); }



        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }
            else
            {
                return;
            }
        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)

        {
            
        }

        private void cargarCombo()
        {

            cboMateriales.DataSource = listMateriales;
            cboMateriales.DisplayMember = "nombre";
            cboMateriales.ValueMember = "codigo";
            cboMateriales.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private bool validarDatos() {
            bool valid = false;

            if (dtpFecha.Value == null) {
                MessageBox.Show("Debe seleccionar una fecha valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return valid;

            }
            if (string.IsNullOrEmpty(txtResp.Text)) {
                MessageBox.Show("Debe escribir un nombre de responsable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return valid;
            }

            valid = true;
            return valid;
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            
            if (!validarDatos()) {
                return;
            }

            
            //crear nuevo detalle
            DetalleOrden nuevoDetalle =
                new DetalleOrden(
                        listMateriales[cboMateriales.SelectedIndex],
                        Convert.ToInt32(nudCantidad.Value));

            if (materialesAgregados.Contains(nuevoDetalle.material.nombre)) {
                MessageBox.Show("Ya se ha cargado un detalle con ese material","Error",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
            else {
                dgvDetalles.Rows.Add(nuevoDetalle.material.codigo,
                    nuevoDetalle.material.nombre,
                    nuevoDetalle.material.stock,
                    nuevoDetalle.cantidad,
                    "quitar"
                    ); ;
                materialesAgregados.Add(nuevoDetalle.material.nombre);
                detalles.Add(nuevoDetalle);
            }
            
            


        }

        private bool ExisteProductoEnGrilla(string text)
        {
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["producto"].Value.Equals(text))
                    return true;
            }
            return false;
        }

       



        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)

            {
                string material = dgvDetalles.Rows[e.RowIndex].Cells[1].Value.ToString();
                materialesAgregados.Remove(material);
                dgvDetalles.Rows.RemoveAt(e.RowIndex);

                


            }
        }

        private void cboMateriales_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex= cboMateriales.SelectedIndex;
            //nudCantidad.Value = listMateriales[selectedIndex].stock;
            
        }
    }
}
