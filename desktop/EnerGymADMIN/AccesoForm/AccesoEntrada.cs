using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using EnerGymADMIN.Services;
using ZXing;

namespace EnerGymADMIN
{
    public partial class AccesoEntrada : Form
    {
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice captureDevice;
        UsuarioService usuarioService;

        // Bandera para evitar procesar múltiples veces
        private bool codigoProcesado = false;

        public AccesoEntrada()
        {
            InitializeComponent();
            usuarioService = new UsuarioService();
        }

        private void AccesoEntrada_Load(object sender, EventArgs e)
        {
            FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (FilterInfoCollection.Count == 0)
            {
                MessageBox.Show("No se encontraron dispositivos de captura de video.");
                return;
            }

            captureDevice = new VideoCaptureDevice(FilterInfoCollection[0].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();

            timer1.Start();
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pcbCamara.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void AccesoEntrada_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (captureDevice.IsRunning)
            {
                captureDevice.Stop();
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (codigoProcesado || pcbCamara.Image == null)
                return;

            BarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode((Bitmap)pcbCamara.Image);

            if (result != null)
            {
                // Establecer bandera para evitar múltiples lecturas
                codigoProcesado = true;

                // Llamar al servicio para procesar el acceso
                var respuesta = await usuarioService.AccesoEntrada(result.Text);
                MessageBox.Show(respuesta.Mensaje, respuesta.Titulo);
                this.Close();

                // Detener cámara y temporizador
                timer1.Stop();
                if (captureDevice.IsRunning)
                {
                    captureDevice.Stop();
                }
            }
        }
    }
}