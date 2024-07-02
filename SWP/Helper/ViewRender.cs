using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace SWP.Helper
{
    public static class ViewRenderer
    {
        public static async Task<string> RenderPartialViewToStringAsync(Controller controller, string viewName, object model, ICompositeViewEngine viewEngine, IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"View {viewName} not found");
                }

                var viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
