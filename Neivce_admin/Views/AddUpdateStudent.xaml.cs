using Neivce_admin.Models;
using Neivce_admin.ViewModels;

namespace Neivce_admin.Views;

public partial class AddUpdateStudent : ContentPage
{
	public AddUpdateStudent(string getSheet)
	{
		InitializeComponent();
		this.BindingContext = new AddUpdateStudentPageViewModel(getSheet);
	}

	public AddUpdateStudent(AdminModel student, string getSheet)
	{
		InitializeComponent();
		this.BindingContext = new AddUpdateStudentPageViewModel(student, getSheet);
	}
}