using Neivce_admin.Models;
using Neivce_admin.ViewModels;

namespace Neivce_admin.Views;

public partial class AddUpdateStudent : ContentPage
{
	public AddUpdateStudent(string getadsub)
	{
		InitializeComponent();
		this.BindingContext = new AddUpdateStudentPageViewModel(getadsub);
	}

	public AddUpdateStudent(AdminModel student, string getadsub)
	{
		InitializeComponent();
		this.BindingContext = new AddUpdateStudentPageViewModel(student, getadsub);
	}
}