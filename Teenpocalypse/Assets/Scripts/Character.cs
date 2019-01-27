using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
	public string Name = "Bill";
	[TextArea(4, 20)]
	public string Bio = "Bill likes pineapples";
    [HideInInspector] public float WorkMultiplier = 1;
	[Range(0, Constants.MAX_VALUE)] public int MaxHealth;
	[Range(0, Constants.MAX_VALUE)] public int InitialRelationship;
	[Range(0.5f, 1.5f)] public float InitialWorkMultiplier = 1;

	[HideInInspector] public int Health;
	[HideInInspector] public int Relationship;
    public int RestingWeeks = 0;
	public bool IsResting { get { return RestingWeeks > 0; } }
	public bool GenerateName = false;

	public Texture Icon;
	public GameObject Sprite;
	[HideInInspector] public Action AssignedAction;
	
	public void Init()
	{
		if (GenerateName)
		{
			NameGenerator newName = new NameGenerator();
			Name = newName.GetNewName();
			Bio = newName.GetNewName() + " likes pineapples";
		}
        RestingWeeks = 0;
        Health = MaxHealth;
		Relationship = InitialRelationship;
		AssignedAction = null;
		WorkMultiplier = InitialWorkMultiplier;
		GameController.Instance.Event_OnWeekStart += OnWeekStart;
	}

	public void OnWeekStart()
	{
		RestingWeeks = Mathf.Max(0, RestingWeeks - 1);
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
		{
			if (AssignedAction != null)
				AssignedAction.AssignedCharacters.Remove(this);
			GameController.Instance.RemoveCharacter(this);
		}
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
		{
			if (AssignedAction != null)
				AssignedAction.AssignedCharacters.Remove(this);
			GameController.Instance.RemoveCharacter(this);
		}
	}

    //Returns true if relationship test succeeds, false otherwise
    public bool TestRelationship(int successModifier)
    {
        if (Random.Range(0, 100 - successModifier) <= Relationship)
            return true;
        return false;
    }
}
