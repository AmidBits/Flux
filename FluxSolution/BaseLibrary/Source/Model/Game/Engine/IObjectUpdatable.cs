namespace Flux.Model.Game.Engine
{
	public interface IObjectUpdatable
	{
		void InitializeObject();
		void UpdateObject(float deltaTime);
	}
}
