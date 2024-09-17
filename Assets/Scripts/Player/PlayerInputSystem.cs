using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial class PlayerInputSystem : SystemBase
{
	private GameInput InputActions;
	private Entity Player;

	protected override void OnCreate()
	{
		if (!SystemAPI.TryGetSingleton<Input>(out Input input))
		{
			EntityManager.CreateEntity(typeof(Input));
		}
		RequireForUpdate<PlayerTag>();
		InputActions = new GameInput();
		InputActions.Enable();
	}
	protected override void OnStartRunning()
	{
		InputActions.GamePlay.Shoot.performed += OnShoot;

		Player = SystemAPI.GetSingletonEntity<PlayerTag>();
	}
	protected override void OnUpdate()
	{
		Vector2 MoveInput = InputActions.GamePlay.Move.ReadValue<Vector2>();
		Vector2 MousePosition = InputActions.GamePlay.MousePosition.ReadValue<Vector2>();
		
		SystemAPI.SetSingleton(new Input { Movement = MoveInput, MousePos = MousePosition });
	}
	protected override void OnStopRunning()
	{
		InputActions.Disable();
		Player = Entity.Null;
	}

	private void OnShoot(InputAction.CallbackContext context)
	{
		if (!SystemAPI.Exists(Player)) { return; }

		SystemAPI.SetComponentEnabled<FireProjectileTag>(Player, true);
	}
}
