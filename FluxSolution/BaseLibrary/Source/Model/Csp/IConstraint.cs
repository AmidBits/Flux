namespace Flux.Csp
{
	public interface IConstraint
	{
		void Check(out ConstraintOperationResult result);
		void Propagate(out ConstraintOperationResult result);
		bool StateChanged();
	}
}
