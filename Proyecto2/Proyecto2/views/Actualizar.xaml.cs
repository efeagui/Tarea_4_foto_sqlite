using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Proyecto2.clases;
using Proyecto2.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto2.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Actualizar : ContentPage
    {
        
        bool estado = true;
        byte[] image;
        public Actualizar()
        {
            InitializeComponent();
        }
       
        private void Salvar_Clicked(object sender, EventArgs e)
        {
            estado = false;
            guardarUbicacion();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
     
        private async void buttoncamera_Clicked(object sender, EventArgs e)
        {
            var camera = new StoreCameraMediaOptions();
            camera.PhotoSize = PhotoSize.Full;
            camera.Name = "img";
            camera.Directory = "MiApp";

            var foto = await CrossMedia.Current.TakePhotoAsync(camera);
            if (foto != null)
            {
                imagefile.Source = ImageSource.FromStream(() => {
                    return foto.GetStream();
                });

                imagefile.IsVisible = true;
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = foto.GetStream();
                    stream.CopyTo(memory);
                    image = memory.ToArray();
                }
            }
        }
        public async void guardarUbicacion()
        {
            refrescar.IsRunning = true;
            if (string.IsNullOrEmpty(descripcion_larga.Text))
            {
                await DisplayAlert("Alerta", "Debe describir la descripcion", "ok");
                return;
            }
            if (imagefile.Source == null)
            {
                await DisplayAlert("Alerta", "Seleccione Imagen", "ok");
                return;
            }


            else
            {
                Crud crud = new Crud();
                Conexion conn = new Conexion();
                var ubicacion = new UbicacionModel()
                {
                    id = Convert.ToInt32(id.Text),
                    nombre = nombre.Text,
                    descripcion = descripcion_larga.Text,
                    fotografia = image
                };
               
                conn.Conn().Update(ubicacion);
                await DisplayAlert("Success", "registro actualizado", "Ok");
                refrescar.IsRunning = false;
            }
        }
        private async void buttonfile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}