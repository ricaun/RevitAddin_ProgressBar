using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin_ProgressBar.Revit
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}