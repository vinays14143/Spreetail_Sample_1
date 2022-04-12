using MultiValueDictionary.Helpers;
using MultiValueDictionary.src.CommandManager;
using MultiValueDictionary.src.Enums;
using System;

namespace MultiValueDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The Multi-Value Dictionary app is a command line application that stores a multivalue dictionary in memory");
            Console.WriteLine("Please enter the commnds in the list:\nKEYS, MEMBERS, ADD, REMOVE, REMOVEALL, CLEAR, KEYIFEXISTS, MEMBEREXISTS, ITEMS");
            Console.WriteLine("To quit. Enter STOP/stop");
            bool quitFlag = false;
            while (!quitFlag)
            {
                var arguments = Console.ReadLine();
                if (arguments.ToUpper() == "STOP")
                {
                    quitFlag = true;
                    return;
                }
                //To validate inputs entered by user
                var validate = ValidateInputs.ValidateInput(arguments);
                
                if (!validate.IsValid)
                {
                    Console.WriteLine("Invalid inputs, Please enter valid inputs");
                    continue;
                }
                var commandManger = new CommandManager();
                var result = commandManger.MultiValueDictionaryOperation(validate.Command, validate.Key, validate.Value);
                Console.WriteLine($"Message: {result.Message}\nOutputValues:\n{result.OutputValue}\nIsSuccess: {result.IsSuccess} ");
                Console.WriteLine("\n");
            }
            
        }
    }
}
