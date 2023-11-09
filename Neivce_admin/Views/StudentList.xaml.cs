using Neivce_admin.ViewModels;

namespace Neivce_admin.Views;

public partial class StudentList : ContentPage
{
	public StudentList(string getSheet)
	{
		InitializeComponent();
		this.BindingContext = new StudentListPageViewModel(getSheet);
	}
}