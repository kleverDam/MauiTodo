using MauiTODO.Helpers;
using MauiTODO.Models;
using MauiTODO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTODO.ViewModels
{
    class AltaEdicionDeTareaViewModel : NotifyBase
    {
        private Tarea _tarea;
        private string _textOk;
        private TareaService _tareaService;
        private Command _guadarTareaComan;
        private Command _camcelComan;


        public Tarea Tarea { get { return _tarea; } }

        public Command GuadarTareaComan { get => _guadarTareaComan; set => _guadarTareaComan = value; }
        public Command CamcelComan { get => _camcelComan; set => _camcelComan = value; }
        public TareaService TareaService { get => _tareaService; set => _tareaService = value; }
        public string TextOk { get => _textOk; set => _textOk = value; }

        public AltaEdicionDeTareaViewModel(Tarea tarea)
        {
            if (Tarea == null) { _tarea = new Tarea(); }

            TareaService = new TareaService();
            this.LoadComan();
            _tarea = tarea;
        }

        public void LoadComan()
        {
            GuadarTareaComan = new Command(this.Cancelar);
            CamcelComan = new Command(this.AltaTarea);
        }
        private async void AltaTarea()
        {
            this.LimpiarMEnsaje();
            try
            {
                bool resultado = await TareaService.CrearTarea(Tarea);

                if (resultado)
                {
                    this.MensajeExito(true, "Alta de tarea correcta");
                }
                else
                {
                    this.MensajeError(true, "Alta de tarea incorrecta");
                }
            }
            catch (Exception ex)
            {
                this.MensajeError(true, $"Error inesperado {ex}");
            }
        }

        private void Cancelar()
        {
            Tarea.IsEdit = false;

        }

        public void LimpiarMEnsaje()
        {
            Tarea.IsVisibleTareaError = false;
            Tarea.IsVisibleTareaExito = false;
            Tarea.ExitoTarea = "";
            Tarea.ErrorTarea = "";
        }

        public void MensajeExito(Boolean b, string mensaje)
        {
            Tarea.IsVisibleTareaExito = b;
            Tarea.ExitoTarea = mensaje;
        }
        public void MensajeError(Boolean b, string mensaje)
        {
            Tarea.IsVisibleTareaError = b;
            Tarea.ErrorTarea = mensaje;
        }

    }

}

