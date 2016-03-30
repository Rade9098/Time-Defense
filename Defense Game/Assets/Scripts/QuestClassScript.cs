using System;

[Serializable]
public class Quest
{
    public string name;
    public string description;
    public bool isActive;
    public bool isCompleted;
    bool[] completeObjectives;
    public string[] objectiveDescriptions;
    public Quest(string questName, int objectiveCount, bool startActive, string descriptionText, string[] objectiveText)
    {
        description = descriptionText;
        name = questName;
        completeObjectives = new bool[objectiveCount];
        for(int i =0; i<objectiveCount; i++)
        {
            completeObjectives[i] = false;
        }
        objectiveDescriptions = objectiveText;
        isActive = startActive;
    }

    public void UpdateObjective(int objectiveNumber)
    {
        completeObjectives[objectiveNumber - 1] = true;
        isCompleted = true;
        foreach (bool objective in completeObjectives)
        {
            if (!objective)
            {
                isCompleted = false;
            }
        }
    }
    public bool getObjective(int objectiveNumber)
    {
        return completeObjectives[objectiveNumber-1];
    }
}
