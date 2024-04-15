using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Automation;

namespace calcUI
{
    internal class CalculatorService
    {
        private AutomationElement calculatorWindow;
        private Process calculatorProcess;

        public void StartCalculator()
        {
            //Process.Start("calc.exe");
            System.Diagnostics.Process.Start("calc.exe");

            // Wait for Calculator to open
            System.Threading.Thread.Sleep(2000);

            // Get all Calculator processes
            Process[] calcProcesses = Process.GetProcessesByName("Calculator");


            // Check if any processes were found
            if (calcProcesses.Length > 0)
            {
                // Get the first process and set the calculatorWindow
                calculatorWindow = AutomationElement.FromHandle(Process.GetProcessesByName("Calculator")[0].MainWindowHandle);
            }
            else
            {
                throw new Exception("Calculator process not found.");
            }

            
        }

        public void CloseCalculator()
        {
            if (calculatorWindow != null)
            {
                // Close the Calculator application
                Process[] calcProcesses = Process.GetProcessesByName("Calculator");
                foreach (Process calcProcess in calcProcesses)
                {
                    calcProcess.CloseMainWindow();
                }
                calculatorWindow = null;
            }
        }

        /// Example method to perform addition in the Calculator
        public int Add(int operand1, int operand2)
        {
            if (calculatorWindow == null)
            {
                throw new InvalidOperationException("Calculator is not started.");
            }

            // Find and click on the buttons for the operands and addition
            ClickButton(GetButton(operand1.ToString()));
            ClickButton(GetButton("Add"));
            ClickButton(GetButton(operand2.ToString()));

            // Click the equals button
            ClickButton(GetButton("Equals"));

            // Get the result
            string result = GetResult();

            return int.Parse(result);
        }

        private AutomationElement GetButton(string name)
        {
            Condition condition = new PropertyCondition(AutomationElement.NameProperty, name);
            return calculatorWindow.FindFirst(TreeScope.Descendants, condition);
        }

        private void ClickButton(AutomationElement button)
        {
            if (button != null)
            {
                InvokePattern invokePattern = button.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                invokePattern?.Invoke();
            }
            else
            {
                throw new Exception("Button not found.");
            }
        }

        private string GetResult()
        {
            AutomationElement resultTextBox = calculatorWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "CalculatorResults"));
            return ((ValuePattern)resultTextBox.GetCurrentPattern(ValuePattern.Pattern)).Current.ToString();
        }

        
    }
}
