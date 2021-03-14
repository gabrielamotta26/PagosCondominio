using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PagosCondominio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Propietario> propietarios = new List<Propietario>();
        List<Propiedad> propiedads = new List<Propiedad>();
        List<Reporte> reportes = new List<Reporte>();

        private void leer()
        {
            //Lee el archivo de propietarios

            FileStream stream1 = new FileStream("propietarios.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader1 = new StreamReader(stream1);


            while (reader1.Peek() > -1)
            {
                Propietario propietario = new Propietario();

                propietario.Dpi1 = int.Parse(reader1.ReadLine());
                propietario.Nombre = reader1.ReadLine();
                propietario.Apellido = reader1.ReadLine();

                propietarios.Add(propietario);

            }
            reader1.Close();

            //Lee el archivo de propiedades

            FileStream stream2 = new FileStream("propiedades.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader2 = new StreamReader(stream2);


            while (reader2.Peek() > -1)
            {
                Propiedad propiedad = new Propiedad();

                propiedad.NumCasa= reader2.ReadLine();
                propiedad.Dpi1 = int.Parse(reader2.ReadLine());
                propiedad.CuotaMantenimiento = float.Parse(reader2.ReadLine());

                propiedads.Add(propiedad);

            }
            reader2.Close();

        }

        private void MostrarDatos()
        {
            for(int x=0; x<propietarios.Count; x++)
            {
                for(int y=0; y < propiedads.Count; y++)
                {
                    if (propietarios[x].Dpi1== propiedads[y].Dpi1)
                    {
                        Reporte reporte = new Reporte();

                        reporte.Nombre = propietarios[x].Nombre;
                        reporte.Apellido = propietarios[x].Apellido;
                        reporte.NumCasa = propiedads[y].NumCasa;
                        reporte.Cuota = propiedads[y].CuotaMantenimiento;

                        reportes.Add(reporte);
                    }
                }
            }


        }

        private void actualizar_grid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = reportes;
            dataGridView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            leer();
            MostrarDatos();
            actualizar_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            reportes = reportes.OrderBy(r => r.Cuota).ToList();

            actualizar_grid();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            //propietario con más propiedades
            List<int> vs = new List<int>();


            for(int x=0; x < propietarios.Count; x++)
            {
                int cont = 0;

                for(int y=0; y < propiedads.Count; y++)
                {
                    if(propietarios[x].Dpi1== propiedads[y].Dpi1)
                    {
                        cont++;
                    }
                }

                vs.Add(cont);
            }


            int mayor = vs.Max();
            int ind = vs.IndexOf(mayor);
            label1.Text = "Propietario con más propiedades: \n" + propietarios[ind].Nombre+" "+propietarios[ind].Apellido;
            

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cuotasAltas="";
            string cuotasBajas = "";
            
            //ordena la lista de reportes descendentemente
            reportes = reportes.OrderByDescending(r => r.Cuota).ToList();
            //actualiza el DataGridView en ese orden
            actualizar_grid();

            //se agregan los nombres completos de los propietarios con mensaualidad más alta
            //de los primeros 3 índices
            for (int x=0; x < 3; x++)
            {
                cuotasAltas += reportes[x].Nombre + " " + reportes[x].Apellido + "\n";
            }

            //se muestra en el label los nombres
            label1.Text = "Cuotas más altas:\n" + cuotasAltas;


            //se ordena la lista de reportes ascendentemente
            reportes = reportes.OrderBy(r => r.Cuota).ToList();

            //se agregan los nombres completos de los propietarios con mensaualidad más baja
            //de los primeros 3 índices
            for (int x = 0; x < 3; x++)
            {
                cuotasBajas += reportes[x].Nombre + " " + reportes[x].Apellido + "\n";
            }

            //se muestra en el segundo label los nombres completos
            label2.Text = "Cuotas más bajas: \n" + cuotasBajas;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            //lista donde se guardarán las cuotas totales de cada propietario
            List<float> cuotas = new List<float>();

            //se recorre la lista de propietarios para compararla con la de propiedades
            for(int x=0; x<propietarios.Count; x++)
            {
                //se crea un número flotante para el propietario y en él sumar cada cuota de sus propiedades
                float cuota = 0;

                for (int y=0; y < propiedads.Count; y++)
                {
                    //si el numero de DPI es el mismo, que se agregue esa cuota a la suma de la cuota mensual; 
                    if (propietarios[x].Dpi1== propiedads[y].Dpi1)
                    {
                        cuota += propiedads[y].CuotaMantenimiento;
                    }
                }

                //se agrega la suma de las cuotas a la lista dinámica 
                cuotas.Add(cuota);
                
            }


            //se ordena la cuota para saber la cuota más alta 
            float Alta = cuotas.Max();
            int ind = cuotas.IndexOf(Alta);
            label1.Text = "Cuota total más alta:\n" + Alta+"\nNombre del propietario:\n"+propietarios[ind].Nombre+" "+propietarios[ind].Apellido ;



        }
    }
}
