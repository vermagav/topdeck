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
		SheetMetal,
		Concrete
	}

	public enum Cue 
	{
		Drag,
		Collide
	}
	
}
