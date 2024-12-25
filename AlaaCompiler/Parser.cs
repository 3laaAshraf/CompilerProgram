using System;
using System.Collections.Generic;

public class Parser
{
    private readonly List<(string TokenType, string Value)> tokens;
    private int currentIndex = 0;
    private List<string> DoneRules;

    public Parser(List<(string TokenType, string Value)> tokens)
    {
        this.tokens = tokens;
        this.DoneRules = new List<string>();
    }

    private (string TokenType, string Value) CurrentToken => currentIndex < tokens.Count ? tokens[currentIndex] : ("EOF", "");

   

    private void Consume(string expectedTokenType)
    {
        if (CurrentToken.TokenType == expectedTokenType)
        {
            currentIndex++;
        }
        else
        {
            throw new Exception($"Expected {expectedTokenType} but found {CurrentToken.TokenType} ('{CurrentToken.Value}').");
        }
    }

    public string ParseProgram()
    {
        
        try
        {
            DoneRules.Add("Parsing Successful! \nParsing Details : \n");
            while (CurrentToken.TokenType != "EOF")
            {
                ParseStatement();
            }
            // return "Code is syntactically valid.";
            return string.Join("\n",   DoneRules);
        }
        catch (Exception ex)
        {
            return $"Syntax Error: {ex.Message}";
        }
    }

    private void ParseStatement()
    {
        if (CurrentToken.TokenType == "KEYWORD" && CurrentToken.Value == "رقم")
        {
            ParseDeclaration();
        }
        else if (CurrentToken.TokenType == "IDENTFIER")
        {
            ParseAssignment();
        }
        else if (CurrentToken.TokenType == "KEYWORD" && CurrentToken.Value == "لو")
        {
            ParseConditional();
        }
        else if (CurrentToken.TokenType == "KEYWORD" && CurrentToken.Value == "طالما")
        {
            ParseWhileLoop();
        }
        else
        {
            throw new Exception($"Unexpected statement starting with {CurrentToken.TokenType} ('{CurrentToken.Value}').");
        }
    }
    private void ParseDeclaration()
    {
        Consume("KEYWORD"); // "رقم"
        string identifier = CurrentToken.Value;
        Consume("IDENTFIER");
        string assign = CurrentToken.Value;
        Consume("ASSIGN");
        string a = CurrentToken.Value;
        ParseExpression(4);

        Consume("SEMICOLON");
       // DoneRules.Add($" Declaration Rule : رقم {identifier} {assign} {a} ");
        DoneRules.Add($" Declaration Rule : رقم {identifier} {assign} {declareString}");
        declareString = "";
        
    }
    private void ParseAssignment()
    {
        string a0 = CurrentToken.Value;

        Consume("IDENTFIER");
        string a1 = CurrentToken.Value;

        Consume("ASSIGN");
        string a2 = CurrentToken.Value;

        ParseExpression(3);
        string a3 = CurrentToken.Value;

        Consume("SEMICOLON");
        string a4 = CurrentToken.Value;

        DoneRules.Add($" Assigen :  {a0} {a1} {assigString}");

    }

    private void ParseConditional()
    {
        Consume("KEYWORD"); // "لو"
        Consume("LPAREN");
        string assig = CurrentToken.Value;
        ParseExpression(1);
        string assi = CurrentToken.Value;

        Consume("RPAREN");
        Consume("LBRACE");
        string assign="";
        while (CurrentToken.TokenType != "RBRACE" && CurrentToken.TokenType != "EOF")
        {
            ParseStatement();
             assign = CurrentToken.Value;
        }
        Consume("RBRACE");
        DoneRules.Add($" If Statement Rule : لو ( {ifString} )");


    }
    public string ifString = "";
    public string whileString = "";
    public string assigString = "";
    public string declareString = "";
    private void ParseExpression(int n=0)
    {
        string a1 = CurrentToken.Value;
        ParseTerm();
        string a3 = "";
        string a4 = "";
        string a2 = CurrentToken.Value;
        string a5 = "";
        while (CurrentToken.TokenType == "OPERATOR")
        {
            a5 = CurrentToken.Value;
            Consume("OPERATOR");

            a3 = CurrentToken.Value;

            ParseTerm();
            a4 = CurrentToken.Value;
        }
        if (n == 1) {
           // DoneRules.Add($" {a1} {a2} {a3}");
            ifString = $" {a1} {a2} {a3} ";
        }
        if (n == 2) {
            whileString = $" {a1} {a2} {a3} ";
        }
        if (n == 3) {
            assigString = $" {a1} {a2} {a3} ";
        }
        if (n == 4){
            declareString = $" {a1} {a2} {a3} ";
        }
        
    }
    private void ParseWhileLoop()
    {
        Consume("KEYWORD"); // "طالما"
        Consume("LPAREN");
        ParseExpression(2);
        Consume("RPAREN");
        Consume("LBRACE");
        while (CurrentToken.TokenType != "RBRACE" && CurrentToken.TokenType != "EOF")
        {
            ParseStatement();
        }
        Consume("RBRACE");
        DoneRules.Add($" While Statement Rule : طالما ( {whileString} )");

    }
    private void ParseTerm()
    {
        if (CurrentToken.TokenType == "NUMBER" || CurrentToken.TokenType == "IDENTFIER")
        {
            Consume(CurrentToken.TokenType);
        }
        else
        {
            throw new Exception($"Expected NUMBER or IDENTFIER but found {CurrentToken.TokenType} ('{CurrentToken.Value}').");
        }
    }

    
}









//using System;
//using System.Collections.Generic;

//public class Parser
//{
//    private readonly List<(string TokenType, string Value)> tokens;
//    private int currentIndex = 0;
//    private List<string> DoneRules;

//    public Parser(List<(string TokenType, string Value)> tokens)
//    {
//        this.tokens = tokens;
//        this.DoneRules = new List<string>();
//    }

//    private (string TokenType, string Value) CurrentToken => currentIndex < tokens.Count ? tokens[currentIndex] : ("EOF", "");



//    private void Consume(string expectedTokenType)
//    {
//        if (CurrentToken.TokenType == expectedTokenType)
//        {
//            currentIndex++;
//        }
//        else
//        {
//            throw new Exception($"Expected {expectedTokenType} but found {CurrentToken.TokenType} ('{CurrentToken.Value}').");
//        }
//    }

//    public string ParseProgram()
//    {
//        try
//        {
//            while (CurrentToken.TokenType != "EOF")
//            {
//                ParseStatement();
//            }
//            // return "Code is syntactically valid.";
//            return string.Join("\n", DoneRules);
//        }
//        catch (Exception ex)
//        {
//            return $"Syntax Error: {ex.Message}";
//        }
//    }

//    private void ParseStatement()
//    {
//        if (CurrentToken.TokenType == "KEYWORD" && CurrentToken.Value == "رقم")
//        {
//            ParseDeclaration();
//        }
//        else if (CurrentToken.TokenType == "IDENTFIER")
//        {
//            ParseAssignment();
//        }
//        else if (CurrentToken.TokenType == "KEYWORD" && CurrentToken.Value == "لو")
//        {
//            ParseConditional();
//        }
//        else if (CurrentToken.TokenType == "KEYWORD" && CurrentToken.Value == "طالما")
//        {
//            ParseWhileLoop();
//        }
//        else
//        {
//            throw new Exception($"Unexpected statement starting with {CurrentToken.TokenType} ('{CurrentToken.Value}').");
//        }
//    }
//    private void ParseDeclaration()
//    {
//        Consume("KEYWORD"); // "رقم"
//        string identifier = CurrentToken.Value;
//        Consume("IDENTFIER");
//        string assign = CurrentToken.Value;
//        Consume("ASSIGN");
//        string a = CurrentToken.Value;
//        ParseExpression();
//        Consume("SEMICOLON");
//        DoneRules.Add($" Declaration Rule : رقم {identifier} {assign} {a} ");

//    }
//    private void ParseAssignment()
//    {
//        Consume("IDENTFIER");
//        string a1 = CurrentToken.Value;

//        Consume("ASSIGN");
//        string a2 = CurrentToken.Value;

//        ParseExpression();
//        string a3 = CurrentToken.Value;

//        Consume("SEMICOLON");
//        string a4 = CurrentToken.Value;

//        DoneRules.Add($" Assigen :  {a1} {a2} {a3} {a4}");

//    }

//    private void ParseConditional()
//    {
//        Consume("KEYWORD"); // "لو"
//        Consume("LPAREN");
//        string assig = CurrentToken.Value;
//        ParseExpression(1);
//        string assi = CurrentToken.Value;

//        Consume("RPAREN");
//        Consume("LBRACE");
//        string assign = "";
//        while (CurrentToken.TokenType != "RBRACE" && CurrentToken.TokenType != "EOF")
//        {
//            ParseStatement();
//            assign = CurrentToken.Value;
//        }
//        Consume("RBRACE");
//        DoneRules.Add($" If Statement Rule : لو ( {ifString} )");


//    }
//    public string ifString = "";
//    public string whileString = "";
//    private void ParseExpression(int n = 0)
//    {
//        string a1 = CurrentToken.Value;
//        ParseTerm();
//        string a3 = "";
//        string a4 = "";
//        string a2 = CurrentToken.Value;

//        while (CurrentToken.TokenType == "OPERATOR")
//        {
//            Consume("OPERATOR");
//            a3 = CurrentToken.Value;

//            ParseTerm();
//            a4 = CurrentToken.Value;
//        }
//        if (n == 1)
//        {
//            // DoneRules.Add($" {a1} {a2} {a3}");
//            ifString = $" {a1} {a2} {a3}";
//        }
//        if (n == 2)
//        {
//            whileString = $" {a1} {a2} {a3}";
//        }
//    }
//    private void ParseWhileLoop()
//    {
//        Consume("KEYWORD"); // "طالما"
//        Consume("LPAREN");
//        ParseExpression(2);
//        Consume("RPAREN");
//        Consume("LBRACE");
//        while (CurrentToken.TokenType != "RBRACE" && CurrentToken.TokenType != "EOF")
//        {
//            ParseStatement();
//        }
//        Consume("RBRACE");
//        DoneRules.Add($" While Statement Rule : طالما ( {whileString} )");

//    }
//    private void ParseTerm()
//    {
//        if (CurrentToken.TokenType == "NUMBER" || CurrentToken.TokenType == "IDENTFIER")
//        {
//            Consume(CurrentToken.TokenType);
//        }
//        else
//        {
//            throw new Exception($"Expected NUMBER or IDENTFIER but found {CurrentToken.TokenType} ('{CurrentToken.Value}').");
//        }
//    }
//}
