using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIReadingTypeParameters
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            //выбираем объект преобразовываем в элемент
            var selectedRef = uidoc.Selection.PickObject(ObjectType.Element, "Выберите элемент");
            var selectedElement = doc.GetElement(selectedRef);

            //проверяем если выбранный элемент является экземпляром семейства, то продолжаем далее
            if (selectedElement is FamilyInstance)
            {
                var familyInstance = selectedElement as FamilyInstance;//преобразовываем

                //считываем параметр и обращаемся к типу выбранного семейства через Symbol
                Parameter widthParameter1 = familyInstance.Symbol.LookupParameter("Ширина");
                //выводим значение ширины
                TaskDialog.Show("width info", widthParameter1.AsDouble().ToString());

                //создаем еще одну переменную, которая работает уже со встроенным параметром
                Parameter widthParameter2 = familyInstance.Symbol.get_Parameter(BuiltInParameter.CASEWORK_WIDTH);
                TaskDialog.Show("width info", widthParameter2.AsDouble().ToString());


            }

           
            return Result.Succeeded;
        }
    }
}
