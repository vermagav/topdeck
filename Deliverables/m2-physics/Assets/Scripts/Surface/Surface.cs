using UnityEngine;
using System.Collections;

/// <summary>
/// Used primarily to define enumerated types associated with a Surface.
/// </summary>
public class Surface : Object {
	public enum Substance
	{
		Solid,
		Sand,
		Stone,
		Metal
	}

	public enum Cue 
	{
		Drag,
		Collide
	}

	/// <summary>
	/// Returns the movement rate to apply to player movement.
	/// TODO: Make this more robust. Currently used as a simple multiplier by the force.
	/// </summary>
	/// <returns>The movement rate.</returns>
	/// <param name="substance">Surface.Substance from CollisionData</param>
	public static float GetMovementRate(Surface.Substance substance)
	{
		switch (substance)
		{
		case Substance.Solid:
			return 0.8f;
		case Substance.Sand:
			return 0.5f;
		case Substance.Stone:
			return 1.0f;
		default:
			return 1;
		}
	}
}
