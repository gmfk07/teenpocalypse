using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
	public string Name = "Bill";
	[TextArea(4, 20)]
	public string Bio = "Bill likes pineapples";
    public float WorkMultiplier = 1;
	[Range(0, Constants.MAX_VALUE)] public int MaxHealth;
	//[Range(0, Constants.MAX_VALUE)] public int MaxEnergy;
	[Range(0, Constants.MAX_VALUE)] public int InitialRelationship;
	//[Range(0, Constants.MAX_VALUE)] public int InitialMorale;

	[HideInInspector] public int Health;
	//[HideInInspector] public int Energy;
	[HideInInspector] public int Relationship;
	//[HideInInspector] public int Morale;

	public Texture Icon;
	[HideInInspector] public Action AssignedAction;
	
	public void Init()
	{
        NameGenerator newName = new NameGenerator();
        Name = newName.GetNewName();
        Bio = newName.GetNewName() + " likes pineapples";
        Health = MaxHealth;
		//Energy = MaxEnergy;
		Relationship = InitialRelationship;
		//Morale = InitialMorale;
		AssignedAction = null;
	}

    //Returns true if character is still alive, false otherwise
    public bool ChangeHealth(int delta)
    {
        Health += delta;
		Health = Mathf.Clamp(Health, 0, MaxHealth);
        if (Health == 0)
            return false;
        return true;
    }

	// Don't use in loop!
	public void ChangeHealthWithDeletion(int delta)
	{
		if (!ChangeHealth(delta))
			GameController.Instance.RemoveCharacter(this);
	}

	//Returns true if character is still present, false otherwise
	public bool ChangeRelationship(int delta)
    {
        Relationship += delta;
		Relationship = Mathf.Clamp(Relationship, 0, Constants.MAX_VALUE);
        if (Relationship == 0)
            return false;
        return true;
    }

	// Don't use in loop!
	public void ChangeRelationshipWithDeletion(int delta)
	{
		if (!ChangeRelationship(delta))
			GameController.Instance.RemoveCharacter(this);
	}

    //Returns true if relationship test succeeds, false otherwise
    public bool TestRelationship(int successModifier)
    {
        if (Random.Range(0, 100 - successModifier) <= Relationship)
            return true;
        return false;
    }
}
