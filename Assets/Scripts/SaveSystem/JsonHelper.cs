using UnityEngine;
using System;


//Данный скрипт позволяет нам кодировать и декодировать в строку JSON классы.
/*
    //Он позволяет переводить в строку простые типы, а именно:
    //Числовые, Строковые, Булевые, Индексированные одномерные массивы, Вектора, цвет в формате RGBA, кватерионы, Листы
    //Данные переменные являются демонстрационными, их задача показать как выглядит соответствующий переводимый тип
    public int Test_Int = 0;
    public float Test_Float = 1.2f;
	... И ДР ЧИСЛОВЫЕ ТИПЫ
	
    public string Test_String = "TEST STRING";

    public bool Test_Bool = false;

    public int[] Test_Int_Array = new int[Длинна(int)];

    public Vector2 Test_Vector2 = new Vector3(1f, 2f);
    public Vector3 Test_Vector3 = new Vector3(1f,2f,3f);
    public Vector4 Test_Vector4 = new Vector4(1f, 2f, 3f, 4f);

    public Color Reds = new Color(1f, 0f, 0f, 1f);
    public Quaternion Quate = new Quaternion();

    public List<string> Test_List = new List<string>();
    Все эти простые типы данных могут содержаться в сериализуемом классе и быть переведены в строку по правилам JSON
    */

//параметр prettyPrint: 1 - отформатировать вывод для удобства чтения, 0 - вывод равен одной длинной строке (стоит по умолчанию)
public static class JsonHelper
{
    //Преобразует строку, записанную по правилм JSON, в выходной объект заданного класса (Т)
    public static T[] FromJson<T>(string json)
    {
        try
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
        catch(Exception error)
        {
            //Если входная строка не соответствует стандарту возвращаем null
            Debug.Log("FromJson error = " + error);
            return null;
        }
    }

    //Преобразует принятый класс в строку по правилам JSON
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    //Тоже, но второй параметр prettyPrint - определяет будут ли добавлены в строку символвы переноса строки для придания ей удобочитаемого вида
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    //Вспомогательный класс
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
