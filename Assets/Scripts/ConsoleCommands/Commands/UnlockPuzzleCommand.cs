using UnityEngine;
using System.Collections;
using System;

public class UnlockPuzzleCommand : ICommand {
    public string Execute(string[] args)
    {
        if (args.Length != 2)
            return "Incorrect number of arguments.";

        int level = 0;
        try
        {
            level = int.Parse(args[1]);
        }
        catch (FormatException e)
        {
            return "Arguement must be an integer.";
        }

        UIManager ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        ui.UnlockDoor(level);

        return "";
    }

    public string GetCommandName()
    {
        return "unlocklevel";
    }

    public string GetHelpText()
    {
        return "Unlocks the specified level in the hub room. Should have 1 arguement, which is an integer.";
    }

}
