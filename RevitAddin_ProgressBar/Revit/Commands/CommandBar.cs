using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin_ProgressBar.Views;

namespace RevitAddin_ProgressBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandBar : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;

            using (var progressBar2View = new ProgressBar2View("Wall2..."))
            {
                var number = 100000;
                progressBar2View.Run($"{number}", number, (i) =>
                {

                });
                progressBar2View.Run($"{number * 2}", number * 2, (i) =>
                {

                });

                if (progressBar2View.IsClosed)
                {
                    System.Console.WriteLine("IsClosed");
                }
            }

            return Result.Succeeded;
        }
    }
}