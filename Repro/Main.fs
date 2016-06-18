
//#load @"C:\PROJ\FSharp.Compiler.Service_585\script.fsx"

open System
open System.IO
open System.Reflection
//Environment.CurrentDirectory <- @"C:\PROJ\FSharp.Compiler.Service_585\bin"
let scriptFilePath = Path.GetFullPath @"myscript.fsx"

let someLib = Path.GetFullPath @"bin/lib/LibraryUsingTypeprovider.dll"
File.WriteAllText(scriptFilePath, @"
#r """ + someLib.Replace(@"\", @"\\") + @"""
open System
Console.WriteLine(""TEST_OUTPUT"")");
open Microsoft.FSharp.Compiler.Interactive.Shell

[<EntryPoint>]
let main args =
  //printfn "%s" LibraryUsingTypeprovider.OtherStuff.t
  let fsiConfig = FsiEvaluationSession.GetDefaultConfiguration()
  let fsiArgs = [| @"C:\fsi.exe" |]
  let inStream = new StringReader("")
  use session = FsiEvaluationSession.Create(fsiConfig, fsiArgs, inStream, Console.Out, Console.Error)
  session.EvalScriptNonThrowing scriptFilePath |> printfn "%A"
  //execScript scriptFilePath
  0
