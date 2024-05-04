using EFARINANGOS5.Models;
using Windows.Networking.Proximity;

namespace EFARINANGOS5.Vistas;

public partial class VPersona : ContentPage
{
	public VPersona()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {

        statusMessage.Text = "";
        if (txtId.Text == "" || txtId.Text==null)
        {
            App.personRepo.AddNewPersona(txtNombre.Text);
            statusMessage.Text = App.personRepo.StatusMessage;
        }
        else {
            Persona people = new Persona();
            people.Id = Convert.ToInt32(txtId.Text);
            people.Name = txtNombre.Text;
            App.personRepo.UpdatePerson(people);
            statusMessage.Text = App.personRepo.StatusMessage;
            txtNombre.Text = "";
            txtId.Text = "";
        }

        DisplayAlert("alerta", statusMessage.Text, "ok");
        cargar_datos();

    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {

        statusMessage.Text = "";
        cargar_datos();

    }

    public void cargar_datos()
    {
        List<Persona> people = App.personRepo.GetAllPersona();
        ListaPersona.ItemsSource = people;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEditar_Clicked(object sender, EventArgs e)
    {
        Button btnEditar = (Button)sender;
        int id = (int)btnEditar.CommandParameter;
        Persona people = App.personRepo.GetPersona(id);
        txtId.Text = people.Id.ToString();
        txtNombre.Text = people.Name;
       // DisplayAlert("alerta", "el valor es" + id.ToString(), "ok");
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
       Button btnEli = (Button)sender;
        int id = (int)btnEli.CommandParameter;
        App.personRepo.DeletePerson(id);

        statusMessage.Text = App.personRepo.StatusMessage;
        cargar_datos();
        DisplayAlert("alerta", statusMessage.Text, "ok");

    }
}