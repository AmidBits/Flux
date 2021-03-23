namespace Flux
{
	/// <summary>Base Implementation of System.Windows.Input.ICommand. This implementation is sometimes named DelegateCommand or RelayCommand.</summary>
	public class Command<T>
		: System.Windows.Input.ICommand
	{
		public event System.EventHandler? CanExecuteChanged = delegate { };

		private readonly System.Func<T?, bool> m_canExecute;
		private readonly System.Action<T?> m_execute;

		public Command(System.Action<T?> execute)
			: this(execute, (T) => { return true; })
		{
		}
		public Command(System.Action<T?> Execute, System.Func<T?, bool> CanExecute)
		{
			m_canExecute = CanExecute;
			m_execute = Execute;
		}

		public void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);
		}

		// ICommand
		public bool CanExecute(object? Parameter)
			=> m_canExecute?.Invoke((T?)Parameter) ?? true;
		public void Execute(object? Parameter)
			=> m_execute((T?)Parameter);
	}
}
