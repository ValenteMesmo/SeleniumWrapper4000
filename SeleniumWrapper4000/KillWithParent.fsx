open System.Diagnostics

let args : string[] =
    fsi.CommandLineArgs
    |> Array.tail

for i in 1..args.Length-1 do
    System.Console.WriteLine args.[i]

let getProcess id : (option<Process>) =
    try
        Some (Process.GetProcessById(id))
    with ex ->
        None

if (args.Length < 2) then
    System.Console.WriteLine "ParentId and at least one childId are required!"
else
    let parentProcess = int args.[0]

    let mutable parentIsRunning = true
    while parentIsRunning do
        System.Threading.Thread.Sleep(100)
        let parentProcess = getProcess parentProcess

        if parentProcess.IsNone then
            parentIsRunning <- false

    for i in 1..args.Length-1 do
        let child = getProcess <| int args.[i]
        if child.IsSome then
            try
                child.Value.Kill()
                ()
            with ex ->
                ()

0