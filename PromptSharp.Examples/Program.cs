using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Figgle;
using PromptSharp.Validations;

namespace PromptSharp.Examples
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.OutputEncoding = Encoding.UTF8;

      Header("Test ET Prompt#", "Menu de prueba para Prompt#");
      Footer("Basada en el trabajo de shibayan");

      RunMenuSample();

      Console.ReadLine();

/*
      RunInputSample();

      RunSelectSample();

      RunPasswordSample();

      RunConfirmSample();

      RunMultiSelectSample();

      RunSelectEnumSample();
*/
    }

    private static void RunMenuSample()
    {
      var resultado = Prompt.Menu("Seleccionar una opcion", new[]
      {
        "Importar libros",
        "Importar autores"
      });

      Console.WriteLine($"Se selecciono la opcion: {resultado}");
    }

    private static void RunInputSample()
    {
      string name = Prompt.Input<string>("What's your name?", validators: new[] {Validators.Required()});

      Console.WriteLine($"Hello, {name}!");
    }

    private static void RunSelectSample()
    {
      var city = Prompt.Select("Select your city",
        new[] {"Seattle", "London", "Tokyo", "New York", "Singapore", "Shanghai"}, pageSize: 3);
      Console.WriteLine($"Hello, {city}!");
    }

    private static void RunPasswordSample()
    {
      string secret = Prompt.Password("Type new password",
        new[] {Validators.Required(), Validators.MinLength(8)});
      Console.WriteLine("Password OK");
    }

    private static void RunConfirmSample()
    {
      var answer = Prompt.Confirm("Are you ready?");
      Console.WriteLine($"Your answer is {answer}");
    }

    private static void RunMultiSelectSample()
    {
      var options = Prompt.MultiSelect("Which cities would you like to visit?",
        new[] {"Seattle", "London", "Tokyo", "New York", "Singapore", "Shanghai"}, pageSize: 3);
      Console.WriteLine($"You picked {string.Join(", ", options)}");
    }

    private static void RunSelectEnumSample()
    {
      var value = Prompt.Select<MyEnum>("Select enum value");
      Console.WriteLine($"You selected {value}");
    }

    public enum MyEnum
    {
      [Display(Name = "Foo value")] Foo,

      [Display(Name = "Bar value")] Bar,

      [Display(Name = "Baz value")] Baz
    }

    private static void Header(string header, string subtitulo = null)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(FiggleFonts.Standard.Render(header));
      //Console.WriteLine(FiggleFonts.Roman.Render("Actualiza"));
      if (subtitulo != null)
      {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(subtitulo);
      }
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine();
    }

    private static void Footer(string footer)
    {
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      Console.WriteLine(footer);
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine();
    }
  }
}
