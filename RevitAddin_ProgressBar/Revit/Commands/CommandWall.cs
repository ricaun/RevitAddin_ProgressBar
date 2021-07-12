using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAddin_ProgressBar.Views;
using System.Linq;

namespace RevitAddin_ProgressBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;
            Selection selection = uidoc.Selection;

            var walls = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .ToList();

            var count = walls.Count;

            using (var progressBarView = new ProgressBar2View($"Wall... {count}"))
            {
                using (TransactionGroup transactionGroup = new TransactionGroup(document))
                {
                    transactionGroup.Start("Walls Change Comments");

                    progressBarView.Run(walls, (wall) =>
                    {
                        using (Transaction transaction = new Transaction(document))
                        {
                            transaction.Start("Wall Change Comments");
                            var parameter = wall.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
                            parameter.Set("Comments 2");
                            transaction.Commit();
                        }
                    });

                    if (progressBarView.IsClosed)
                        transactionGroup.RollBack();
                    else
                        transactionGroup.Assimilate();
                }
            }

            return Result.Succeeded;
        }
    }
}