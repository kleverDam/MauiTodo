using MauiTODO.Helpers;
using MauiTODO.Models;
using MauiTODO.Services;
using System.Collections.ObjectModel;


namespace MauiTODO.ViewModels
{
    public class HomePageViewModel :NotifyBase
    {
        private Command _closeLoginComan;
        private Command _altaTareaComan;
        private Command _bajaTareaComan;
        private Command _editarTareaComan;
        private Models.Tarea _tarea;
        private TareaService _tareaService;
        private ObservableCollection<Tarea> _tareaList;
        public Command CloseLoginComand { get => _closeLoginComan; set => _closeLoginComan = value; }
        public Command AltaTareaComan{ get => _altaTareaComan; set => _altaTareaComan = value; }
        public Command BajaTareaComan{ get => _bajaTareaComan; set => _bajaTareaComan = value; }
        public Command EditarTareaComan{ get => _editarTareaComan; set => _editarTareaComan = value; }


        public Tarea Tarea { get => _tarea; set => _tarea = value; }
        public ObservableCollection<Tarea> Tareas { get => _tareaList; set => _tareaList = value;  }

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
            _altaTareaComan = new Command(this.CloseLogin);
            _bajaTareaComan = new Command(this.CloseLogin);
            _editarTareaComan = new Command(this.CloseLogin);

        }

        public async void LoadTareasAsync()
        {
            try
            {
                Tareas = await _tareaService.ObtenerTareas();
                if (Tarea == null)
                {
                    Tarea.IsVisibleTareaError = true;
                    Tarea.ErrorTarea = "Error al cargar tarea";
                }
                else {
                    Tarea.IsVisibleTareaExito = true;
                    Tarea.ExitoTarea = "Tareas cargadas";
                }
            }
            catch (Exception ex)
            {
                Tarea.IsVisibleTareaError = true;
                Tarea.ErrorTarea = $"Error al obtener tareas: {ex.Message}";
            }
        }

        private void LoadCommands()
        {
            _closeLoginComan = new Command(CloseLogin);
            _altaTareaComan = new Command(AltaTarea);
            _bajaTareaComan = new Command(BajaTarea);
            _editarTareaComan = new Command(EditarTarea);
        }

        // Puedes agregar métodos para manejar otros comandos, como AltaTarea, BajaTarea, EditarTarea, etc.

        private async void AltaTarea()
        {
            // Lógica para añadir una nueva tarea
        }

        private async void BajaTarea()
        {
            // Lógica para eliminar una tarea
        }

        private async void EditarTarea()
        {
            // Lógica para editar una tarea
        }
        private async void CloseLogin()
        {
            try
            {
                if (Shell.Current != null)
                {

                    await Shell.Current.GoToAsync($"//LoginPage");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la navegación.
                Console.WriteLine($"Error al navegar a la página MainPage: {ex.Message}");
            }
        }
       
     
    }
}
    