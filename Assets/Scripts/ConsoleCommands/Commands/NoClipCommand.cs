using UnityEngine;
using System.Collections;
using System;

public class NoClipCommand : ICommand
{
    private GameObject player = null;

    public string Execute(string[] args)
    {
        if(args.Length > 2)
        {
            return "Error - Incorrect number of arguments. Enter 'noclip' to toggle, 'noclip on' or 'noclip off'.";
        }

        /* First time we need to find the player */
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

        /* If the player couldn't be found, stop */
        if(player == null)
        {
            return "Error - No player found in scene. There should be a GameObject named 'Player'.";
        }

        PlayerMove move = player.GetComponent<PlayerMove>();
        if(move == null)
        {
            return "Error - Player does not have PlayerMove component attached. Cannot enter Noclipmode";
        }

        /* No args, just toggle */
        if(args.Length == 1)
        {
            move.NoclipEnabled = !move.NoclipEnabled;
        }
        else if(args[1].ToLower().Equals("on"))
        {
            move.NoclipEnabled = true;
        }
        else if (args[1].ToLower().Equals("off"))
        {
            move.NoclipEnabled = false;
        }
        else
        {
            return "Error - Unrecognized word " + args[1] + ". Enter 'on' or 'off'.";
        }

        if(move.NoclipEnabled)
        {
            return "Noclip mode enabled.";
        }
        else
        {
            return "Noclip mode disabled.";
        }

    }

    public string GetCommandName()
    {
        return "noclip";
    }

    public string GetHelpText()
    {
        string help = "Enables or disables noclip mode. In noclip mode, the player moves in the direction of the camera. Gravity is not applied and the player can clip through walls and other obstacles.\n" +
            "Enter 'noclip' to toggle noclip mode. Enter 'noclip on' to enable, or 'noclip off' to disable.";
        return help;
    }
}
