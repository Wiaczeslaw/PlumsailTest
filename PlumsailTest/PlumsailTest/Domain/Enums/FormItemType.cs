using System.ComponentModel;

namespace PlumsailTest.Domain.Enums
{
    public enum FormItemType
    {
        [Description("Числовое поле")]
        Numeric = 0,
        [Description("Выпадающий список")]
        Select = 1,
        [Description("Галочки")] 
        Check = 2,
        [Description("Текстовое поле")]
        String = 4,
        [Description("Многострочное текстовое поле")]
        StringList = 5,
        [Description("Выпадающий список с множественным выбором")] 
        Radio = 6,
        [Description("Дата")] 
        Date = 7
    }
}