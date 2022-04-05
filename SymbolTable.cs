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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC446_Assignment_6_Nguyen
{
    public class SymbolTable
    {
        public const int TableSize = 211;

        public static List<entryTable>[] symboltable;


        /// <summary>
        /// this will have the tokens, depth, and lexeme to be used during the hash of the symbol table
        /// </summary>
        public class entryTable
        {
            public int depth;
            public string Token;
            public string lexeme;
            public RecordEnum typeOfEntry;
        }

        /// <summary>
        /// Record Enum to use for variable, constants, and functions
        /// </summary>
        public enum RecordEnum
        {
            Variable,
            Constant,
            Function
        }

        /// <summary>
        /// Variable Enum to use for any records for int, char, and float
        /// </summary>
        public enum VariableEnum
        {
            IntRec,
            CharRec,
            FloatRec
        }

        /// <summary>
        /// This will have the parameters and the references 
        /// </summary>
        public enum ParamEnum
        {
            RefRec,
            ValueRec,
        }

        /// <summary>
        ///  type of variable (use an enumerated data type),
        ///  offset(use an integer variable) and size(use an integer variable).
        /// </summary>
        public class Variable : entryTable
        {
            public int offset;
            public int size;
            public VariableEnum varType;
        }

        /// <summary>
        /// appropriate fields to store either a real or integer value
        /// </summary>
        public class Constant : entryTable
        {
            public int offset;
            public int size;
            public VariableEnum costType;
        }

        /// <summary>
        ///  size of local variables (this is the total required for all
        /// locals), number of parameters, type of each parameter, and 
        /// return type.
        /// </summary>
        public class Function : entryTable
        {
            public int offset;
            public int size;
            public VariableEnum funType;
        }

        public static void symList()
        {
            symboltable = new List<entryTable>[TableSize];
            for (int i = 0; i < TableSize; i++)
            {
                symboltable[i] = new List<entryTable>();
            }
        }
        /// <summary>
        /// referenced pseduocode from class and comparing with peers
        /// </summary>
        /// <param name="Lexeme"></param>
        /// <param name="Token"></param>
        /// <param name="depth"></param>
        public static void insert(string Lexeme, string Token, int depth, RecordEnum record)
        {
            uint x;
            x = hash(Lexeme);

            entryTable EntryVar = new entryTable();


            EntryVar.lexeme = Lexeme;
            EntryVar.Token = Token;
            EntryVar.depth = depth;
            EntryVar.typeOfEntry = record;

            //no error checking performed

            EntryVar.depth = depth;
            //add to front of list
            symboltable[x].Insert(0, EntryVar);

        }


        /// <summary>
        /// will use the entry table created and will look up a certain lexeme and return where that lexeme is located 
        /// </summary>
        /// <param name="tempLexeme"></param>
        /// <returns></returns>
        public static entryTable lookUp(string lex)
        {
            entryTable lookUpReturn = new entryTable();
            foreach (var tableEntry in symboltable[hash(lex)])
            {
                if (tableEntry.lexeme == lex)
                {
                    lookUpReturn = tableEntry;
                }
            }
            return (lookUpReturn);
        }

        /// <summary>
        /// will delete the depth of the lexeme and remove it from the list. 
        /// used the link below for part of this function
        /// https://stackoverflow.com/questions/10018957/how-to-remove-item-from-list-in-c
        /// </summary>
        /// <param name="depth"></param>
        public static void deleteDepth(int depth)
        {
            foreach (List<entryTable> locationCount in symboltable)
            {

                if (locationCount.Count > 0)
                {
                    locationCount.RemoveAll(remove => remove.depth == depth);
                }
            }
        }

        /// <summary>
        /// this is for displaying the variables: lexeme, token, and the depth
        /// </summary>
        /// <param name="depth"></param>
        public static void writeTable(int depth)
        {
            string pad = " ";
            Console.WriteLine("Lexeme" + pad.PadRight(12) + "Token" + pad.PadRight(12) + "Depth" + pad.PadRight(12) + "Type");
            Console.WriteLine("_______________________________________________________________");

            foreach (List<entryTable> info in symboltable)
            {


                if (info.Count > 0)
                {
                    foreach (var i in info)
                    {
                        if (i.depth == depth)
                        {
                            Console.WriteLine(i.lexeme.PadRight(20) + i.Token.PadRight(17) + i.depth + pad.PadRight(12) + i.typeOfEntry + "\n");
                        }
                    }

                }
            }
            Console.WriteLine("_______________________________________________________________");
            Console.WriteLine("_______________________________________________________________\n\n");

        }

        /// <summary>
        /// this was referenced with https://www.softwaretestinghelp.com/c-sharp/csharp-collections/ and talked with my peers about this. This is the 
        /// hash function for the program
        /// </summary>
        /// <param name="Lexeme"></param>
        /// <returns></returns>
        private static uint hash(string Lexeme)
        {
            uint h = 0, g;

            foreach (char temp in Lexeme)
            {
                h = (h << 24) + (byte)temp;
                if ((g = h & 0xf0000000) != 0)
                {
                    h = h ^ (g >> 24);
                    h = h ^ g;
                }
            }
            return h % TableSize;
        }
    }
}