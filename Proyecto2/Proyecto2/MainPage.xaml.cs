using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Proyecto2.clases;
using Proyecto2.model;
using Proyecto2.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Proyecto2
{
    public partial class MainPage : ContentPage
    {
        List<UbicacionModel> service;
      
        byte[] image;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
        }

        public async void guardarUbicacion()
        {

            if (string.IsNullOrEmpty(nombre.Text))
            {
                await DisplayAlert("Alerta", "Debe digitalizar su nombre", "ok");
                return;
            }
            if (string.IsNullOrEmpty(descripcion_larga.Text))
            {
                await DisplayAlert("Alerta", "Debe digitalizar su descripcion", "ok");
                return;
            }
            if (imagefile.Source == null)
            {
                await DisplayAlert("Alerta", "Debe tomarse una selfie", "ok");
                return;
            }


            else
            {

                Crud crud = new Crud();
                Conexion conn = new Conexion();

                var ubicacion = new UbicacionModel()
                {
                    id = 0,
                    nombre = nombre.Text,
                    descripcion = descripcion_larga.Text,
                    fotografia = image

                };
                refresc.IsRunning = true;
                conn.Conn().CreateTable<UbicacionModel>();
                conn.Conn().Insert(ubicacion);
                await DisplayAlert("Success", "Registro guardado con exito", "Ok");
                nombre.Text = "";
                descripcion_larga.Text = "";
                refresc.IsRunning = false;
            }
        }
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ubicaciones());
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

        private void Salvar_Clicked(object sender, EventArgs e)
        {
            guardarUbicacion();

        }
    }
}