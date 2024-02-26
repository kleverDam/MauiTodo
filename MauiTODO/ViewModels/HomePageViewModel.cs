using MauiTODO.Models;
using MauiTODO.View;
using System.Collections.ObjectModel;


namespace MauiTODO.ViewModels
{
    public class HomePageViewModel
    {
        private Command _closeLoginComan;
        private Command _altaTareaComan;
        private Command _bajaTareaComan;
        private Command _editarTareaComan;
        private Models.UsuarioLogin _usuario;
        private Models.Tarea _tarea;
        //private Command _altaTarea;
        //private Command _bajaTarea;
        //private Command _editarTarea;
        //private Boolean _isEditTarea;
        //private Boolean _isArchived;

        public Command CloseLoginComand { get => _closeLoginComan; set => _closeLoginComan = value; }
        public Command AltaTareaComan{ get => _altaTareaComan; set => _altaTareaComan = value; }
        public Command BajaTareaComan{ get => _bajaTareaComan; set => _bajaTareaComan = value; }
        public Command EditarTareaComan{ get => _editarTareaComan; set => _editarTareaComan = value; }


        public UsuarioLogin UsuarioLogin { get => _usuario; set => _usuario = value; }
        public Tarea Tarea { get => _tarea; set => _tarea = value; }

        //Borrar despues 
        private ObservableCollection<Tarea> _tareas;
        public ObservableCollection<Tarea> Tareas { get => _tareas; set => _tareas = value; }

        public HomePageViewModel()
        {
            _closeLoginComan = new Command(this.CloseLogin);
            _altaTareaComan = new Command(this.CloseLogin);
            _bajaTareaComan = new Command(this.CloseLogin);
            _editarTareaComan = new Command(this.CloseLogin);
        }
        public HomePageViewModel(string usernameAndPassword)
        {
            _usuario = new UsuarioLogin();
            var parts = usernameAndPassword.Split(',');
            if (parts.Length >= 2)
            {
                UsuarioLogin.Username = parts[0];
                UsuarioLogin.Password = parts[1];
            }
           
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
        private async void AlraTarea()
        {
            try
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync($"//AltaTareaPage");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la navegación.
                Console.WriteLine($"Error al dar de alta una tarea: {ex.Message}");
            }
        }
     
    }
}
