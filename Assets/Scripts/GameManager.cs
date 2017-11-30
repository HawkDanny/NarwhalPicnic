using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum Scenario
    {
        Idle,
        Tutorial,
        Party,
        Construction
    }

    //Managers
    public TutorialManager tutorialMan;
    public PartyManager partyMan;
    public ConstructionManager constructionMan;
    public VRManager vrMan;

    //The current state of the game
    public Scenario currentScenario;


    //Set up the first scenario
	void Start ()
    {
        //The very first scenario run
        RunScenario(Scenario.Idle);
	}

    //TEMPORARILY HANDLE INPUT
    void Update()
    {
        //Draw lines on the corners of the boundaries
        Debug.DrawLine(vrMan.Corner0, new Vector3(vrMan.Corner0.x, vrMan.Corner0.y + 1, vrMan.Corner0.z), Color.cyan);
        Debug.DrawLine(vrMan.Corner1, new Vector3(vrMan.Corner1.x, vrMan.Corner1.y + 1, vrMan.Corner1.z), Color.cyan);
        Debug.DrawLine(vrMan.Corner2, new Vector3(vrMan.Corner2.x, vrMan.Corner2.y + 1, vrMan.Corner2.z), Color.cyan);
        Debug.DrawLine(vrMan.Corner3, new Vector3(vrMan.Corner3.x, vrMan.Corner3.y + 1, vrMan.Corner3.z), Color.cyan);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            RunScenario(Scenario.Party);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            RunScenario(Scenario.Construction);
    }

    //Runs the appropriate scenario within the corresponding manager
    public void RunScenario(Scenario nextScenario)
    {
        StopScenario(currentScenario);

        switch (nextScenario) {
            case Scenario.Idle:
                break;
            case Scenario.Tutorial:
                //tutorialMan.Run();
                break;
            case Scenario.Party:
                partyMan.Run();
                break;
            case Scenario.Construction:
                constructionMan.Run();
                break;
        }

        currentScenario = nextScenario;
    }

    //Stops the current scenario
    private void StopScenario(Scenario currentScenario) 
    {
        switch (currentScenario)
        {
            case Scenario.Idle:
                break;
            case Scenario.Tutorial:
                //tutorialMan.Stop();
                break;
            case Scenario.Party:
                partyMan.Stop();
                break;
            case Scenario.Construction:
                constructionMan.Stop();
                break;
        }
    }
}
