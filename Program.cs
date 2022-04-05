/// <summary>
/// Name: Jenny Nguyen 
/// Assignment: 5
/// Description: This will allow the declaration of constants in the language. You will
//have to add the reserved word const to your lexical analyzer and
//rewrite the DECL and PROG procedure in your parser.
//    Add the appropriate semantic actions to your parser to insert all
//constants, variables and functions into your symbol table. For
//constants you will have to set either the value or valuer field as
//returned by your lexical analyzer. Variables will require the type, size
//and current offset of the new variable.The first variable at a given
//depth will be at offset 0. Integers have size 2, characters have size 1
//and float has size 4. Update the offset field by the size of the new
//variable after inserting the variable. Functions will require that you
//keep track of the size of all local variables and formal parameters, the
//number and type of all formal parameters and the return type of the
//function. In all three cases the fields lexeme, token and depth are
//required.
/// </summary>
using System;

namespace CSC446_Assignment_6_Nguyen
{
    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            //start:
            Lexie.LexicalAnalyzer(args);
            Console.WriteLine("Lexical Analyzer completed. Commencing Parser.\n");
            SymbolTable.symList();
            Parser.Parse();
            Console.WriteLine("\nParser completed completed.");


            //    string continueProgram;

            //cp:
            //    Console.WriteLine("\nDo you want to enter another file? Enter Y for yes and N for to exit the program");
            //    continueProgram = Console.ReadLine();

            //    if (continueProgram.ToLower() == "n")
            //    {
            //        System.Environment.Exit(0);
            //    }
            //    else if (continueProgram.ToLower() == "y")
            //    {
            //        Console.Clear();
            //        Lexie.MatchTokens.Clear();
            //        Lexie.Token.Equals(null);
            //        Lexie.counting = 0;
            //        Parser.increments = 0;
            //        goto start;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid Response.");
            //        goto cp;
            //    }


        }
    }
}