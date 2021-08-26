namespace Flux.Csp
{
	[System.Flags]
	public enum ConstraintOperationResult
	{
		Satisfied = 0x1,
		Violated = 0x2,
		Undecided = 0x4,
		Propagated = 0x8
	}
}
