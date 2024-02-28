using MauiTODO.Helpers;
using MauiTODO.Models;
using MauiTODO.Services;
using System.Collections.ObjectModel;


namespace MauiTODO.ViewModels
{
    public class HomePageViewModel : NotifyBase
    {

        private Tarea _tareaSeleccionada;
        private Models.Tarea _tarea;
        private TareaService _tareaService;
        private ObservableCollection<Tarea> _tareaList;

        private Command _closeLoginComan;
        private Command _altaTareaComan;
        private Command _bajaTareaComan;
        private Command _editarTareaComan;
        public Command CloseLoginComand { get => _closeLoginComan; set => _closeLoginComan = value; }
        public Command AltaTareaComan { get => _altaTareaComan; set =>_altaTareaComan = value; }
        public Command BajaTareaComan { get => _bajaTareaComan; set => _bajaTareaComan = value; }
        public Command EditarTareaComan { get => _editarTareaComan; set => _editarTareaComan = value; }
        public Tarea TareaSeleccionada { get => _tareaSeleccionada; set { _tareaSeleccionada = value; OnPropertyChanged(); } }


        public Tarea Tarea { get => _tarea; set => _tarea = value; }
        public ObservableCollection<Tarea> Tareas { get => _tareaList; set { _tareaList = value; OnPropertyChanged(); } }

        public HomePageViewModel()
        {

            _tarea = new Tarea();
            _tareaService = new TareaService();
            this.LoadTareasAsync();
            this.LoadCommands();
        }

        public void loadComan()
        {
            _closeLoginComan = new Command(this.CloseLogin);
            _altaTareaComan = new Command(this.AltaTarea);
            _bajaTareaComan = new Command(this.BajaTarea);
            _editarTareaComan = new Command(this.EditarTarea);

        }

        public async void LoadTareasAsync()
        {
            this.limpiarMEnsaje();
            try
            {
                Tareas = await _tareaService.ObtenerTareas();
                if (Tareas == null)
                {
                    this.mensajeError(true, $"Error al cargar el listado de tareas ");
                }
                else
                {
                    this.mensajeExito(true, $"Carga completa");
                }
            }
            catch (Exception ex)
            {
                this.mensajeError(true, $"Error al obtener tareas: {ex.Message}");
            }
        }

        private void LoadCommands()
        {
            _closeLoginComan = new Command(CloseLogin);
            _altaTareaComan = new Command(AltaTarea);
            _bajaTareaComan = new Command(BajaTarea);
            _editarTareaComan = new Command(EditarTarea);
        }

        //  AltaTarea, BajaTarea, EditarTarea.
        private async void AltaTarea()
        {
            this.limpiarMEnsaje();
            try
            {
                if (Shell.Current != null)
                {
                    if (TareaSeleccionada != null) { 
                        TareaSeleccionada.IsEdit= true;
                    }
                    await Shell.Current.GoToAsync($"//AltaEdicionTarea");
                }
            }
            catch (Exception ex)
            {
                this.mensajeError(true, $"Error al navegar a la página MainPage: {ex}");
            }
        }

        private void BajaTarea()
        {
            this.limpiarMEnsaje();
            try
            {
                if (TareaSeleccionada != null)
                {
                    // Usa Result para esperar la tarea asíncrona de manera síncrona
                    bool resultado = _tareaService.BorrarTarea(TareaSeleccionada).Result;

                    if (resultado)
                    {
                        this.mensajeExito(true, $"Tarea eliminada con exito");
                    }
                    else
                    {
                        this.mensajeError(true, $"Error al eliminar la tarea");
                    }
                }
                else
                {
                    this.mensajeError(true, $"Debe seleccionar una tarea antes");
                }
            }
            catch (Exception ex)
            {
                this.mensajeError(true, $"Error inesperado {ex}");
            }
        }

        private async void EditarTarea()
        {
            this.limpiarMEnsaje();
            try
            {
                if (TareaSeleccionada != null)
                {
                    // Puedes modificar la tarea y luego llamar al método de edición del servicio
                    Tarea tareaEditada = new Tarea(/* Nuevas propiedades de la tarea */);
                    bool resultado = await _tareaService.EditarTarea(tareaEditada);

                    if (resultado)
                    {
                        this.mensajeExito(true, $"Tarea editada con exito");
                    }
                    else
                    {
                        this.mensajeError(true, $"Error al Editar la tarea");
                    }
                }
                else
                {
                    this.mensajeError(true, $"Debe seleccionar una tarea antes");
                }
            }
            catch (Exception ex)
            {
                this.mensajeError(true, $"Error inesperado {ex}");
            }
        }
        private async void CloseLogin()
        {
            this.limpiarMEnsaje();
            try
            {
                if (Shell.Current != null)
                {

                    await Shell.Current.GoToAsync($"//LoginPage");
                }
            }
            catch (Exception ex)
            {
                this.mensajeError(true, $"Error al navegar a la página MainPage: {ex}");
            }
        }

        public void limpiarMEnsaje() { 
            Tarea.IsVisibleTareaError=false;
            Tarea.IsVisibleTareaExito= false;
            Tarea.ExitoTarea = "";
            Tarea.ErrorTarea = "";
        }
        public void mensajeExito(Boolean b, string mensaje) { 
            Tarea.IsVisibleTareaExito= b;
            Tarea.ExitoTarea= mensaje;
        }
        public void mensajeError(Boolean b, string mensaje)
        {
            Tarea.IsVisibleTareaError = b;
            Tarea.ErrorTarea= mensaje;
        }
    }
}
