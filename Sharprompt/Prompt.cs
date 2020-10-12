using System;
using System.Collections.Generic;

using Sharprompt.Forms;
using Sharprompt.Internal;
using Sharprompt.Validations;

namespace Sharprompt
{
  public static class Prompt
  {
    public static int Menu(string header, IEnumerable<string> items)
    {
      List<(int, string)> itemsReales = new List<(int, string)>();
      int idx = 0;

      foreach (string item in items)
      {
        itemsReales.Add((idx++, item));
      }

      var form = Select<(int, string)>(header, itemsReales, null, null,
        tuple => $"[{tuple.Item1:00}] .... {tuple.Item2}");

      return 0;
    }

    public static T Input<T>(string message, object defaultValue = null,
        IList<Func<object, ValidationResult>> validators = null)
    {
      using var form = new Input<T>(message, defaultValue, validators);

      return form.Start();
    }

    public static string Password(string message, IList<Func<object, ValidationResult>> validators = null)
    {
      using var form = new Password(message, validators);

      return form.Start();
    }

    public static bool Confirm(string message, bool? defaultValue = null)
    {
      using var form = new Confirm(message, defaultValue);

      return form.Start();
    }

    /// <summary>
    /// Permite seleccionar un valor a partir de un enum determindado
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="pageSize"></param>
    /// <param name="defaultValue"></param>
    /// <param name="valueSelector"></param>
    /// <returns></returns>
    public static T Select<T>(string message, int? pageSize = null, T? defaultValue = null,
        Func<T, string> valueSelector = null) where T : struct, Enum
    {
      var items = (T[])Enum.GetValues(typeof(T));

      using var form = new Select<T>(message, items, pageSize, defaultValue,
          valueSelector ?? (x => x.GetDisplayName()));

      return form.Start();
    }

    /// <summary>
    /// Retorna el elemento que se selecciona entre una enumeracion de los mismos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="items"></param>
    /// <param name="pageSize"></param>
    /// <param name="defaultValue"></param>
    /// <param name="valueSelector"></param>
    /// <returns></returns>
    public static T Select<T>(string message, IEnumerable<T> items, int? pageSize = null,
        object defaultValue = null, Func<T, string> valueSelector = null)
    {
      using var form = new Select<T>(message, items, pageSize, defaultValue,
          valueSelector ?? (x => x.ToString()));

      return form.Start();
    }

    public static IEnumerable<T> MultiSelect<T>(string message, int? pageSize = null, int minimum = 1,
        int maximum = -1, Func<T, string> valueSelector = null) where T : struct, Enum
    {
      var items = (T[])Enum.GetValues(typeof(T));

      using var form = new MultiSelect<T>(message, items, pageSize, minimum, maximum,
          valueSelector ?? (x => x.GetDisplayName()));

      return form.Start();
    }

    public static IEnumerable<T> MultiSelect<T>(string message, IEnumerable<T> items, int? pageSize = null,
        int minimum = 1, int maximum = -1, Func<T, string> valueSelector = null)
    {
      using var form = new MultiSelect<T>(message, items, pageSize, minimum, maximum,
          valueSelector ?? (x => x.ToString()));

      return form.Start();
    }

    public static class ColorSchema
    {
      public static ConsoleColor Answer { get; set; } = ConsoleColor.Cyan;
      public static ConsoleColor Select { get; set; } = ConsoleColor.Green;
      public static ConsoleColor DisabledOption { get; set; } = ConsoleColor.DarkCyan;
    }
  }
}
