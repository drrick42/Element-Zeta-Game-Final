using UnityEngine;
using System.Collections;

public interface ICommand {

    /// <summary>
    /// Returns the name of the command.
    /// This is the string which will be entered by the user on the command line.
    /// </summary>
    string GetCommandName();

    /// <summary>
    /// Executes a command.
    /// The first string in args is the name of the command, the rest are command line arguements.
    /// Returns the command output. If no output, returns an empty string.
    /// </summary>
    string Execute(string[] args);

    /// <summary>
    /// Returns a string explaining what the command is and how to use it.
    /// </summary>
    string GetHelpText();
}
