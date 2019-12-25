using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public partial class Calculator : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Page Load");
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (result.Text == "0")
            {
                result.Text = b.Text;
            }
            else if (b.Text == "(")
            {
                if (isOperator(result.Text[result.Text.Length - 1]))
                {
                    result.Text += b.Text;
                }
            }
            else if (b.Text == ")")
            {
                if (!isOperator(result.Text[result.Text.Length - 1]))
                {
                    result.Text += b.Text;
                }
            }
            else
            {
                result.Text += b.Text;
            }
        }

        protected void Operator_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            char lastNum = result.Text[result.Text.Length - 1];

            if (result.Text != "0" & isOperator(lastNum) != true)
            {
                if (b.Text == "x")
                {
                    result.Text += "*";
                }
                else
                {
                    result.Text += b.Text;
                }
            }
        }

        protected void Equal_Click(object sender, EventArgs e)
        {
            char lastNum = result.Text[result.Text.Length - 1];

            if (isOperator(lastNum) != true)
            {
                detectBracket(result.Text);
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            result.Text = "0";
        }

        public float countValue(string[] arrOperation, float[] arrResult)
        {
            for (int i = 0; i < arrOperation.Length; i++)
            {
                if (arrOperation[i] == "")
                {
                    arrOperation = arrOperation.Where(w => w != arrOperation[i]).ToArray();
                }
            }


            while (countMulDiv(arrOperation) != 0)
            {
                for (int i = 0; i < arrOperation.Length; i++)
                {
                    if (arrOperation[i] == "*")
                    {
                        arrResult[i] = arrResult[i] * arrResult[i + 1];
                        arrResult = arrResult.Where((source, index) => index != i + 1).ToArray();
                        arrOperation = arrOperation.Where((source, index) => index != i).ToArray();
                    }
                    else if (arrOperation[i] == "/")
                    {
                        arrResult[i] = arrResult[i] / arrResult[i + 1];
                        arrResult = arrResult.Where((source, index) => index != i + 1).ToArray();
                        arrOperation = arrOperation.Where((source, index) => index != i).ToArray();
                    }
                }
            }

            if (arrResult.Length == 1)
            {
                return arrResult[0];
            }

            while (countPlusMin(arrOperation) != 0)
            {
                for (int i = 0; i < arrOperation.Length; i++)
                {
                    if (arrOperation[i] == "+")
                    {
                        arrResult[i] = arrResult[i] + arrResult[i + 1];
                        arrResult = arrResult.Where((source, index) => index != i + 1).ToArray();
                        arrOperation = arrOperation.Where((source, index) => index != i).ToArray();
                    }
                    else if (arrOperation[i] == "-")
                    {
                        arrResult[i] = arrResult[i] - arrResult[i + 1];
                        arrResult = arrResult.Where((source, index) => index != i + 1).ToArray();
                        arrOperation = arrOperation.Where((source, index) => index != i).ToArray();
                    }
                }
            }

            return arrResult[0];
        }

        public bool isOperator(char x)
        {
            if (x == '+' | x == '-' | x == '*' | x == '/')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int countMulDiv(string[] arrOperation)
        {
            int count = 0;
            foreach (var operation in arrOperation)
            {
                if (operation == "*" | operation == "/")
                {
                    count += 1;
                }
            }

            return count;

        }

        public int countPlusMin(string[] arrOperation)
        {
            int count = 0;
            foreach (var operation in arrOperation)
            {
                if (operation == "+" | operation == "-")
                {
                    count += 1;
                }
            }

            return count;

        }

        public void detectBracket(string text)
        {
            int[] stack = new int[] { };
            bool isBracket = false;
            foreach (var tx in text)
            {
                if (tx == '(')
                {
                    stack = stack.Concat(new int[] { 1 }).ToArray();
                    isBracket = true;

                }
                else if (tx == ')')
                {
                    isBracket = true;
                    if (stack.Length == 0)
                    {
                        stack = stack.Concat(new int[] { 1 }).ToArray();
                    }
                    else
                    {
                        stack = stack.Where((source, index) => index != stack.Length - 1).ToArray();
                    }
                }
            }

            if (stack.Length != 0 && isBracket)
            {
                result.Text = "Error";

            } else if (isBracket)
            {
                fragBracket(result.Text);
            }
            else
            {
                float[] results = Regex.Split(result.Text, @"-|\+|\*|\/").Select(x => Convert.ToSingle(x)).ToArray();

                string[] operations = Regex.Split(result.Text, @"[0-9]+").ToArray();

                result.Text = countValue(operations, results).ToString();
            }
        }

        public void fragBracket(string text)
        {
            int[] stack = new int[] { };
            string[] arrbrackets = new string[] { "" };
            int idx = 0;


            foreach (var tx in text)
            {
                if (stack.Length != 0)
                {
                    arrbrackets[idx] += tx;
                } else if (stack.Length == 0)
                {
                    arrbrackets[0] += tx;
                }

                if (tx == '(')
                {
                    stack = stack.Concat(new int[] { 1 }).ToArray();
                    idx += 1;
                    arrbrackets = arrbrackets.Concat(new String[] { "" }).ToArray();
                }
                else if (tx == ')')
                {
                    if (stack.Length == 0)
                    {
                        stack = stack.Concat(new int[] { 1 }).ToArray();
                    }
                    else
                    {
                        stack = stack.Where((source, index) => index != stack.Length - 1).ToArray();
                    }
                }
            }

            char[] charsToRemove = { '(', ')' };
            string[] removeNoise = new string[] { };
            string temp = "";

            foreach (var st in arrbrackets)
            {
                removeNoise = removeNoise.Concat(new String[] { st.TrimEnd(charsToRemove) }).ToArray();
            }


            int[] indexes = new int[] { };
            char[] operationsindex = new char[] { };
            for (int i = 0; i <= removeNoise.Length-1; i++)
            {
                for (int j=0; j<=removeNoise[i].Length-1; j++)
                {
                    if (removeNoise[i][j] == '(')
                    {
                        if (removeNoise[i][j+1] == '*' | removeNoise[i][j+1] == '/')
                        {
                            removeNoise[i] = removeNoise[i].Replace('(', '1');
                        }
                        else
                        {
                            removeNoise[i] = removeNoise[i].Replace('(', '0');
                        }
                        indexes = indexes.Concat(new int[] { i + 1 }).ToArray();
                        operationsindex = operationsindex.Concat(new char[] { (removeNoise[i][j+1]) }).ToArray();
                    }
                }
            }

            if (indexes.Length != 0)
            {
                for (int i=0; i<=indexes.Length-1; i++)
                {
                    removeNoise[indexes[i]] += operationsindex[i];
                }
            }
            

            float summary = 0;
            for (int i = removeNoise.Length-1; i >= 0; i--)
            {
                if (i != removeNoise.Length - 1)
                {
                    removeNoise[i] += summary.ToString();
                }

                float[] results = Regex.Split(removeNoise[i], @"-|\+|\*|\/").Select(x => Convert.ToSingle(x)).ToArray();

                string[] operations = Regex.Split(removeNoise[i], @"[0-9]+").ToArray();

                summary = countValue(operations, results);

            }

            result.Text = summary.ToString();


        }
    }
}


