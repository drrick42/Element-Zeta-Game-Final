using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneCommand : ICommand {

    public string Execute(string[] args)
    {
        if (args.Length != 2)
            return "Incorrect number of arguments.";

        //Scene scene = SceneManager.GetSceneByName(args[1]);

        //if (scene.name == null)
        //    return "No such scene: " + args[1];

        UIManager ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        ui.LoadLevel(args[1]);

        return "";
    }

    public string GetCommandName()
    {
        return "loadscene";
    }

    public string GetHelpText()
    {
        return "Loads the specified scene. Should have 1 arguement which is the name of the scene to be loaded.";
    }
}
