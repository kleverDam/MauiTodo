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
    public class AltaEdicionDeTareaViewModel : NotifyBase
    {
        private Tarea _tarea;
        private string _tareaName;
        private string _tareaDesc;
        private TareaService _tareaService;
        private Command _guadarTareaComan;
        private Command _camcelComan;
        private string _guardarCrearText;
        private bool _isEdit;

        public Tarea Tarea { get => _tarea; set => _tarea = value; }
        public string GuardarCrearText { get => _guardarCrearText; set { _guardarCrearText = value; OnPropertyChanged(); } }
        public string TareaName { get => _tareaName; set { _tareaName = value; OnPropertyChanged(); } }
        public string TareaDesc{ get => _tareaDesc; set { _tareaDesc = value; OnPropertyChanged(); } }
        public Command GuadarTareaComan { get => _guadarTareaComan; set => _guadarTareaComan = value; }
        public Command CancelarComand { get => _camcelComan; set => _camcelComan = value; }
        public TareaService TareaService { get => _tareaService; set => _tareaService = value; }
        public bool IsEdit { get => _isEdit; set { _isEdit = value; OnPropertyChanged(); } }


        public AltaEdicionDeTareaViewModel() {
            IsEdit = false;
            Tarea = new Tarea();
            GuardarCrearText = "Crear";
            LoadComan();
        }

        public AltaEdicionDeTareaViewModel(IDictionary<string, object> parameters)
        {
            _tareaService = new TareaService();

            if (parameters != null && parameters.TryGetValue("Tarea", out var tarea))
            {
                Tarea = tarea as Tarea;
                GuardarCrearText = "Guardar";
                IsEdit = true;
            }
            LoadComan();
        }


        public void LoadComan()
        {
            GuadarTareaComan = new Command(this.AltaTarea);
            CancelarComand = new Command(this.Cancelar);
        }

        private async void AltaTarea()
        {
            this.LimpiarMEnsaje();
            try
            {
                if (IsEdit)
                {
                    Tarea.name = TareaName;
                    Tarea.descripcion= TareaDesc;
                    bool resultado = await TareaService.EditarTarea(Tarea);

                    if (resultado)
                    {
                        this.MensajeExito(true, "Alta de tarea correcta");
                    }
                    else
                    {
                        this.MensajeError(true, "Alta de tarea incorrecta");
                    }
                }
                else
                {
                    Tarea=new Tarea();
                    Tarea.name= TareaName;
                    Tarea.descripcion = TareaDesc;
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

            }
            catch (Exception ex)
            {
                this.MensajeError(true, $"Error inesperado {ex}");
            }
        }

        private void Cancelar()
        {
            if (IsEdit)
            {
                _tarea = new Tarea();
                GuardarCrearText = "Crear";
                IsEdit = false;
                LimpiarMEnsaje();
            }
            else
            {
                if (Shell.Current != null)
                {
                    Shell.Current.Navigation.PopAsync();
                }
            }
        }

        public void LimpiarMEnsaje()
        {
            Tarea.IsVisibleTareaError = false;
            Tarea.IsVisibleTareaExito = false;
            Tarea.ExitoTarea = "";
            Tarea.ErrorTarea = "";
        }

        public void MensajeExito(bool isVisible, string mensaje)
        {
            Tarea.IsVisibleTareaExito = isVisible;
            Tarea.ExitoTarea = mensaje;
        }

        public void MensajeError(bool isVisible, string mensaje)
        {
            Tarea.IsVisibleTareaError = isVisible;
            Tarea.ErrorTarea = mensaje;
        }
    }
}