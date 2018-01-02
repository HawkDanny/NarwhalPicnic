using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum Scenario
    {
        Idle,
        Party,
        Construction,
        Phone
    }

    //Managers
    public TutorialManager tutorialMan;
    public PartyManager partyMan;
    public ConstructionManager constructionMan;
    public VRManager vrMan;
    public PhoneManager phoneMan;

    //The current state of the game
    public Scenario currentScenario;
    private Scenario lastNonIdleScenario;

    //Idle instructions
    public Instructions idleInstructions;

    //Things needed to set the monitor instructions
    public Camera monitorCamera;
    public GameObject textGO;
    public GameObject shadowGO;


    //Set up the first scenario
	void Start ()
    {
        //The very first scenario run
        RunScenario(Scenario.Idle);
	}

    //TEMPORARILY HANDLE INPUT
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            RunScenario(Scenario.Party);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            RunScenario(Scenario.Construction);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            RunScenario(Scenario.Phone);

        //When a scenario is complete, go back to idle
        if (IsScenarioComplete(currentScenario))
        {
            //RunScenario(Scenario.Idle);
        }
    }

    //Runs the appropriate scenario within the corresponding manager
    public void RunScenario(Scenario nextScenario)
    {
        StopScenario(currentScenario);

        switch (nextScenario) {
            case Scenario.Idle:
                SetInstructions(idleInstructions);
                break;
            case Scenario.Party:
                partyMan.Run(vrMan);
                SetInstructions(partyMan.instructions);
                break;
            case Scenario.Construction:
                constructionMan.Run();
                SetInstructions(constructionMan.instructions);
                break;
            case Scenario.Phone:
                phoneMan.Run();
                SetInstructions(phoneMan.instructions);
                break;
        }

        currentScenario = nextScenario;
    }

    //Stops the current scenario and updates lastNonIdleScenario
    private void StopScenario(Scenario currentScenario) 
    {
        if (currentScenario != Scenario.Idle)
            lastNonIdleScenario = currentScenario;

        switch (currentScenario)
        {
            case Scenario.Idle:
                break;
            case Scenario.Party:
                partyMan.Stop();
                break;
            case Scenario.Construction:
                constructionMan.Stop();
                break;
            case Scenario.Phone:
                phoneMan.Stop();
                break;
        }
    }

    private bool IsScenarioComplete(Scenario currentScenario)
    {
        switch (currentScenario)
        {
            case Scenario.Idle:
                return false; //This is controlled by photomanager
            case Scenario.Party:
                if (partyMan.poppedBalloons >= partyMan.numBalloons)
                    return true;
                else
                    return false;
            case Scenario.Construction:
                if (constructionMan.isDemolished)
                    return true;
                else
                    return false;
            case Scenario.Phone:
                if (phoneMan.answeredPhone)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    //Run the next real scenario
    public void RunNextScenario()
    {
        if (lastNonIdleScenario + 1 == Scenario.Idle)
            RunScenario(lastNonIdleScenario + 2);
        else
            RunScenario(lastNonIdleScenario + 1);
    }

    /// <summary>
    /// Sets the instructions of the monitor according to the scene manager's Instructions object
    /// </summary>
    /// <param name="inst">The scene manager's Instructions objects</param>
    private void SetInstructions(Instructions inst)
    {
        monitorCamera.backgroundColor = inst.backgroundColor;
        Text text = textGO.GetComponent<Text>();
        Text shadow = shadowGO.GetComponent<Text>();
        text.color = inst.textColor;
        text.text = inst.text;
        shadow.color = inst.shadowColor;
        shadow.text = inst.text;
    }
}
