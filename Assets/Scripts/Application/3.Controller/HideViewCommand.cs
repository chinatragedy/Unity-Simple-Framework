using PureMVC.Interfaces;
using PureMVC.Patterns;

public class HideViewCommand : SimpleCommand, ICommand
{
	public override void Execute(INotification note)
	{
		var type = (ViewType)note.Body;
		switch (type)
		{
			default:
				break;
		}
	}
}
