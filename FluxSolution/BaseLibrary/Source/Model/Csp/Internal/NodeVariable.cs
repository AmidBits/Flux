namespace Flux.Csp
{
  internal class NodeVariable
    : Node
  {
    internal VariableInteger Variable { get; set; }

    internal NodeVariable(VariableInteger variable, string label)
      : base(label)
    {
      this.Variable = variable;
    }

    internal NodeVariable(VariableInteger variable)
    {
      this.Variable = variable;
    }

  }
}
