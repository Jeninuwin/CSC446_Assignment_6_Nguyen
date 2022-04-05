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
    /// Defines the <see cref="Parser" />.
    /// </summary>
    public class Parser
    {
        public static int currentOffset = 0;
        public static int charOffset = 1;
        public static int floatOffset = 4;
        public static int intOffset = 2;
        public static int totalOffset;
        public static int depth = 0;
        public static int tempies;

        public static SymbolTable.entryTable val = new SymbolTable.entryTable();

        public SymbolTable sym = new SymbolTable();
        /// <summary>
        /// Defines the increments  while setting it to 0.
        /// </summary>
        public static int increments = 0;
        public static string temp = "";
        /// <summary>
        /// The Parse will grab the MatchToken list and will call Prog to start the parsing
        /// </summary>
        public static void Parse()
        {
            //Console.WriteLine(temp.PadRight(22, ' ') + "Tokens");
            //Console.WriteLine(temp.PadRight(14, ' ') + "________________________");
            //for (int i = 0; i < Lexie.counting; i++)
            //{
            //    Console.WriteLine(Lexie.MatchTokens[i].PadLeft(28, ' '));
            //}

            Prog();

        }

        /// <summary>
        /// The Prog sees if the the current MatchToken equals either int, float, or char. If it doesn't equal any of those go to eofft
        /// PROG -> TYPE idt REST PROG |
        //const idt = num; PROG |
        public static void Prog()
        {
            bool done = false;

            while (done == false) //if done is true it will exit the loop and prog will be completed
            {
                switch (Lexie.MatchTokens[increments])
                {
                    case "intt":
                    case "floatt":
                    case "chart":
                        {
                            //offset increase base on type 
                            if (Lexie.MatchTokens[increments] == "intt")
                            {
                                currentOffset = intOffset;
                            }
                            else if (Lexie.MatchTokens[increments] == "floatt")
                            {
                                currentOffset = floatOffset;
                            }
                            else
                            {
                                currentOffset = charOffset;
                            }

                            totalOffset += currentOffset;

                            increments++;

                            switch (Lexie.MatchTokens[increments]) //this will see if the MatchTokens will match "idt" if it does call Rest, else error
                            {
                                case "idt":
                                    {
                                        if (Lexie.MatchTokens[increments] == "commat")
                                        {
                                            //store after the value into a temp                                       
                                            //lookup list for dups 
                                            val = SymbolTable.lookUp(Lexie.LexemeString[increments]);
                                            if (val.lexeme != Lexie.LexemeString[increments])
                                            {
                                                //insert after storing the value
                                                SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                                            }

                                            else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                                            {
                                                SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                                            }


                                            else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                                            {
                                                Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                                                Environment.Exit(1);
                                                break;

                                            }
                                        }
                                        else
                                        {
                                            //store after the value into a temp                                       
                                            //lookup list for dups 
                                            val = SymbolTable.lookUp(Lexie.LexemeString[increments]);

                                            if (val.lexeme != Lexie.LexemeString[increments])
                                            {
                                                increments++; //looks ahead to determine if function or variable
                                                if (Lexie.MatchTokens[increments] == "commat" || Lexie.MatchTokens[increments] == "semit")
                                                {
                                                    increments--;
                                                    SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                                                }
                                                else
                                                {
                                                    increments--;
                                                    //insert after storing the value
                                                    SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Function);
                                                }


                                            }

                                            else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                                            {
                                                increments++; //looks ahead to determine if function or variable
                                                if (Lexie.MatchTokens[increments] == "commat" || Lexie.MatchTokens[increments] == "semit")
                                                {
                                                    increments--;
                                                    SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                                                }
                                                else
                                                {
                                                    increments--;
                                                    //insert after storing the value
                                                    SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Function);
                                                }
                                            }


                                            else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                                            {
                                                Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                                                Environment.Exit(1);
                                                break;

                                            }

                                        }
                                        increments++;
                                        Rest();
                                        break;
                                    }
                                case "constt":
                                    {
                                        //store after the value into a temp                                       
                                        //lookup list for dups 
                                        val = SymbolTable.lookUp(Lexie.LexemeString[increments]);
                                        if (val.lexeme != Lexie.LexemeString[increments])
                                        {
                                            //insert after storing the value
                                            SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Constant);
                                        }

                                        else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                                        {
                                            SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Constant);
                                        }


                                        else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                                        {
                                            Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                                            Environment.Exit(1);
                                            break;

                                        }
                                        increments++;
                                        Prog();
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'idt'. Not correct Grammar.");
                                        Environment.Exit(1);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "constt":
                        increments++;
                        switch (Lexie.MatchTokens[increments])
                        {
                            case "idt":
                                {
                                    //store after the value into a temp                                       
                                    //lookup list for dups 
                                    val = SymbolTable.lookUp(Lexie.LexemeString[increments]);
                                    if (val.lexeme != Lexie.LexemeString[increments])
                                    {
                                        //insert after storing the value
                                        SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Constant);
                                    }

                                    else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                                    {
                                        SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Constant);
                                    }


                                    else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                                    {
                                        Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                                        Environment.Exit(1);
                                        break;

                                    }
                                    increments++;
                                    switch (Lexie.MatchTokens[increments])
                                    {
                                        case "assignopt":
                                            {
                                                increments++;
                                                switch (Lexie.MatchTokens[increments])
                                                {
                                                    case "numt":
                                                        {
                                                            //check if float or int then increase offset 
                                                            if (Lexie.MatchTokens[increments] == "intt")
                                                            {
                                                                totalOffset += intOffset;
                                                            }
                                                            else if (Lexie.MatchTokens[increments] == "floatt")
                                                            {
                                                                totalOffset += floatOffset;
                                                            }
                                                            increments++;
                                                            switch (Lexie.MatchTokens[increments])
                                                            {
                                                                case "semit":
                                                                    {
                                                                        increments++;
                                                                        Prog();
                                                                        break;
                                                                    }
                                                                case "eoftt":
                                                                    done = true;
                                                                    break;
                                                                default:
                                                                    {
                                                                        done = true;
                                                                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'semit'. Not correct Grammar.");
                                                                        Environment.Exit(1);
                                                                        break;
                                                                    }
                                                            }
                                                            break;
                                                        }
                                                    case "eoftt":
                                                        done = true;
                                                        break;
                                                    default:
                                                        {
                                                            done = true;
                                                            Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'numt'. Not correct Grammar.");
                                                            Environment.Exit(1);
                                                            break;
                                                        }
                                                }
                                                break;
                                            }
                                        case "eoftt":
                                            done = true;
                                            break;
                                        default:
                                            {
                                                done = true;
                                                Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'assignopt'. Not correct Grammar.");
                                                Environment.Exit(1);
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "eoftt":
                                done = true;
                                break;
                            default:
                                {
                                    done = true;
                                    Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'idt'. Not correct Grammar.");
                                    Environment.Exit(1);
                                    break;
                                }
                        }
                        break;
                    case "eoftt":
                        done = true;
                        break;
                    default:
                        {
                            done = true;
                            Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'intt', 'floatt', 'constt' or 'chart'. Not correct Grammar.");
                            Environment.Exit(1);
                            break;
                        }
                }

            }
        }

        /// <summary>
        /// The Rest will see if the MatchTokens fit either lparent, commat, or semit.
        /// </summary>
        public static void Rest()
        {
            switch (Lexie.MatchTokens[increments])
            {
                case "lparent":
                    {
                        //increase depth
                        depth++;
                        increments++;
                        Paramlist();

                        if (Lexie.MatchTokens[increments] == "rparent")
                        {
                            increments++;
                            Compound();
                        }
                        break;
                    }
                case "semit":
                case "commat":
                    {
                        IDTail();
                        if (Lexie.MatchTokens[increments] == "semit")
                        {
                            increments++;
                        }

                        else
                        {
                            Console.WriteLine("Error:  " + Lexie.MatchTokens[increments] + " was found when searching for 'semit'. Not correct Grammar.");
                            Environment.Exit(1);
                        }
                        break;
                    }
                //case "rparent":
                //    break;
                default:
                    {
                        Console.WriteLine("Error:  " + Lexie.MatchTokens[increments] + " was found when searching for 'semit', 'commat', or 'lparent'. Not correct Grammar.");
                        Environment.Exit(1);
                        break;
                    }
            }
        }

        /// <summary>
        /// The Paramlist will see if the MatchTokens fit either int, float, char, or rparent
        /// </summary>
        public static void Paramlist()
        {
            switch (Lexie.MatchTokens[increments])
            {
                case "intt":
                case "floatt":
                case "chart":
                    {
                        //increase offset 
                        if (Lexie.MatchTokens[increments] == "intt")
                        {
                            currentOffset = intOffset;
                        }
                        else if (Lexie.MatchTokens[increments] == "floatt")
                        {
                            currentOffset = floatOffset;
                        }
                        else
                        {
                            currentOffset = charOffset;
                        }

                        totalOffset += currentOffset;

                        increments++;
                        if (Lexie.MatchTokens[increments] == "idt")
                        {
                            val = SymbolTable.lookUp(Lexie.LexemeString[increments]);
                            if (val.lexeme != Lexie.LexemeString[increments])
                            {
                                //insert after storing the value
                                SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                            }

                            else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                            {
                                SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                            }


                            else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                            {
                                Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                                Environment.Exit(1);
                                break;

                            }
                            increments++;
                            ParamTail();
                        }

                        else
                        {
                            Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'idt'. Not correct Grammar.");
                            Environment.Exit(1);
                        }
                        break;
                    }
                case "rparent":
                    break;
                default:
                    {
                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'rparent'. Not correct Grammar."); Environment.Exit(1);
                        break;
                    }
            }
        }

        /// <summary>
        /// The ParamTail will see if the MatchTokens fit either int, float, char, or rparent
        /// </summary>
        public static void ParamTail()
        {
            switch (Lexie.MatchTokens[increments])
            {
                case "commat":
                    {
                        increments++;

                        if (Lexie.MatchTokens[increments] == "intt" | Lexie.MatchTokens[increments] == "floatt" | Lexie.MatchTokens[increments] == "chart")
                        {
                            if (Lexie.MatchTokens[increments] == "intt")
                            {
                                currentOffset = intOffset;
                            }
                            else if (Lexie.MatchTokens[increments] == "floatt")
                            {
                                currentOffset = floatOffset;
                            }
                            else
                            {
                                currentOffset = charOffset;
                            }

                            totalOffset += currentOffset;

                            increments++;

                            switch (Lexie.MatchTokens[increments]) //this will see if the MatchTokens will match "idt" if it does call Rest, else error
                            {
                                case "idt":
                                    {
                                        //lookup for dups and insert
                                        val = SymbolTable.lookUp(Lexie.LexemeString[increments]);
                                        if (val.lexeme != Lexie.LexemeString[increments])
                                        {
                                            //insert after storing the value
                                            SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                                        }

                                        else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                                        {
                                            SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                                        }


                                        else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                                        {
                                            Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                                            Environment.Exit(1);
                                            break;

                                        }
                                        increments++;
                                        ParamTail();
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'idt'. Not correct Grammar.");
                                        Environment.Exit(1);
                                        break;
                                    }
                            }
                        }

                        else
                        {
                            Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'intt', 'floatt', or 'chart'. Not correct Grammar.");
                            Environment.Exit(1);

                        }
                        break;
                    }
                case "rparent":
                    break;
                default:
                    {
                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'rparent'. Not correct Grammar."); Environment.Exit(1);
                        Environment.Exit(1);
                        break;
                    }

            }
        }

        /// <summary>
        /// The Compound will see if the MatchTokens start with a '{' if not then it's an error 
        /// </summary>
        public static void Compound()
        {
            switch (Lexie.MatchTokens[increments])
            {
                case "openCurlyParent":
                    {
                        increments++;
                        Decl();
                        StatList();

                        if (Lexie.MatchTokens[increments] == "closeCurlyParent")
                        {
                            increments++;
                            Prog();
                            if (Lexie.MatchTokens[increments] == "eoftt")
                            {
                                break;
                            }
                        }

                        else
                        {
                            Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'closeCurlyParent'. Not correct Grammar.");
                            Environment.Exit(1);

                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'openCurlyParent'. Not correct Grammar.");
                        Environment.Exit(1);
                        break;
                    }
            }
        }

        /// <summary>
        /// The Decl will see if the MatchToken matches either int, float, char, or '}'. If it doesn't then it will throw an error
        /// DECL -> TYPE IDLIST |
        //const idt = num; DECL |
        /// </summary>
        public static void Decl()
        {
            switch (Lexie.MatchTokens[increments])
            {
                case "intt":
                case "floatt":
                case "chart":
                    {
                        //offsets increase 
                        if (Lexie.MatchTokens[increments] == "intt")
                        {
                            currentOffset = intOffset;
                        }
                        else if (Lexie.MatchTokens[increments] == "floatt")
                        {
                            currentOffset = floatOffset;
                        }
                        else
                        {
                            currentOffset = charOffset;
                        }

                        totalOffset += currentOffset;

                        increments++;
                        IDList();
                        break;
                    }
                case "closeCurlyParent":
                    {
                        //decrease depth 
                        if (depth > 0)
                        {
                            SymbolTable.writeTable(depth);
                        }
                        SymbolTable.deleteDepth(depth);
                        depth--;
                        SymbolTable.writeTable(depth);
                        break;
                    }
                case "constt":
                    {
                        increments++;
                        switch (Lexie.MatchTokens[increments])
                        {
                            case "idt":
                                {
                                    //lookup for dups and insert
                                    val = SymbolTable.lookUp(Lexie.LexemeString[increments]);
                                    if (val.lexeme != Lexie.LexemeString[increments])
                                    {
                                        //insert after storing the value
                                        SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Constant);
                                    }

                                    else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                                    {
                                        SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Constant);
                                    }

                                    else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                                    {
                                        Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                                        Environment.Exit(1);
                                        break;

                                    }
                                    increments++;
                                    switch (Lexie.MatchTokens[increments])
                                    {
                                        case "assignopt":
                                            {
                                                increments++;
                                                switch (Lexie.MatchTokens[increments])
                                                {
                                                    case "numt":
                                                        {
                                                            //check if float or int then increase offset 
                                                            if (Lexie.MatchTokens[increments] == "intt")
                                                            {
                                                                totalOffset += intOffset;
                                                            }
                                                            else if (Lexie.MatchTokens[increments] == "floatt")
                                                            {
                                                                totalOffset += floatOffset;
                                                            }

                                                            increments++;
                                                            switch (Lexie.MatchTokens[increments])
                                                            {
                                                                case "semit":
                                                                    {
                                                                        increments++;
                                                                        Decl();
                                                                        break;
                                                                    }
                                                                case "eoftt":
                                                                    break;
                                                                default:
                                                                    {
                                                                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'semit'. Not correct Grammar.");
                                                                        Environment.Exit(1);
                                                                        break;
                                                                    }
                                                            }
                                                            break;
                                                        }
                                                    case "eoftt":
                                                        break;
                                                    default:
                                                        {
                                                            Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'numt'. Not correct Grammar.");
                                                            Environment.Exit(1);
                                                            break;
                                                        }
                                                }
                                                break;
                                            }
                                        case "eoftt":
                                            break;
                                        default:
                                            {
                                                Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'assignopt'. Not correct Grammar.");
                                                Environment.Exit(1);
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'intt', 'floatt', 'const' or 'chart'. Not correct Grammar.");
                        Environment.Exit(1);
                        break;
                    }
            }

        }

        /// <summary>
        /// The IDList will see if the MatchToken Matches with idt and semit after. if not then an error has occurred
        /// </summary>
        public static void IDList()
        {
            switch (Lexie.MatchTokens[increments])
            {
                case "idt":
                    {
                        //lookup for dups and insert
                        val = SymbolTable.lookUp(Lexie.LexemeString[increments]);
                        if (val.lexeme != Lexie.LexemeString[increments])
                        {
                            //insert after storing the value
                            SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                        }

                        else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                        {
                            SymbolTable.insert(Lexie.LexemeString[increments], Lexie.MatchTokens[increments - 1], depth, SymbolTable.RecordEnum.Variable);
                        }


                        else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                        {
                            Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                            Environment.Exit(1);
                            break;

                        }
                        increments++;
                        IDTail();

                        switch (Lexie.MatchTokens[increments])
                        {
                            case "semit":
                                {
                                    increments++;
                                    Decl();
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'semit'. Not correct Grammar.");
                                    Environment.Exit(1);
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'idt'. Not correct Grammar.");
                        Environment.Exit(1);
                        break;
                    }
            }
        }

        /// <summary>
        /// The IDTail will see if the MatchToken matches with Commat or Semmit. If it doesn't then it's an error
        /// </summary>
        public static void IDTail()
        {
            string position;
            switch (Lexie.MatchTokens[increments])
            {
                case "commat":
                    {
                        if (currentOffset == intOffset)
                        {
                            position = "intt";
                        }
                        else if (currentOffset == charOffset)
                        {
                            position = "chart";
                        }
                        else
                        {
                            position = "floatt";
                        }

                        //increase sym tab offset += local current offset
                        totalOffset += currentOffset;



                        increments++;
                        switch (Lexie.MatchTokens[increments])
                        {
                            case "idt":
                                {
                                    //lookup for dups and insert
                                    val = SymbolTable.lookUp(Lexie.LexemeString[increments]);
                                    if (val.lexeme != Lexie.LexemeString[increments])
                                    {
                                        //insert after storing the value
                                        SymbolTable.insert(Lexie.LexemeString[increments], position, depth, SymbolTable.RecordEnum.Variable);
                                    }

                                    else if (val.lexeme == Lexie.LexemeString[increments] && val.depth != depth)
                                    {
                                        SymbolTable.insert(Lexie.LexemeString[increments], position, depth, SymbolTable.RecordEnum.Variable);
                                    }


                                    else if (val.lexeme == Lexie.LexemeString[increments] && val.depth == depth)
                                    {
                                        Console.WriteLine("Error: " + val.lexeme + " was found when searching for duplicates. The depth found at:" + depth);
                                        Environment.Exit(1);
                                        break;

                                    }

                                    increments++;
                                    IDTail();
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'idt'. Not correct Grammar.");
                                    Environment.Exit(1);
                                    break;
                                }
                        }
                        break;
                    }
                case "semit":
                    {
                        break;

                    }
                default:
                    {
                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'semit'. Not correct Grammar.");
                        Environment.Exit(1);
                        break;
                    }
            }
        }

        public static void StatList()
        {
            Statement();
            increments++;
            switch (Lexie.LexemeString[increments])
            {
                case "semit":
                    {
                        StatList();
                        break;
                    }
                case "eoftt":
                    break;
                default:
                    {
                        Console.WriteLine("Error: " + Lexie.MatchTokens[increments] + " was found when searching for 'semit'. Not correct Grammar.");
                        Environment.Exit(1);
                        break;
                        break;
                    }
            }
        }

        public static void Statement()
        {
            switch (Lexie.LexemeString[increments])
            {
                case "assignopt":
                    {
                        AssignStat();
                        break;
                    }
                default:
                    {
                        IOStat();
                        break;
                    }
            }
        }

        public static void AssignStat()
        {
            increments++;

            switch (Lexie.LexemeString[increments]) {
                case "idt": {

                        increments++;
                        switch (Lexie.LexemeString[increments])
                        {
                            case "assignopt":
                                {
                                    increments++;
                                    Expr();
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public static void IOStat()
        {
            //do nothing
        }

        public static void Expr()
        {
            Relation();
        }

        public static void Relation()
        {
            SimpleExpr();
        }

        public static void SimpleExpr()
        {
            SignOp();
            Term();
            MoreTerm();
        }

        public static void MoreTerm()
        {
            Addop();
            Term();
            MoreTerm();
        }

        public static void Term()
        {
            Factor();
            MoreFactor();
        }

        public static void MoreFactor()
        {
            Mulop();
            Factor();
            MoreFactor();
        }

        public static void Factor()
        {
            increments++;

            switch (Lexie.LexemeString[increments])
            {
                case "idt":
                    {   
                        break;
                    }
                case "numt":
                    {
                        break;
                    }
                case "lparent":
                    {
                        Expr();
                        increments++;
                        switch (Lexie.LexemeString[increments])
                        {
                            case "rparent":
                                {
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public static void Addop()
        {

        }

        public static void Mulop()
        {

        }

        public static void SignOp()
        {

        }
    }
}
