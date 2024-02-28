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
        private static Tarea _tarea;
        private string _textOk;
        private TareaService _tareaService;
        private Command _guadarTareaComan;
        private Command _camcelComan;


        public Tarea Tarea { get { return _tarea; } }
        public AltaEdicionDeTareaViewModel()
        {
            if (Tarea == null){_tarea = new Tarea();}
           
            _tareaService = new TareaService();
            this.loadComan();
        }

        public void loadComan()
        {
            _guadarTareaComan = new Command(this.Cancelar);
            _camcelComan = new Command(this.AltaTarea);
        }
        private async void AltaTarea()
        {
            this.limpiarMEnsaje();
            try
            {
                bool resultado = await _tareaService.CrearTarea(Tarea);

                if (resultado)
                {
                    this.mensajeExito(true, "Alta de tarea correcta");
                }
                else
                {
                    this.mensajeError(true, "Alta de tarea incorrecta");
                }
            }
            catch (Exception ex)
            {
                this.mensajeError(true, $"Error inesperado {ex}");
            }
        }

        private async void Cancelar()
        { 
            Tarea.IsEdit = false;

        }

            public void limpiarMEnsaje()
        {
            Tarea.IsVisibleTareaError = false;
            Tarea.IsVisibleTareaExito = false;
            Tarea.ExitoTarea = "";
            Tarea.ErrorTarea = "";
        }

        public void mensajeExito(Boolean b, string mensaje)
        {
            Tarea.IsVisibleTareaExito = b;
            Tarea.ExitoTarea = mensaje;
        }
        public void mensajeError(Boolean b, string mensaje)
        {
            Tarea.IsVisibleTareaError = b;
            Tarea.ErrorTarea = mensaje;
        }

    }

}

