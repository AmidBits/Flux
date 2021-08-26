namespace Flux.Csp
{
	public interface IState<T>
	{
		int Depth { get; }
		System.TimeSpan Runtime { get; }
		int Backtracks { get; }
		System.Collections.Generic.IList<System.Collections.Generic.IDictionary<string, IVariable<T>>> Solutions { get; }
		System.Collections.Generic.IDictionary<string, IVariable<T>> OptimalSolution { get; }
		System.Collections.Generic.IList<IVariable<T>> Variables { get; }

		void SetVariables(System.Collections.Generic.IEnumerable<IVariable<T>> variableList);
		void SetConstraints(System.Collections.Generic.IEnumerable<IConstraint> constraintList);

		StateOperationResult Search();
		StateOperationResult Search(IVariable<int> optimiseVariable, int timeOut = int.MaxValue);
		StateOperationResult SearchAllSolutions();
	}
}
