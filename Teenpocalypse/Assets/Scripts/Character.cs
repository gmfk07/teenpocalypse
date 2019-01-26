using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
	public string Name = "Bill";
	[TextArea(4, 20)]
	public string Bio = "Bill likes pineapples";
	[Range(0, Constants.MAX_VALUE)] public int MaxHealth;
	//[Range(0, Constants.MAX_VALUE)] public int MaxEnergy;
	[Range(0, Constants.MAX_VALUE)] public int InitialRelationship;
	//[Range(0, Constants.MAX_VALUE)] public int InitialMorale;

	[HideInInspector] public int Health;
	//[HideInInspector] public int Energy;
	[HideInInspector] public int Relationship;
	//[HideInInspector] public int Morale;
	
	public void Init()
	{
		Health = MaxHealth;
		//Energy = MaxEnergy;
		Relationship = InitialRelationship;
		//Morale = InitialMorale;
	}
}
