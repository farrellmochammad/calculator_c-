using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;

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

        public bool isDoubleOperator(string x)
        {
            bool stat = false;
            for (int i=0; i<=x.Length-2; i++)
            {
                if (isOperator(x[i]) & isOperator(x[i+1]))
                {
                    stat = true;
                }
            }

            return stat;
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
            char[] arrIdxOperations = new char[] { };
            int idx = 0;


            for (int i = 0; i <= text.Length - 1; i++)
            {
                if (stack.Length != 0)
                {
                    arrbrackets[idx] += text[i];
                } else if (stack.Length == 0)
                {
                    arrbrackets[0] += text[i];
                }

                if (text[i] == '(')
                {
                    stack = stack.Concat(new int[] { 1 }).ToArray();
                    idx += 1;
                    arrbrackets = arrbrackets.Concat(new String[] { "" }).ToArray();

                    if (i != 0)
                    {
                        arrIdxOperations = arrIdxOperations.Concat(new char[] { text[i - 1] }).ToArray();
                    }

                }
                else if (text[i] == ')')
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

            string[] charsToRemove = { "(", ")" };
            string[] removeNoise = new string[] { };
            string[] arrSummaries = new string[] { };
            string[] arrtemp = new string[] { };
            bool frag = false;
            int countSum = 0;

            for (int i = 0; i <= arrbrackets.Length - 1; i++)
            {

                if (i != 0)
                {
                    foreach (var c in charsToRemove)
                    {
                        arrbrackets[i] = arrbrackets[i].Replace(c, string.Empty);
                    }
                }
            }

            for (int i = 1; i <= arrbrackets.Length - 1; i++)
            {
                if (isOperator(arrbrackets[i][arrbrackets[i].Length - 1]) && !frag)
                {
                    frag = true;
                    arrtemp = arrtemp.Concat(new string[] { arrbrackets[i] }).ToArray();
                }
                else if (frag)
                {
                    if (!isOperator(arrbrackets[i][arrbrackets[i].Length - 1]))
                    {
                        arrtemp = arrtemp.Concat(new string[] { arrbrackets[i] }).ToArray();
                        arrSummaries = arrSummaries.Concat(new string[] { "x" }).ToArray();
                        frag = false;
                    }
                }
                else
                {
                    float[] results = Regex.Split(arrbrackets[i], @"-|\+|\*|\/").Select(x => Convert.ToSingle(x)).ToArray();

                    string[] operations = Regex.Split(arrbrackets[i], @"[0-9]+").ToArray();

                    arrSummaries = arrSummaries.Concat(new string[] { countValue(operations, results).ToString() }).ToArray();
                }
           }


            string[] arrtempSum = new string[] { };
            if (arrtemp.Length != 0)
            {
                string st = "";
                for (int i=arrtemp.Length-1; i>=0; i--)
                {
                    if (!isOperator(arrtemp[i][arrtemp[i].Length - 1]))
                    {
                        if (st!="")
                        {
                            arrtempSum = arrtempSum.Concat(new string[] {st}).ToArray();
                        }

                        st = "";

                        float[] results = Regex.Split(arrtemp[i], @"-|\+|\*|\/").Select(x => Convert.ToSingle(x)).ToArray();

                        string[] operations = Regex.Split(arrtemp[i], @"[0-9]+").ToArray();

                        st += countValue(operations, results).ToString();
                    }
                    else
                    {
                        st = arrtemp[i] + st;

                        float[] results = Regex.Split(st, @"-|\+|\*|\/").Select(x => Convert.ToSingle(x)).ToArray();

                        string[] operations = Regex.Split(st, @"[0-9]+").ToArray();

                        st = countValue(operations, results).ToString();

                        if (i==0)
                        {
                            arrtempSum = arrtempSum.Concat(new string[] { st }).ToArray();
                        }
                    }
                }
            }


            int countSumTemp = 0;
            for (int i=0; i<=arrSummaries.Length-1; i++)
            {
                if (arrSummaries[i] == "x")
                {
                    var theSummaryTemp = arrSummaries[i];
                    var aStringBuilderTemp = new StringBuilder(theSummaryTemp);

                    aStringBuilderTemp.Remove(i, 1);
                    aStringBuilderTemp.Insert(i, arrtempSum[countSumTemp]);
                    countSumTemp += 1;
                    arrSummaries[i] = aStringBuilderTemp.ToString();
                }
            }


            var theSummary = arrbrackets[0];
            var aStringBuilder = new StringBuilder(theSummary);

            for (int i=0; i<= aStringBuilder.Length-1; i++)
            {
                if (aStringBuilder[i] == '(')
                {
                    aStringBuilder.Remove(i, 1);
                    aStringBuilder.Insert(i, arrSummaries[countSum]);
                    countSum += 1;
                }
            }

            float[] resultsSum = Regex.Split(aStringBuilder.ToString(), @"-|\+|\*|\/").Select(x => Convert.ToSingle(x)).ToArray();

            string[] operationsSum = Regex.Split(aStringBuilder.ToString(), @"[0-9]+").ToArray();

            result.Text = countValue(operationsSum, resultsSum).ToString();


            /*int[] indexes = new int[] { };
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


            for (int i = 0; i<=removeNoise.Length-1; i++)
            {
                if (isDoubleOperator(removeNoise[i]))
                {
                    removeNoise[i - 1] += removeNoise[i][removeNoise[i].Length - 1].ToString();
                    removeNoise[i] = removeNoise[i].Remove(removeNoise[i].Length - 1);
                }
            }


            if (isOperator(removeNoise[removeNoise.Length - 1][removeNoise[removeNoise.Length - 1].Length - 1]))
            {
                removeNoise[0] += removeNoise[removeNoise.Length - 1][removeNoise[removeNoise.Length - 1].Length - 1].ToString();
                removeNoise[removeNoise.Length - 1] = removeNoise[removeNoise.Length - 1].Replace(removeNoise[removeNoise.Length - 1][removeNoise[removeNoise.Length - 1].Length - 1],' ');
            }

            foreach (var st in removeNoise)
            {
                System.Diagnostics.Debug.WriteLine("st" + st);
            }

            float summary = 0;
            for (int i = removeNoise.Length-1; i >= 0; i--)
            {
                if (i != removeNoise.Length - 1)
                {
                    removeNoise[i] += summary.ToString();
                    System.Diagnostics.Debug.WriteLine("st remove noises" + removeNoise[i]);
                }

                float[] results = Regex.Split(removeNoise[i], @"-|\+|\*|\/").Select(x => Convert.ToSingle(x)).ToArray();

                string[] operations = Regex.Split(removeNoise[i], @"[0-9]+").ToArray();

                summary = countValue(operations, results);

            }

            result.Text = summary.ToString();*/


        }

    }
}


