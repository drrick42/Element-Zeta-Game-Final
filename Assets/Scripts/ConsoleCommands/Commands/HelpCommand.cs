using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

public class HelpCommand : ICommand
{
    public string GetCommandName()
    {
        return "help";
    }

    public string Execute(string[] args)
    {
        /* If no other args, list all the possible commands */
        if (args.Length == 1)
            return GetCommandListText();

        /* If 2 args, args[1] is the command the user wants info about */
        else if (args.Length == 2)
            return GetCommandHelpInfo(args[1]);

        else
            return "Invalid number of arguements. Enter 'help' for a list of commands or 'help <commmand-name> for info on a specific command.";
    }

    public string GetHelpText()
    {
        return "Displays help info.\nEnter 'help' to see a list of available commands.\nEnter 'help <command-name>' to see how to use a specific command.";
    }

    private string GetCommandListText()
    {
        List<string> commands = CommandInterpreter.GetAvailableCommandNames(); ;

        /* Make them alphabetical */
        commands.Sort();

        StringBuilder helpText = new StringBuilder("Available commands: \n");
        foreach (string s in commands)
        {
            helpText.Append(s);
            helpText.Append('\n');
        }
        helpText.Append("Enter 'help <command-name>' for more info.");

        return helpText.ToString();
    }

    private string GetCommandHelpInfo(string commandName)
    {
        ICommand command = CommandInterpreter.GetCommandInstance(commandName);

        if (command == null)
            return "No such command: " + commandName;

        return commandName + ":\n" + command.GetHelpText();
    }
}
