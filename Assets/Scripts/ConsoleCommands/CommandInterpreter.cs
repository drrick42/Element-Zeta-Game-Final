using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;

public static class CommandInterpreter {

    /// <summary>
    /// Maps the name of the command (that the user would enter) to an instance of it.
    /// </summary>
    private static Dictionary<string, ICommand> commandMap;
    private static bool initialized = false;

    /// <summary>
    /// Given raw console input, parses the input into an array of arguements.
    /// Finds the ICommand class corresponding to the first arguement and executes that command.
    /// Returns a string. The return string will either be the output of the command or error info.
    /// If no command is entered, returns an empty string */
    /// </summary>
	public static string ExecuteCommand(string rawInput)
    {
        string[] args = ParseCommandString(rawInput);
        
        /* If the user only entered spaces or nothing, just return back to the console. */
        if (args == null)
            return "";

        if (!initialized)
            Initialize();

        /* The first arg is the name of the command */
        string commandName = args[0];
        
        /* Get an instance of the command */
        ICommand command = GetCommandInstance(commandName);

        /* If the type is null, there is no class with that name */
        if (command == null)
            return "No such command: " + commandName + "\nEnter 'help' for a list of commands.";

        /* Execute the command and return its output */
        return command.Execute(args);
    }

    /// <summary>
    /// Creates the command dictionary and fills it.
    /// </summary>
    private static void Initialize()
    {
        commandMap = new Dictionary<string, ICommand>();
        FindCommands();
        initialized = true;
    }

    /// <summary>
    /// Looks through each class in the project's assembly. If it inherits from ICommand, adds an isntance of it to the dictionary.
    /// </summary>
    private static void FindCommands()
    {
        ICommand tmp = new HelpCommand(); //Temporary object to let me see all the types in the project's assembly
        /* Loops through each type available in the project */
        foreach (Type t in tmp.GetType().Assembly.GetTypes())
        {
            /* For each type, loop through all of the interfaces it inherits */
            Type[] interfaces = t.GetInterfaces();
            foreach (Type i in interfaces)
            {
                /* If one of the interfaces is ICommand, add it to the list */
                if (i.Equals(typeof(ICommand)))
                {
                    /* Create an instance of the command and add it to the dictionary */
                    ICommand commandInstance = (ICommand)Activator.CreateInstance(t);
                    string commandName = commandInstance.GetCommandName();
                    commandMap[commandName] = commandInstance;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Splits rawInput into an array of strings separated by space characters.
    /// If no valid characters were entered, returns null.
    /// </summary>
    private static string[] ParseCommandString(string rawInput)
    {
        string[] split = rawInput.Split(' ');
        List<string> args = new List<string>();

        /* Use the list to filter out empty strings */
        /* If the user enters more than one space, each extra character will appear as an empty string */
        foreach(string s in split)
        {
            if (s.Length > 0)
                args.Add(s);
        }

        if (args.Count == 0)
            return null;

        return args.ToArray();
    }

    /// <summary>
    /// Returns an instance of the class named by commandName.
    /// If no such class exists, returns null.
    /// </summary>
    public static ICommand GetCommandInstance(string commandName)
    {
        if (commandMap.ContainsKey(commandName))
            return commandMap[commandName];
        else
            return null;
    }

    /// <summary>
    /// Returns an unsorted list of the names of all available commands.
    /// Each name can be used with GetCommandInstance.
    /// </summary>
    public static List<string> GetAvailableCommandNames()
    {
        List<string> commands = new List<string>();
        foreach(string s in commandMap.Keys)
        {
            commands.Add(s);
        }
        return commands;
    }
}
